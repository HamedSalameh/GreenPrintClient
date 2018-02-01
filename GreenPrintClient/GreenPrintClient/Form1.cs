using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenPrintClient
{
    public partial class formGreenPrintClientMain : Form
    {
        public formGreenPrintClientMain()
        {
            InitializeComponent();
        }
       
        private void Init()
        {
            this.WindowState = FormWindowState.Minimized;
            errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            errorProvider.BlinkRate = 0;

            txtSMSNumber.Enabled = cbRecipientSMS.Checked;

        }

        private void formGreenPrintClientMain_Load(object sender, EventArgs e)
        {
            Init();

            var path = @"c:\temp";
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) //<<======Now it wont give exception**
            {
                //Load data correspondin to "MyName"
                //Populate a globale variable List<string> which will be
                //bound to grid at some later stage
                if (InvokeRequired)
                {
                    // after we've done all the processing, 
                    this.Invoke(new MethodInvoker(delegate {
                        // load the control with the appropriate data
                        this.WindowState = FormWindowState.Normal;
                    }));
                    return;
                }
            }
            MessageBox.Show("Change detected!") ;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        private void txtClientID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientID.Text)== false && txtClientID.Text.Length < 5)
            {
                errorProvider.SetError(txtClientID, "ClientID value is too short");
            }
            else
            {
                errorProvider.SetError(txtClientID, "");
            }
        }

        private void rbRemoteSign_CheckedChanged(object sender, EventArgs e)
        {
            cbRecipientSMS.Enabled = rbRemoteSign.Checked;
            txtSMSNumber.Enabled = cbRecipientSMS.Checked;

            cbRecipientEmail.Enabled = rbRemoteSign.Checked;
            txtEmailAddress.Enabled = cbRecipientEmail.Checked;
        }
        private void cbRecipientSMS_CheckedChanged(object sender, EventArgs e)
        {
            txtSMSNumber.Enabled = cbRecipientSMS.Checked;

            if (txtSMSNumber.Enabled == false)
            {
                txtSMSNumber.Text = "";
                errorProvider.SetError(txtSMSNumber, "");
            }
                
        }
        private void cbRecipientEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailAddress.Enabled = cbRecipientEmail.Checked;
        }

        // Validations
        private void txtSMSNumber_Leave(object sender, EventArgs e)
        {
            if (txtSMSNumber.Enabled)
            {
                if (string.IsNullOrEmpty(txtSMSNumber.Text))
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number not set");
                }
                else if (txtSMSNumber.Text.Length > Consts.DEFAULT_MAX_PHONE_NUMBER_LENGTH)
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number is too long");
                }
                else if (txtSMSNumber.Text.Length < Consts.DEFAULT_MIN_PHONE_NUMBER_LENGTH)
                {
                    errorProvider.SetError(txtSMSNumber, "Phone number is too short");
                }
                else
                {
                    errorProvider.SetError(txtSMSNumber, "");
                }
            }
        }
        private void txtEmailAddress_Leave(object sender, EventArgs e)
        {
            if (txtEmailAddress.Enabled)
            {
                if (string.IsNullOrEmpty(txtEmailAddress.Text))
                {
                    errorProvider.SetError(txtEmailAddress, "Email address not set");
                }
                else if (txtEmailAddress.Text.Length > Consts.DEFAULT_MAX_EMAIL_ADDRESS_LENGTH)
                {
                    errorProvider.SetError(txtEmailAddress, "Email address is too long");
                }
                else
                {
                    errorProvider.SetError(txtEmailAddress, "");
                }
            }
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (lstCCList.Items.Count > Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC)
            {
                MessageBox.Show($"You have reached the maximum supported number of recipients ({Consts.DEFAULT_MAX_SUPPORTED_ITEMS_IN_CC})",
                    "Add Recipient",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

                return;
            }

            lstCCList.Items.Add(txtAddCC.Text);
            txtAddCC.Text = "";
        }
    }
}
