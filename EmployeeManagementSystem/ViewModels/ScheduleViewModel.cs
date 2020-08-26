using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmployeeManagementSystem
{
    public class ScheduleViewModel : BaseViewModel
    {
        #region Properties

        #region Relay Commands

        public RelayCommand<object> MinSelectCommand { get; set; }
        public RelayCommand<object> HourSelectCommand { get; set; }
        public RelayCommand AddShiftCommand { get; set; }
        public RelayCommand EditShiftCommand { get; set; }
        public RelayCommand DeleteShiftCommand { get; set; }
        public RelayCommand CompleteShiftAddCommand { get; set; }
        public RelayCommand CancelShiftAddCommand { get; set; }
        public RelayCommand NextWeekCommand { get; set; }
        public RelayCommand PreviousWeekCommand { get; set; }
        public RelayCommand ReturnCommand { get; set; }

        #endregion

        #region Weekday List

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

        // Regular Props
        public bool Complete { get; set; }
        public bool HasStartTimeBeenSet { get; set; }
        public int WeekCounter { get; set; }

        private int calcWorkHours;

        public int CalcWorkHours
        {
            get { return calcWorkHours; }
            set { calcWorkHours = value; OnPropertyChanged(nameof(CalcWorkHours)); }
        }


        private List<EmployeeModel> employeeList;

        public List<EmployeeModel> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; }
        }

        /// <summary>
        /// Stores the data of the selected Employee from the drop down menu
        /// </summary>
        private EmployeeModel selectedEmployee;

        public EmployeeModel SelectedEmployee
        {
            get { return selectedEmployee; }
            set 
            { 
                selectedEmployee = value; 
                OnPropertyChanged(nameof(SelectedEmployee)); 
                EditShiftCommand.RaiseCanExecuteChanged(); 
                DeleteShiftCommand.RaiseCanExecuteChanged();
                AddShiftCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// When clicked it will return if there is an appointment selected, if so requiry will return true, else false
        /// Allows for edit and delete command to enable 
        /// </summary>
        private ShiftModel selectedShift;

        public ShiftModel SelectedShift
        {
            get { return selectedShift; }
            set 
            { 
                selectedShift = value; 
                OnPropertyChanged(nameof(SelectedShift));
                DeleteShiftCommand.RaiseCanExecuteChanged();
            }
        }

        #region Calendar Dates 

        /// <summary>
        /// Saves the selected date for database saving 
        /// </summary>
        private DateTime selectedShiftDate;

        public DateTime SelectedShiftDate
        {
            get { return selectedShiftDate; }
            set
            {
                selectedShiftDate = value;
                OnPropertyChanged(nameof(SelectedShiftDate));

                // Updates properties and allows for saving to database 
                Day = SelectedShiftDate.Day;
                Month = SelectedShiftDate.Month;
                Year = SelectedShiftDate.Year;
            }
        }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateTime CurrentDate { get; set; }

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

        #region ClockTimeSelections 

        private int startSelectedHour;

        public int StartSelectedHour
        {
            get { return startSelectedHour; }
            set { startSelectedHour = value; OnPropertyChanged(nameof(StartSelectedHour)); }
        }

        private int startSelectedMinute;

        public int StartSelectedMinute
        {
            get { return startSelectedMinute; }
            set { startSelectedMinute = value; OnPropertyChanged(nameof(StartSelectedMinute)); }
        }

        private int endSelectedHour;

        public int EndSelectedHour
        {
            get { return endSelectedHour; }
            set { endSelectedHour = value; OnPropertyChanged(nameof(EndSelectedHour)); }
        }

        private int endSelectedMinute;

        public int EndSelectedMinute
        {
            get { return endSelectedMinute; }
            set { endSelectedMinute = value; OnPropertyChanged(nameof(EndSelectedMinute)); }
        }
        #endregion

        #region Clock Visibility 

        private bool clockControlVisibility;

        public bool ClockControlVisibility
        {
            get { return clockControlVisibility; }
            set { clockControlVisibility = value; OnPropertyChanged(nameof(ClockControlVisibility)); }
        }

        private bool hourClockVisibility;

        public bool HourClockVisibility
        {
            get { return hourClockVisibility; }
            set { hourClockVisibility = value; OnPropertyChanged(nameof(HourClockVisibility)); }
        }

        private bool minClockVisibility;

        public bool MinClockVisibility
        {
            get { return minClockVisibility; }
            set { minClockVisibility = value; OnPropertyChanged(nameof(minClockVisibility)); }
        }

        private bool confirmationVisibility;

        public bool ConfirmationVisiblity
        {
            get { return confirmationVisibility; }
            set { confirmationVisibility = value; OnPropertyChanged(nameof(ConfirmationVisiblity)); }
        }



        #endregion


        private string clockDisplayText;

        public string ClockDisplayText
        {
            get { return clockDisplayText; }
            set { clockDisplayText = value; OnPropertyChanged(nameof(ClockDisplayText)); }
        }

        private bool _AMPMBool;

        public bool AMPMBool
        {
            get { return _AMPMBool; }
            set { _AMPMBool = value; OnPropertyChanged(nameof(AMPMBool)); }
        }

        // Full List to be filtered through 
        private List<ShiftModel> fullShiftList;

        public List<ShiftModel> FullShiftList
        {
            get { return fullShiftList; }
            set { fullShiftList = value; OnPropertyChanged(nameof(FullShiftList)); }
        }


        #endregion

        #region Constructor

        public ScheduleViewModel()
        {
            // Init Commands 
            // This command allows for access of the current element being clicked 
            MinSelectCommand = new RelayCommand<object>(CheckMinPressed);
            HourSelectCommand = new RelayCommand<object>(CheckHourPressed);

            EditShiftCommand = new RelayCommand(() => EditShift(), () => CheckValue<ShiftModel>(SelectedShift));
            DeleteShiftCommand = new RelayCommand(() => DeleteShift(), () => CheckValue<ShiftModel>(SelectedShift));
            AddShiftCommand = new RelayCommand(() => AddShift(), () => CheckValue<EmployeeModel>(SelectedEmployee));
            NextWeekCommand = new RelayCommand(() => IncrementWeek());
            PreviousWeekCommand = new RelayCommand(() => DecrementWeek());
            ReturnCommand = new RelayCommand(() => Return());

            // Init Lists

            SundayList = new ObservableCollection<ShiftModel>();
            MondayList = new ObservableCollection<ShiftModel>();
            TuesdayList = new ObservableCollection<ShiftModel>();
            WednesdayList = new ObservableCollection<ShiftModel>();
            ThursdayList = new ObservableCollection<ShiftModel>();
            FridayList = new ObservableCollection<ShiftModel>();
            SaturdayList = new ObservableCollection<ShiftModel>();



            // This will be used to commit the shift and save 
            CompleteShiftAddCommand = new RelayCommand(() => SaveShift(), () => CheckIfShiftReadyToSave());
            CancelShiftAddCommand = new RelayCommand(() => CancelShiftAdd());

            // Generates the list for the Combobox 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();

            // Setting selected Date to current date 
            SelectedShiftDate = DateTime.Now;

            // Sets the default clock display text 
            ClockDisplayText = "Select Starting Hour";

            // Sets the starting value to AM
            AMPMBool = true;

            // Gets the current date to populate the scheduler 
            CurrentDate = DateTime.Now;
            AdjustWeekDayDates();

            // Set Starting visibility of clocks 
            ClockControlVisibility = true;
            HourClockVisibility = true;
            MinClockVisibility = true;
            ConfirmationVisiblity = true;

            // Sorts through the lists and populates the individual days 
            ValidateAndReturnWeekdayLists();
        }

        #endregion

        #region Methods

        // Collect the hour value from the selected button and save to variable 
        public void CheckHourPressed(object sender)
        {
            if (HasStartTimeBeenSet)
            {
                try
                {
                    EndSelectedHour = AdjustClockToMilitary((Button)sender);
                    HourClockVisibility = true;
                    MinClockVisibility = false;
                    ClockDisplayText = "Select Ending Minutes";

                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to Hour parse");
                }
            }
            else
            {
                try
                {
                    StartSelectedHour = AdjustClockToMilitary((Button)sender);
                    HourClockVisibility = true;
                    MinClockVisibility = false;
                    ClockDisplayText = "Select Starting Hour";
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to Hour parse");
                }
            }
        }

        // Collect the minute value from the selected button and save to variable 
        public void CheckMinPressed(object sender)
        {
            if(HasStartTimeBeenSet)
            {
                try
                {
                    EndSelectedMinute = Int32.Parse(((Button)sender).Content.ToString());
                    HourClockVisibility = true;
                    MinClockVisibility = true;
                    HasStartTimeBeenSet = false;
                    ConfirmationVisiblity = false;
                    CalculateShiftHours();
                    
                    // Sets that the final time has been inputted 
                    Complete = true;
                    CompleteShiftAddCommand.RaiseCanExecuteChanged();
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to parse");
                }
            }
            else 
            {
                try
                {
                    StartSelectedMinute = Int32.Parse(((Button)sender).Content.ToString());
                    HourClockVisibility = false;
                    MinClockVisibility = true;
                    HasStartTimeBeenSet = true;
                    ClockDisplayText = "Select Ending Hour";
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to parse");
                }
            }
        }

        // Changes visibility of clock controls 
        public void AddShift()
        {
            ClockControlVisibility = false;
            HourClockVisibility = false;
        }

        // Commits the shift to save 
        public void SaveShift()
        {
            // Save to database 
            DataBaseHelper.AddShift(SelectedEmployee, StartSelectedHour, StartSelectedMinute, EndSelectedHour, EndSelectedMinute,
                Day, Month, Year);

            // Hides the Control on save 
            ClockControlVisibility = true;
            MinClockVisibility = true;
            HourClockVisibility = true;
            ConfirmationVisiblity = true;

            ValidateAndReturnWeekdayLists();
        }

        // This will wait for the selection of the start and end shift before 
        // prompting the ability to save 
        public bool CheckIfShiftReadyToSave()
        {
            if (Complete)
                return true;
            else
                return false;
        }

        // Allows for editing of an existing shift 
        public void EditShift()
        {
            //TODO :: Open the clocks again and take in new values that will edit the currently selected lists 
        }

        // Allows for deleting selected shifts 
        public void DeleteShift()
        {
            DataBaseHelper.DeleteShift(SelectedShift);
            ValidateAndReturnWeekdayLists();
        }

        // Adjusts the time value depending on if AM or PM is selected 
        public int AdjustClockToMilitary(Button button)
        {
            // Gets the content from the button and returns a int 
            var convertedContent = Int32.Parse(button.Content.ToString());

            // If true, then do not convert to military time
            if (AMPMBool)
            {
                // Checking for 12AM which should return 0
                if (convertedContent == 12)
                    return 0;
                else
                    return convertedContent;
            }

            // If PM then convert to military time 
            else if (!AMPMBool)
            {
                // 12PM should not be converted, therefore simply return it
                if (convertedContent == 12)
                    return convertedContent;
                else
                    return convertedContent + 12;
            }

            // This will alert of any errors 
            else
                return 99;
        }

        // Takes into account the days of the week that the program can 
        // Be opened to adjust to proper day of week dates 
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
                    AugmentDate(-2,-1,0,1,2,3,4);
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

        // Sets the date for each day depending on the inputted increments 
        public void AugmentDate(int sundayIncrement, int mondayIncrement, int tuesdayIncrement, int wednesdayIncrement,
            int thursdayIncrement, int fridayIncrement, int saturdayIncrement)
        {
            SundayDate = CurrentDate.AddDays(sundayIncrement).ToString("MMMM dd, yyyy");
            MondayDate = CurrentDate.AddDays(mondayIncrement).ToString("MMMM dd, yyyy");
            TuesdayDate = CurrentDate.AddDays(tuesdayIncrement).ToString("MMMM dd, yyyy");
            WednesdayDate = CurrentDate.AddDays(wednesdayIncrement).ToString("MMMM dd, yyyy");
            ThursdayDate = CurrentDate.AddDays(thursdayIncrement).ToString("MMMM dd, yyyy");
            FridayDate = CurrentDate.AddDays(fridayIncrement).ToString("MMMM dd, yyyy");
            SaturdayDate = CurrentDate.AddDays(saturdayIncrement).ToString("MMMM dd, yyyy");
        }

        // Advances the scheduler by 1 week
        public void IncrementWeek()
        {
            // Increments the week counter -> Recalc the dates of the days 
            WeekCounter = 1;
            SundayDate = Convert.ToDateTime(SundayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            MondayDate = Convert.ToDateTime(MondayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            TuesdayDate = Convert.ToDateTime(TuesdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            WednesdayDate = Convert.ToDateTime(WednesdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            ThursdayDate = Convert.ToDateTime(ThursdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            FridayDate = Convert.ToDateTime(FridayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            SaturdayDate = Convert.ToDateTime(SaturdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");

            // Checks for any dates that are present within the current week 
            ValidateAndReturnWeekdayLists();
        }

        // Reduces the scheduler by 1 week
        public void DecrementWeek()
        {            
            // Decrements the week counter -> Recalc the dates of the days 
            WeekCounter = -1;
            SundayDate = Convert.ToDateTime(SundayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            MondayDate = Convert.ToDateTime(MondayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            TuesdayDate = Convert.ToDateTime(TuesdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            WednesdayDate = Convert.ToDateTime(WednesdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            ThursdayDate = Convert.ToDateTime(ThursdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            FridayDate = Convert.ToDateTime(FridayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");
            SaturdayDate = Convert.ToDateTime(SaturdayDate).AddDays(WeekCounter * 7).ToString("MMMM dd, yyyy");

            // Checks for any dates that are present within the current week 
            ValidateAndReturnWeekdayLists();
        }

        // Change visibility and return all values back to default 
        public void CancelShiftAdd()
        {
            HourClockVisibility = true;
            MinClockVisibility = true;
            ClockControlVisibility = true;

            // Reset the values 
            StartSelectedHour = 0;
            StartSelectedMinute = 0;
            EndSelectedHour = 0;
            EndSelectedMinute = 0;

            // Sets the date picker back to current date 
            SelectedShiftDate = DateTime.Now;

            // Sets the has time been set to false so it opens proper clock 
            HasStartTimeBeenSet = false;
        }

        // Inserts the required items into the seperate lists that can be populated 
        public void ValidateAndReturnWeekdayLists()
        {
            // Empties lists so they can be filled again depending on the week 
            SundayList.Clear();
            MondayList.Clear();
            TuesdayList.Clear();
            WednesdayList.Clear();
            ThursdayList.Clear();
            FridayList.Clear();
            SaturdayList.Clear();

            // Iterates over the entire list finding values that match with the required date
            FullShiftList = DataBaseHelper.ReadShiftDb();
            foreach(var shift in FullShiftList)
            {
                if (shift.Day == Convert.ToDateTime(SundayDate).Day && shift.Month == Convert.ToDateTime(SundayDate).Month)
                    SundayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(MondayDate).Day && shift.Month == Convert.ToDateTime(MondayDate).Month)
                    MondayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(TuesdayDate).Day && shift.Month == Convert.ToDateTime(TuesdayDate).Month)
                    TuesdayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(WednesdayDate).Day && shift.Month == Convert.ToDateTime(WednesdayDate).Month)
                    WednesdayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(ThursdayDate).Day && shift.Month == Convert.ToDateTime(ThursdayDate).Month)
                    ThursdayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(FridayDate).Day && shift.Month == Convert.ToDateTime(FridayDate).Month)
                    FridayList.Add(shift);
                else if (shift.Day == Convert.ToDateTime(SaturdayDate).Day && shift.Month == Convert.ToDateTime(SaturdayDate).Month)
                    SaturdayList.Add(shift);
            }
        }

        public bool CheckValue<T>(object value)
        {
            if ((T)value != null)
                return true;
            else 
                return false;
        }

        public void CalculateShiftHours()
        {
            CalcWorkHours = (Math.Abs(StartSelectedHour - EndSelectedHour) + (StartSelectedMinute - EndSelectedMinute));
        }

        public void Return()
        {
            MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
        }

        #endregion
    }
}
