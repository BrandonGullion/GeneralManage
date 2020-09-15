using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ShiftModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int EmployeeId { get; set; }
        [NotNull]
        public int StartHourValue { get; set; }
        [NotNull]
        public double StartMinValue { get; set; }
        [NotNull]
        public int EndtHourValue { get; set; }
        [NotNull]
        public double EndMinValue { get; set; }
        [NotNull]
        public int Day { get; set; }
        [NotNull]
        public int Month { get; set; }
        [NotNull]
        public int Year { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Hex { get; set; }
        [NotNull]
        public int Width { get; set; }
        [NotNull]
        public int Margin { get; set; }
        [NotNull]
        public double Total { get; set; }
    }
}
