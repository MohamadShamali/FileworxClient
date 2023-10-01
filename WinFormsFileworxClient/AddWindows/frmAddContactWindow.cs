using FileworxObjectClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fileworx_Client.AddWindows
{
    public partial class frmAddContactWindow : Form
    {
        public event OnFormCloseHandler OnFormClose;

        clsContact contactToEdit = new clsContact();
        public frmAddContactWindow()
        {
            InitializeComponent();

            cboDirection.SelectedIndex = 0;
        }

        public frmAddContactWindow(clsContact contact)
        {
             InitializeComponent();

            contactToEdit = contact;

            txtName.Text = contact.Name;
            cboDirection.SelectedIndex = (contact.Direction == (ContactDirection.Transmit | ContactDirection.Receive))? 0 :
                                         (contact.Direction == (ContactDirection.Transmit)) ? 1 :
                                         2;
            txtTransmitLoction.Text = contact.TransmitLocation;
            txtReceiveLocation.Text = contact.ReceiveLocation;

            this.Text = "Edit Contact";
        }

        private bool validateDate()
        {
            if (!String.IsNullOrEmpty(txtName.Text))
            {
                if(!String.IsNullOrEmpty(txtTransmitLoction.Text) | (!txtTransmitLoction.Enabled)) 
                {
                    if (!String.IsNullOrEmpty(txtReceiveLocation.Text) | (!txtReceiveLocation.Enabled))
                    {
                        return true;
                    }
                }
            }

                return false;
        }

        private void cboDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtReceiveLocation.Enabled = true;
            txtTransmitLoction.Enabled = true;
            btnReceiveBrowse.Enabled = true;
            btnTransmitBrowse.Enabled = true;

            if (cboDirection.SelectedIndex == 1)
            {
                txtReceiveLocation.Enabled = false;
                btnReceiveBrowse.Enabled = false;
            }

            if (cboDirection.SelectedIndex == 2)
            {
                txtTransmitLoction.Enabled = false;
                btnTransmitBrowse.Enabled = false;
            }
        }

        private void btnTransmitBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtTransmitLoction.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnReceiveBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtReceiveLocation.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (validateDate()) 
            {
                // Add Case
                if (String.IsNullOrEmpty(contactToEdit.Name))
                {
                    var newContact = new clsContact()
                    {
                        Id = Guid.NewGuid(),
                        Name = txtName.Text,
                        Description = String.Empty,
                        CreatorId = WinFormsGlobal.LoggedInUser.Id,
                        CreatorName = WinFormsGlobal.LoggedInUser.Name,
                        Direction = (cboDirection.SelectedIndex == 0) ? (ContactDirection.Transmit | ContactDirection.Receive) :
                                    (cboDirection.SelectedIndex == 1) ? ContactDirection.Transmit :
                                                                        ContactDirection.Receive,
                        TransmitLocation = txtTransmitLoction.Text,
                        ReceiveLocation = txtReceiveLocation.Text,
                        Enabled = true
                    };

                    await newContact.InsertAsync();
                }

                // Edit Case
                else
                {
                    await contactToEdit.ReadAsync();

                    contactToEdit.Name = txtName.Text;
                    contactToEdit.LastModifierId = WinFormsGlobal.LoggedInUser.Id;
                    contactToEdit.LastModifierName = WinFormsGlobal.LoggedInUser.Name;
                    contactToEdit.Direction = (cboDirection.SelectedIndex == 0) ? (ContactDirection.Transmit | ContactDirection.Receive) :
                                              (cboDirection.SelectedIndex == 1) ? ContactDirection.Transmit :
                                              ContactDirection.Receive;
                    contactToEdit.TransmitLocation = txtTransmitLoction.Text;
                    contactToEdit.ReceiveLocation = txtReceiveLocation.Text;

                    await contactToEdit.UpdateAsync();
                }


                if (OnFormClose != null)
                {
                    await OnFormClose();
                }

                this.Close();
            }

            else
            {
                MessageBox.Show("Empty fields");
            }
        }
    }
}
