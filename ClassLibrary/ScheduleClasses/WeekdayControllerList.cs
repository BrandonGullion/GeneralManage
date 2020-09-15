using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassLibrary
{
    public class WeekdayControllerList : PropChanged
    {
        #region Properties 
        public DateTime CurrentDate { get; set; }
        public List<ShiftModel> FullShiftList { get; set; }

        #endregion

        #region Days of the week 

        public ObservableCollection<string> Weekdays { get; set; }
        public string SundayDate { get; set; }
        public string MondayDate { get; set; }
        public string TuesdayDate { get; set; }
        public string WednesdayDate { get; set; }
        public string ThursdayDate { get; set; }
        public string FridayDate { get; set; }
        public string SaturdayDate { get; set; }

        #endregion

        #region Day of the week lists
        public ObservableCollection<ShiftModel> SundayList { get; set; }
        public ObservableCollection<ShiftModel> MondayList { get; set; }
        public ObservableCollection<ShiftModel> TuesdayList { get; set; }
        public ObservableCollection<ShiftModel> WednesdayList { get; set; }
        public ObservableCollection<ShiftModel> ThursdayList { get; set; }
        public ObservableCollection<ShiftModel> FridayList { get; set; }
        public ObservableCollection<ShiftModel> SaturdayList { get; set; }
        public ObservableCollection<ObservableCollection<ShiftModel>> WeekdayLists { get; set; }

        #endregion

        #region Constructor 

        public WeekdayControllerList(List<ShiftModel> shiftModels)
        {
            // Creates a new instance of the weekday lists and populates the variables 
            WeekdayLists = new ObservableCollection<ObservableCollection<ShiftModel>>() {
                SundayList,  MondayList, TuesdayList, WednesdayList, ThursdayList, FridayList, SaturdayList};

            // News each collection within the weekdays lists
            for (var i = 0; i < 7; i++)
                WeekdayLists[i] = new ObservableCollection<ShiftModel>();

            // Sets the shift list passed in to a local variable 
            FullShiftList = shiftModels;

            // Setting current date so the dates can be calculated 
            CurrentDate = DateTime.Now;

            // Creating instance of Weekdays 
            Weekdays = new ObservableCollection<string>() { SundayDate, MondayDate, TuesdayDate, WednesdayDate, ThursdayDate, FridayDate, SaturdayDate };

            // Adjusting the date based on the current day 
            AdjustWeekDayDates();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Increments the selected day depending on the date time 
        /// </summary>

        public void AugmentDate(int sundayIncrement, int mondayIncrement, int tuesdayIncrement, int wednesdayIncrement,
            int thursdayIncrement, int fridayIncrement, int saturdayIncrement)
        {
            Weekdays[0] = CurrentDate.AddDays(sundayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[1] = CurrentDate.AddDays(mondayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[2] = CurrentDate.AddDays(tuesdayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[3] = CurrentDate.AddDays(wednesdayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[4] = CurrentDate.AddDays(thursdayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[5] = CurrentDate.AddDays(fridayIncrement).ToString("MMMM dd, yyyy");
            Weekdays[6] = CurrentDate.AddDays(saturdayIncrement).ToString("MMMM dd, yyyy");
        }

        /// <summary>
        /// Checks the current date and adjusts the days of the week date accordingly
        /// </summary>
        public void AdjustWeekDayDates()
        {
            switch (CurrentDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    AugmentDate(0, 1, 2, 3, 4, 5, 6);
                    break;
                case DayOfWeek.Monday:
                    AugmentDate(-1, 0, 1, 2, 3, 4, 5);
                    break;
                case DayOfWeek.Tuesday:
                    AugmentDate(-2, -1, 0, 1, 2, 3, 4);
                    break;
                case DayOfWeek.Wednesday:
                    AugmentDate(-3, -2, -1, 0, 1, 2, 3);
                    break;
                case DayOfWeek.Thursday:
                    AugmentDate(-4, -3, -2, -1, 0, 1, 2);
                    break;
                case DayOfWeek.Friday:
                    AugmentDate(-5, -4, -3, -2, -1, 0, 1);
                    break;
                case DayOfWeek.Saturday:
                    AugmentDate(-6, -5, -4, -3, -2, -1, 0);
                    break;
            }
        }

        /// <summary>
        /// Updates the lists so the values can be updated 
        /// </summary>
        public void UpdateLists(List<ShiftModel> shiftModels)
        {
            // Empties lists so they can be filled again depending on the week 
            foreach (var dayList in WeekdayLists)
                dayList.Clear();

            // Iterates over the entire list finding values that match with the required date, adding them to the 
            // Corresponding day and ordering the lists by start hour 
            foreach (var shift in shiftModels)
            {
                for(var i = 0; i < 7; i++)
                {
                    if (shift.Day == Convert.ToDateTime(Weekdays[i]).Day && shift.Month == Convert.ToDateTime(Weekdays[i]).Month)
                    {
                        WeekdayLists[i].Add(shift);
                        WeekdayLists[i] = new ObservableCollection<ShiftModel>(WeekdayLists[i].OrderBy(d => d.StartHourValue).ToList());
                    }
                }
            }
        }

        /// <summary>
        /// Increases the week by 7 days whenever pressed
        /// </summary>
        public void ChangeWeek(int weekCounter)
        {
            // Iterates over list and alters the displayed date 
            for (int i = 0; i < 6; i++)
                Weekdays[i] = Convert.ToDateTime(Weekdays[i]).AddDays(weekCounter * 7).ToString("MMMM dd, yyyy");

            // Checks for any dates that are present within the current week 
            UpdateLists(FullShiftList);
        }

        #endregion
    }
}
