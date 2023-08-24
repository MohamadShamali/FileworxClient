namespace Fileworx_Client
{
    partial class AddImageWindow
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
            this.tclTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblBodyTitle = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescriptionTitle = new System.Windows.Forms.Label();
            this.lblTitleTitle = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.previewBrowsedPictureBox = new System.Windows.Forms.PictureBox();
            this.browseImageButton = new System.Windows.Forms.Button();
            this.imagePathTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tclTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBrowsedPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tclTabControl
            // 
            this.tclTabControl.Controls.Add(this.tabPage1);
            this.tclTabControl.Controls.Add(this.tabPage2);
            this.tclTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tclTabControl.Location = new System.Drawing.Point(0, 0);
            this.tclTabControl.Name = "tclTabControl";
            this.tclTabControl.SelectedIndex = 0;
            this.tclTabControl.Size = new System.Drawing.Size(485, 506);
            this.tclTabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblBodyTitle);
            this.tabPage1.Controls.Add(this.txtBody);
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.txtTitle);
            this.tabPage1.Controls.Add(this.lblDescriptionTitle);
            this.tabPage1.Controls.Add(this.lblTitleTitle);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(477, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Description";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblBodyTitle
            // 
            this.lblBodyTitle.AutoSize = true;
            this.lblBodyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBodyTitle.Location = new System.Drawing.Point(11, 69);
            this.lblBodyTitle.Name = "lblBodyTitle";
            this.lblBodyTitle.Size = new System.Drawing.Size(39, 13);
            this.lblBodyTitle.TabIndex = 13;
            this.lblBodyTitle.Text = "Body:";
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(92, 69);
            this.txtBody.MaxLength = 10000;
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(372, 346);
            this.txtBody.TabIndex = 11;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(92, 40);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(372, 20);
            this.txtDescription.TabIndex = 10;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(92, 11);
            this.txtTitle.MaxLength = 255;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(372, 20);
            this.txtTitle.TabIndex = 9;
            // 
            // lblDescriptionTitle
            // 
            this.lblDescriptionTitle.AutoSize = true;
            this.lblDescriptionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionTitle.Location = new System.Drawing.Point(11, 43);
            this.lblDescriptionTitle.Name = "lblDescriptionTitle";
            this.lblDescriptionTitle.Size = new System.Drawing.Size(75, 13);
            this.lblDescriptionTitle.TabIndex = 7;
            this.lblDescriptionTitle.Text = "Description:";
            // 
            // lblTitleTitle
            // 
            this.lblTitleTitle.AutoSize = true;
            this.lblTitleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleTitle.Location = new System.Drawing.Point(11, 13);
            this.lblTitleTitle.Name = "lblTitleTitle";
            this.lblTitleTitle.Size = new System.Drawing.Size(36, 13);
            this.lblTitleTitle.TabIndex = 6;
            this.lblTitleTitle.Text = "Title:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.previewBrowsedPictureBox);
            this.tabPage2.Controls.Add(this.browseImageButton);
            this.tabPage2.Controls.Add(this.imagePathTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(477, 480);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Image";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // previewBrowsedPictureBox
            // 
            this.previewBrowsedPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBrowsedPictureBox.Location = new System.Drawing.Point(17, 59);
            this.previewBrowsedPictureBox.Name = "previewBrowsedPictureBox";
            this.previewBrowsedPictureBox.Size = new System.Drawing.Size(442, 350);
            this.previewBrowsedPictureBox.TabIndex = 2;
            this.previewBrowsedPictureBox.TabStop = false;
            // 
            // browseImageButton
            // 
            this.browseImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseImageButton.Location = new System.Drawing.Point(384, 24);
            this.browseImageButton.Name = "browseImageButton";
            this.browseImageButton.Size = new System.Drawing.Size(75, 23);
            this.browseImageButton.TabIndex = 1;
            this.browseImageButton.Text = "Browse";
            this.browseImageButton.UseVisualStyleBackColor = true;
            this.browseImageButton.Click += new System.EventHandler(this.browseImageButton_Click);
            // 
            // imagePathTextBox
            // 
            this.imagePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePathTextBox.Location = new System.Drawing.Point(17, 24);
            this.imagePathTextBox.Name = "imagePathTextBox";
            this.imagePathTextBox.Size = new System.Drawing.Size(362, 20);
            this.imagePathTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 33);
            this.panel1.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(402, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.cancelAddNewsbutton_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(321, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.saveAddNewsButton_Click);
            // 
            // AddImageWindow
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tclTabControl);
            this.MinimumSize = new System.Drawing.Size(501, 545);
            this.Name = "AddImageWindow";
            this.Text = "Add Image";
            this.tclTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewBrowsedPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tclTabControl;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label lblBodyTitle;
        public System.Windows.Forms.TextBox txtBody;
        public System.Windows.Forms.TextBox txtDescription;
        public System.Windows.Forms.TextBox txtTitle;
        public System.Windows.Forms.Label lblDescriptionTitle;
        public System.Windows.Forms.Label lblTitleTitle;
        public System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.PictureBox previewBrowsedPictureBox;
        public System.Windows.Forms.Button browseImageButton;
        public System.Windows.Forms.TextBox imagePathTextBox;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSave;
    }
}