using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace EmployeeManagementSystem
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string UserName { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public int AuthorityLevel { get; set; }
    }
}
