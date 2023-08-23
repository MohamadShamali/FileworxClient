using FileworxObjectClassLibrary;
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

namespace Fileworx_Client
{
    public partial class AddUserWindow : Form
    {
        clsUser userToEdit = new clsUser();
        public AddUserWindow()
        {
            InitializeComponent();
            isAdminComboBox.SelectedIndex = 0;
        }

        public AddUserWindow(clsUser userToEdit)
        {
            InitializeComponent();

            signUpNameTextBox.Text = userToEdit.Name;
            signUpLoginNameTextBox.Text = userToEdit.Username;
            signUpPasswordTextBox.Text = userToEdit.Password;
            isAdminComboBox.SelectedIndex = userToEdit.IsAdmin ? 1 : 0;

            this.userToEdit = userToEdit;

            if (this.userToEdit.Username == "admin")
            {
                signUpLoginNameTextBox.Enabled = false;
                isAdminComboBox.Enabled = false;
            }

            this.Text = "Edit User";
        }

        protected virtual void createButton_Click(object sender, EventArgs e)
        {

            // Add Case
            if(!String.IsNullOrEmpty(userToEdit.Name)) 
            {
                if ((!String.IsNullOrEmpty(signUpNameTextBox.Text)) && (!String.IsNullOrEmpty(signUpLoginNameTextBox.Text))
                   && (!String.IsNullOrEmpty(signUpPasswordTextBox.Text)))
                {
                    try
                    {
                        clsUser newUser = new clsUser()
                        {
                            Id = Guid.NewGuid(),
                            CreatorId = Global.LoggedInUser.Id,
                            CreatorName = Global.LoggedInUser.Name,
                            Name = signUpNameTextBox.Text,
                            Username = signUpLoginNameTextBox.Text,
                            Password = signUpPasswordTextBox.Text,
                            IsAdmin = ((isAdminComboBox.SelectedIndex == 0) ? false : true),
                            Class = clsBusinessObject.Type.User
                        };

                        newUser.Insert();

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
                if ((!String.IsNullOrEmpty(signUpNameTextBox.Text)) && (!String.IsNullOrEmpty(signUpLoginNameTextBox.Text))
                              && (!String.IsNullOrEmpty(signUpPasswordTextBox.Text)))
                {
                    try
                    {
                        userToEdit.LastModifierId = Global.LoggedInUser.Id;
                        userToEdit.LastModifierName = Global.LoggedInUser.Name;
                        userToEdit.Name = signUpNameTextBox.Text;
                        userToEdit.Username = signUpLoginNameTextBox.Text;
                        userToEdit.Password = signUpPasswordTextBox.Text;
                        userToEdit.IsAdmin = ((isAdminComboBox.SelectedIndex == 0) ? false : true);

                        userToEdit.Update();

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
        }

    } 
}

