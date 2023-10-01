using FileworxObjectClassLibrary.Models;
using FileworxObjectClassLibrary.Queries;

namespace Web_Fileworx_Client.Models
{
    public class UsersServices
    {
        public List<clsUser> AllUsers = new List<clsUser>();
        public clsUser LoggedInUser { get; set; } = null;
        public clsUser SelectedUser { get; set; } = null;
        public QuerySource QuerySource { get; set; } = QuerySource.ES;

        public async Task AddDBUsersToUsersList()
        {
            clsUserQuery allUsersQuery = new clsUserQuery();
            allUsersQuery.Source = QuerySource;
            AllUsers = await allUsersQuery.RunAsync();
        }

        public async Task RefreshUsersList()
        {
            await AddDBUsersToUsersList();
        }

    }
}
