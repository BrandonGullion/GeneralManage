using ClassLibrary;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;


namespace EmployeeManagementSystem
{
    public class YearlyMetricViewModel : BaseViewModel
    {
        #region Properties
        // RelayCommands
        public RelayCommand IncrementYearCommand { get; set; }
        public RelayCommand DecrementYearCommand { get; set; }

        // Observable Collections 
        public ObservableCollection<MetricModel> MetricModelList { get; set; }
        public ObservableCollection<double> YearlyHourList { get; set; }
        public ObservableCollection<double> YearlyWageList { get; set; }
        public ObservableCollection<string> MonthStringList { get; set; }
        // Series 
        private SeriesCollection yearlyHourSeries;
        public SeriesCollection YearlyHourSeries
        {
            get { return yearlyHourSeries; }
            set { yearlyHourSeries = value; OnPropertyChanged(nameof(YearlyHourSeries)); }
        }
        private SeriesCollection yearlyWageSeries;
        public SeriesCollection YearlyWageSeries
        {
            get { return yearlyWageSeries; }
            set { yearlyWageSeries = value; OnPropertyChanged(nameof(YearlyWageSeries)); }
        }

        // Regular Properties 
        private DateTime currentDate;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; OnPropertyChanged(nameof(CurrentDate)); }
        }

        #endregion

        #region Constructor

        public YearlyMetricViewModel()
        {
            // Sets current date
            CurrentDate = DateTime.Now;

            // Init Obv. Collections 
            MetricModelList = new ObservableCollection<MetricModel>();
            YearlyHourList = new ObservableCollection<double>();
            YearlyWageList = new ObservableCollection<double>();
            MonthStringList = new ObservableCollection<string>();

            // RelayCommands
            IncrementYearCommand = new RelayCommand(() => ChangeYear(1));
            DecrementYearCommand = new RelayCommand(() => ChangeYear(-1));

            // Sets the initial lists 
            PopulateLists();
            PopulateSeries();
        }

        #endregion


        #region Methods

        public void PopulateLists()
        {
            // Fills the metric model list 
            MetricModelList = DataBaseHelper.ReadAllDB<MetricModel>(DataBaseHelper.EmployeeDatabase);

            // Clears the lists so allow for updating 
            if (YearlyHourList.Count != 0)
                YearlyHourList.Clear();
            if (YearlyWageList.Count != 0)
                YearlyWageList.Clear();

            // Sets a list of months that can be used to compare to metric model
            for (var i = 0; i < 12; i++)
            {
                YearlyHourList.Add(0);
                YearlyWageList.Add(0);

                // Only Populate on start when the list is empty
                if(MonthStringList.Count < 12)
                    MonthStringList.Add(new DateTime(2020, i + 1, 1).ToString("MMMM"));
            }

            // Iterates over the lists and populates them 
            foreach(var metricModel in MetricModelList)
            {
                if(metricModel.Year == CurrentDate.Year)
                {
                    YearlyHourList[metricModel.Month] += metricModel.Hours;
                    YearlyWageList[metricModel.Month] += (metricModel.Hours * metricModel.Wage);
                }
            }
        }

        public void PopulateSeries()
        {
            // Clears the previous list if not null 
            if (YearlyHourSeries != null)
                YearlyHourSeries.Clear();
            if (YearlyWageSeries != null)
                YearlyWageSeries.Clear();

            // Populates the series 
            YearlyHourSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(YearlyHourList),
                DataLabels=true} };

            YearlyWageSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(YearlyWageList),
                DataLabels=true } };
        }

        public void ChangeYear(int counter)
        {
            CurrentDate = CurrentDate.AddYears(1 * counter);

            PopulateLists();
            PopulateSeries();
        }

        #endregion
    }
}
