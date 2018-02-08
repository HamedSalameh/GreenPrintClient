namespace GreenPrintClient
{
    partial class formGreenPrintClientMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGreenPrintClientMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbCountriesPhonePrefix = new System.Windows.Forms.ComboBox();
            this.txtAddCC = new System.Windows.Forms.TextBox();
            this.btnAddToList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstCCList = new System.Windows.Forms.ListBox();
            this.rbRemoteSign = new System.Windows.Forms.RadioButton();
            this.rbDeviceSign = new System.Windows.Forms.RadioButton();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.cbRecipientEmail = new System.Windows.Forms.CheckBox();
            this.txtSMSNumber = new System.Windows.Forms.TextBox();
            this.cbRecipientSMS = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtDocumentName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtClientID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(487, 98);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Details";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Change ...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(132, 63);
            this.txtDocumentName.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(316, 22);
            this.txtDocumentName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Document Name";
            // 
            // txtClientID
            // 
            this.txtClientID.Enabled = false;
            this.txtClientID.Location = new System.Drawing.Point(132, 31);
            this.txtClientID.Margin = new System.Windows.Forms.Padding(4);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(217, 22);
            this.txtClientID.TabIndex = 5;
            this.txtClientID.Leave += new System.EventHandler(this.txtClientID_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Client ID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbCountriesPhonePrefix);
            this.groupBox2.Controls.Add(this.txtAddCC);
            this.groupBox2.Controls.Add(this.btnAddToList);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lstCCList);
            this.groupBox2.Controls.Add(this.rbRemoteSign);
            this.groupBox2.Controls.Add(this.rbDeviceSign);
            this.groupBox2.Controls.Add(this.txtEmailAddress);
            this.groupBox2.Controls.Add(this.cbRecipientEmail);
            this.groupBox2.Controls.Add(this.txtSMSNumber);
            this.groupBox2.Controls.Add(this.cbRecipientSMS);
            this.groupBox2.Location = new System.Drawing.Point(16, 121);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(487, 361);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Signing && Delivery options";
            // 
            // cmbCountriesPhonePrefix
            // 
            this.cmbCountriesPhonePrefix.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCountriesPhonePrefix.FormattingEnabled = true;
            this.cmbCountriesPhonePrefix.Location = new System.Drawing.Point(165, 92);
            this.cmbCountriesPhonePrefix.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCountriesPhonePrefix.Name = "cmbCountriesPhonePrefix";
            this.cmbCountriesPhonePrefix.Size = new System.Drawing.Size(81, 24);
            this.cmbCountriesPhonePrefix.TabIndex = 29;
            this.cmbCountriesPhonePrefix.SelectedIndexChanged += new System.EventHandler(this.cmbCountriesPhonePrefix_SelectedIndexChanged);
            this.cmbCountriesPhonePrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCountriesPhonePrefix_KeyPress);
            // 
            // txtAddCC
            // 
            this.txtAddCC.Location = new System.Drawing.Point(20, 194);
            this.txtAddCC.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddCC.Name = "txtAddCC";
            this.txtAddCC.Size = new System.Drawing.Size(335, 22);
            this.txtAddCC.TabIndex = 28;
            // 
            // btnAddToList
            // 
            this.btnAddToList.Location = new System.Drawing.Point(369, 191);
            this.btnAddToList.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddToList.Name = "btnAddToList";
            this.btnAddToList.Size = new System.Drawing.Size(100, 28);
            this.btnAddToList.TabIndex = 27;
            this.btnAddToList.Text = "Add";
            this.btnAddToList.UseVisualStyleBackColor = true;
            this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Send signed document as CC to ...";
            // 
            // lstCCList
            // 
            this.lstCCList.FormattingEnabled = true;
            this.lstCCList.ItemHeight = 16;
            this.lstCCList.Location = new System.Drawing.Point(20, 226);
            this.lstCCList.Margin = new System.Windows.Forms.Padding(4);
            this.lstCCList.Name = "lstCCList";
            this.lstCCList.Size = new System.Drawing.Size(448, 116);
            this.lstCCList.TabIndex = 25;
            // 
            // rbRemoteSign
            // 
            this.rbRemoteSign.AutoSize = true;
            this.rbRemoteSign.Location = new System.Drawing.Point(12, 69);
            this.rbRemoteSign.Margin = new System.Windows.Forms.Padding(4);
            this.rbRemoteSign.Name = "rbRemoteSign";
            this.rbRemoteSign.Size = new System.Drawing.Size(172, 21);
            this.rbRemoteSign.TabIndex = 22;
            this.rbRemoteSign.Text = "Sign via remote device";
            this.rbRemoteSign.UseVisualStyleBackColor = true;
            this.rbRemoteSign.CheckedChanged += new System.EventHandler(this.rbRemoteSign_CheckedChanged);
            // 
            // rbDeviceSign
            // 
            this.rbDeviceSign.AutoSize = true;
            this.rbDeviceSign.Checked = true;
            this.rbDeviceSign.Location = new System.Drawing.Point(12, 41);
            this.rbDeviceSign.Margin = new System.Windows.Forms.Padding(4);
            this.rbDeviceSign.Name = "rbDeviceSign";
            this.rbDeviceSign.Size = new System.Drawing.Size(194, 21);
            this.rbDeviceSign.TabIndex = 21;
            this.rbDeviceSign.TabStop = true;
            this.rbDeviceSign.Text = "Sign via connected device";
            this.rbDeviceSign.UseVisualStyleBackColor = true;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Enabled = false;
            this.txtEmailAddress.Location = new System.Drawing.Point(165, 126);
            this.txtEmailAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(303, 22);
            this.txtEmailAddress.TabIndex = 20;
            this.txtEmailAddress.TextChanged += new System.EventHandler(this.txtEmailAddress_TextChanged);
            this.txtEmailAddress.Leave += new System.EventHandler(this.txtEmailAddress_Leave);
            // 
            // cbRecipientEmail
            // 
            this.cbRecipientEmail.AutoSize = true;
            this.cbRecipientEmail.Enabled = false;
            this.cbRecipientEmail.Location = new System.Drawing.Point(37, 128);
            this.cbRecipientEmail.Margin = new System.Windows.Forms.Padding(4);
            this.cbRecipientEmail.Name = "cbRecipientEmail";
            this.cbRecipientEmail.Size = new System.Drawing.Size(118, 21);
            this.cbRecipientEmail.TabIndex = 19;
            this.cbRecipientEmail.Text = "Sign via Email";
            this.cbRecipientEmail.UseVisualStyleBackColor = true;
            this.cbRecipientEmail.CheckedChanged += new System.EventHandler(this.cbRecipientEmail_CheckedChanged);
            // 
            // txtSMSNumber
            // 
            this.txtSMSNumber.Enabled = false;
            this.txtSMSNumber.Location = new System.Drawing.Point(256, 94);
            this.txtSMSNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtSMSNumber.Name = "txtSMSNumber";
            this.txtSMSNumber.Size = new System.Drawing.Size(212, 22);
            this.txtSMSNumber.TabIndex = 18;
            this.txtSMSNumber.Leave += new System.EventHandler(this.txtSMSNumber_Leave);
            // 
            // cbRecipientSMS
            // 
            this.cbRecipientSMS.AutoSize = true;
            this.cbRecipientSMS.Enabled = false;
            this.cbRecipientSMS.Location = new System.Drawing.Point(37, 97);
            this.cbRecipientSMS.Margin = new System.Windows.Forms.Padding(4);
            this.cbRecipientSMS.Name = "cbRecipientSMS";
            this.cbRecipientSMS.Size = new System.Drawing.Size(113, 21);
            this.cbRecipientSMS.TabIndex = 17;
            this.cbRecipientSMS.Text = "Sign via SMS";
            this.cbRecipientSMS.UseVisualStyleBackColor = true;
            this.cbRecipientSMS.CheckedChanged += new System.EventHandler(this.cbRecipientSMS_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMessages);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnSubmit);
            this.groupBox3.Location = new System.Drawing.Point(16, 489);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(487, 129);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(20, 23);
            this.txtMessages.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(448, 56);
            this.txtMessages.TabIndex = 31;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(256, 87);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(369, 87);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 28);
            this.btnSubmit.TabIndex = 29;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // formGreenPrintClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 634);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "formGreenPrintClientMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GreenPrintClient";
            this.Load += new System.EventHandler(this.formGreenPrintClientMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocumentName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAddCC;
        private System.Windows.Forms.Button btnAddToList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstCCList;
        private System.Windows.Forms.RadioButton rbRemoteSign;
        private System.Windows.Forms.RadioButton rbDeviceSign;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.CheckBox cbRecipientEmail;
        private System.Windows.Forms.TextBox txtSMSNumber;
        private System.Windows.Forms.CheckBox cbRecipientSMS;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cmbCountriesPhonePrefix;
        private System.Windows.Forms.Button button1;
    }
}

