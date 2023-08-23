namespace Fileworx_Client
{
    partial class AddNewsWindow
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
            this.label4 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.bodyTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.tiltleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelAddNewsbutton = new System.Windows.Forms.Button();
            this.saveAddNewsButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Body:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.AllowDrop = true;
            this.categoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.categoryComboBox.Items.AddRange(new object[] {
            "General",
            "Sports",
            "Health",
            "Politics"});
            this.categoryComboBox.Location = new System.Drawing.Point(97, 73);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(372, 21);
            this.categoryComboBox.TabIndex = 5;
            // 
            // bodyTextBox
            // 
            this.bodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bodyTextBox.Location = new System.Drawing.Point(97, 102);
            this.bodyTextBox.MaxLength = 10000;
            this.bodyTextBox.Multiline = true;
            this.bodyTextBox.Name = "bodyTextBox";
            this.bodyTextBox.Size = new System.Drawing.Size(372, 347);
            this.bodyTextBox.TabIndex = 7;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(97, 46);
            this.descriptionTextBox.MaxLength = 255;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(372, 20);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // tiltleTextBox
            // 
            this.tiltleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tiltleTextBox.Location = new System.Drawing.Point(97, 17);
            this.tiltleTextBox.MaxLength = 255;
            this.tiltleTextBox.Name = "tiltleTextBox";
            this.tiltleTextBox.Size = new System.Drawing.Size(372, 20);
            this.tiltleTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Category:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelAddNewsbutton);
            this.panel1.Controls.Add(this.saveAddNewsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 33);
            this.panel1.TabIndex = 13;
            // 
            // cancelAddNewsbutton
            // 
            this.cancelAddNewsbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelAddNewsbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelAddNewsbutton.Location = new System.Drawing.Point(402, 5);
            this.cancelAddNewsbutton.Name = "cancelAddNewsbutton";
            this.cancelAddNewsbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelAddNewsbutton.TabIndex = 13;
            this.cancelAddNewsbutton.Text = "Cancel";
            this.cancelAddNewsbutton.UseVisualStyleBackColor = true;
            this.cancelAddNewsbutton.Click += new System.EventHandler(this.cancelAddNewsbutton_Click);
            // 
            // saveAddNewsButton
            // 
            this.saveAddNewsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAddNewsButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveAddNewsButton.Location = new System.Drawing.Point(321, 5);
            this.saveAddNewsButton.Name = "saveAddNewsButton";
            this.saveAddNewsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAddNewsButton.TabIndex = 14;
            this.saveAddNewsButton.Text = "Save";
            this.saveAddNewsButton.UseVisualStyleBackColor = true;
            this.saveAddNewsButton.Click += new System.EventHandler(this.saveAddNewsButton_Click);
            // 
            // AddNewsWindow
            // 
            this.AcceptButton = this.saveAddNewsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.bodyTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.tiltleTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(501, 545);
            this.Name = "AddNewsWindow";
            this.Text = "Add News";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.ComboBox categoryComboBox;
        protected System.Windows.Forms.TextBox bodyTextBox;
        protected System.Windows.Forms.TextBox descriptionTextBox;
        protected System.Windows.Forms.TextBox tiltleTextBox;
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button cancelAddNewsbutton;
        public System.Windows.Forms.Button saveAddNewsButton;
    }
}