using EmployeeManagementSystem.Pages;
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


        // Current Display page for the right side of the screen
        private ApplicationPage displayPage;
        public ApplicationPage DisplayPage
        {
            get { return displayPage; }
            set { displayPage = value; OnPropertyChanged(nameof(DisplayPage)); }
        }

        #endregion

        #region Constructor

        public MetricsViewModel()
        {
            // Relay Commands 
            ReturnHomeCommand = new RelayCommand(() => MainWindow.mainWindow.MainContentFrame.Content = new Dashboard());
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
