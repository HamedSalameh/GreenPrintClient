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

namespace GreenPrintClient.CustomControls
{
    /// <summary>
    /// Interaction logic for ccAddPhoneNumber.xaml
    /// </summary>
    public partial class ccAddPhoneNumber : UserControl
    {
        public ccAddPhoneNumber()
        {
            InitializeComponent();
        }

        public void SetItemSource(Dictionary<string, string> source, int defaultIndex)
        {
            cmbCountryPhonePrefix.ItemsSource = source;
            cmbCountryPhonePrefix.DisplayMemberPath = "Key";
            cmbCountryPhonePrefix.SelectedValuePath = "Value";

            if (cmbCountryPhonePrefix.Items.Count > defaultIndex)
                cmbCountryPhonePrefix.SelectedItem = cmbCountryPhonePrefix.Items[defaultIndex];
        }

        private void cmbCountryPhonePrefix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
