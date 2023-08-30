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
            txtUsername.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        private async void logInButton_Click(object sender, EventArgs e)
        {
            clsUser tryingToLogIn= new clsUser() { Username = txtUsername.Text, Password = txtPassword.Text };
            LogInValidationResult validateResult = tryingToLogIn.ValidateLogin();

            if(validateResult == LogInValidationResult.Valid)
            {
                tryingToLogIn.Read();

                Global.LoggedInUser = tryingToLogIn;

                // Open Fileworx form in a new thread
                frmFileworx fileworx = await frmFileworx.Create();
                var fileworxThread = new Thread(()=> Application.Run(fileworx));
                fileworxThread.SetApartmentState(ApartmentState.STA); // Set the thread to STA mode
                fileworxThread.Start();

                // close this form
                this.Close();
            }

            else if (validateResult == LogInValidationResult.WrongPassword)
            {
                txtPassword.Text = String.Empty;

                MessageBox.Show($"Invalid password for {tryingToLogIn.Username}, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (validateResult == LogInValidationResult.WrongUser)
            {
                txtPassword.Text = String.Empty;

                MessageBox.Show($"There is no username with the name {tryingToLogIn.Username}, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void closeThis()
        {
            Close();
        }
    }
}
