using FileworxObjectClassLibrary;

namespace Web_Fileworx_Client.Models
{
    public class UsersServices
    {
        public List<clsUser> AllUsers = new List<clsUser>();
        public clsUser LoggedInUser { get; set; } = null;
        public clsUser SelectedUser { get; set; } = null;
        private QuerySource querySource { get; set; } = QuerySource.ES;

        public async Task AddDBUsersToUsersList()
        {
            clsUserQuery allUsersQuery = new clsUserQuery();
            allUsersQuery.Source = querySource;
            AllUsers = await allUsersQuery.Run();
        }

        public void RefreshUsersList()
        {
            AddDBUsersToUsersList();
        }

    }
}
