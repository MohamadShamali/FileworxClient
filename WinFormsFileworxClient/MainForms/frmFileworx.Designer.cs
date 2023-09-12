namespace Fileworx_Client
{
    partial class frmFileworx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileworx));
            this.txtBody = new System.Windows.Forms.RichTextBox();
            this.tpgPreview = new System.Windows.Forms.TabPage();
            this.tclPreview = new System.Windows.Forms.TabControl();
            this.tpgImage = new System.Windows.Forms.TabPage();
            this.picImagePreview = new System.Windows.Forms.PictureBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.lblDateTitle = new System.Windows.Forms.Label();
            this.lblTitleTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colutmnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelSending = new System.Windows.Forms.Button();
            this.btnSendTo = new System.Windows.Forms.Button();
            this.pnlDataSourceSelection = new System.Windows.Forms.Panel();
            this.lblDataStoreSourceTitle = new System.Windows.Forms.Label();
            this.cboDataStoreSource = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.signOutButton = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.msiUsersList = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSortByAlphabetically = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSortByOldest = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSortByRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSortBy = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAddNews = new System.Windows.Forms.ToolStripMenuItem();
            this.msiAddImage = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMenuStrip = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwFiles = new System.Windows.Forms.ListView();
            this.cmsFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiEditFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRemoveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tpgPreview.SuspendLayout();
            this.tclPreview.SuspendLayout();
            this.tpgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlDataSourceSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.mnuMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBody
            // 
            this.txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBody.Location = new System.Drawing.Point(3, 3);
            this.txtBody.Name = "txtBody";
            this.txtBody.ReadOnly = true;
            this.txtBody.Size = new System.Drawing.Size(1153, 108);
            this.txtBody.TabIndex = 0;
            this.txtBody.Text = "";
            // 
            // tpgPreview
            // 
            this.tpgPreview.Controls.Add(this.txtBody);
            this.tpgPreview.Location = new System.Drawing.Point(4, 22);
            this.tpgPreview.Name = "tpgPreview";
            this.tpgPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPreview.Size = new System.Drawing.Size(1159, 114);
            this.tpgPreview.TabIndex = 0;
            this.tpgPreview.Text = "Preview";
            this.tpgPreview.UseVisualStyleBackColor = true;
            // 
            // tclPreview
            // 
            this.tclPreview.Controls.Add(this.tpgPreview);
            this.tclPreview.Controls.Add(this.tpgImage);
            this.tclPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tclPreview.Location = new System.Drawing.Point(0, 90);
            this.tclPreview.Name = "tclPreview";
            this.tclPreview.SelectedIndex = 0;
            this.tclPreview.Size = new System.Drawing.Size(1167, 140);
            this.tclPreview.TabIndex = 82;
            // 
            // tpgImage
            // 
            this.tpgImage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tpgImage.Controls.Add(this.picImagePreview);
            this.tpgImage.Location = new System.Drawing.Point(4, 22);
            this.tpgImage.Name = "tpgImage";
            this.tpgImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpgImage.Size = new System.Drawing.Size(1159, 114);
            this.tpgImage.TabIndex = 1;
            this.tpgImage.Text = "Image";
            // 
            // picImagePreview
            // 
            this.picImagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImagePreview.Location = new System.Drawing.Point(3, 3);
            this.picImagePreview.Name = "picImagePreview";
            this.picImagePreview.Size = new System.Drawing.Size(1153, 108);
            this.picImagePreview.TabIndex = 0;
            this.picImagePreview.TabStop = false;
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(125, 64);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(0, 15);
            this.lblCategory.TabIndex = 92;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(125, 37);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 15);
            this.lblDate.TabIndex = 91;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(125, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 15);
            this.lblTitle.TabIndex = 90;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryTitle.Location = new System.Drawing.Point(35, 64);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(67, 15);
            this.lblCategoryTitle.TabIndex = 89;
            this.lblCategoryTitle.Text = "Category:";
            // 
            // lblDateTitle
            // 
            this.lblDateTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDateTitle.AutoSize = true;
            this.lblDateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTitle.Location = new System.Drawing.Point(35, 37);
            this.lblDateTitle.Name = "lblDateTitle";
            this.lblDateTitle.Size = new System.Drawing.Size(41, 15);
            this.lblDateTitle.TabIndex = 88;
            this.lblDateTitle.Text = "Date:";
            // 
            // lblTitleTitle
            // 
            this.lblTitleTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitleTitle.AutoSize = true;
            this.lblTitleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleTitle.Location = new System.Drawing.Point(35, 10);
            this.lblTitleTitle.Name = "lblTitleTitle";
            this.lblTitleTitle.Size = new System.Drawing.Size(39, 15);
            this.lblTitleTitle.TabIndex = 87;
            this.lblTitleTitle.Text = "Title:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCategory);
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.lblCategoryTitle);
            this.panel2.Controls.Add(this.lblDateTitle);
            this.panel2.Controls.Add(this.lblTitleTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1167, 90);
            this.panel2.TabIndex = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 714;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Creation Date";
            this.columnHeader1.Width = 172;
            // 
            // colutmnHeader1
            // 
            this.colutmnHeader1.Text = "Title";
            this.colutmnHeader1.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlDataSourceSelection);
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
            this.panel1.TabIndex = 85;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnCancelSending);
            this.panel3.Controls.Add(this.btnSendTo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(757, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 60);
            this.panel3.TabIndex = 88;
            this.panel3.Visible = false;
            // 
            // btnCancelSending
            // 
            this.btnCancelSending.Location = new System.Drawing.Point(8, 30);
            this.btnCancelSending.Name = "btnCancelSending";
            this.btnCancelSending.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSending.TabIndex = 1;
            this.btnCancelSending.Text = "Cancel";
            this.btnCancelSending.UseVisualStyleBackColor = true;
            this.btnCancelSending.Click += new System.EventHandler(this.btnCancelSending_Click);
            // 
            // btnSendTo
            // 
            this.btnSendTo.Location = new System.Drawing.Point(89, 30);
            this.btnSendTo.Name = "btnSendTo";
            this.btnSendTo.Size = new System.Drawing.Size(75, 23);
            this.btnSendTo.TabIndex = 0;
            this.btnSendTo.Text = "Send To";
            this.btnSendTo.UseVisualStyleBackColor = true;
            this.btnSendTo.Click += new System.EventHandler(this.btnSendTo_Click);
            // 
            // pnlDataSourceSelection
            // 
            this.pnlDataSourceSelection.BackColor = System.Drawing.Color.Transparent;
            this.pnlDataSourceSelection.Controls.Add(this.lblDataStoreSourceTitle);
            this.pnlDataSourceSelection.Controls.Add(this.cboDataStoreSource);
            this.pnlDataSourceSelection.Controls.Add(this.btnRefresh);
            this.pnlDataSourceSelection.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDataSourceSelection.Location = new System.Drawing.Point(937, 0);
            this.pnlDataSourceSelection.Name = "pnlDataSourceSelection";
            this.pnlDataSourceSelection.Size = new System.Drawing.Size(230, 60);
            this.pnlDataSourceSelection.TabIndex = 87;
            // 
            // lblDataStoreSourceTitle
            // 
            this.lblDataStoreSourceTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataStoreSourceTitle.AutoSize = true;
            this.lblDataStoreSourceTitle.Location = new System.Drawing.Point(17, 12);
            this.lblDataStoreSourceTitle.Name = "lblDataStoreSourceTitle";
            this.lblDataStoreSourceTitle.Size = new System.Drawing.Size(95, 13);
            this.lblDataStoreSourceTitle.TabIndex = 87;
            this.lblDataStoreSourceTitle.Text = "Data Store Source";
            // 
            // cboDataStoreSource
            // 
            this.cboDataStoreSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDataStoreSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataStoreSource.FormattingEnabled = true;
            this.cboDataStoreSource.Items.AddRange(new object[] {
            "Database",
            "Elasticsearch"});
            this.cboDataStoreSource.Location = new System.Drawing.Point(15, 30);
            this.cboDataStoreSource.Name = "cboDataStoreSource";
            this.cboDataStoreSource.Size = new System.Drawing.Size(103, 21);
            this.cboDataStoreSource.TabIndex = 86;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(124, 28);
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
            this.signOutButton.Location = new System.Drawing.Point(18572, 24);
            this.signOutButton.Name = "signOutButton";
            this.signOutButton.Size = new System.Drawing.Size(75, 23);
            this.signOutButton.TabIndex = 84;
            this.signOutButton.Text = "Sign Out";
            this.signOutButton.UseVisualStyleBackColor = true;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(226, 34);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(55, 13);
            this.lblWelcome.TabIndex = 81;
            this.lblWelcome.Text = "Welcome,";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(287, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 82;
            // 
            // msiUsersList
            // 
            this.msiUsersList.Name = "msiUsersList";
            this.msiUsersList.Size = new System.Drawing.Size(142, 22);
            this.msiUsersList.Text = "Users List";
            this.msiUsersList.Click += new System.EventHandler(this.usersListToolStripMenuItem_Click);
            // 
            // msiSortByAlphabetically
            // 
            this.msiSortByAlphabetically.Name = "msiSortByAlphabetically";
            this.msiSortByAlphabetically.Size = new System.Drawing.Size(149, 22);
            this.msiSortByAlphabetically.Text = "Alphabetically";
            this.msiSortByAlphabetically.Click += new System.EventHandler(this.alphabeticallyToolStripMenuItem_Click);
            // 
            // msiSortByOldest
            // 
            this.msiSortByOldest.Name = "msiSortByOldest";
            this.msiSortByOldest.Size = new System.Drawing.Size(149, 22);
            this.msiSortByOldest.Text = "Oldest";
            this.msiSortByOldest.Click += new System.EventHandler(this.oldestToolStripMenuItem_Click);
            // 
            // msiSortByRecent
            // 
            this.msiSortByRecent.Checked = true;
            this.msiSortByRecent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.msiSortByRecent.Name = "msiSortByRecent";
            this.msiSortByRecent.Size = new System.Drawing.Size(149, 22);
            this.msiSortByRecent.Text = "Recent";
            this.msiSortByRecent.Click += new System.EventHandler(this.recentToolStripMenuItem_Click);
            // 
            // msiSortBy
            // 
            this.msiSortBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiSortByRecent,
            this.msiSortByOldest,
            this.msiSortByAlphabetically});
            this.msiSortBy.Name = "msiSortBy";
            this.msiSortBy.Size = new System.Drawing.Size(142, 22);
            this.msiSortBy.Text = "Sort By..";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiSortBy,
            this.msiUsersList,
            this.contactsListToolStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // contactsListToolStripMenuItem
            // 
            this.contactsListToolStripMenuItem.Name = "contactsListToolStripMenuItem";
            this.contactsListToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.contactsListToolStripMenuItem.Text = "Contacts List";
            this.contactsListToolStripMenuItem.Click += new System.EventHandler(this.contactsListToolStripMenuItem_Click);
            // 
            // msiAddNews
            // 
            this.msiAddNews.Name = "msiAddNews";
            this.msiAddNews.Size = new System.Drawing.Size(132, 22);
            this.msiAddNews.Text = "Add News";
            this.msiAddNews.Click += new System.EventHandler(this.addNewsToolStripMenuItem_Click);
            // 
            // msiAddImage
            // 
            this.msiAddImage.Name = "msiAddImage";
            this.msiAddImage.Size = new System.Drawing.Size(132, 22);
            this.msiAddImage.Text = "Add Image";
            this.msiAddImage.Click += new System.EventHandler(this.addImageToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiAddImage,
            this.msiAddNews,
            this.sendFilesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sendFilesToolStripMenuItem
            // 
            this.sendFilesToolStripMenuItem.Name = "sendFilesToolStripMenuItem";
            this.sendFilesToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.sendFilesToolStripMenuItem.Text = "Send Files";
            this.sendFilesToolStripMenuItem.Click += new System.EventHandler(this.sendFilesToolStripMenuItem_Click);
            // 
            // mnuMenuStrip
            // 
            this.mnuMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.showToolStripMenuItem,
            this.userToolStripMenuItem});
            this.mnuMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuMenuStrip.Name = "mnuMenuStrip";
            this.mnuMenuStrip.Size = new System.Drawing.Size(1167, 24);
            this.mnuMenuStrip.TabIndex = 83;
            this.mnuMenuStrip.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiSignOut});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Text = "User";
            // 
            // msiSignOut
            // 
            this.msiSignOut.Name = "msiSignOut";
            this.msiSignOut.Size = new System.Drawing.Size(120, 22);
            this.msiSignOut.Text = "Sign Out";
            this.msiSignOut.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel1.Controls.Add(this.lvwFiles);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.mnuMenuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel2.Controls.Add(this.tclPreview);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1167, 674);
            this.splitContainer1.SplitterDistance = 440;
            this.splitContainer1.TabIndex = 1;
            // 
            // lvwFiles
            // 
            this.lvwFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colutmnHeader1,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwFiles.FullRowSelect = true;
            this.lvwFiles.HideSelection = false;
            this.lvwFiles.Location = new System.Drawing.Point(25, 90);
            this.lvwFiles.MultiSelect = false;
            this.lvwFiles.Name = "lvwFiles";
            this.lvwFiles.Size = new System.Drawing.Size(1117, 336);
            this.lvwFiles.TabIndex = 86;
            this.lvwFiles.UseCompatibleStateImageBehavior = false;
            this.lvwFiles.View = System.Windows.Forms.View.Details;
            this.lvwFiles.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwFiles_ItemChecked);
            this.lvwFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.newsListView_MouseClick);
            // 
            // cmsFiles
            // 
            this.cmsFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiEditFile,
            this.cmiRemoveFile});
            this.cmsFiles.Name = "contextMenuStrip1";
            this.cmsFiles.Size = new System.Drawing.Size(139, 48);
            // 
            // cmiEditFile
            // 
            this.cmiEditFile.Name = "cmiEditFile";
            this.cmiEditFile.Size = new System.Drawing.Size(138, 22);
            this.cmiEditFile.Text = "Edit File";
            this.cmiEditFile.Click += new System.EventHandler(this.editFileToolStripMenuItem_Click);
            // 
            // cmiRemoveFile
            // 
            this.cmiRemoveFile.Name = "cmiRemoveFile";
            this.cmiRemoveFile.Size = new System.Drawing.Size(138, 22);
            this.cmiRemoveFile.Text = "Remove File";
            this.cmiRemoveFile.Click += new System.EventHandler(this.removeFileToolStripMenuItem_Click);
            // 
            // frmFileworx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1167, 674);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1183, 713);
            this.Name = "frmFileworx";
            this.Text = "Fileworx";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileWorx_FormClosed);
            this.Resize += new System.EventHandler(this.FileWorx_Resize);
            this.tpgPreview.ResumeLayout(false);
            this.tclPreview.ResumeLayout(false);
            this.tpgImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.pnlDataSourceSelection.ResumeLayout(false);
            this.pnlDataSourceSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.mnuMenuStrip.ResumeLayout(false);
            this.mnuMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmsFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picImagePreview;
        private System.Windows.Forms.RichTextBox txtBody;
        private System.Windows.Forms.TabPage tpgPreview;
        private System.Windows.Forms.TabControl tclPreview;
        private System.Windows.Forms.TabPage tpgImage;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Label lblDateTitle;
        private System.Windows.Forms.Label lblTitleTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader colutmnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button signOutButton;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolStripMenuItem msiUsersList;
        private System.Windows.Forms.ToolStripMenuItem msiSortByAlphabetically;
        private System.Windows.Forms.ToolStripMenuItem msiSortByOldest;
        private System.Windows.Forms.ToolStripMenuItem msiSortByRecent;
        private System.Windows.Forms.ToolStripMenuItem msiSortBy;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiAddNews;
        private System.Windows.Forms.ToolStripMenuItem msiAddImage;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mnuMenuStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwFiles;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiSignOut;
        private System.Windows.Forms.ContextMenuStrip cmsFiles;
        private System.Windows.Forms.ToolStripMenuItem cmiEditFile;
        private System.Windows.Forms.ToolStripMenuItem cmiRemoveFile;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboDataStoreSource;
        private System.Windows.Forms.Panel pnlDataSourceSelection;
        private System.Windows.Forms.Label lblDataStoreSourceTitle;
        private System.Windows.Forms.ToolStripMenuItem sendFilesToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancelSending;
        private System.Windows.Forms.Button btnSendTo;
        private System.Windows.Forms.ToolStripMenuItem contactsListToolStripMenuItem;
    }
}