using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class WeekdayGenerator
    {
        #region Properties 

        // Current Week
        public static DateTime SundayDate { get; set; }
        public static DateTime MondayDate { get; set; }
        public static DateTime TuesdayDate { get; set; }
        public static DateTime WednesdayDate { get; set; }
        public static DateTime ThursdayDate { get; set; }
        public static DateTime FridayDate { get; set; }
        public static DateTime SaturdayDate { get; set; }

        // Previous Week

        public static DateTime PreviousSundayDate { get; set; }
        public static DateTime PreviousMondayDate { get; set; }
        public static DateTime PreviousTuesdayDate { get; set; }
        public static DateTime PreviousWednesdayDate { get; set; }
        public static DateTime PreviousThursdayDate { get; set; }
        public static DateTime PreviousFridayDate { get; set; }
        public static DateTime PreviousSaturdayDate { get; set; }

        public static ObservableCollection<DateTime> Weekdays { get; set; }
        public static ObservableCollection<DateTime> BiWeeklyWeekdays { get; set; }
        #endregion

        public WeekdayGenerator()
        {
            Weekdays = new ObservableCollection<DateTime>() { SundayDate, MondayDate, TuesdayDate, WednesdayDate, ThursdayDate,
            FridayDate, SaturdayDate};
        }

        public static ObservableCollection<DateTime> ReturnWeekdayList()
        {
            return Weekdays = new ObservableCollection<DateTime>() { SundayDate, MondayDate, TuesdayDate, WednesdayDate, ThursdayDate,
            FridayDate, SaturdayDate};
        }

        public static ObservableCollection<DateTime> ReturnBiWeeklyWeekdays() 
        {
            return BiWeeklyWeekdays = new ObservableCollection<DateTime>()
            {
                // Previous Week
                PreviousSundayDate, PreviousMondayDate, PreviousTuesdayDate, PreviousWednesdayDate, PreviousThursdayDate,
                PreviousFridayDate, PreviousSaturdayDate,

                // Current Week
                SundayDate, MondayDate, TuesdayDate, WednesdayDate, ThursdayDate,
                FridayDate, SaturdayDate

            };
        }
    }
}
