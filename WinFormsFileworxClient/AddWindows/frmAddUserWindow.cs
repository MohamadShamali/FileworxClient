using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using FileworxObjectClassLibrary.Models;

namespace Fileworx_Client
{
    public partial class frmAddUserWindow : Form
    {
        clsUser userToEdit = new clsUser();
        public event OnFormCloseHandler OnFormClose;
        public frmAddUserWindow()
        {
            InitializeComponent();
            cboIsAdmin.SelectedIndex = 0;
        }

        public frmAddUserWindow(clsUser userToEdit)
        {
            InitializeComponent();

            txtName.Text = userToEdit.Name;
            txtUsername.Text = userToEdit.Username;
            txtPassword.Text = userToEdit.Password;
            cboIsAdmin.SelectedIndex = userToEdit.IsAdmin ? 1 : 0;

            this.userToEdit = userToEdit;

            if (this.userToEdit.Username == "admin")
            {
                txtUsername.Enabled = false;
                cboIsAdmin.Enabled = false;
            }

            this.Text = "Edit User";
            btnCreate.Text = "Edit";
        }

        private bool validateData()
        {
            if ((!String.IsNullOrEmpty(txtName.Text)) && (!String.IsNullOrEmpty(txtUsername.Text))
                              && (!String.IsNullOrEmpty(txtPassword.Text)))
            {
                return true;
            }
            else { return false; }
        }

        private async void createButton_Click(object sender, EventArgs e)
        {

            // Add Case
            if(String.IsNullOrEmpty(userToEdit.Name)) 
            {
                if (validateData())
                {
                    try
                    {
                        clsUser newUser = new clsUser()
                        {
                            Id = Guid.NewGuid(),
                            CreatorId = WinFormsGlobal.LoggedInUser.Id,
                            CreatorName = WinFormsGlobal.LoggedInUser.Name,
                            Name = txtName.Text,
                            Username = txtUsername.Text,
                            Password = txtPassword.Text,
                            IsAdmin = ((cboIsAdmin.SelectedIndex == 0) ? false : true),
                            Class = Type.User
                        };

                        await newUser.InsertAsync();

                        MessageBox.Show("Account Created! \n\rLog in with the entered information", "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch (FormatException)
                    {
                        MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                    }

                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Duplicated username"))
                            MessageBox.Show("This Username is used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        DialogResult = DialogResult.None;
                    }
                }
                else
                {
                    MessageBox.Show("Empty Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            // Edit Case
            else 
            {
                if (validateData())
                {
                    try
                    {
                        userToEdit.LastModifierId = WinFormsGlobal.LoggedInUser.Id;
                        userToEdit.LastModifierName = WinFormsGlobal.LoggedInUser.Name;
                        userToEdit.Name = txtName.Text;
                        userToEdit.Username = txtUsername.Text;
                        userToEdit.Password = txtPassword.Text;
                        userToEdit.IsAdmin = ((cboIsAdmin.SelectedIndex == 0) ? false : true);

                        await userToEdit.InsertAsync();

                        MessageBox.Show($"Account Updated!", "Account Updated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    catch (FormatException)
                    {
                        MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                    }

                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Duplicated username"))
                            MessageBox.Show("This Username is used", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("An Error Occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        DialogResult = DialogResult.None;
                    }
                }
                else
                {
                    MessageBox.Show("Empty Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.None;
                }
            }

            if (OnFormClose != null)
            {
                await OnFormClose();
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    } 
}

