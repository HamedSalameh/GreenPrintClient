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

        private void formGreenPrintClientMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

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
    }
}
