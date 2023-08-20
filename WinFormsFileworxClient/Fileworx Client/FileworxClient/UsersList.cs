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
    public partial class UsersList : Form
    {
        public User LoggedInUser;
        private StreamReader userReader;
        private List<User> AllUsers = new List<User>();
        public UsersList(User loggedInUser)
        {
            InitializeComponent();

            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer2.FixedPanel = FixedPanel.Panel1;

            LoggedInUser = loggedInUser;
            label7.Text = loggedInUser.Name;

            usersListView.Items.Clear();
            addAllSavedUsers();
        }

        private void addAllSavedUsers()
        {
            AllUsers.Clear();
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

                        addThisUserToListView(thisUser);
                        AllUsers.Add(thisUser);
                    }
                }
            }
        }
        private void addThisUserToListView(User user)
        {
            var listViewUser = new ListViewItem($"{user.UserName}");
            listViewUser.SubItems.Add($"{user.Name}");
            if (user.IsAdmin)
            {
                listViewUser.SubItems.Add("Yes");
            }
            else
            {
                listViewUser.SubItems.Add("No");
            }
           usersListView.Items.Add(listViewUser);
        }

        private void usersListView_Click(object sender, EventArgs e)
        {
            if (usersListView.Items.Count > 0)
            {
                User selectedUser = findSelectedUser();
                userNameLabel.Text = selectedUser.UserName;
                nameLabel.Text = selectedUser.Name;

                if (selectedUser.IsAdmin)
                {
                    isAdminLabel.Text = "Yes";
                }
                else
                {
                    isAdminLabel.Text = "No";
                }
            }
        }

        private User findSelectedUser()
        {
            User foundUser =
                (from User in AllUsers
                 where User.UserName == (usersListView.SelectedItems[0].Text)
                 select User).FirstOrDefault();
            return foundUser;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            DialogResult result = addUser.ShowDialog();

            if (result == DialogResult.OK)
            {
                usersListView.Items.Clear();
                addAllSavedUsers();
            }

            if (result == DialogResult.Cancel)
            {
                addUser.Close();
            }
        }

        private void clearEveryLabels()
        {
            userNameLabel.Text=String.Empty; 
            nameLabel.Text=String.Empty;
            isAdminLabel.Text=String.Empty;
        }

        private void usersListView_MouseClick(object sender, MouseEventArgs e)
        {
            User r = findSelectedUser();

            if (e.Button == MouseButtons.Right)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    clearEveryLabels();
                    File.Delete(Path.Combine(EditBeforeRun.savedusersDirectory, r.GUID.ToString() + ".txt"));
                    usersListView.SelectedItems[0].Remove();
                }
            }
        }

        private void usersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditUser editUser = new EditUser(findSelectedUser());
            DialogResult result = editUser.ShowDialog();

            if (result == DialogResult.OK) 
            { 
             usersListView.Items.Clear();
                addAllSavedUsers();
                clearEveryLabels();
            }
        }
    }
}
