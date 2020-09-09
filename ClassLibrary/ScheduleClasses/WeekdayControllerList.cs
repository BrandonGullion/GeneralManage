using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class WeekdayControllerList : PropChanged
    {
        #region Days of the week 


        private string sundayDate;

        public string SundayDate
        {
            get { return sundayDate; }
            set { sundayDate = value; OnPropertyChanged(nameof(SundayDate)); }
        }

        private string mondayDate;

        public string MondayDate
        {
            get { return mondayDate; }
            set { mondayDate = value; OnPropertyChanged(nameof(MondayDate)); }
        }
        private string tuesdayDate;

        public string TuesdayDate
        {
            get { return tuesdayDate; }
            set { tuesdayDate = value; OnPropertyChanged(nameof(TuesdayDate)); }
        }
        private string wednesdayDate;

        public string WednesdayDate
        {
            get { return wednesdayDate; }
            set { wednesdayDate = value; OnPropertyChanged(nameof(WednesdayDate)); }
        }
        private string thursdayDate;

        public string ThursdayDate
        {
            get { return thursdayDate; }
            set { thursdayDate = value; OnPropertyChanged(nameof(ThursdayDate)); }
        }
        private string fridayDate;

        public string FridayDate
        {
            get { return fridayDate; }
            set { fridayDate = value; OnPropertyChanged(nameof(FridayDate)); }
        }
        private string saturdayDate;

        public string SaturdayDate
        {
            get { return saturdayDate; }
            set { saturdayDate = value; OnPropertyChanged(nameof(SaturdayDate)); }
        }

        #endregion

        #region Day of the week lists


        private ObservableCollection<ShiftModel> sundayList;

        public ObservableCollection<ShiftModel> SundayList
        {
            get { return sundayList; }
            set { sundayList = value; OnPropertyChanged(nameof(SundayList)); }
        }

        private ObservableCollection<ShiftModel> mondayList;

        public ObservableCollection<ShiftModel> MondayList
        {
            get { return mondayList; }
            set { mondayList = value; OnPropertyChanged(nameof(MondayList)); }
        }

        private ObservableCollection<ShiftModel> tuesdayList;

        public ObservableCollection<ShiftModel> TuesdayList
        {
            get { return tuesdayList; }
            set { tuesdayList = value; OnPropertyChanged(nameof(TuesdayList)); }
        }

        private ObservableCollection<ShiftModel> wednesdayList;

        public ObservableCollection<ShiftModel> WednesdayList
        {
            get { return wednesdayList; }
            set { wednesdayList = value; OnPropertyChanged(nameof(WednesdayList)); }
        }

        private ObservableCollection<ShiftModel> thursdayList;

        public ObservableCollection<ShiftModel> ThursdayList
        {
            get { return thursdayList; }
            set { thursdayList = value; OnPropertyChanged(nameof(ThursdayList)); }
        }

        private ObservableCollection<ShiftModel> fridayList;

        public ObservableCollection<ShiftModel> FridayList
        {
            get { return fridayList; }
            set { fridayList = value; OnPropertyChanged(nameof(FridayList)); }
        }

        private ObservableCollection<ShiftModel> saturdayList;

        public ObservableCollection<ShiftModel> SaturdayList
        {
            get { return saturdayList; }
            set { saturdayList = value; OnPropertyChanged(nameof(SaturdayList)); }
        }

        #endregion

        public void UpdateLists()
        {

        }

    }
}
