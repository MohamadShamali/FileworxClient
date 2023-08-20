using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fileworx_Client
{
    public partial class LogIn : Form
    {
        private StreamReader userReader;
        private List<User> AllUsers = new List<User>();
        public User LoggedInUser;

        public LogIn()
        {
            InitializeComponent();
            addAllSavedUsers();
        }

        private string[] getLogInInfo()
        {
            return new string[] { String.Empty, logInLogInNameTextBox.Text, logInPasswordTextBox.Text };
        }

        private void logInClearTextboxes()
        {
            logInLogInNameTextBox.Text = String.Empty;
            logInPasswordTextBox.Text = String.Empty;
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            AllUsers.Clear();
            addAllSavedUsers();

            Guid guid = Guid.NewGuid() ;
            string[] gg = { String.Empty, String.Empty, String.Empty, guid.ToString() , "false" };
            User loggedInUser = new User(  gg);
            bool Allow = false;
            string[] inputUser = getLogInInfo();

            foreach (User user in AllUsers)
            {
                if ((user.UserName == inputUser[1]) && (user.Password == inputUser[2]))
                {
                    Allow = true;
                    loggedInUser = user;
                    LoggedInUser = user;
                    break;
                }
            } 

            if (Allow)
            {
                this.Hide();
                FileWorx fileWorx = new FileWorx(loggedInUser);
                DialogResult result = fileWorx.ShowDialog();
                if (result == DialogResult.Cancel) 
                {
                    this.Show();
                }
                logInClearTextboxes();
            }

            else
            {
                logInPasswordTextBox.Text = String.Empty;
                MessageBox.Show("Invalid Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void addAllSavedUsers()
        {
            string[] savedUsersDirectorys = Directory.GetFiles(EditBeforeRun.savedusersDirectory);

            foreach (string file in savedUsersDirectorys)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    userReader = new StreamReader(fs);
                    string accountRecord = userReader.ReadLine();

                    if (accountRecord != null)
                    {
                        string[] thisUserArray = accountRecord.Split(EditBeforeRun.Seperator, StringSplitOptions.None);
                        User thisUser = new User(thisUserArray);
                        AllUsers.Add(thisUser);
                    }
                }
            }
        }
        
    }
}
