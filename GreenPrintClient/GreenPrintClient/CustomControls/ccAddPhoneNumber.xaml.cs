using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GreenPrintClient.CustomControls
{
    /// <summary>
    /// Interaction logic for ccAddPhoneNumber.xaml
    /// </summary>

    public class PhoneNumberRoutedEventArgs : RoutedEventArgs
    {
        private readonly string phoneNumberValue;

        public PhoneNumberRoutedEventArgs(RoutedEvent routedEvent,
                                          string selectedItem)
            : base(routedEvent)
        {
            this.phoneNumberValue = selectedItem;
        }

        public string PhoneNumberValue
        {
            get
            {
                return phoneNumberValue;
            }
        }
    }

    public partial class ccAddPhoneNumber : UserControl
    {
        public static readonly RoutedEvent PhoneNumberConfirmedEvent =
         EventManager.RegisterRoutedEvent("PhoneNumberConfirmedEvent",
                      RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                      typeof(ccAddPhoneNumber));

        public event RoutedEventHandler PhoneNumberConfirmed
        {
            add { AddHandler(PhoneNumberConfirmedEvent, value); }
            remove { RemoveHandler(PhoneNumberConfirmedEvent, value); }
        }

        void RaisePhoneNumberConfirmedEvent(string selectedItem)
        {
            PhoneNumberRoutedEventArgs newEventArgs =
                  new PhoneNumberRoutedEventArgs(ccAddPhoneNumber.PhoneNumberConfirmedEvent,
                                                 selectedItem);
            RaiseEvent(newEventArgs);
        }

        public ccAddPhoneNumber()
        {
            InitializeComponent();
        }

        public void SetItemSource(Dictionary<string, string> source, int defaultIndex, bool overwrite = false)
        {
            if (cmbCountryPhonePrefix.ItemsSource != null && overwrite == false)
                return;

            cmbCountryPhonePrefix.ItemsSource = source;
            cmbCountryPhonePrefix.DisplayMemberPath = "Key";
            cmbCountryPhonePrefix.SelectedValuePath = "Value";

            if (cmbCountryPhonePrefix.Items.Count > defaultIndex)
                cmbCountryPhonePrefix.SelectedItem = cmbCountryPhonePrefix.Items[defaultIndex];
        }

        private void cmbCountryPhonePrefix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            RaisePhoneNumberConfirmedEvent("+" + cmbCountryPhonePrefix.SelectedValue + "-" + txtSMSNumber.Text);
            txtSMSNumber.Text = "";
        }

        private void txtSMSNumber_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtSMSNumber.Text.Length < 9)
            {
                btnConfirm.IsEnabled = false;
            }
            else
            {
                btnConfirm.IsEnabled = true;
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    btnConfirm_Click(this, e);
                }
            }
        }
    }
}
