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
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.cmbCountriesPhonePrefix = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDocumentName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtClientID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Details";
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(99, 51);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(238, 20);
            this.txtDocumentName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Document Name";
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(99, 25);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(238, 20);
            this.txtClientID.TabIndex = 5;
            this.txtClientID.Leave += new System.EventHandler(this.txtClientID_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 293);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Signing && Delivery options";
            // 
            // txtAddCC
            // 
            this.txtAddCC.Location = new System.Drawing.Point(15, 158);
            this.txtAddCC.Name = "txtAddCC";
            this.txtAddCC.Size = new System.Drawing.Size(252, 20);
            this.txtAddCC.TabIndex = 28;
            // 
            // btnAddToList
            // 
            this.btnAddToList.Location = new System.Drawing.Point(277, 155);
            this.btnAddToList.Name = "btnAddToList";
            this.btnAddToList.Size = new System.Drawing.Size(75, 23);
            this.btnAddToList.TabIndex = 27;
            this.btnAddToList.Text = "Add";
            this.btnAddToList.UseVisualStyleBackColor = true;
            this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Send signed document as CC to ...";
            // 
            // lstCCList
            // 
            this.lstCCList.FormattingEnabled = true;
            this.lstCCList.Location = new System.Drawing.Point(15, 184);
            this.lstCCList.Name = "lstCCList";
            this.lstCCList.Size = new System.Drawing.Size(337, 95);
            this.lstCCList.TabIndex = 25;
            // 
            // rbRemoteSign
            // 
            this.rbRemoteSign.AutoSize = true;
            this.rbRemoteSign.Location = new System.Drawing.Point(9, 56);
            this.rbRemoteSign.Name = "rbRemoteSign";
            this.rbRemoteSign.Size = new System.Drawing.Size(133, 17);
            this.rbRemoteSign.TabIndex = 22;
            this.rbRemoteSign.Text = "Sign via remote device";
            this.rbRemoteSign.UseVisualStyleBackColor = true;
            this.rbRemoteSign.CheckedChanged += new System.EventHandler(this.rbRemoteSign_CheckedChanged);
            // 
            // rbDeviceSign
            // 
            this.rbDeviceSign.AutoSize = true;
            this.rbDeviceSign.Checked = true;
            this.rbDeviceSign.Location = new System.Drawing.Point(9, 33);
            this.rbDeviceSign.Name = "rbDeviceSign";
            this.rbDeviceSign.Size = new System.Drawing.Size(152, 17);
            this.rbDeviceSign.TabIndex = 21;
            this.rbDeviceSign.TabStop = true;
            this.rbDeviceSign.Text = "Sign via connected device";
            this.rbDeviceSign.UseVisualStyleBackColor = true;
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Enabled = false;
            this.txtEmailAddress.Location = new System.Drawing.Point(124, 102);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(228, 20);
            this.txtEmailAddress.TabIndex = 20;
            this.txtEmailAddress.Leave += new System.EventHandler(this.txtEmailAddress_Leave);
            // 
            // cbRecipientEmail
            // 
            this.cbRecipientEmail.AutoSize = true;
            this.cbRecipientEmail.Enabled = false;
            this.cbRecipientEmail.Location = new System.Drawing.Point(28, 104);
            this.cbRecipientEmail.Name = "cbRecipientEmail";
            this.cbRecipientEmail.Size = new System.Drawing.Size(92, 17);
            this.cbRecipientEmail.TabIndex = 19;
            this.cbRecipientEmail.Text = "Sign via Email";
            this.cbRecipientEmail.UseVisualStyleBackColor = true;
            this.cbRecipientEmail.CheckedChanged += new System.EventHandler(this.cbRecipientEmail_CheckedChanged);
            // 
            // txtSMSNumber
            // 
            this.txtSMSNumber.Enabled = false;
            this.txtSMSNumber.Location = new System.Drawing.Point(192, 76);
            this.txtSMSNumber.Name = "txtSMSNumber";
            this.txtSMSNumber.Size = new System.Drawing.Size(160, 20);
            this.txtSMSNumber.TabIndex = 18;
            this.txtSMSNumber.Leave += new System.EventHandler(this.txtSMSNumber_Leave);
            // 
            // cbRecipientSMS
            // 
            this.cbRecipientSMS.AutoSize = true;
            this.cbRecipientSMS.Enabled = false;
            this.cbRecipientSMS.Location = new System.Drawing.Point(28, 79);
            this.cbRecipientSMS.Name = "cbRecipientSMS";
            this.cbRecipientSMS.Size = new System.Drawing.Size(90, 17);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 397);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 105);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(15, 19);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(337, 46);
            this.txtMessages.TabIndex = 31;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(192, 71);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(277, 71);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 29;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbCountriesPhonePrefix
            // 
            this.cmbCountriesPhonePrefix.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCountriesPhonePrefix.FormattingEnabled = true;
            this.cmbCountriesPhonePrefix.Location = new System.Drawing.Point(124, 75);
            this.cmbCountriesPhonePrefix.Name = "cmbCountriesPhonePrefix";
            this.cmbCountriesPhonePrefix.Size = new System.Drawing.Size(62, 21);
            this.cmbCountriesPhonePrefix.TabIndex = 29;
            this.cmbCountriesPhonePrefix.SelectedIndexChanged += new System.EventHandler(this.cmbCountriesPhonePrefix_SelectedIndexChanged);
            this.cmbCountriesPhonePrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCountriesPhonePrefix_KeyPress);
            // 
            // formGreenPrintClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 515);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formGreenPrintClientMain";
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
    }
}

