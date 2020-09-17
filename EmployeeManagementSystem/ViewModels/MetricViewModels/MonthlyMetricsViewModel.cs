using ClassLibrary;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace EmployeeManagementSystem
{
    public class MonthlyMetricsViewModel : BaseViewModel
    {
        #region Properties

        // RelayCommands 
        public RelayCommand IncrementMonthCommand { get; set; }
        public RelayCommand DecrementMonthCommand { get; set; }

        // Observable collections 
        public ObservableCollection<MetricModel> MetricModelList { get; set; }
        public ObservableCollection<double> MonthlyHourList { get; set; }
        public ObservableCollection<double> MonthlyWageList { get; set; }

        public ObservableCollection<string> Days { get; set; }


        // Series 
        private SeriesCollection monthlyHoursSeries;
        public SeriesCollection MonthlyHoursSeries
        {
            get { return monthlyHoursSeries; }
            set { monthlyHoursSeries = value; OnPropertyChanged(nameof(MonthlyHoursSeries)); }
        }

        private SeriesCollection monthlyWageSeries;
        public SeriesCollection MonthlyWageSeries
        {
            get { return monthlyWageSeries; }
            set { monthlyWageSeries = value; OnPropertyChanged(nameof(MonthlyWageSeries)); }
        }

        // Regular Props
        public DateTime CurrentDate { get; set; }
        public int DaysInMonth { get; set; }

        private DateTime firstDayOfMonth;
        public DateTime FirstDayOfMonth
        {
            get { return firstDayOfMonth; }
            set { firstDayOfMonth = value; OnPropertyChanged(nameof(FirstDayOfMonth)); }
        }

        private DateTime lastDayOfMonth;
        public DateTime LastDayOfMonth
        {
            get { return lastDayOfMonth; }
            set { lastDayOfMonth = value; OnPropertyChanged(nameof(LastDayOfMonth)); }
        }

        public Func<int, int> Formatter { get; set; }
        public CartesianMapper<int> Mapper { get; set; }

        #endregion

        #region Constructor
        public MonthlyMetricsViewModel()
        {
            // Gets the current month
            CurrentDate = DateTime.Now;
            DaysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);

            // New instances of the collections 
            MetricModelList = new ObservableCollection<MetricModel>();
            MonthlyHourList = new ObservableCollection<double>();
            MonthlyWageList = new ObservableCollection<double>();
            Days = new ObservableCollection<string>();

            Mapper = new CartesianMapper<int>().X((value, index) => index + 50).Y((value, index) => value);

            PopulateAndUpdateList();
            PopulateAndUpdateSeries();

            // RelayCommands
            IncrementMonthCommand = new RelayCommand(() => ChangeMonth(1));
            DecrementMonthCommand = new RelayCommand(() => ChangeMonth(-1));

            FirstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);

        }

        #endregion

        #region Methods

        public void PopulateAndUpdateList()
        {
            MetricModelList = DataBaseHelper.ReadAllDB<MetricModel>(DataBaseHelper.EmployeeDatabase);

            // Clears the lists, used for repopulating the month list view when the month is changed 
            if (MonthlyHourList.Count != 0)
                MonthlyHourList.Clear();
            if (MonthlyWageList.Count != 0)
                MonthlyWageList.Clear();
            if (Days.Count != 0)
                Days.Clear();

            // Creates hour list indexes depending on 
            for(var i = 0; i < DaysInMonth; i++)
            {
                MonthlyHourList.Add(0);
                MonthlyWageList.Add(0);
                Days.Add((i + 1).ToString());
            }

            /// Iterate through the metric models and add them to the required index depending on the day
            foreach(var metricModel in MetricModelList)
            {
                if(metricModel.Year == CurrentDate.Year && metricModel.Month == CurrentDate.Month)
                {
                    MonthlyHourList[metricModel.Day - 1] += metricModel.Hours;
                    MonthlyWageList[metricModel.Day - 1] += (metricModel.Hours * metricModel.Wage);
                }
            }
        }

        public void PopulateAndUpdateSeries()
        {
            // Clears the Series if they contain any information 
            if(MonthlyWageSeries != null)
            {
                if (MonthlyHoursSeries.Count != 0)
                    MonthlyHoursSeries.Clear();
                if (MonthlyWageSeries.Count != 0)
                    MonthlyWageSeries.Clear();
            }

            // Updates the Series following the list updates for new values 
            MonthlyHoursSeries = new SeriesCollection(Mapper) { new ColumnSeries { Values = new ChartValues<double>(MonthlyHourList),
                DataLabels = true } };
            MonthlyWageSeries = new SeriesCollection(Mapper) { new ColumnSeries { Values = new ChartValues<double>(MonthlyWageList), 
                DataLabels = true}};
        }

        public void ChangeMonth(int monthCounter)
        {
            // Increments the month
            CurrentDate = CurrentDate.AddMonths(1 * monthCounter);
            DaysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);

            // Updates the last and first day of month 
            FirstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfMonth = FirstDayOfMonth.AddMonths(1).AddDays(-1);

            // Populates the lists and series again 
            PopulateAndUpdateList();
            PopulateAndUpdateSeries();
        }

        #endregion

    }
}
