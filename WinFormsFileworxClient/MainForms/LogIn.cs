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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileworxObjectClassLibrary;

namespace Fileworx_Client
{
    public partial class LogIn : Form
    {
        
        public LogIn()
        {
            InitializeComponent();
        }

        private void logInClearTextboxes()
        {
            logInLogInNameTextBox.Text = String.Empty;
            logInPasswordTextBox.Text = String.Empty;
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            clsUser tryingToLogIn= new clsUser() { Username = logInLogInNameTextBox.Text, Password = logInPasswordTextBox.Text };
            LogInValidationResult validateResult = tryingToLogIn.ValidateLogin();

            if(validateResult == LogInValidationResult.Valid)
            {
                tryingToLogIn.Read();

                Global.LoggedInUser = tryingToLogIn;

                // Open Fileworx form in a new thread
                FileWorx fileworx = new FileWorx();
                var fileworxThread = new Thread(()=> Application.Run(fileworx));
                fileworxThread.Start();

                // close this form
                this.Close();
            }

            else if (validateResult == LogInValidationResult.WrongPassword)
            {
                logInPasswordTextBox.Text = String.Empty;

                MessageBox.Show($"Invalid password for {tryingToLogIn.Username}, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (validateResult == LogInValidationResult.WrongUser)
            {
                logInPasswordTextBox.Text = String.Empty;

                MessageBox.Show($"There is no username with the name {tryingToLogIn.Username}, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void closeThis()
        {
            Close();
        }
    }
}
