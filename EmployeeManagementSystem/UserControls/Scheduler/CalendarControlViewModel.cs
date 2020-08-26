using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.UserControls.Scheduler
{
    public class CalendarControlViewModel : BaseViewModel
    {
        #region Days Of Week Adjustable Grid Height

        /// <summary>
        /// These props will be used to adjust the grid sizes for the schedule days
        /// When a shift is added the height will be adjusted based on the added shift control
        /// 
        /// TODO :: Must be cleared and returned to original value when switching weeks and then
        /// re populated based on the controls found for that week 
        /// 
        /// </summary>

        private int sundayHeight;

        public int SundayHeight
        {
            get { return sundayHeight; }
            set { sundayHeight = value; OnPropertyChanged(nameof(SundayHeight)); }
        }
        private int mondayHeight;

        public int MondayHeight
        {
            get { return mondayHeight; }
            set { mondayHeight = value; OnPropertyChanged(nameof(MondayHeight)); }
        }
        private int tuesdayHeight;

        public int TuesdayHeight
        {
            get { return tuesdayHeight; }
            set { tuesdayHeight = value; OnPropertyChanged(nameof(TuesdayHeight)); }
        }
        private int wednesdayHeight;

        public int WednesdayHeight
        {
            get { return wednesdayHeight; }
            set { wednesdayHeight = value; OnPropertyChanged(nameof(WednesdayHeight)); }
        }
        private int thursdayHeight;

        public int ThursdayHeight
        {
            get { return thursdayHeight; }
            set { thursdayHeight = value; OnPropertyChanged(nameof(ThursdayHeight)); }
        }
        private int fridayHeight;

        public int FridayHeight
        {
            get { return fridayHeight; }
            set { fridayHeight = value; OnPropertyChanged(nameof(FridayHeight)); }
        }
        private int saturdayHeight;

        public int SaturdayHeight
        {
            get { return saturdayHeight; }
            set { saturdayHeight = value; OnPropertyChanged(nameof(SaturdayHeight)); }
        }


        #endregion

        #region Days of the week DateTime props for updating dates 

        /// <summary>
        /// 
        /// These are used when the week changes to display the correct days, a method will be 
        /// used to track when the week selection is changed, that will initiate a new list to be 
        /// formed in regards to individuals scheduled for that week 
        /// 
        /// </summary>


        private DateTime currentSundayDate;

        public DateTime CurrentSundayDate
        {
            get { return currentSundayDate; }
            set { currentSundayDate = value; OnPropertyChanged(nameof(CurrentSundayDate)); }
        }


        private DateTime currentMondayDate;

        public DateTime CurrentMondayDate
        {
            get { return currentMondayDate; }
            set { currentMondayDate = value; OnPropertyChanged(nameof(CurrentMondayDate)); }
        }

        private DateTime currentTuesdayDate;

        public DateTime CurrentTuesdayDate
        {
            get { return currentTuesdayDate; }
            set { currentTuesdayDate = value; OnPropertyChanged(nameof(CurrentTuesdayDate)); }
        }
        private DateTime currentWednesdayDate;

        public DateTime CurrentWednesdayDate
        {
            get { return currentWednesdayDate; }
            set { currentWednesdayDate = value; OnPropertyChanged(nameof(CurrentWednesdayDate)); }
        }
        private DateTime currentThursdayDate;

        public DateTime CurrentThursdayDate
        {
            get { return currentThursdayDate; }
            set { currentThursdayDate = value; OnPropertyChanged(nameof(CurrentThursdayDate)); }
        }
        private DateTime currentFridayDate;

        public DateTime CurrentFridayDate
        {
            get { return currentFridayDate; }
            set { currentFridayDate = value; OnPropertyChanged(nameof(CurrentFridayDate)); }
        }
        private DateTime currentSaturdayDate;

        public DateTime CurrentSaturdayDate
        {
            get { return currentSaturdayDate; }
            set { currentSaturdayDate = value; OnPropertyChanged(nameof(CurrentSaturdayDate)); }
        }


        #endregion

        #region AppointmentItemControl Props 

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }




        #endregion
    }
}
