using FileworxObjectClassLibrary;

namespace Web_Fileworx_Client.Models
{
    public class UsersServices
    {
        public List<clsUser> AllUsers = new List<clsUser>();
        public clsUser LoggedInUser { get; set; } = null;
        public clsUser SelectedUser { get; set; } = null;

        public void AddDBUsersToUsersList()
        {
            clsUserQuery allUsersQuery = new clsUserQuery();
            AllUsers = allUsersQuery.Run();
        }

        public void RefreshUsersList()
        {
            AddDBUsersToUsersList();
        }

    }
}
