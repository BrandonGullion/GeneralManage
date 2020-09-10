using ClassLibrary;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        public RelayCommand ManualSaveCommand { get; set; }

        #endregion


        #endregion

        // Regular Props
        public bool Complete { get; set; }
        public bool IsEditing { get; set; }
        public bool HasStartTimeBeenSet { get; set; }
        public int WeekCounter { get; set; }

        private bool manualShiftInputBool;
        public bool ManualShiftInputBool
        {
            get { return manualShiftInputBool; }
            set { manualShiftInputBool = value; OnPropertyChanged(nameof(ManualShiftInputBool)); }
        }


        private WeekdayControllerList weekdayController;
        public WeekdayControllerList WeekdayController
        {
            get { return weekdayController; }
            set { weekdayController = value; OnPropertyChanged(nameof(WeekdayController)); }
        }

        private decimal calcWorkHours;
        public decimal CalcWorkHours
        {
            get { return calcWorkHours; }
            set { calcWorkHours = value; OnPropertyChanged(nameof(CalcWorkHours)); }
        }

        private bool manualInputBool;

        // If true, disable the clock, else clock will be used 
        public bool ManualInputBool
        {
            get { return manualInputBool; }
            set { manualInputBool = value; OnPropertyChanged(nameof(ManualInputBool)); }
        }

        public List<int> HourList { get; set; }
        public List<int> MinList { get; set; }


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
                EditShiftCommand.RaiseCanExecuteChanged();
            }
        }

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
                EditShiftCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime selectedShiftEndDate;

        public DateTime SelectedShiftEndDate
        {
            get { return selectedShiftEndDate; }
            set { selectedShiftEndDate = value; OnPropertyChanged(nameof(SelectedShiftEndDate)); }
        }

        #region ClockTimeSelections 

        private int startSelectedHour;

        public int StartSelectedHour
        {
            get { return startSelectedHour; }
            set 
            { 
                startSelectedHour = value; 
                OnPropertyChanged(nameof(StartSelectedHour)); 
                ManualSaveCommand.RaiseCanExecuteChanged(); 
            }
        }

        private decimal startSelectedMinute;

        public decimal StartSelectedMinute
        {
            get { return startSelectedMinute; }
            set { startSelectedMinute = value; OnPropertyChanged(nameof(StartSelectedMinute)); }
        }

        private int endSelectedHour;

        public int EndSelectedHour
        {
            get { return endSelectedHour; }
            set 
            { 
                endSelectedHour = value; 
                OnPropertyChanged(nameof(EndSelectedHour)); 
                ManualSaveCommand.RaiseCanExecuteChanged();
            }
        }

        private decimal endSelectedMinute;

        public decimal EndSelectedMinute
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
            NextWeekCommand = new RelayCommand(() => WeekdayController.ChangeWeek(1));
            PreviousWeekCommand = new RelayCommand(() => WeekdayController.ChangeWeek(-1));
            ReturnCommand = new RelayCommand(() => Return());

            HourList = new List<int>() {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23 };
            MinList = new List<int>() {0,5,10,15,20,25,30,35,40,45,50,55};

            // This will be used to commit the shift and save 
            CompleteShiftAddCommand = new RelayCommand(() => SaveShift(), () => CheckIfShiftReadyToSave());
            CancelShiftAddCommand = new RelayCommand(() => CancelShiftAdd());
            ManualSaveCommand = new RelayCommand(() => SaveShift(), () => CheckInt(StartSelectedHour, EndSelectedHour));


            // Generates the list for the Combobox 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();

            // Setting selected Date to current date 
            SelectedShiftDate = DateTime.Now;
            SelectedShiftEndDate = DateTime.Now;

            // Sets the default clock display text 
            ClockDisplayText = "Select Starting Hour";

            // Sets the starting value to AM
            AMPMBool = true;

            // Set Starting visibility of clocks 
            ClockControlVisibility = true;
            HourClockVisibility = true;
            MinClockVisibility = true;
            ConfirmationVisiblity = true;

            // Need to change the database helper and move it to the core of the class library 
            FullShiftList = DataBaseHelper.ReadShiftDb();
            WeekdayController = new WeekdayControllerList(DataBaseHelper.ReadShiftDb());

            // Sorts through the lists and populates the individual days 
            WeekdayController.UpdateLists(FullShiftList);
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
            // Checks if the timespan is over 2 days 
            if(SelectedShiftDate != SelectedShiftEndDate)
            {
                // Throws exception if start date begins after the end date 
                if(SelectedShiftDate > SelectedShiftEndDate)
                {
                    MessageBox.Show("Shift start date cannot be after end date");

                    // Hides the Control on save 
                    ClockControlVisibility = true;
                    MinClockVisibility = true;
                    HourClockVisibility = true;
                    ConfirmationVisiblity = true;
                    return;
                }

                // throws exception if the shift selected is more than 1 day 
                if (Math.Abs(SelectedShiftEndDate.Day - SelectedShiftDate.Day) > 1)
                {
                    MessageBox.Show("The shift end date can not span more than 1 day");
                    ClockControlVisibility = true;
                    MinClockVisibility = true;
                    HourClockVisibility = true;
                    ConfirmationVisiblity = true;
                    return;
                }

                /// If IsEditing and shift extends over 2 days then split into 2 shifts and save again 
                if (IsEditing)
                {
                    // Splits the Shift Between days ** particularly for night shift workers **
                    DataBaseHelper.UpdateShift(SelectedShift, StartSelectedHour, StartSelectedMinute, 24, 0, SelectedShiftDate.Day,
                        SelectedShiftDate.Month, SelectedShiftDate.Year, 10);

                    DataBaseHelper.UpdateShift(SelectedShift, 0, 0, EndSelectedHour, EndSelectedMinute, SelectedShiftEndDate.Day,
                        SelectedShiftEndDate.Month, SelectedShiftEndDate.Year, 10);

                    // Hides the Control on save 
                    ClockControlVisibility = true;
                    MinClockVisibility = true;
                    HourClockVisibility = true;
                    ConfirmationVisiblity = true;

                    FullShiftList = DataBaseHelper.ReadShiftDb();

                    // Refresh list to make any changes 
                    WeekdayController.UpdateLists(FullShiftList);

                    return;
                }

                // Splits the Shift Between days ** particularly for night shift workers **
                // Code is unreachable when IsEditing is true 
                DataBaseHelper.AddShift(SelectedEmployee, StartSelectedHour, StartSelectedMinute, 24, 0, SelectedShiftDate.Day,
                    SelectedShiftDate.Month, SelectedShiftDate.Year, 10);

                DataBaseHelper.AddShift(SelectedEmployee, 0, 0, EndSelectedHour, EndSelectedMinute, SelectedShiftEndDate.Day,
                    SelectedShiftEndDate.Month, SelectedShiftEndDate.Year, 10);

                // Hides the Control on save 
                ClockControlVisibility = true;
                MinClockVisibility = true;
                HourClockVisibility = true;
                ConfirmationVisiblity = true;

                FullShiftList = DataBaseHelper.ReadShiftDb();

                // Refresh list to make any changes 
                WeekdayController.UpdateLists(FullShiftList);

                return;
            }

            // If editing selected shift the saving will end here 
            if (IsEditing)
            {
                DataBaseHelper.UpdateShift(SelectedShift, StartSelectedHour, StartSelectedMinute, EndSelectedHour, EndSelectedMinute,
                    SelectedShiftDate.Day, SelectedShiftDate.Month, SelectedShiftDate.Year, 10);

                // Hides the Control on save 
                ClockControlVisibility = true;
                MinClockVisibility = true;
                HourClockVisibility = true;
                ConfirmationVisiblity = true;

                FullShiftList = DataBaseHelper.ReadShiftDb();

                // Refresh list to make any changes 
                WeekdayController.UpdateLists(FullShiftList);

                return;
            }

            // Save to database 
            DataBaseHelper.AddShift(SelectedEmployee, StartSelectedHour, StartSelectedMinute, EndSelectedHour, EndSelectedMinute,
                SelectedShiftDate.Day, SelectedShiftDate.Month, SelectedShiftDate.Year, 10);

            // Hides the Control on save 
            ClockControlVisibility = true;
            MinClockVisibility = true;
            HourClockVisibility = true;
            ConfirmationVisiblity = true;

            FullShiftList = DataBaseHelper.ReadShiftDb();

            // Refresh and return the 
            WeekdayController.UpdateLists(FullShiftList);
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
            // Sets IsEditing to true to change save methods 
            IsEditing = true;
            ClockControlVisibility = false;
            HourClockVisibility = false;
        }

        // Allows for deleting selected shifts 
        public void DeleteShift()
        {
            DataBaseHelper.DeleteShift(SelectedShift);
            FullShiftList = DataBaseHelper.ReadShiftDb();

            WeekdayController.UpdateLists(FullShiftList);
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

        // Change visibility and return all values back to default 
        public void CancelShiftAdd()
        {
            HourClockVisibility = true;
            MinClockVisibility = true;
            ClockControlVisibility = true;
            ConfirmationVisiblity = true;

            // Reset the values 
            StartSelectedHour = 0;
            StartSelectedMinute = 0;
            EndSelectedHour = 0;
            EndSelectedMinute = 0;

            // Sets the date picker back to current date 
            SelectedShiftDate = DateTime.Now;
            SelectedShiftEndDate = DateTime.Now;

            // Sets the has time been set to false so it opens proper clock 
            HasStartTimeBeenSet = false;
        }

        // Checks objects to make sure they are not null 
        public bool CheckValue<T>(object value)
        {
            if ((T)value != null)
                return true;
            else 
                return false;
        }

        // Checks ints to make sure they are not all 0, allows for 1 val, or 2
        public bool CheckInt(int val1, int val2)
        {
            if (val1 != 0 && val2 != 0)
                return true;
            else
                return false;
        }

        // Calc shift hours to display total work times 
        public void CalculateShiftHours()
        {
            CalcWorkHours = Math.Abs(StartSelectedHour - EndSelectedHour) + Math.Abs(StartSelectedMinute - EndSelectedMinute)/60;
        }

        // Return to dashboard 
        public void Return()
        {
            MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
        }

        #endregion
    }
}
