using SQLite;

namespace ClassLibrary
{
    public class HoursMetricModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public double TotalHours { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Wage { get; set; }
    }
}
