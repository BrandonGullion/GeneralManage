using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassLibrary
{

    /// <summary>
    /// May not be using this class 
    /// </summary>

    public class MetricListController : PropChanged
    {
        #region Properties 

        public DateTime CurrentTime { get; set; }
        public ObservableCollection<int>  WeeklyHourList { get; set; }
        public ObservableCollection<DateTime> Weekdays { get; set; }

        #endregion

        #region Constructor 

        public MetricListController()
        {
            // Grabs and sets the current date
            CurrentTime = DateTime.Now;

            // Fills the weekday list 
            Weekdays = WeekdayGenerator.ReturnWeekdayList();
        }


        #endregion

        #region Methods 




        #endregion
    }

}
