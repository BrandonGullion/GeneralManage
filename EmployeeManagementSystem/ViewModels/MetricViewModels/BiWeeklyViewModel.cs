using ClassLibrary;
using ClassLibrary.DataModels;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EmployeeManagementSystem
{
    public class BiWeeklyViewModel : BaseViewModel
    {
        // Relay Commands 
        public RelayCommand IncrementWeekCommand { get; set; }
        public RelayCommand DecrementWeekCommand { get; set; }

        // Observable Collections 
        public ObservableCollection<double> BiWeeklyHourList { get; set; }
        public ObservableCollection<double> BiWeeklyWageCostList { get; set; }
        public ObservableCollection<DateTime> Weekdays { get; set; }
        public ObservableCollection<MetricModel> MetricModelList { get; set; }
        public ObservableCollection<string> StringWeekdays { get; set; }

        // Series Properties 
        private SeriesCollection biWeeklyHoursSeries;

        public SeriesCollection BiWeeklyHoursSeries
        {
            get { return biWeeklyHoursSeries; }
            set { biWeeklyHoursSeries = value; OnPropertyChanged(nameof(BiWeeklyHoursSeries)); }
        }

        private SeriesCollection biWeeklyWageSeries;

        public SeriesCollection BiWeeklyWageSeries
        {
            get { return biWeeklyWageSeries; }
            set { biWeeklyWageSeries = value; OnPropertyChanged(nameof(BiWeeklyWageSeries)); }
        }

        // Regular Properties 
        public Func<double, string> Formatter { get; set; }
        public DateTime[] WeekdayLabels { get; set; }
        public DateTime CurrentDate { get; set; }

        public BiWeeklyViewModel()
        {
            // RelayCommands 
            IncrementWeekCommand = new RelayCommand(() => ChangeWeek(1));
            DecrementWeekCommand = new RelayCommand(() => ChangeWeek(-1));

            // Sets the current time 
            CurrentDate = DateTime.Now;

            // This is used to format the actual numbers in the charts 
            Formatter = value => value.ToString("N");

            // Generating Starting Lists
            Weekdays = WeekdayGenerator.ReturnBiWeeklyWeekdays();
            StringWeekdays = new ObservableCollection<string>();
            BiWeeklyHourList = new ObservableCollection<double>() { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
            BiWeeklyWageCostList = new ObservableCollection<double>() { 0,0,0,0,0,0,0,0,0,0,0,0,0,0 };

            // Adjust dates depending on the current day the program is started 
            AdjustWeekDayDates();

            // Fills the metric models list 
            MetricModelList = DataBaseHelper.ReadAllDB<MetricModel>(DataBaseHelper.EmployeeDatabase);

            // Updates the Hour list and the wage list depending on the dates and sets the according series 
            UpdateLists();
            UpdateSeries();
        }

        // Adjusts weekdays depending on the current weekday 
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
        public void AugmentDate(int sundayIncrement, int mondayIncrement, int tuesdayIncrement, int wednesdayIncrement,
            int thursdayIncrement, int fridayIncrement, int saturdayIncrement)
        {
            // Sets the Previous weeks dates 
            Weekdays[0] = CurrentDate.AddDays(sundayIncrement - 7);
            Weekdays[1] = CurrentDate.AddDays(mondayIncrement - 7);
            Weekdays[2] = CurrentDate.AddDays(tuesdayIncrement - 7);
            Weekdays[3] = CurrentDate.AddDays(wednesdayIncrement - 7);
            Weekdays[4] = CurrentDate.AddDays(thursdayIncrement - 7);
            Weekdays[5] = CurrentDate.AddDays(fridayIncrement - 7);
            Weekdays[6] = CurrentDate.AddDays(saturdayIncrement - 7);

            // Sets the Current Weekdays depending on the current weekday 
            Weekdays[7] = CurrentDate.AddDays(sundayIncrement);
            Weekdays[8] = CurrentDate.AddDays(mondayIncrement);
            Weekdays[9] = CurrentDate.AddDays(tuesdayIncrement);
            Weekdays[10] = CurrentDate.AddDays(wednesdayIncrement);
            Weekdays[11] = CurrentDate.AddDays(thursdayIncrement);
            Weekdays[12] = CurrentDate.AddDays(fridayIncrement);
            Weekdays[13] = CurrentDate.AddDays(saturdayIncrement);
        }

        public void UpdateLists()
        {
            // Goes through all models within the given list 
            foreach (var metricModel in MetricModelList)
            {
                // Setting for loop to iterate through the weeks, each list is set up sunday -> saturday 
                for (var i = 0; i < BiWeeklyHourList.Count; i++)
                {
                    // If the selected metric model day, month and year match the currently selected week, it is added to the list 
                    // of hours that follows the sunday -> saturday rule
                    if (metricModel.Day == Weekdays[i].Day && metricModel.Month == Weekdays[i].Month && metricModel.Year == Weekdays[i].Year)
                    {
                        // Adds the hours to WeeklyHoursList
                        BiWeeklyHourList[i] += metricModel.Hours;

                        // Adds hours * wage to get total dollars spent and adds to associated day 
                        BiWeeklyWageCostList[i] += (metricModel.Hours * metricModel.Wage);

                        // **** May implement another page to show the 2 week hour usage ****
                        // ValuePairs.AddOrUpdate(metricModel.Name, metricModel.Hours, (metricModelName, hours) => hours + metricModel.Hours);
                    }
                }
            }

            // Adds the dynamic weekdays to the list 
            foreach (var weekday in Weekdays)
                StringWeekdays.Add($"{weekday.ToString("dddd")}\n{weekday.ToString("dd")}th");
        }

        public void UpdateSeries()
        {
            BiWeeklyHoursSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(BiWeeklyHourList) } };
            BiWeeklyWageSeries = new SeriesCollection() { new ColumnSeries { Values = new ChartValues<double>(BiWeeklyWageCostList) } };
        }

        public void ChangeWeek(int weekCounter)
        {
            // Clears the weekday List before repopulating 
            StringWeekdays.Clear();

            // Increments the weekdays depending on the button pressed 
            for (int i = 0; i < Weekdays.Count; i++)
            {
                Weekdays[i] = Weekdays[i].AddDays(weekCounter * 14);
                StringWeekdays.Add($"{Weekdays[i].ToString("dddd")}\n{ Weekdays[i].ToString("dd")}th");
            }

            ClearAllLists();

            UpdateLists();
            UpdateSeries();
        }

        public void ClearAllLists()
        {
            for (var i = 0; i < BiWeeklyHourList.Count; i++)
            {
                BiWeeklyHourList[i] = 0;
                BiWeeklyWageCostList[i] = 0;
            }
        }
    }
}
