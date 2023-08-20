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
    public partial class EditUser : Form
    {
        private string prevTextFilePath;
        private StreamWriter userEditer;
        private string GUID;
        private string prevPassword;
        public EditUser(User editedUser)
        {
            InitializeComponent();
            nameTextBox.Text = editedUser.Name;
            loginNameTextBox.Text = editedUser.UserName;
            prevPassword = editedUser.Password;
            if (editedUser.IsAdmin)
            {
                isAdminComboBox.SelectedIndex = 1;
            }
            else
            {
                isAdminComboBox.SelectedIndex = 0;
            }


            GUID = editedUser.GUID.ToString();
            prevTextFilePath = EditBeforeRun.savedusersDirectory + @"\" + GUID + ".txt";
        }

        private void makeEditButton_Click(object sender, EventArgs e)
        {
            bool isAdmin;
            if (isAdminComboBox.SelectedIndex == 0)
            {
                isAdmin = false;
            }
            else { isAdmin = true; }
            string[] data = { nameTextBox.Text, loginNameTextBox.Text, prevPassword, GUID, isAdmin.ToString() };
            User editedUser = new User(data);

            editTxtFile(editedUser);
        }

        private void editTxtFile(User user)
        {
            File.Delete(prevTextFilePath);
            FileStream fs = new FileStream(prevTextFilePath, FileMode.Create, FileAccess.Write);
            userEditer = new StreamWriter(fs);
            userEditer.AutoFlush = true;
            userEditer.WriteLine($"{user.Name}%%$$##{user.UserName}%%$$##{user.Password}%%$$##{user.GUID.ToString()}%%$$##{user.IsAdmin}");
            userEditer?.Close();
        }
    }
}
