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
    public partial class frmUsersList : Form
    {
        private static List<clsUser> allUsers = new List<clsUser>();
        public frmUsersList()
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

        private void refreshUsersList()
        {
            addDBUsersToUsersList();
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

        private void onAddFormClose()
        {
            refreshUsersList();
            addUsersListItemsToListView();
        }

        private void onEditFormClose()
        {
            int selectedIndex = usersListView.SelectedItems[0].Index;

            refreshUsersList();
            addUsersListItemsToListView();

            usersListView.SelectedIndices.Clear();
            usersListView.SelectedIndices.Add(selectedIndex);
        }

        private void displaySelectedUser(clsUser selectedUser)
        {
            userNameLabel.Text = selectedUser.Username;
            nameLabel.Text = selectedUser.Name;
            isAdminLabel.Text = selectedUser.IsAdmin ? "Yes" : "No";
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUserWindow addUserWindow = new frmAddUserWindow();
            addUserWindow.OnFormClose += onAddFormClose;
            addUserWindow.Show();
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

            displaySelectedUser(selectedUser);
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
                frmAddUserWindow editUser = new frmAddUserWindow(selectedUser);
                editUser.OnFormClose += onEditFormClose;
                editUser.Show();
            }
        }

        private void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser selectedUser = findSelectedUser();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete{selectedUser.Username}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clearAllLabels();
                selectedUser.Delete();
                addDBUsersToUsersList();
                addUsersListItemsToListView();
            }
        }
    }
}
