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
        private QuerySource querySource { get; set; } = QuerySource.ES;
        public frmUsersList()
        {
            InitializeComponent();
        }

        public static async Task<frmUsersList> Create()
        {
            var usersList = new frmUsersList();

            // UI
            usersList.lblName.Text = Global.LoggedInUser.Name;
            usersList.cboDataStoreSource.SelectedIndex = 1;

            await usersList.addDBUsersToUsersList();
            usersList.addUsersListItemsToListView();

            return usersList;
        }

        private async Task addDBUsersToUsersList()
        {
            clsUserQuery allUsersQuery = new clsUserQuery();
            allUsersQuery.Source = querySource;
            allUsers = await allUsersQuery.Run();
        }

        private async Task refreshUsersList()
        {
            await addDBUsersToUsersList();
        }

        private void addUsersListItemsToListView()
        {
            lvwUsers.Items.Clear();
            foreach (clsUser user in allUsers)
            {
                var listViewUser = new ListViewItem($"{user.Username}");
                listViewUser.SubItems.Add($"{user.Name}");
                listViewUser.SubItems.Add(user.IsAdmin? "Yes":"No");
                lvwUsers.Items.Add(listViewUser);
            }
        }

        private void clearAllLabels()
        {
            lblUsername.Text = String.Empty;
            lblNamee.Text = String.Empty;
            lblIsAdmin.Text = String.Empty;
        }

        private clsUser findSelectedUser()
        {
            clsUser selectedUser =
                (from user in allUsers
                 where user.Username == (lvwUsers.SelectedItems[0].Text)
                 select user).FirstOrDefault();
            return selectedUser;
        }

        private async Task onAddFormClose()
        {
            await refreshUsersList();
            addUsersListItemsToListView();
        }

        private async Task onEditFormClose()
        {
            int selectedIndex = lvwUsers.SelectedItems[0].Index;

            await refreshUsersList();
            addUsersListItemsToListView();

            lvwUsers.SelectedIndices.Clear();
            lvwUsers.SelectedIndices.Add(selectedIndex);
        }

        private void displaySelectedUser(clsUser selectedUser)
        {
            lblUsername.Text = selectedUser.Username;
            lblNamee.Text = selectedUser.Name;
            lblIsAdmin.Text = selectedUser.IsAdmin ? "Yes" : "No";
        }

        //------------------------ Event Handlers ------------------------//

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
                    cmsUsersList.Show(lvwUsers, new Point(e.X, e.Y));

                    if (selectedUser.Username == "admin") cmsUsersList.Items[1].Enabled = false;
                    else cmsUsersList.Items[1].Enabled = true;
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

        private async void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser selectedUser = findSelectedUser();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete{selectedUser.Username}?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clearAllLabels();
                await selectedUser.DeleteAsync();
                addDBUsersToUsersList();
                addUsersListItemsToListView();
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cboDataStoreSource.SelectedIndex == 0)
            {
                querySource = QuerySource.DB;
            }
            if (cboDataStoreSource.SelectedIndex == 1)
            {
                querySource = QuerySource.ES;
            }

            await refreshUsersList();
            addUsersListItemsToListView();
        } 
    }
}
