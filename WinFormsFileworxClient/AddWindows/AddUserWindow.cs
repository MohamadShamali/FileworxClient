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
        public AddUserWindow()
        {
            InitializeComponent();
            isAdminComboBox.SelectedIndex = 0;
        }

        protected virtual void createButton_Click(object sender, EventArgs e)
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

    } 
}

