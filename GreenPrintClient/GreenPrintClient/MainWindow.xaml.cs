using System;
using System.Collections.Generic;
using System.Linq;
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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

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

        private void cbSignViaSMS_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtDocumentName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAddCCAddress_Click(object sender, RoutedEventArgs e)
        {
            if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
            {
                this.ShowMessageAsync("Add Recipient", $"You have reached the maximum supported number of recipients ({ Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    MessageDialogStyle.Affirmative);
                return;
            }

            lstCCList.Items.Add(txtAddCC.Text);
            txtAddCC.Text = "";
        }

        private void cmbCountryPhonePrefix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            countryCodeList = Countries.GetData();
            countryCodeList = Countries.GetDetailedDataDic();

            settings.TryGetValue("InboxFolder", out inboxFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                System.Windows.Forms.MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "GreenPrint | Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Application.Current.Shutdown();
                return;
            }

            settings.TryGetValue("SubmittedFolder", out submittedFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                System.Windows.Forms.MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "GreenPrint | Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Application.Current.Shutdown();
                return;
            }

            settings.TryGetValue("FailedFolder", out failedFolder);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                System.Windows.Forms.MessageBox.Show("Unable to process printing job, could not get inbox folder name.", "GreenPrint | Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Application.Current.Shutdown();
                return;
            }

            settings.TryGetValue("DSORServiceURL", out serviceURL);
            if (string.IsNullOrEmpty(inboxFolder))
            {
                System.Windows.Forms.MessageBox.Show("GreenPrint service URL could not be loaded.", "GreenPrint | Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Application.Current.Shutdown();
                return;
            }

            //cmbCountriesPhonePrefix.Enabled = cbRecipientSMS.Checked;

            if (countryCodeList != null)
            {
                cmbCountryPhonePrefix.ItemsSource = countryCodeList;
                cmbCountryPhonePrefix.DisplayMemberPath = "Key";
                cmbCountryPhonePrefix.SelectedValuePath = "Value";
            }

            string clientID = string.Empty;
            settings.TryGetValue("ClientID", out clientID);
            if (clientID != string.Empty)
            {
                appbar_ClientID.Text = clientID;
            }
        }
    }
}
