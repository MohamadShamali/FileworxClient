namespace Fileworx_Client
{
    partial class frmAddUserWindow
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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblUsernameTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblIsAdminTitle = new System.Windows.Forms.Label();
            this.cboIsAdmin = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Fileworx_Client.Resource1.Logo;
            this.picLogo.Location = new System.Drawing.Point(155, 24);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(198, 41);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 23;
            this.picLogo.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(155, 154);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(194, 20);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordTitle.Location = new System.Drawing.Point(62, 155);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(64, 15);
            this.lblPasswordTitle.TabIndex = 4;
            this.lblPasswordTitle.Text = "Password:";
            // 
            // btnCreate
            // 
            this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreate.Location = new System.Drawing.Point(155, 223);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(114, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.createButton_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(155, 118);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(194, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(155, 80);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblUsernameTitle
            // 
            this.lblUsernameTitle.AutoSize = true;
            this.lblUsernameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsernameTitle.Location = new System.Drawing.Point(62, 119);
            this.lblUsernameTitle.Name = "lblUsernameTitle";
            this.lblUsernameTitle.Size = new System.Drawing.Size(68, 15);
            this.lblUsernameTitle.TabIndex = 2;
            this.lblUsernameTitle.Text = "Username:";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(62, 80);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(44, 15);
            this.lblNameTitle.TabIndex = 0;
            this.lblNameTitle.Text = "Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblIsAdminTitle
            // 
            this.lblIsAdminTitle.AutoSize = true;
            this.lblIsAdminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsAdminTitle.Location = new System.Drawing.Point(63, 193);
            this.lblIsAdminTitle.Name = "lblIsAdminTitle";
            this.lblIsAdminTitle.Size = new System.Drawing.Size(54, 15);
            this.lblIsAdminTitle.TabIndex = 6;
            this.lblIsAdminTitle.Text = "Is Admin";
            // 
            // cboIsAdmin
            // 
            this.cboIsAdmin.AllowDrop = true;
            this.cboIsAdmin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIsAdmin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboIsAdmin.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.cboIsAdmin.Location = new System.Drawing.Point(155, 189);
            this.cboIsAdmin.Name = "cboIsAdmin";
            this.cboIsAdmin.Size = new System.Drawing.Size(194, 21);
            this.cboIsAdmin.TabIndex = 7;
            // 
            // frmAddUserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 261);
            this.Controls.Add(this.lblIsAdminTitle);
            this.Controls.Add(this.cboIsAdmin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPasswordTitle);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblUsernameTitle);
            this.Controls.Add(this.lblNameTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(501, 300);
            this.MinimumSize = new System.Drawing.Size(501, 300);
            this.Name = "frmAddUserWindow";
            this.Text = "Add User";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox picLogo;
        protected System.Windows.Forms.TextBox txtPassword;
        protected System.Windows.Forms.Label lblPasswordTitle;
        protected System.Windows.Forms.Button btnCreate;
        protected System.Windows.Forms.TextBox txtUsername;
        protected System.Windows.Forms.TextBox txtName;
        protected System.Windows.Forms.Label lblUsernameTitle;
        protected System.Windows.Forms.Label lblNameTitle;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Label lblIsAdminTitle;
        protected System.Windows.Forms.ComboBox cboIsAdmin;
    }
}