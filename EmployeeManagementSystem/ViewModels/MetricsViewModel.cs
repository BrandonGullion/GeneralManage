using ClassLibrary;
using EmployeeManagementSystem.Pages;
using EmployeeManagementSystem.ValueConverters;
using GalaSoft.MvvmLight.Command;


namespace EmployeeManagementSystem
{
    public class MetricsViewModel : BaseViewModel
    {
        #region Properties 
        public RelayCommand ReturnHomeCommand { get; set; }
        public RelayCommand WeeklyMetricsCommand { get; set; }
        public RelayCommand BiWeeklyMetricsCommand { get; set; }
        public RelayCommand MonthlyMetricsCommand { get; set; }
        public RelayCommand YearlyMetricCommand { get; set; }

        private MainWindowViewModel mainWindowVM;

        public MainWindowViewModel MainWindowVM
        {
            get { return mainWindowVM; }
            set { mainWindowVM = value; OnPropertyChanged(nameof(MainWindowVM)); }
        }


        // Current Display page for the right side of the screen
        private object displayPage;
        public object DisplayPage
        {
            get { return displayPage; }
            set { displayPage = AppEnumToPageConverter.ChangePage(value); OnPropertyChanged(nameof(DisplayPage)); }
        }

        #endregion

        #region Constructor

        public MetricsViewModel(MainWindowViewModel vm)
        {
            // Init props 
            MainWindowVM = vm;

            // Relay Commands 
            ReturnHomeCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.Dashboard);
            WeeklyMetricsCommand = new RelayCommand(() => DisplayPage = ApplicationPage.WeeklyMetricPage);
            BiWeeklyMetricsCommand = new RelayCommand(() => DisplayPage = ApplicationPage.BiWeeklyMetricPage);
            MonthlyMetricsCommand = new RelayCommand(() => DisplayPage = ApplicationPage.MonthlyMetricPage);
            YearlyMetricCommand = new RelayCommand(() => DisplayPage = ApplicationPage.YearlyMetricPage);

        }

        #endregion

        #region Methods



        #endregion
    }
}
