using ClassLibrary;
using EmployeeManagementSystem.ValueConverters;
using GalaSoft.MvvmLight.Command;

namespace EmployeeManagementSystem
{

    public class DashboardViewModel : BaseViewModel
    {
        #region Properties 

        // Relay Commands 
        public RelayCommand OpenCommand { get; set; }
        public RelayCommand ScheduleCommand { get; set; }
        public RelayCommand EmployeeCommand { get; set; }
        public RelayCommand VacationCommand { get; set; }
        public RelayCommand MetricCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }



        private MainWindowViewModel mainWindowVM ;
        public MainWindowViewModel MainWindowVM
        {
            get { return mainWindowVM; }
            set { mainWindowVM = value; OnPropertyChanged(nameof(MainWindowVM)); }
        }

        private object dashboardPage;
        public object DashboardPage
        {
            get { return dashboardPage; }
            set { dashboardPage = value; OnPropertyChanged(nameof(DashboardPage)); }
        }


        #endregion

        #region Constructor 

        public DashboardViewModel(object page, MainWindowViewModel VM)
        {
            DashboardPage = page;
            MainWindowVM = VM;

            // Relay Commands 
            OpenCommand = new RelayCommand(() => Open());
            ScheduleCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.SchedulePage);
            EmployeeCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.EmployeePage);
            VacationCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.VacationPage);
            MetricCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.EmployeeMetricPage);
            SettingsCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.SettingsPage);

        }

        #endregion

        #region Methods 

        public void Open()
        {

        }
        
        #endregion
    }
}
