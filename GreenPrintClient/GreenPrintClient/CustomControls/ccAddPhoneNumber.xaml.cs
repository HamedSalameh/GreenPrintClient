using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    public class pnDataContext
    {
        public pnDataContext()
        {
            cachedPhoneNumbers = new ObservableCollection<string>();
        }

        public pnDataContext(List<string> PhoneNumbers)
        {
            if(PhoneNumbers == null)
            {
                throw new ArgumentException("Phone Numbers list must be null", nameof(PhoneNumbers));
            }

            if (PhoneNumbers != null && PhoneNumbers.Count > 0)
            {
                foreach(var item in PhoneNumbers)
                {
                    cachedPhoneNumbers.Add(item);
                }
            }
        }

        public ObservableCollection<string> cachedPhoneNumbers
        {
            get;
            set;
        }
    }

    public partial class ccAddPhoneNumber : UserControl
    {
        public pnDataContext dc = new pnDataContext();

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

            dc = new pnDataContext();
            dc.cachedPhoneNumbers = new ObservableCollection<string>();

            dc.cachedPhoneNumbers.Add("aaa");
            dc.cachedPhoneNumbers.Add("aab");
            dc.cachedPhoneNumbers.Add("aac");
            dc.cachedPhoneNumbers.Add("aba");
            dc.cachedPhoneNumbers.Add("abb");
            dc.cachedPhoneNumbers.Add("abc");
            dc.cachedPhoneNumbers.Add("aca");
            dc.cachedPhoneNumbers.Add("acb");
            dc.cachedPhoneNumbers.Add("acc");
            dc.cachedPhoneNumbers.Add("acc1");
            dc.cachedPhoneNumbers.Add("acc2");


            cmbAutoComplete.ItemsSource = dc.cachedPhoneNumbers;
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
            var dynamicItemsSouce = dc.cachedPhoneNumbers.Where(s => s.StartsWith(txtSMSNumber.Text, System.StringComparison.InvariantCultureIgnoreCase)).Take(8).ToList();

            cmbAutoComplete.ItemsSource = dynamicItemsSouce;

            if (dynamicItemsSouce != null && dynamicItemsSouce.Count > 0)
            {
                cmbAutoComplete.Visibility = Visibility.Visible;
                cmbAutoComplete.IsDropDownOpen = true;
            }
            else
            {
                cmbAutoComplete.Visibility = Visibility.Hidden;
                cmbAutoComplete.IsDropDownOpen = true;
            }


            txtSMSNumber.Focus();

            if (txtSMSNumber.Text.Length < 9)
            {
                btnConfirm.IsEnabled = false;
            }
            else
            {
                btnConfirm.IsEnabled = true;
            }

            if (e.Key == System.Windows.Input.Key.Enter && btnConfirm.IsEnabled == true)
            {
                btnConfirm_Click(this, e);
            }
        }

        private void cmbAutoComplete_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
        }

        private void cmbAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAutoComplete.Visibility = Visibility.Hidden;
            if (cmbAutoComplete.SelectedValue != null)
                txtSMSNumber.Text = cmbAutoComplete.SelectedValue.ToString();
        }

        private void txtSMSNumber_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key < System.Windows.Input.Key.D0 || e.Key > System.Windows.Input.Key.D9)
            {
                System.Windows.MessageBox.Show("Please enter only digits (0-9) for a phone number", "Phone Number", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                e.Handled = true;
                return;
            }
        }
    }


}
