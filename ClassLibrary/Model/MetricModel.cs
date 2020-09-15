using SQLite;

namespace ClassLibrary
{
    public class MetricModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public double Hours { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Wage { get; set; }
    }
}
