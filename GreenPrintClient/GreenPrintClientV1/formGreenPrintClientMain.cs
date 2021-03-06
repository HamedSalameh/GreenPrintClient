﻿using GreenPrintClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace GreenPrintClient
{
    public partial class formGreenPrintClientMain : Form
    {
        private string serviceURL, inboxFolder, submittedFolder, failedFolder;
        private readonly string prodURL = "https://requestharbor.azurewebsites.net/api/RequestHarbor";
        private readonly string localURL = "http://localhost:7071/api/RequestHarbor";

        Dictionary<string, string> settings;
        Dictionary<string, string> countryCodeList;

        public formGreenPrintClientMain()
        {
            InitializeComponent();
        }

        private void Init()
        {
            settings = SettingsManager.LoadSettings();
            countryCodeList = Countries.GetData();

            this.WindowState = FormWindowState.Minimized;
            errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            errorProvider.BlinkRate = 0;

            txtSMSNumber.Enabled = cbRecipientSMS.Checked;
            cmbCountriesPhonePrefix.Enabled = cbRecipientSMS.Checked;

            if (countryCodeList != null)
            {
                cmbCountriesPhonePrefix.DataSource = new BindingSource(countryCodeList, null);
                cmbCountriesPhonePrefix.DisplayMember = "Key";
                cmbCountriesPhonePrefix.ValueMember = "Value";

                cmbCountriesPhonePrefix.SelectedIndex = 122; // Default Israel
            }

            string clientID = string.Empty;
            settings.TryGetValue("ClientID", out clientID);
            if (clientID != string.Empty)
            {
                txtClientID.Text = clientID;
            }
        }

        private void formGreenPrintClientMain_Load(object sender, EventArgs e)
        {
            Init();

            settings.TryGetValue("InboxFolder", out inboxFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            settings.TryGetValue("SubmittedFolder", out submittedFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            settings.TryGetValue("FailedFolder", out failedFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            settings.TryGetValue("DSORServiceURL", out serviceURL);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                MessageBox.Show("GreenPrint service URL could not be loaded.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = inboxFolder;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) //<<======Now it wont give exception**
            {
                //Load data correspondin to "MyName"
                //Populate a globale variable List<string> which will be
                //bound to grid at some later stage
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // load the control with the appropriate data
                        this.WindowState = FormWindowState.Normal;
                    }));
                    return;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtDocumentName.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtSMSNumber.Text = string.Empty;
            lstCCList.Items.Clear();

            if (InvokeRequired)
            {
                // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate
                {
                        // load the control with the appropriate data
                        this.WindowState = FormWindowState.Minimized;
                }));
                return;
            }
            else
                this.WindowState = FormWindowState.Minimized;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitPrint();
        }

        private void txtClientID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientID.Text) == false && txtClientID.Text.Length < 5)
            {
                errorProvider.SetError(txtClientID, "ClientID value is too short");
            }
            else
            {
                errorProvider.SetError(txtClientID, "");
            }
        }

        private void rbRemoteSign_CheckedChanged(object sender, EventArgs e)
        {
            cbRecipientSMS.Enabled = rbRemoteSign.Checked;
            txtSMSNumber.Enabled = cbRecipientSMS.Checked;

            cbRecipientEmail.Enabled = rbRemoteSign.Checked;
            txtEmailAddress.Enabled = cbRecipientEmail.Checked;
        }
        private void cbRecipientSMS_CheckedChanged(object sender, EventArgs e)
        {
            txtSMSNumber.Enabled = cbRecipientSMS.Checked;
            cmbCountriesPhonePrefix.Enabled = cbRecipientSMS.Checked;
            cmbCountriesPhonePrefix.SelectedIndex = 1;
            cmbCountriesPhonePrefix.SelectedIndex = 122;

            if (txtSMSNumber.Enabled == false)
            {
                txtSMSNumber.Text = "";
                errorProvider.SetError(txtSMSNumber, "");
            }

        }
        private void cbRecipientEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailAddress.Enabled = cbRecipientEmail.Checked;
        }

        // Validations
        private void txtSMSNumber_Leave(object sender, EventArgs e)
        {
            if (txtSMSNumber.Enabled)
            {
                if (string.IsNullOrEmpty(txtSMSNumber.Text))
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number not set");
                }
                else if (txtSMSNumber.Text.Length > Consts.DEFAULT_MAX_PHONE_NUMBER_LENGTH)
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number is too long");
                }
                else if (txtSMSNumber.Text.Length < Consts.DEFAULT_MIN_PHONE_NUMBER_LENGTH)
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number is too short");
                }
                else
                {
                    errorProvider.SetError(txtSMSNumber, "");
                }
            }
        }
        private void txtEmailAddress_Leave(object sender, EventArgs e)
        {
            if (txtEmailAddress.Enabled)
            {
                if (string.IsNullOrEmpty(txtEmailAddress.Text))
                {
                    errorProvider.SetError(txtEmailAddress, "Email address not set");
                }
                else if (txtEmailAddress.Text.Length > Consts.DEFAULT_MAX_EMAIL_ADDRESS_LENGTH)
                {
                    errorProvider.SetError(txtEmailAddress, "Email address is too long");
                }
                else
                {
                    errorProvider.SetError(txtEmailAddress, "");
                }
            }
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
            {
                MessageBox.Show($"You have reached the maximum supported number of recipients ({Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    "Add Recipient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            lstCCList.Items.Add(txtAddCC.Text);
            txtAddCC.Text = "";
        }

        // Actions
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

        private void SubmitPrint()
        {
            string documentName = string.Empty;
            string CCList_emails = extractEmailCCList();
            string CCList_phones = extractPhoneNumbersCCList();
            string recipientSMSNumber = "+" + cmbCountriesPhonePrefix.SelectedValue.ToString() + "-" + txtSMSNumber.Text;

            // Clear any message in messages text box
            txtMessages.Clear();

            WebRequest request = WebRequest.Create(serviceURL);
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  

            // Try get the document name if it was provided, otherwise, generate one
            if (string.IsNullOrEmpty(txtDocumentName.Text))
            {
                var len = txtClientID.Text.IndexOf("@") > 0 ? txtClientID.Text.IndexOf("@") : txtClientID.Text.Length - 1;
                var dateSignature = DateTime.UtcNow.ToUnixTime();

                var clientIDwithoutAtSign = txtClientID.Text.Substring(0, len);
                documentName = $"{clientIDwithoutAtSign}-{dateSignature.ToString()}";
            }

            // Build DSO request
            DocumentSigningOperationRequest req = new DocumentSigningOperationRequest();
            req.ClientID = txtClientID.Text;
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

            var resultStatus = submitViaWebRequest(request, re);
            txtMessages.Text = resultStatus;
        }

        private static string submitViaWebRequest(WebRequest request, string re)
        {
            string status = string.Empty;
            WebResponse response = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;


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
                Cursor.Current = Cursors.WaitCursor;

                if (response != null)
                    response.Close();
            }

            return status;
        }

        private void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCountriesPhonePrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtSMSNumber.Text = cmbCountriesPhonePrefix.SelectedValue.ToString();
        }

        private void cmbCountriesPhonePrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
            }
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
                MessageBox.Show("Could not detect latest print job.", "Submit", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Could not read latest print job.", "Submit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(Ex.Message);
                latestPrintedDocument = null;

            }
            finally
            {
            }

            return latestPrintedDocument;
        }
    }
}
