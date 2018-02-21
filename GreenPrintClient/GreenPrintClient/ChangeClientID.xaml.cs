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
            DialogResult = true;
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
