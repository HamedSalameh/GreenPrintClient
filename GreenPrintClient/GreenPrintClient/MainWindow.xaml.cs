using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreenPrintClient.Helpers;
using MahApps.Metro.Controls;

namespace GreenPrintClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string serviceURL, inboxFolder, submittedFolder, failedFolder;
        private readonly string prodURL = "https://requestharbor.azurewebsites.net/api/RequestHarbor";
        private readonly string localURL = "http://localhost:7071/api/RequestHarbor";

        Dictionary<string, string> settings;
        Dictionary<string, string> countryCodeList;

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
            countryCodeList = Countries.GetData();

            //txtSMSNumber.Enabled = cbRecipientSMS.Checked;
            //cmbCountriesPhonePrefix.Enabled = cbRecipientSMS.Checked;

            //if (countryCodeList != null)
            //{
            //    cmbCountriesPhonePrefix.DataSource = new BindingSource(countryCodeList, null);
            //    cmbCountriesPhonePrefix.DisplayMember = "Key";
            //    cmbCountriesPhonePrefix.ValueMember = "Value";

            //    cmbCountriesPhonePrefix.SelectedIndex = 122; // Default Israel
            //}

            string clientID = string.Empty;
            settings.TryGetValue("ClientID", out clientID);
            if (clientID != string.Empty)
            {
                appbar_ClientID.Text = clientID;
            }
        }
    }
}
