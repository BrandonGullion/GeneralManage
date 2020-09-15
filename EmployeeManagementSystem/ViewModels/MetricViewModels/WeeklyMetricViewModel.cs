using ClassLibrary;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public class WeeklyMetricViewModel : BaseViewModel
    {
        #region Properties 
        // Commands 
        public RelayCommand IncrementWeekCommand { get; set; }
        public RelayCommand DecrementWeekCommand { get; set; }

        // Observable Collections 
        public ObservableCollection<double> WeeklyHourList { get; set; }
        public ObservableCollection<double> WeeklyWageCostList { get; set; }
        public ObservableCollection<DateTime> Weekdays { get; set; }
        public ObservableCollection<MetricModel> MetricModelList { get; set; }

        // Series 
        private SeriesCollection weeklyHourUsageSeries;
        public SeriesCollection WeeklyHourUsageSeries
        {
            get { return weeklyHourUsageSeries; }
            set { weeklyHourUsageSeries = value; OnPropertyChanged(nameof(WeeklyHourUsageSeries)); }
        }

        private SeriesCollection weeklyWageCostSeries;
        public SeriesCollection WeeklyWageCostSeries
        {
            get { return weeklyWageCostSeries; }
            set { weeklyWageCostSeries = value; OnPropertyChanged(nameof(WeeklyWageCostSeries)); }
        }

        private SeriesCollection hoursByEmployeeSeries;
        public SeriesCollection HoursByEmployeeSeries
        {
            get { return hoursByEmployeeSeries; }
            set { hoursByEmployeeSeries = value; OnPropertyChanged(nameof(HoursByEmployeeSeries)); }
        }

        // Regular Properties 
        public Func<double, string> Formatter { get; set; }
        public ConcurrentDictionary<string, double> valuePairs { get; set; }
        public string[] WeekdayLabels { get; set; } = { "Sunday", "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday"};
        public DateTime CurrentDate { get; set; }

        #endregion

        #region Constructor 

        public WeeklyMetricViewModel()
        {
            // Sets the current time 
            CurrentDate = DateTime.Now;

            // This is used to format the actual numbers in the charts 
            Formatter = value => value.ToString("N");

            // Fills list with Weekdays of type DateTime 
            Weekdays = WeekdayGenerator.ReturnWeekdayList();

            // Adjusts to the current week 
            AdjustWeekDayDates();

            // Fills the metric model list 
            MetricModelList = DataBaseHelper.ReadAllDB<MetricModel>(DataBaseHelper.EmployeeDatabase);

            // Creating lists to include all 0's before iterating over the lists 
            WeeklyHourList = new ObservableCollection<double>() { 0,0,0,0,0,0,0};
            WeeklyWageCostList = new ObservableCollection<double>() { 0, 0, 0, 0, 0, 0, 0 };

            valuePairs = new ConcurrentDictionary<string, double>();

            // Updates the list and populates the required information depending on the week 
            UpdateLists();

            // Runs to make sure that the value pairs are up to date 
            UpdateEmployeeHourDictionary();

            // Creating series for the associated charts to bind to 
            UpdateSeries();

            // RelayCommands
            IncrementWeekCommand = new RelayCommand(() => ChangeWeek(1));
            DecrementWeekCommand = new RelayCommand(() => ChangeWeek(-1));
        }

        #endregion

        #region Methods 

        // Base for augmenting dates, this is used in the adjust weekday dates 
        public void AugmentDate(int sundayIncrement, int mondayIncrement, int tuesdayIncrement, int wednesdayIncrement,
            int thursdayIncrement, int fridayIncrement, int saturdayIncrement)
        {
            Weekdays[0] = CurrentDate.AddDays(sundayIncrement);
            Weekdays[1] = CurrentDate.AddDays(mondayIncrement);
            Weekdays[2] = CurrentDate.AddDays(tuesdayIncrement);
            Weekdays[3] = CurrentDate.AddDays(wednesdayIncrement);
            Weekdays[4] = CurrentDate.AddDays(thursdayIncrement);
            Weekdays[5] = CurrentDate.AddDays(fridayIncrement);
            Weekdays[6] = CurrentDate.AddDays(saturdayIncrement);
        }

        // Checks the current date and adjusts the days of the week date accordingly
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

        // Iterates through lists and matches them to the correct day 
        public void UpdateLists()
        {
            // Goes through all models within the given list 
            foreach(var metricModel in MetricModelList)
            {
                // Setting for loop to iterate through the weeks, each list is set up sunday -> saturday 
                for(var i = 0; i < 7; i++)
                {
                    // If the selected metric model day, month and year match the currently selected week, it is added to the list 
                    // of hours that follows the sunday -> saturday rule
                    if (metricModel.Day == Weekdays[i].Day && metricModel.Month == Weekdays[i].Month && metricModel.Year == Weekdays[i].Year)
                    {
                        // Adds the hours to WeeklyHoursList
                        WeeklyHourList[i] += metricModel.Hours;

                        // Adds hours * wage to get total dollars spent and adds to associated day 
                        WeeklyWageCostList[i] += (metricModel.Hours * metricModel.Wage);
                    }
                }
            }
        }

        // Updates depending on the employee name and associates a metric model accumulated hours 

        /// <summary>
        /// TODO :: Check the days against the metric model and only add it to the dictionary if the metric 
        ///         model days are within the correct days 
        /// </summary>

        public void UpdateEmployeeHourDictionary()
        {
            foreach (var metricModel in MetricModelList)
            {
                valuePairs.AddOrUpdate(metricModel.Name, metricModel.Hours, (metricModelName, hours) => hours + metricModel.Hours);
            }
        }

        /// <summary>
        /// Increments or decrements the current week and fills lists accordingly 
        /// </summary>
        /// <param name="weekCounter">1 to increment by a week, or -1 to decrement</param>
        public void ChangeWeek(int weekCounter)
        {
            // Increments the weekdays depending on the button pressed 
            for (int i = 0; i < Weekdays.Count; i++)
                Weekdays[i] = Weekdays[i].AddDays(weekCounter * 7);

            ClearAllLists();

            UpdateLists();
            UpdateEmployeeHourDictionary();
            UpdateSeries();
        }

        public void UpdateSeries()
        {
            WeeklyHourUsageSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(WeeklyHourList) } };
            WeeklyWageCostSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(WeeklyWageCostList) } };
            HoursByEmployeeSeries = new SeriesCollection() { new RowSeries { Values = new ChartValues<double>(valuePairs.Values) } };
        }

        public void ClearAllLists()
        {
            for(var i = 0; i < 7; i++)
            {
                WeeklyHourList[i] = 0;
                WeeklyWageCostList[i] = 0;
                valuePairs.Clear();
            }
        }


        #endregion
    }

}
