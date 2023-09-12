namespace Fileworx_Client.AddWindows
{
    partial class frmAddContactWindow
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
            this.lblDirection = new System.Windows.Forms.Label();
            this.cboDirection = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtReceiveLocation = new System.Windows.Forms.TextBox();
            this.lblReceiveLocation = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtTransmitLoction = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblTransmitLocation = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.btnTransmitBrowse = new System.Windows.Forms.Button();
            this.btnReceiveBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirection.Location = new System.Drawing.Point(50, 115);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(59, 15);
            this.lblDirection.TabIndex = 30;
            this.lblDirection.Text = "Direction:";
            // 
            // cboDirection
            // 
            this.cboDirection.AllowDrop = true;
            this.cboDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboDirection.Items.AddRange(new object[] {
            "Transmit and Receive",
            "Transmit",
            "Receive"});
            this.cboDirection.Location = new System.Drawing.Point(170, 111);
            this.cboDirection.Name = "cboDirection";
            this.cboDirection.Size = new System.Drawing.Size(194, 21);
            this.cboDirection.TabIndex = 31;
            this.cboDirection.SelectedIndexChanged += new System.EventHandler(this.cboDirection_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(291, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::Fileworx_Client.Resource1.Logo;
            this.picLogo.Location = new System.Drawing.Point(171, 25);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(198, 41);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 34;
            this.picLogo.TabStop = false;
            // 
            // txtReceiveLocation
            // 
            this.txtReceiveLocation.Location = new System.Drawing.Point(170, 180);
            this.txtReceiveLocation.Name = "txtReceiveLocation";
            this.txtReceiveLocation.Size = new System.Drawing.Size(115, 20);
            this.txtReceiveLocation.TabIndex = 29;
            // 
            // lblReceiveLocation
            // 
            this.lblReceiveLocation.AutoSize = true;
            this.lblReceiveLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiveLocation.Location = new System.Drawing.Point(49, 181);
            this.lblReceiveLocation.Name = "lblReceiveLocation";
            this.lblReceiveLocation.Size = new System.Drawing.Size(104, 15);
            this.lblReceiveLocation.TabIndex = 28;
            this.lblReceiveLocation.Text = "Receive Location:";
            // 
            // btnCreate
            // 
            this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreate.Location = new System.Drawing.Point(171, 224);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(114, 23);
            this.btnCreate.TabIndex = 32;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtTransmitLoction
            // 
            this.txtTransmitLoction.Location = new System.Drawing.Point(170, 144);
            this.txtTransmitLoction.Name = "txtTransmitLoction";
            this.txtTransmitLoction.Size = new System.Drawing.Size(115, 20);
            this.txtTransmitLoction.TabIndex = 27;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(171, 81);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 25;
            // 
            // lblTransmitLocation
            // 
            this.lblTransmitLocation.AutoSize = true;
            this.lblTransmitLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransmitLocation.Location = new System.Drawing.Point(49, 147);
            this.lblTransmitLocation.Name = "lblTransmitLocation";
            this.lblTransmitLocation.Size = new System.Drawing.Size(108, 15);
            this.lblTransmitLocation.TabIndex = 26;
            this.lblTransmitLocation.Text = "Transmit Loaction:";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(50, 81);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(44, 15);
            this.lblNameTitle.TabIndex = 24;
            this.lblNameTitle.Text = "Name:";
            // 
            // btnTransmitBrowse
            // 
            this.btnTransmitBrowse.Location = new System.Drawing.Point(289, 142);
            this.btnTransmitBrowse.Name = "btnTransmitBrowse";
            this.btnTransmitBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnTransmitBrowse.TabIndex = 35;
            this.btnTransmitBrowse.Text = "Browse";
            this.btnTransmitBrowse.UseVisualStyleBackColor = true;
            this.btnTransmitBrowse.Click += new System.EventHandler(this.btnTransmitBrowse_Click);
            // 
            // btnReceiveBrowse
            // 
            this.btnReceiveBrowse.Location = new System.Drawing.Point(289, 179);
            this.btnReceiveBrowse.Name = "btnReceiveBrowse";
            this.btnReceiveBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnReceiveBrowse.TabIndex = 36;
            this.btnReceiveBrowse.Text = "Browse";
            this.btnReceiveBrowse.UseVisualStyleBackColor = true;
            this.btnReceiveBrowse.Click += new System.EventHandler(this.btnReceiveBrowse_Click);
            // 
            // frmAddContactWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 261);
            this.Controls.Add(this.btnReceiveBrowse);
            this.Controls.Add(this.btnTransmitBrowse);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.cboDirection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.txtReceiveLocation);
            this.Controls.Add(this.lblReceiveLocation);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtTransmitLoction);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblTransmitLocation);
            this.Controls.Add(this.lblNameTitle);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(501, 300);
            this.MinimumSize = new System.Drawing.Size(501, 300);
            this.Name = "frmAddContactWindow";
            this.Text = "Add Contact";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblDirection;
        protected System.Windows.Forms.ComboBox cboDirection;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.PictureBox picLogo;
        protected System.Windows.Forms.TextBox txtReceiveLocation;
        protected System.Windows.Forms.Label lblReceiveLocation;
        protected System.Windows.Forms.Button btnCreate;
        protected System.Windows.Forms.TextBox txtTransmitLoction;
        protected System.Windows.Forms.TextBox txtName;
        protected System.Windows.Forms.Label lblTransmitLocation;
        protected System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.Button btnTransmitBrowse;
        private System.Windows.Forms.Button btnReceiveBrowse;
    }
}