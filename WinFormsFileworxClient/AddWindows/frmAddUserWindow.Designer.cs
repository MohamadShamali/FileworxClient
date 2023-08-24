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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.signUpPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            this.signUpLoginNameTextBox = new System.Windows.Forms.TextBox();
            this.signUpNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.isAdminComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Fileworx_Client.Resource1.Logo;
            this.pictureBox2.Location = new System.Drawing.Point(155, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(198, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 23;
            this.pictureBox2.TabStop = false;
            // 
            // signUpPasswordTextBox
            // 
            this.signUpPasswordTextBox.Location = new System.Drawing.Point(155, 154);
            this.signUpPasswordTextBox.Name = "signUpPasswordTextBox";
            this.signUpPasswordTextBox.Size = new System.Drawing.Size(194, 20);
            this.signUpPasswordTextBox.TabIndex = 5;
            this.signUpPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // createButton
            // 
            this.createButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.createButton.Location = new System.Drawing.Point(155, 223);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(114, 23);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // signUpLoginNameTextBox
            // 
            this.signUpLoginNameTextBox.Location = new System.Drawing.Point(155, 118);
            this.signUpLoginNameTextBox.Name = "signUpLoginNameTextBox";
            this.signUpLoginNameTextBox.Size = new System.Drawing.Size(194, 20);
            this.signUpLoginNameTextBox.TabIndex = 3;
            // 
            // signUpNameTextBox
            // 
            this.signUpNameTextBox.Location = new System.Drawing.Point(155, 80);
            this.signUpNameTextBox.Name = "signUpNameTextBox";
            this.signUpNameTextBox.Size = new System.Drawing.Size(194, 20);
            this.signUpNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(275, 223);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(74, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Is Admin";
            // 
            // isAdminComboBox
            // 
            this.isAdminComboBox.AllowDrop = true;
            this.isAdminComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isAdminComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.isAdminComboBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.isAdminComboBox.Location = new System.Drawing.Point(155, 189);
            this.isAdminComboBox.Name = "isAdminComboBox";
            this.isAdminComboBox.Size = new System.Drawing.Size(194, 21);
            this.isAdminComboBox.TabIndex = 7;
            // 
            // AddUserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 261);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.isAdminComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.signUpPasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.signUpLoginNameTextBox);
            this.Controls.Add(this.signUpNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(501, 300);
            this.MinimumSize = new System.Drawing.Size(501, 300);
            this.Name = "AddUserWindow";
            this.Text = "Add User";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox pictureBox2;
        protected System.Windows.Forms.TextBox signUpPasswordTextBox;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Button createButton;
        protected System.Windows.Forms.TextBox signUpLoginNameTextBox;
        protected System.Windows.Forms.TextBox signUpNameTextBox;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button cancelButton;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.ComboBox isAdminComboBox;
    }
}