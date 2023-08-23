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
    public partial class UsersList : Form
    {
        private static List<clsUser> allUsers = new List<clsUser>();
        public UsersList()
        {
            InitializeComponent();

            label7.Text = Global.LoggedInUser.Name;

            addDBUsersToUsersList();
            addUsersListItemsToListView();
        }

        private void addDBUsersToUsersList()
        {
            clsUserQuery allUsersQuery = new clsUserQuery();
            allUsers = allUsersQuery.Run();
        }

        private void addUsersListItemsToListView()
        {
            usersListView.Items.Clear();
            foreach (clsUser user in allUsers)
            {
                var listViewUser = new ListViewItem($"{user.Username}");
                listViewUser.SubItems.Add($"{user.Name}");
                listViewUser.SubItems.Add(user.IsAdmin? "Yes":"No");
                usersListView.Items.Add(listViewUser);
            }
        }

        private void clearAllLabels()
        {
            userNameLabel.Text = String.Empty;
            nameLabel.Text = String.Empty;
            isAdminLabel.Text = String.Empty;
        }

        private clsUser findSelectedUser()
        {
            clsUser selectedUser =
                (from user in allUsers
                 where user.Username == (usersListView.SelectedItems[0].Text)
                 select user).FirstOrDefault();
            return selectedUser;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            DialogResult result = addUserWindow.ShowDialog();

            if (result == DialogResult.OK)
            {
                addDBUsersToUsersList();
                addUsersListItemsToListView();
            }

            if (result == DialogResult.Cancel)
            {
                addUserWindow.Close();
            }
        }

        private void usersListView_MouseClick(object sender, MouseEventArgs e)
        {
            clsUser selectedUser = findSelectedUser();            

            if (e.Button == MouseButtons.Right)
            {
                if (selectedUser != null)
                {
                    contextMenuStrip1.Show(usersListView, new Point(e.X, e.Y));

                    if (selectedUser.Username == "admin") contextMenuStrip1.Items[1].Enabled = false;
                    else contextMenuStrip1.Items[1].Enabled = true;
                }
            }

            userNameLabel.Text = selectedUser.Username;
            nameLabel.Text = selectedUser.Name;
            isAdminLabel.Text = selectedUser.IsAdmin ? "Yes" : "No";
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser selectedUser = findSelectedUser();

            if ((selectedUser.Username == "admin") && (Global.LoggedInUser.Username != "admin"))
            {
                MessageBox.Show("You don't have the access to edit the admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                AddUserWindow editUser = new AddUserWindow(selectedUser);
                DialogResult result = editUser.ShowDialog();

                if (result == DialogResult.OK)
                {
                    addDBUsersToUsersList();
                    addUsersListItemsToListView();
                }
            }
        }

        private void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser selectedUser = findSelectedUser();
            if (selectedUser.Username != "admin")
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to delete{selectedUser.Username}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    clearAllLabels();
                    selectedUser.Delete();
                    addDBUsersToUsersList();
                    addUsersListItemsToListView();
                }
            }

            else
            {
                MessageBox.Show("You can not delete the admin user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
