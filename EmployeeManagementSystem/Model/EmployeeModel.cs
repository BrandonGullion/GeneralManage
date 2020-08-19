using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class EmployeeModel
    {
        /// <summary>
        /// The basic employee profile 
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        public string Position { get; set; }
        public string HourlyWage { get; set; }
        public string PhoneNumber { get; set; }
        public int AuthorityLevel { get; set; }
        public string Ext { get; set; }
        public string FirstLetter { get; set; }

    }
}
