using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GreenPrintClient.CustomControls
{
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
        }

        private void txtEmailAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEmailAddress.Text.Length < 5 || txtEmailAddress.Text.Length > Consts.DEFAULT_MAX_EMAIL_ADDRESS_LENGTH || txtEmailAddress.Text.Contains("@") == false)
            {
                btnConfirm.IsEnabled = false;
            }
            else
            {
                btnConfirm.IsEnabled = true;

                if(e.Key == Key.Enter)
                {
                    btnConfirm_Click(this, e);
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            RaisePhoneNumberConfirmedEvent(txtEmailAddress.Text);
            txtEmailAddress.Text = "";
        }
    }
}
