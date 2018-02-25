using GreenPrintClient.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GreenPrintClient
{
    /// <summary>
    /// Interaction logic for ChangeClientID.xaml
    /// </summary>
    public partial class ChangeClientID : Window
    {
        public string NewClientID
        {
            get { return txtNewClientID.Text; }
        }

        public ChangeClientID(string oldClientID = "")
        {
            InitializeComponent();

            txtNewClientID.Text = oldClientID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Validate credentials with server
            bool isValid = false;
            UserValidation userValidationContract = new UserValidation();
            userValidationContract.UserName = txtNewClientID.Text;
            userValidationContract.password = txtNewClientPassword.Password;

            isValid = valideUserCredentialsWithServer(userValidationContract);
            
            if (isValid)
            {
                DialogResult = true;
            }
            else
            {
                
                txtServerResponse.Text = "Change user name(client ID) failed.\r\nThe supplied username or password were not valid.";
                wChangeClient.Height = 280;
            }
                
        }

        private bool valideUserCredentialsWithServer(UserValidation userValidationContract)
        {
            string req = JsonConvert.SerializeObject(userValidationContract);

            WebRequest request = WebRequest.Create($"http://localhost:49639/account/TestUserCredentials");

            string status = string.Empty;
            WebResponse response = null;
            bool validationResult = false;

            try
            {
                // Set the Method property of the request to POST.  
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(req);
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
                    return false;
                }

                String responseString = string.Empty;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                     responseString = reader.ReadToEnd();
                }

                Boolean.TryParse(responseString, out validationResult);

                return validationResult;
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

            return false;
        }

        private void txtNewClientID_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                DialogResult = true;
            }
        }

        private void txtNewClientID_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNewClientID.Text.Length >= 5)
            {
                Save.IsEnabled = true;
            }
            else
            {
                Save.IsEnabled = false;
            }
        }
    }
}
