using GreenPrintClient.CustomControls;
using GreenPrintClient.Helpers;
using GreenPrintClient.Helpers.Contracts;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace GreenPrintClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string GPServicesBase, PRServiceURL, UMServiceURL, inboxFolder, submittedFolder, failedFolder, clientID;

        Dictionary<string, string> settings;
        Dictionary<string, string> countryCodeList;
        List<string> rcc;
        List<string> cachedPhoneNumbers;

        SnackbarMessageQueue sbUIMessageQueue;
        SnackbarMessageQueue sbUIFatalMessageQueue;
        ChangeClientID changeClientID;

        LocalStorage LocalStorage;

        public Boolean IsAddCC_AddingSMS { get; set; }
        public bool IsAddCC_AddingEmail { get; set; }

        private async Task<bool> validateClientIDAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GPServicesBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = $"{GPServicesBase}{UMServiceURL}/" + clientID;
            HttpResponseMessage response = await client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                // Pre conditions met
                var res = response.Content?.ReadAsStringAsync()?.Result;
                ClientValidationResponse clientValidationResponse = null;

                if (string.IsNullOrEmpty(res))
                {
                    txtMessages.Inlines.Add("Could not parse server response, please try again");
                    return false;
                }

                try
                {
                    clientValidationResponse = JsonConvert.DeserializeObject<ClientValidationResponse>(res.Remove(0, 1).Replace("\\", "").Remove(res.Remove(0, 1).Replace("\\", "").Length - 1));

                    txtMessages.Inlines.Add(clientValidationResponse.Message);
                    txtMessages.Inlines.Add(": ");
                    Hyperlink hyperLink = new Hyperlink()
                    {
                        NavigateUri = new Uri(clientValidationResponse.HyperLink)
                    };
                    hyperLink.Inlines.Add(clientValidationResponse.HyperLinkName);
                    hyperLink.RequestNavigate += HyperLink_RequestNavigate;

                    txtMessages.Inlines.Add(hyperLink);
                }
                catch
                {
                    clientValidationResponse = new ClientValidationResponse();
                    clientValidationResponse.Message = res.Replace("\"", "").Replace("\\", "");

                    txtMessages.Inlines.Add(clientValidationResponse.Message);
                }
                return false;
            }

            return true;
        }

        public MainWindow()
        {
            InitializeComponent();

            LocalStorage = new LocalStorage();
            
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            settings = SettingManager.LoadSettings();
            rcc = SettingManager.LoadRCCList();
            cachedPhoneNumbers = LocalStorage.LoadPhoneNumbers();

            countryCodeList = Countries.GetData();
            countryCodeList = Countries.GetDetailedDataDic();

            //sbUIMessageQueue = sbUIMessages.MessageQueue;
            //sbUIFatalMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(30000));
            // Temporary Fatal errors message queues
            //sbUIMessages.MessageQueue = sbUIFatalMessageQueue;

            controlAddPhoneNumber.PhoneNumberConfirmed += ControlAddPhoneNumber_PhoneNumberConfirmed;
            controlAddEmailAddress.EmailAddressConfirmed += ControlAddEmailAddress_EmailAddressConfirmed;

            IsAddCC_AddingSMS = true;

            validateCriticalSettings();
            populateCCList();

            pbLoading.Visibility = Visibility.Hidden;

            if (countryCodeList != null)
            {
                cmbCountryPhonePrefix.ItemsSource = countryCodeList;
                cmbCountryPhonePrefix.DisplayMemberPath = "Key";
                cmbCountryPhonePrefix.SelectedValuePath = "Value";

                cmbCountryPhonePrefix.SelectedItem = cmbCountryPhonePrefix.Items[111];
            }

            rbDeviceSign.IsChecked = true;

            settings.TryGetValue("ClientID", out clientID);
            if (clientID != string.Empty)
            {
                txtClientID.Text = clientID;
                //appbar_ClientID.Text = clientID;
            }

        }

        private void updateCCList(string newCCItem)
        {
            if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
            {
                System.Windows.MessageBox.Show($"You have reached the maximum supported number of recipients ({ Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    "Add CC Address",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            //Update registry
            var _rcc = SettingManager.LoadRCCList();
            if (_rcc == null)
                _rcc = new List<string>();

            if (_rcc.IndexOf(newCCItem) == -1 || _rcc.Contains(newCCItem) == false)
            {
                _rcc.Add(newCCItem);
                lstCCList.Items.Add(newCCItem);
            }

            SettingManager.updateRCCList(_rcc);
        }

        private void ControlAddEmailAddress_EmailAddressConfirmed(object sender, RoutedEventArgs e)
        {
            string mailAddress = ((EmailAddressRoutedEventArgs)e)?.EmailAddressValue;

            updateCCList(mailAddress);
        }

        private void ControlAddPhoneNumber_PhoneNumberConfirmed(object sender, RoutedEventArgs e)
        {
            string number = ((PhoneNumberRoutedEventArgs)e)?.PhoneNumberValue;

            updateCCList(number);
        }

        private void populateCCList()
        {
            if (rcc != null && rcc.Count > 0)
            {
                foreach (var item in rcc)
                {
                    lstCCList.Items.Add(item);
                }
            }
        }

        private void validateCriticalSettings()
        {

            settings.TryGetValue("InboxFolder", out inboxFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                System.Windows.MessageBox.Show($"Unable to process printing job, could not get inbox folder name.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            settings.TryGetValue("SubmittedFolder", out submittedFolder);
            if (string.IsNullOrEmpty(submittedFolder))
            {
                System.Windows.MessageBox.Show($"Unable to process printing job, could not get inbox folder name.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            settings.TryGetValue("FailedFolder", out failedFolder);
            if (string.IsNullOrEmpty(failedFolder))
            {
                System.Windows.MessageBox.Show($"Unable to process printing job, could not get inbox folder name.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            settings.TryGetValue("GPServicesBase", out GPServicesBase);
            if (string.IsNullOrEmpty(GPServicesBase))
            {
                System.Windows.MessageBox.Show($"GreenPrint service URL coould not be laded.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            settings.TryGetValue("UMS", out UMServiceURL);
            if (string.IsNullOrEmpty(UMServiceURL))
            {
                System.Windows.MessageBox.Show($"GreenPrint service URL coould not be laded.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }

            settings.TryGetValue("PRS", out PRServiceURL);
            if (string.IsNullOrEmpty(PRServiceURL))
            {
                System.Windows.MessageBox.Show($"GreenPrint service URL coould not be laded.",
                    "GreenPrint Client Initialization",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }
        }

        // Event Handlers
        private void cbSignViaSMS_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtDocumentName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void btnAddCCAddress_Click(object sender, RoutedEventArgs e)
        //{
        //    if (txtAddCC.Text.Length < 5)
        //        return;

        //    if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
        //    {
        //        System.Windows.MessageBox.Show($"You have reached the maximum supported number of recipients ({ Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
        //            "Add CC Address",
        //            MessageBoxButton.OK,
        //            MessageBoxImage.Warning);
        //        return;
        //    }

        //    var newItem = txtAddCC.Text;
        //    lstCCList.Items.Add(txtAddCC.Text);
        //    txtAddCC.Text = "";

        //    // Update registry
        //    var _rcc = SettingManager.LoadRCCList();
        //    if (_rcc == null)
        //        _rcc = new List<string>();

        //    // if the item is not in the list, then add it
        //    if (_rcc.IndexOf(newItem) == -1 || _rcc.Contains(newItem) == false)
        //    {
        //        _rcc.Add(newItem);
        //    }

        //    SettingManager.updateRCCList(_rcc);
        //}

        private void cmbCountryPhonePrefix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstCCList_ItemMouseDoubleClick(object sender, RoutedEventArgs e)
        {

            lstCCList.Items.Remove(lstCCList.SelectedItem);
            List<string> _rcc = lstCCList.Items.Cast<String>().ToList();

            SettingManager.updateRCCList(_rcc);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtDocumentName.Text = "";
            txtSMSNumber.Text = "";
            txtEmailAddress.Text = "";

            txtMessages.Text = "";
        }

        private void HyperLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Uri.ToString());
            }
            catch (Exception Ex)
            {
                System.Windows.MessageBox.Show($"GreenPrint client was not able to navigate to {e?.Uri?.ToString()} due to internal error.\r\n{Ex.Message}",
                    "Navigate",
                    MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            // Reset the messages windows
            txtMessages.Text = "";
            // Show the loading spinnger
            pbLoading.Visibility = Visibility.Visible;
            // Execute pre-conditions validations
            bool validationResult = await validateClientIDAsync();
            if (validationResult == false)
            {
                pbLoading.Visibility = Visibility.Hidden;
                return;
            }

            string documentName = string.Empty;
            string CCList_emails = extractEmailCCList();
            string CCList_phones = extractPhoneNumbersCCList();

            if (chkSendCopyToMe.IsChecked.Value == true)
            {
                CCList_emails += "," + clientID;
            }

            if (cmbCountryPhonePrefix.SelectedValue == null)
            {
                cmbCountryPhonePrefix.SelectedItem = cmbCountryPhonePrefix.Items[111]; // default to israel
            }
            string recipientSMSNumber = "+" + cmbCountryPhonePrefix.SelectedValue.ToString() + "-" + txtSMSNumber.Text;

            // Clear any message in messages text box
            txtMessages.Text = "";

            WebRequest request = WebRequest.Create($"{GPServicesBase}{PRServiceURL}");
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  

            // Try get the document name if it was provided, otherwise, generate one
            documentName = buildDocumentName();

            // Build DSO request
            DocumentSigningOperationRequest req = buildDSORequest(documentName, CCList_emails, CCList_phones, recipientSMSNumber);
            // Get the printed document as byte array
            byte[] data = null;
            data = GetLatestPrint();
            if (req == null)
                return;
            // Add the printed document bytes to the request object
            req.DocumentBytes = data;

            // Serialize the request object
            MemoryStream memStream = new MemoryStream();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, req);
                data = ms.ToArray();
            }

            var serializedRequest = JsonConvert.SerializeObject(req);

            var resultStatus = submitViaWebRequest(request, serializedRequest);
            txtMessages.Text = resultStatus;

            pbLoading.Visibility = Visibility.Hidden;
        }

        private void btnChangeClientID_Click(object sender, RoutedEventArgs e)
        {
            changeClientID = new ChangeClientID(clientID);
            changeClientID.Closed += ChangeClientID_Closed;

            changeClientID.ShowDialog();
        }

        private void ChangeClientID_Closed(object sender, EventArgs e)
        {
            if (changeClientID.DialogResult == true)
            {
                clientID = changeClientID.NewClientID;
                SettingManager.UpdateClientID(clientID);
                txtClientID.Text = clientID;
            }
        }

        private void txtAddCC_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void btnAddCC_SMSNumber_Click(object sender, RoutedEventArgs e)
        {
            btnAddCC_SMSNumber.IsEnabled = false;
            btnAddCC_EmailAddress.IsEnabled = true;

            controlAddPhoneNumber.SetCountryListItemSource(countryCodeList, 111, false);
            controlAddPhoneNumber.HideAutoComplete();
        }

        private void btnAddCC_EmailAddress_Click(object sender, RoutedEventArgs e)
        {
            btnAddCC_SMSNumber.IsEnabled = true;
            btnAddCC_EmailAddress.IsEnabled = false;

            controlAddEmailAddress.HideAutoComplete();
        }

        private void lstCCList_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        // Private helper methods
        private string buildDocumentName()
        {
            string documentName = txtDocumentName.Text;

            if (string.IsNullOrEmpty(txtDocumentName.Text))
            {
                var len = clientID.IndexOf("@") > 0 ? clientID.IndexOf("@") : clientID.Length - 1;
                var dateSignature = DateTime.UtcNow.ToUnixTime();

                var clientIDwithoutAtSign = clientID.Substring(0, len);
                documentName = $"{clientIDwithoutAtSign}-{dateSignature.ToString()}";
            }

            return documentName;
        }
        private DocumentSigningOperationRequest buildDSORequest(string documentName, string CCList_emails, string CCList_phones, string recipientSMSNumber)
        {
            DocumentSigningOperationRequest req = new DocumentSigningOperationRequest();
            req.ClientID = clientID;
            req.DocumentName = documentName;
            req.DocumentBytes = null;
            req.GuestSign_RecipientEmailAddress = txtEmailAddress.Text;
            req.GuestSign_RecipientSMSNumber = recipientSMSNumber;
            req.CarbonCopy_EMailAddressesList = CCList_emails;
            req.CarbonCopy_SMSPhoneNumbersList = CCList_phones;
            return req;
        }
        private string extractEmailCCList()
        {
            List<string> emails = new List<string>();
            string list = string.Empty;

            if (lstCCList.Items != null && lstCCList.Items.Count > 0)
            {
                foreach (var item in lstCCList.Items)
                {
                    if (item.ToString().Contains("@"))
                    {
                        if (emails.Contains(item.ToString()) == false)
                        {
                            emails.Add(item.ToString());
                        }
                    }
                }
            }

            if (emails != null && emails.Count > 0)
            {
                list = string.Join(",", emails.ToArray());
            }

            return list;
        }   
        private string extractPhoneNumbersCCList()
        {
            List<string> phoneNumbers = new List<string>();
            string list = string.Empty;

            if (lstCCList.Items != null && lstCCList.Items.Count > 0)
            {
                foreach (var item in lstCCList.Items)
                {

                    if (item.ToString().Contains("@") == false)
                    {
                        // Not an email
                        var newitem = item.ToString().Replace('-', ' ').Replace('.', ' ').Replace(" ", "");    // removing dashes
                        // removing dots
                        long newnumber;
                        long.TryParse(newitem, out newnumber);

                        if (newnumber != 0)
                        {
                            // successful parse
                            string extractedPhoneNumber = newitem;
                            if (phoneNumbers.Contains(extractedPhoneNumber) == false)
                            {
                                phoneNumbers.Add(extractedPhoneNumber);
                            }
                        }
                    }
                }
            }

            if (phoneNumbers != null && phoneNumbers.Count > 0)
            {
                list = string.Join(",", phoneNumbers.ToArray());
            }


            return list;
        }
        private byte[] GetLatestPrint()
        {
            byte[] latestPrintedDocument = null;
            var directory = new DirectoryInfo(inboxFolder);
            var recentPrintJob = directory.GetFiles()
                                                     .OrderByDescending(f => f.LastWriteTime)
                                                     .First();

            if (recentPrintJob == null || recentPrintJob.FullName.Length < 1)
            {
                System.Windows.MessageBox.Show($"GreenPrint client software was not able to retreive the latest print job, please try again",
                    "Retrieve Printed Document",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return null;
            }

            try
            {
                if (File.Exists(recentPrintJob.FullName))
                {
                    latestPrintedDocument = File.ReadAllBytes(recentPrintJob.FullName);
                }
            }
            catch
            {
                // log
                System.Windows.MessageBox.Show($"GreenPrint client software was not able to retreive the latest print job, please try again",
                    "Retrieve Printed Document",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                latestPrintedDocument = null;
            }
            finally
            {
            }

            return latestPrintedDocument;
        }
        private static string submitViaWebRequest(WebRequest request, string re)
        {
            string status = string.Empty;
            WebResponse response = null;

            try
            {

                byte[] byteArray = Encoding.UTF8.GetBytes(re);
                // Set the ContentType property of the WebRequest.  
                request.ContentType = "application/json";
                // Set the ContentLength property of the WebRequest.  
                request.ContentLength = byteArray.Length;
                // Get the request stream.  
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.  
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.  
                dataStream.Close();
                // Get the response.  
                response = request.GetResponse();
                // Get the status.  
                status = ((HttpWebResponse)response).StatusDescription;

                if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                {
                    status += Environment.NewLine;
                    // Get the stream containing content returned by the server.  
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.  
                    status += responseFromServer;
                    // Clean up the streams.  
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (Exception Ex)
            {
                status = "An error happened while trying to send the request.\r\n" + Ex.Message;
            }
            finally
            {

                if (response != null)
                    response.Close();
            }

            return status;
        }
    }


}
