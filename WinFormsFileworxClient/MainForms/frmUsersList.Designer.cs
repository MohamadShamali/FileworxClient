namespace Fileworx_Client
{
    partial class frmUsersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsersList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwUsers = new System.Windows.Forms.ListView();
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.mnuMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblIsAdmin = new System.Windows.Forms.Label();
            this.lblIsAdminTitle = new System.Windows.Forms.Label();
            this.lblNamee = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.lblUsernameTitle = new System.Windows.Forms.Label();
            this.cmsUsersList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiEditUser = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRemoveUser = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.mnuMenuStrip.SuspendLayout();
            this.cmsUsersList.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel1.Controls.Add(this.lvwUsers);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.mnuMenuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel2.Controls.Add(this.lblIsAdmin);
            this.splitContainer1.Panel2.Controls.Add(this.lblIsAdminTitle);
            this.splitContainer1.Panel2.Controls.Add(this.lblNamee);
            this.splitContainer1.Panel2.Controls.Add(this.lblUsername);
            this.splitContainer1.Panel2.Controls.Add(this.lblNameTitle);
            this.splitContainer1.Panel2.Controls.Add(this.lblUsernameTitle);
            this.splitContainer1.Size = new System.Drawing.Size(1167, 674);
            this.splitContainer1.SplitterDistance = 578;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvwUsers
            // 
            this.lvwUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colutmnHeader1,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwUsers.FullRowSelect = true;
            this.lvwUsers.HideSelection = false;
            this.lvwUsers.Location = new System.Drawing.Point(25, 90);
            this.lvwUsers.MultiSelect = false;
            this.lvwUsers.Name = "lvwUsers";
            this.lvwUsers.Size = new System.Drawing.Size(1115, 478);
            this.lvwUsers.TabIndex = 87;
            this.lvwUsers.UseCompatibleStateImageBehavior = false;
            this.lvwUsers.View = System.Windows.Forms.View.Details;
            this.lvwUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.usersListView_MouseClick);
            // 
            // colutmnHeader1
            // 
            this.colutmnHeader1.Text = "Username";
            this.colutmnHeader1.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Is Admin?";
            this.columnHeader2.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.signOutButton);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 60);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 60);
            this.panel1.TabIndex = 86;
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
            this.picLogo.Location = new System.Drawing.Point(25, 7);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(198, 41);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 80;
            this.picLogo.TabStop = false;
            // 
            // signOutButton
            // 
            this.signOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.signOutButton.Location = new System.Drawing.Point(13904, 24);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(75, 23);
            this.signOutButton.TabIndex = 84;
            this.signOutButton.Text = "Sign Out";
            this.signOutButton.UseVisualStyleBackColor = true;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(229, 35);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(55, 13);
            this.lblWelcome.TabIndex = 81;
            this.lblWelcome.Text = "Welcome,";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(286, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 82;
            // 
            // mnuMenuStrip
            // 
            this.mnuMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuMenuStrip.Name = "mnuMenuStrip";
            this.mnuMenuStrip.Size = new System.Drawing.Size(1167, 24);
            this.mnuMenuStrip.TabIndex = 35;
            this.mnuMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.addUserToolStripMenuItem.Text = "&Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // lblIsAdmin
            // 
            this.lblIsAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblIsAdmin.Location = new System.Drawing.Point(152, 59);
            this.lblIsAdmin.Name = "lblIsAdmin";
            this.lblIsAdmin.Size = new System.Drawing.Size(341, 13);
            this.lblIsAdmin.TabIndex = 33;
            // 
            // lblIsAdminTitle
            // 
            this.lblIsAdminTitle.AutoSize = true;
            this.lblIsAdminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsAdminTitle.Location = new System.Drawing.Point(48, 57);
            this.lblIsAdminTitle.Name = "lblIsAdminTitle";
            this.lblIsAdminTitle.Size = new System.Drawing.Size(66, 15);
            this.lblIsAdminTitle.TabIndex = 32;
            this.lblIsAdminTitle.Text = "Is Admin:";
            // 
            // lblNamee
            // 
            this.lblNamee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblNamee.Location = new System.Drawing.Point(152, 36);
            this.lblNamee.Name = "lblNamee";
            this.lblNamee.Size = new System.Drawing.Size(341, 13);
            this.lblNamee.TabIndex = 31;
            // 
            // lblUsername
            // 
            this.lblUsername.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblUsername.Location = new System.Drawing.Point(152, 13);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(341, 13);
            this.lblUsername.TabIndex = 30;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(48, 34);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(49, 15);
            this.lblNameTitle.TabIndex = 29;
            this.lblNameTitle.Text = "Name:";
            // 
            // lblUsernameTitle
            // 
            this.lblUsernameTitle.AutoSize = true;
            this.lblUsernameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsernameTitle.Location = new System.Drawing.Point(48, 13);
            this.lblUsernameTitle.Name = "lblUsernameTitle";
            this.lblUsernameTitle.Size = new System.Drawing.Size(77, 15);
            this.lblUsernameTitle.TabIndex = 28;
            this.lblUsernameTitle.Text = "Username:";
            // 
            // cmsUsersList
            // 
            this.cmsUsersList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiEditUser,
            this.cmiRemoveUser});
            this.cmsUsersList.Name = "contextMenuStrip1";
            this.cmsUsersList.Size = new System.Drawing.Size(181, 70);
            // 
            // cmiEditUser
            // 
            this.cmiEditUser.Name = "cmiEditUser";
            this.cmiEditUser.Size = new System.Drawing.Size(180, 22);
            this.cmiEditUser.Text = "Edit User";
            this.cmiEditUser.Click += new System.EventHandler(this.editUserToolStripMenuItem_Click);
            // 
            // cmiRemoveUser
            // 
            this.cmiRemoveUser.Name = "cmiRemoveUser";
            this.cmiRemoveUser.Size = new System.Drawing.Size(180, 22);
            this.cmiRemoveUser.Text = "Remove User";
            this.cmiRemoveUser.Click += new System.EventHandler(this.removeUserToolStripMenuItem_Click);
            // 
            // frmUsersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1167, 674);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1183, 713);
            this.Name = "frmUsersList";
            this.Text = "Users List";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.mnuMenuStrip.ResumeLayout(false);
            this.mnuMenuStrip.PerformLayout();
            this.cmsUsersList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblIsAdmin;
        private System.Windows.Forms.Label lblIsAdminTitle;
        private System.Windows.Forms.Label lblNamee;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.Label lblUsernameTitle;
        private System.Windows.Forms.MenuStrip mnuMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ListView lvwUsers;
        private System.Windows.Forms.ColumnHeader colutmnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip cmsUsersList;
        private System.Windows.Forms.ToolStripMenuItem cmiEditUser;
        private System.Windows.Forms.ToolStripMenuItem cmiRemoveUser;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDataStoreSourceTitle;
        private System.Windows.Forms.ComboBox cboDataStoreSource;
        private System.Windows.Forms.Button btnRefresh;
    }
}