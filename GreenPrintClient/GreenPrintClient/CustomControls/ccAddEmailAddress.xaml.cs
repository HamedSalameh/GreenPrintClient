using GreenPrintClient.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenPrintClient.CustomControls
{
    public class emailsDataContext
    {
        public emailsDataContext()
        {
            cachedEmailAddresses = new ObservableCollection<string>();
        }

        public emailsDataContext(List<string> EmailAddresses)
        {
            if (EmailAddresses == null)
            {
                throw new ArgumentException("Phone Numbers list must not be null", nameof(EmailAddresses));
            }

            cachedEmailAddresses = new ObservableCollection<string>();

            if (EmailAddresses != null && EmailAddresses.Count > 0)
            {
                foreach (var item in EmailAddresses)
                {
                    cachedEmailAddresses.Add(item);
                }
            }
        }

        public void UpdateList(List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                cachedEmailAddresses.Clear();

                foreach (var item in list)
                {
                    cachedEmailAddresses.Add(item);
                }
            }
        }

        public ObservableCollection<string> cachedEmailAddresses
        {
            get;
            set;
        }
    }

    public class EmailAddressRoutedEventArgs : RoutedEventArgs
    {
        private readonly string emailAddressValue;

        public EmailAddressRoutedEventArgs(RoutedEvent routedEvent,
                                          string EmailAddress)
            : base(routedEvent)
        {
            this.emailAddressValue = EmailAddress;
        }

        public string EmailAddressValue
        {
            get
            {
                return emailAddressValue;
            }
        }
    }


    /// <summary>
    /// Interaction logic for ccAddEmailAddress.xaml
    /// </summary>
    public partial class ccAddEmailAddress : UserControl
    {
        public emailsDataContext dc = new emailsDataContext();
        List<string> cachedEmailAddresses;
        LocalStorage localStorage;

        public static readonly RoutedEvent EmailAddressConfirmedEvent = EventManager.RegisterRoutedEvent("EmailAddressConfirmed", 
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ccAddEmailAddress));

        public event RoutedEventHandler EmailAddressConfirmed
        {
            add { AddHandler(EmailAddressConfirmedEvent, value); }
            remove { RemoveHandler(EmailAddressConfirmedEvent, value); }
        }

        void RaisePhoneNumberConfirmedEvent(string addressValue)
        {
            EmailAddressRoutedEventArgs newEventArgs =
                  new EmailAddressRoutedEventArgs(ccAddEmailAddress.EmailAddressConfirmedEvent,
                                                 addressValue);
            RaiseEvent(newEventArgs);
        }

        public ccAddEmailAddress()
        {
            InitializeComponent();
            localStorage = new LocalStorage();

            cachedEmailAddresses = localStorage.LocalEMailAddresses();

            if (cachedEmailAddresses != null)
                dc = new emailsDataContext(cachedEmailAddresses);
            else dc = new emailsDataContext();

            cmbAutoComplete.ItemsSource = dc.cachedEmailAddresses;
        }

        private void txtEmailAddress_KeyUp(object sender, KeyEventArgs e)
        {
            refreshAutoComplete();

            if (txtEmailAddress.Text.Length < 5 || txtEmailAddress.Text.Length > Consts.DEFAULT_MAX_EMAIL_ADDRESS_LENGTH || txtEmailAddress.Text.Contains("@") == false)
            {
                btnConfirm.IsEnabled = false;
            }
            else
            {
                btnConfirm.IsEnabled = true;

                if (e.Key == Key.Enter)
                {
                    btnConfirm_Click(this, e);
                }
            }
        }

        private void refreshAutoComplete()
        {
            var dynamicItemsSouce = dc.cachedEmailAddresses.Where(s => s.StartsWith(txtEmailAddress.Text, System.StringComparison.InvariantCultureIgnoreCase)).Take(8).ToList();

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

            txtEmailAddress.Focus();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var updatedList = localStorage.AddEmailAddress(txtEmailAddress.Text);
            dc.UpdateList(updatedList);

            cmbAutoComplete.Visibility = Visibility.Hidden;
            cmbAutoComplete.IsDropDownOpen = false;

            RaisePhoneNumberConfirmedEvent(txtEmailAddress.Text);
            txtEmailAddress.Text = "";
        }

        private void cmbAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAutoComplete.Visibility = Visibility.Hidden;
            if (cmbAutoComplete.SelectedValue != null)
                txtEmailAddress.Text = cmbAutoComplete.SelectedValue.ToString();
        }
    }
}
