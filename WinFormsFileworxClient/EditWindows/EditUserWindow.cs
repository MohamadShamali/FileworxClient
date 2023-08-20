using FileworxObjectClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public partial class EditUserWindow : AddUserWindow
    {
        clsUser userToEdit = new clsUser();
        public EditUserWindow(clsUser userToEdit)
        {
            InitializeComponent();

            signUpNameTextBox.Text = userToEdit.Name;
            signUpLoginNameTextBox.Text = userToEdit.Username;
            signUpPasswordTextBox.Text = userToEdit.Password;
            isAdminComboBox.SelectedIndex = userToEdit.IsAdmin ? 1 : 0;

            this.userToEdit = userToEdit;

            if(this.userToEdit.Username == "admin")
            {
                signUpLoginNameTextBox.Enabled = false;
                isAdminComboBox.Enabled = false;
            }
        }

        protected override void createButton_Click(object sender, EventArgs e)
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
                    userToEdit.IsAdmin = ((isAdminComboBox.SelectedIndex == 0)? false:true);

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
