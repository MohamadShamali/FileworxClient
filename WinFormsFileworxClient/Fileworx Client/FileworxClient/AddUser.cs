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
    public partial class AddUser : Form
    {
        private StreamWriter userWriter;
        public AddUser()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            Guid guid = Guid.NewGuid();
            string[] signUpInfo = getSignUpInfo(guid);

            if ((signUpInfo[0] != String.Empty) && (signUpInfo[1] != String.Empty) && (signUpInfo[2] != String.Empty))
            {
                try
                {
                    User newUser = new User(signUpInfo);

                    writeTxtFile(newUser);

                    MessageBox.Show("Account Created! \n\rLog in with the entered information", "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    signUpClearTextboxes();
                }

                catch (FormatException)
                {
                    MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void signUpClearTextboxes()
        {
            signUpNameTextBox.Text = String.Empty;
            signUpLoginNameTextBox.Text = String.Empty;
            signUpPasswordTextBox.Text = String.Empty;
        }

        private string[] getSignUpInfo(Guid guid)
        {
            return new string[] { signUpNameTextBox.Text, signUpLoginNameTextBox.Text, signUpPasswordTextBox.Text, guid.ToString() , "false" };
        }

        private void writeTxtFile(User newUser)
        {
            FileStream fs = new FileStream($"{EditBeforeRun.savedusersDirectory}" + $"\\{newUser.GUID.ToString()}.txt", FileMode.Create, FileAccess.Write);
            userWriter = new StreamWriter(fs);
            userWriter.AutoFlush = true;
            userWriter.WriteLine($"{newUser.Name}%%$$##{newUser.UserName}%%$$##{newUser.Password}%%$$##{newUser.GUID.ToString()}%%$$##{newUser.IsAdmin}");
            userWriter?.Close();
        }
    } 
}

