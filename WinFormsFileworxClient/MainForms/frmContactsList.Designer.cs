namespace Fileworx_Client.MainForms
{
    partial class frmContactsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContactsList));
            this.lvwContacts = new System.Windows.Forms.ListView();
            this.colutmnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblDataStoreSourceTitle = new System.Windows.Forms.Label();
            this.cboDataStoreSource = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.signOutButton = new System.Windows.Forms.Button();
            this.mnuMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAddContact = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.cmsUsersList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiEditContact = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRemoveContact = new System.Windows.Forms.ToolStripMenuItem();
            this.disableContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.mnuMenuStrip.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.cmsUsersList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwContacts
            // 
            this.lvwContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwContacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colutmnHeader1,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwContacts.FullRowSelect = true;
            this.lvwContacts.HideSelection = false;
            this.lvwContacts.Location = new System.Drawing.Point(21, 103);
            this.lvwContacts.MultiSelect = false;
            this.lvwContacts.Name = "lvwContacts";
            this.lvwContacts.Size = new System.Drawing.Size(1115, 504);
            this.lvwContacts.TabIndex = 90;
            this.lvwContacts.UseCompatibleStateImageBehavior = false;
            this.lvwContacts.View = System.Windows.Forms.View.Details;
            this.lvwContacts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwContacts_ItemChecked);
            this.lvwContacts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwContacts_MouseClick);
            // 
            // colutmnHeader1
            // 
            this.colutmnHeader1.Text = "Contact Name";
            this.colutmnHeader1.Width = 329;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Direction";
            this.columnHeader1.Width = 221;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Creation Date";
            this.columnHeader2.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.signOutButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 60);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 60);
            this.panel1.TabIndex = 89;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblDataStoreSourceTitle);
            this.panel3.Controls.Add(this.cboDataStoreSource);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(942, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 60);
            this.panel3.TabIndex = 88;
            // 
            // lblDataStoreSourceTitle
            // 
            this.lblDataStoreSourceTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDataStoreSourceTitle.AutoSize = true;
            this.lblDataStoreSourceTitle.Location = new System.Drawing.Point(12, 9);
            this.lblDataStoreSourceTitle.Name = "lblDataStoreSourceTitle";
            this.lblDataStoreSourceTitle.Size = new System.Drawing.Size(95, 13);
            this.lblDataStoreSourceTitle.TabIndex = 87;
            this.lblDataStoreSourceTitle.Text = "Data Store Source";
            // 
            // cboDataStoreSource
            // 
            this.cboDataStoreSource.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cboDataStoreSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataStoreSource.FormattingEnabled = true;
            this.cboDataStoreSource.Items.AddRange(new object[] {
            "Database",
            "Elasticsearch"});
            this.cboDataStoreSource.Location = new System.Drawing.Point(10, 27);
            this.cboDataStoreSource.Name = "cboDataStoreSource";
            this.cboDataStoreSource.Size = new System.Drawing.Size(103, 21);
            this.cboDataStoreSource.TabIndex = 86;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefresh.Location = new System.Drawing.Point(119, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 85;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(25, 8);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(198, 41);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 80;
            this.picLogo.TabStop = false;
            // 
            // signOutButton
            // 
            this.signOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.signOutButton.Location = new System.Drawing.Point(17405, 24);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(75, 23);
            this.signOutButton.TabIndex = 84;
            this.signOutButton.Text = "Sign Out";
            this.signOutButton.UseVisualStyleBackColor = true;
            // 
            // mnuMenuStrip
            // 
            this.mnuMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuMenuStrip.Name = "mnuMenuStrip";
            this.mnuMenuStrip.Size = new System.Drawing.Size(1167, 24);
            this.mnuMenuStrip.TabIndex = 88;
            this.mnuMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiAddContact});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // msiAddContact
            // 
            this.msiAddContact.Name = "msiAddContact";
            this.msiAddContact.Size = new System.Drawing.Size(141, 22);
            this.msiAddContact.Text = "&Add Contact";
            this.msiAddContact.Click += new System.EventHandler(this.msiAddContact_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSend);
            this.pnlButtons.Enabled = false;
            this.pnlButtons.Location = new System.Drawing.Point(21, 629);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1115, 49);
            this.pnlButtons.TabIndex = 91;
            this.pnlButtons.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(940, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1021, 17);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cmsUsersList
            // 
            this.cmsUsersList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiEditContact,
            this.cmiRemoveContact,
            this.disableContactToolStripMenuItem});
            this.cmsUsersList.Name = "contextMenuStrip1";
            this.cmsUsersList.Size = new System.Drawing.Size(181, 92);
            // 
            // cmiEditContact
            // 
            this.cmiEditContact.Name = "cmiEditContact";
            this.cmiEditContact.Size = new System.Drawing.Size(162, 22);
            this.cmiEditContact.Text = "Edit Contact";
            this.cmiEditContact.Click += new System.EventHandler(this.cmiEditContact_Click);
            // 
            // cmiRemoveContact
            // 
            this.cmiRemoveContact.Name = "cmiRemoveContact";
            this.cmiRemoveContact.Size = new System.Drawing.Size(162, 22);
            this.cmiRemoveContact.Text = "Remove Contact";
            this.cmiRemoveContact.Click += new System.EventHandler(this.cmiRemoveContact_Click);
            // 
            // disableContactToolStripMenuItem
            // 
            this.disableContactToolStripMenuItem.Name = "disableContactToolStripMenuItem";
            this.disableContactToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disableContactToolStripMenuItem.Text = "Disable Contact";
            this.disableContactToolStripMenuItem.Click += new System.EventHandler(this.disableContactToolStripMenuItem_Click);
            // 
            // frmContactsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 674);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.lvwContacts);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMenuStrip);
            this.MinimumSize = new System.Drawing.Size(1183, 713);
            this.Name = "frmContactsList";
            this.Text = "Contacts List";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.mnuMenuStrip.ResumeLayout(false);
            this.mnuMenuStrip.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.cmsUsersList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwContacts;
        private System.Windows.Forms.ColumnHeader colutmnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDataStoreSourceTitle;
        private System.Windows.Forms.ComboBox cboDataStoreSource;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.MenuStrip mnuMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiAddContact;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip cmsUsersList;
        private System.Windows.Forms.ToolStripMenuItem cmiEditContact;
        private System.Windows.Forms.ToolStripMenuItem cmiRemoveContact;
        private System.Windows.Forms.ToolStripMenuItem disableContactToolStripMenuItem;
    }
}