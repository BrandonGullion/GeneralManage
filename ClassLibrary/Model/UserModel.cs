using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClassLibrary
{
    public class UserModel
    {
        // SQL Props

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string UserName { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public int AuthorityLevel { get; set; }
        [NotNull]
        public string Salt { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        // Regular Props 
        [Ignore]
        public UserSettings CurrentUserSettings { get; set; }

        // Constructor 
        public UserModel()
        {
            CurrentUserSettings = new UserSettings(AuthorityLevel);
        }
    }
}
