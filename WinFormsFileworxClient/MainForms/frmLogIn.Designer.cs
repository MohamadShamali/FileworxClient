namespace Fileworx_Client
{
    partial class LogIn
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logInButton = new System.Windows.Forms.Button();
            this.logInPasswordTextBox = new System.Windows.Forms.TextBox();
            this.logInLogInNameTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Fileworx_Client.Resource1.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(136, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(136, 173);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(194, 23);
            this.logInButton.TabIndex = 13;
            this.logInButton.Text = "Login";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // logInPasswordTextBox
            // 
            this.logInPasswordTextBox.Location = new System.Drawing.Point(136, 136);
            this.logInPasswordTextBox.MaxLength = 255;
            this.logInPasswordTextBox.Name = "logInPasswordTextBox";
            this.logInPasswordTextBox.Size = new System.Drawing.Size(194, 20);
            this.logInPasswordTextBox.TabIndex = 12;
            this.logInPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // logInLogInNameTextBox
            // 
            this.logInLogInNameTextBox.Location = new System.Drawing.Point(136, 98);
            this.logInLogInNameTextBox.MaxLength = 255;
            this.logInLogInNameTextBox.Name = "logInLogInNameTextBox";
            this.logInLogInNameTextBox.Size = new System.Drawing.Size(194, 20);
            this.logInLogInNameTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(53, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Password:";
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(53, 99);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(68, 15);
            this.Title.TabIndex = 9;
            this.Title.Text = "Username:";
            // 
            // LogIn
            // 
            this.AcceptButton = this.logInButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 247);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(this.logInPasswordTextBox);
            this.Controls.Add(this.logInLogInNameTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(427, 286);
            this.MinimumSize = new System.Drawing.Size(427, 286);
            this.Name = "LogIn";
            this.Text = "Log In";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.TextBox logInPasswordTextBox;
        private System.Windows.Forms.TextBox logInLogInNameTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Title;
    }
}

