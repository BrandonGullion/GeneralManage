using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VacationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int EmployeeSQLId { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string StartDate { get; set; }
        [NotNull]
        public string EndDate { get; set; }
        [NotNull]
        public string Hex { get; set; }
    }
}
