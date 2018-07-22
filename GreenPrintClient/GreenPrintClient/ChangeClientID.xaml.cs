using GreenPrintClient.Contracts;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace GreenPrintClient
{
    /// <summary>
    /// Interaction logic for ChangeClientID.xaml
    /// </summary>
    public partial class ChangeClientID : Window
    {
        private string gpServerBase;

        public string NewClientID
        {
            get { return txtNewClientID.Text; }
        }

        public ChangeClientID(string GreenPrintServerBaseAddress, string oldClientID = "")
        {
            if(string.IsNullOrEmpty(GreenPrintServerBaseAddress))
            {
                throw new ArgumentException("Base server address must not be empty or null", nameof(GreenPrintServerBaseAddress));
            }

            InitializeComponent();

            txtNewClientID.Text = oldClientID;
            gpServerBase = GreenPrintServerBaseAddress;
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

        private async void Save_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Validate credentials with server

            UserValidation userValidationContract = new UserValidation
            {
                UserName = txtNewClientID.Text,
                password = txtNewClientPassword.Password
            };


            await valideUserCredentialsWithServer(userValidationContract);
            wChangeClient.Focus();
        }

        private async Task valideUserCredentialsWithServer(UserValidation userValidationContract)
        {
            btnConfirm_Idle.Visibility = Visibility.Hidden;
            btnConfirm_Waiting.Visibility = Visibility.Visible;

            string req = JsonConvert.SerializeObject(userValidationContract);

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri($"{gpServerBase}/Account/TestUserCredentials")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string path = $"{gpServerBase}/Account/TestUserCredentials";
            HttpResponseMessage response = await client.PostAsync(path,
                new StringContent(req, Encoding.UTF8, "application/json"));

            // Pre conditions met
            var res = response.Content?.ReadAsStringAsync()?.Result;
            ClientValidationResponse clientValidationResponse = null;

            if (string.IsNullOrEmpty(res))
            {
                txtServerResponse.Inlines.Add("Could not parse server response, please try again");
            }

            try
            {
                txtServerResponse.Inlines.Clear();
                clientValidationResponse = JsonConvert.DeserializeObject<ClientValidationResponse>(res.Remove(0, 1).Replace("\\", "").Remove(res.Remove(0, 1).Replace("\\", "").Length - 1));

                if (clientValidationResponse.HttpStatusCode == (int)HttpStatusCode.OK)
                {
                    btnConfirm_Idle.Visibility = Visibility.Visible;
                    btnConfirm_Waiting.Visibility = Visibility.Hidden;

                    DialogResult = true;
                }

                txtServerResponse.TextWrapping = TextWrapping.Wrap;
                txtServerResponse.Inlines.Add(clientValidationResponse.Message);
                txtServerResponse.Inlines.Add(": ");
                Hyperlink hyperLink = new Hyperlink()
                {
                    NavigateUri = new Uri(clientValidationResponse.HyperLink)
                };
                hyperLink.Inlines.Add(clientValidationResponse.HyperLinkName);
                hyperLink.RequestNavigate += HyperLink_RequestNavigate;

                txtServerResponse.Inlines.Add(hyperLink);

                wChangeClient.Height = 300;
                wChangeClient.Focus();
                
            }
            catch
            {
                txtServerResponse.Inlines.Clear();
                clientValidationResponse = new ClientValidationResponse
                {
                    Message = res.Replace("\"", "").Replace("\\", "")
                };

                txtServerResponse.Inlines.Add(clientValidationResponse.Message);

                wChangeClient.Height = 300;
                wChangeClient.Focus();
            }

            btnConfirm_Idle.Visibility = Visibility.Visible;
            btnConfirm_Waiting.Visibility = Visibility.Hidden;
        }

        private void HyperLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            throw new NotImplementedException();
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
