﻿using System;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreenPrintClient.Helpers;
using GreenPrintClient.Helpers.Contracts;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;

namespace GreenPrintClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string GPServicesBase, PRServiceURL, UMServiceURL, inboxFolder, submittedFolder, failedFolder, clientID;
        bool submitting = false;
        private readonly string prodURL = "https://requestharbor.azurewebsites.net/api/RequestHarbor";
        private readonly string localURL = "http://localhost:7071/api/RequestHarbor";

        Dictionary<string, string> settings;
        Dictionary<string, string> countryCodeList;
        List<string> rcc;

        SnackbarMessageQueue sbUIMessageQueue;
        SnackbarMessageQueue sbUIFatalMessageQueue;

        private void cbSignViaSMS_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtDocumentName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAddCCAddress_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddCC.Text.Length < 5)
                return;

            if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
            {
                System.Windows.MessageBox.Show($"You have reached the maximum supported number of recipients ({ Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    "Add CC Address",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var newItem = txtAddCC.Text;
            lstCCList.Items.Add(txtAddCC.Text);
            txtAddCC.Text = "";

            // Update registry
            var _rcc = SettingManager.LoadRCCList();
            if (_rcc == null)
                _rcc = new List<string>();

            // if the item is not in the list, then add it
            if (_rcc.IndexOf(newItem) == -1 || _rcc.Contains(newItem) == false)
            {
                _rcc.Add(newItem);
            }

            SettingManager.updateRCCList(_rcc);
        }

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

        private async Task<bool> validateClientIDAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GPServicesBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            clientID = "hamedsalam1i@gmail.com";
            string path = $"{GPServicesBase}{UMServiceURL}/" + clientID;
            HttpResponseMessage response = await client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                // Pre conditions met
                var res = response.Content.ReadAsStringAsync().Result;
                ClientValidationResponse clientValidationResponse = null;

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

        private void HyperLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool validationResult = await validateClientIDAsync();
            if (validationResult == false)
            {
                return;
            }

            string documentName = string.Empty;
            string CCList_emails = extractEmailCCList();
            string CCList_phones = extractPhoneNumbersCCList();

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
            if (string.IsNullOrEmpty(txtDocumentName.Text))
            {
                //var len = appbar_ClientID.Text.IndexOf("@") > 0 ? appbar_ClientID.Text.IndexOf("@") : appbar_ClientID.Text.Length - 1;
                //var dateSignature = DateTime.UtcNow.ToUnixTime();

                //var clientIDwithoutAtSign = appbar_ClientID.Text.Substring(0, len);
                //documentName = $"{clientIDwithoutAtSign}-{dateSignature.ToString()}";
            }

            // Build DSO request
            DocumentSigningOperationRequest req = new DocumentSigningOperationRequest();
            //req.ClientID = appbar_ClientID.Text;
            req.DocumentName = documentName;
            req.DocumentBytes = null;
            req.GuestSign_RecipientEmailAddress = txtEmailAddress.Text;
            req.GuestSign_RecipientSMSNumber = recipientSMSNumber;
            req.CarbonCopy_EMailAddressesList = CCList_emails;
            req.CarbonCopy_SMSPhoneNumbersList = CCList_phones;

            byte[] data = null;

            data = GetLatestPrint();

            if (req == null)
                return;

            req.DocumentBytes = data;

            MemoryStream memStream = new MemoryStream();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, req);
                data = ms.ToArray();
            }

            var re = JsonConvert.SerializeObject(req);

            //prSubmitting.IsActive = true;
            var resultStatus = submitViaWebRequest(request, re);
            //prSubmitting.IsActive = false;
            txtMessages.Text = resultStatus;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Init()
        {
            settings = SettingManager.LoadSettings();
            rcc = SettingManager.LoadRCCList();

            countryCodeList = Countries.GetData();
            countryCodeList = Countries.GetDetailedDataDic();

            //sbUIMessageQueue = sbUIMessages.MessageQueue;
            //sbUIFatalMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(30000));
            // Temporary Fatal errors message queues
            //sbUIMessages.MessageQueue = sbUIFatalMessageQueue;

            validateCriticalSettings();
            populateCCList();

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
                //appbar_ClientID.Text = clientID;
            }


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

        private void txtAddCC_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void txtAddCC_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
                {
                    //this.ShowMessageAsync("Add Recipient", $"You have reached the maximum supported number of recipients ({ Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    //    MessageDialogStyle.Affirmative);
                    return;
                }

                if (string.IsNullOrEmpty(txtAddCC.Text) == false && txtAddCC.Text.Length > 5)
                {
                    lstCCList.Items.Add(txtAddCC.Text);
                    txtAddCC.Text = "";
                }
            }
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
                System.Windows.Forms.MessageBox.Show("Could not detect latest print job.", "Submit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                if (File.Exists(recentPrintJob.FullName))
                {
                    latestPrintedDocument = File.ReadAllBytes(recentPrintJob.FullName);
                }
            }
            catch (Exception Ex)
            {
                // log
                System.Windows.Forms.MessageBox.Show("Could not read latest print job.", "Submit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(Ex.Message);
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
