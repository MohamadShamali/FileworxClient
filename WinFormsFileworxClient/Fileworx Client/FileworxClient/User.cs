using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileworx_Client
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid GUID { get; set; }
        public bool IsAdmin { get; set; }

        public User(string[] userInfoArray)
        {
            IsAdmin = false;
            Name = userInfoArray[0];
            UserName = userInfoArray[1];
            Password = userInfoArray[2];
            GUID = new Guid(userInfoArray[3]);
            IsAdmin = bool.Parse(userInfoArray[4]); 

            if (UserName == "admin")
            {
                IsAdmin = true;
            }
        }
    }
}
