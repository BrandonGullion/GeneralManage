using SQLite;
using System.Windows.Documents.DocumentStructures;

namespace EmployeeManagementSystem
{
    public class AvailabilityModel : BaseViewModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int EmployeeId { get; set; }

        // Morning Weekday Bools 
        [NotNull]
        public bool SundayMorning { get; set; }
        [NotNull]
        public bool MondayMorning { get; set; }
        [NotNull]
        public bool TuesdayMorning { get; set; }
        [NotNull]
        public bool WednesdayMorning { get; set; }
        [NotNull]
        public bool ThursdayMorning { get; set; }
        [NotNull]
        public bool FridayMorning { get; set; }
        [NotNull]
        public bool SaturdayMorning { get; set; }

        // Afternoon Weekday Bools
        [NotNull]
        public bool SundayAfternoon { get; set; }
        [NotNull]
        public bool MondayAfternoon { get; set; }
        [NotNull]
        public bool TuesdayAfternoon { get; set; }
        [NotNull]
        public bool WednesdayAfternoon { get; set; }
        [NotNull]
        public bool ThursdayAfternoon { get; set; }
        [NotNull]
        public bool FridayAfternoon { get; set; }
        [NotNull]
        public bool SaturdayAfternoon { get; set; }

        // Evening Weekday Bools 
        public bool SundayEvening { get; set; }
        [NotNull]
        public bool MondayEvening { get; set; }
        [NotNull]
        public bool TuesdayEvening { get; set; }
        [NotNull]
        public bool WednesdayEvening { get; set; }
        [NotNull]
        public bool ThursdayEvening { get; set; }
        [NotNull]
        public bool FridayEvening { get; set; }
        [NotNull]
        public bool SaturdayEvening { get; set; }

        // Night Weekday Bools 
        [NotNull]
        public bool SundayNight { get; set; }
        [NotNull]
        public bool MondayNight { get; set; }
        [NotNull]
        public bool TuesdayNight { get; set; }
        [NotNull]
        public bool WednesdayNight { get; set; }
        [NotNull]
        public bool ThursdayNight { get; set; }
        [NotNull]
        public bool FridayNight { get; set; }
        [NotNull]
        public bool SaturdayNight { get; set; }
    }
}
