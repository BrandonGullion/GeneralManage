using ClassLibrary;
using EmployeeManagementSystem.ValueConverters;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem
{
    public class SettingsPageViewModel : BaseViewModel
    {
        #region Properties 

        // Relay Commands 
        public RelayCommand ReturnDashboardCommand { get; set; }
        public RelayCommand OpenUserPageCommand { get; set; }
        public RelayCommand UpdatePasswordCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }

        // Regular Properties 

        // Application page enum that changes and updates with new page 
        private MainWindowViewModel mainWindowVM;
        public MainWindowViewModel MainWindowVM
        {
            get { return mainWindowVM; }
            set { mainWindowVM = value; OnPropertyChanged(nameof(MainWindowVM)); }
        }

        private object currentApplicationPage;
        public object CurrentApplicationPage
        {
            get { return currentApplicationPage; }
            set
            { 
                currentApplicationPage =  AppEnumToPageConverter.ChangePage(value); 
                OnPropertyChanged(nameof(CurrentApplicationPage)); 
            }
        }

        // TODO :: Encrypt all passwords when moving them to the database
        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value; OnPropertyChanged(nameof(OldPassword)); }
        }

        // Lists 
        public ObservableCollection<UserModel> UserList { get; set; }



        #endregion


        #region Constructor 

        public SettingsPageViewModel(MainWindowViewModel vm)
        {
            // Init Props 
            MainWindowVM = vm;

            // Relay Commands 
            ReturnDashboardCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.Dashboard);
            OpenUserPageCommand = new RelayCommand(() => CurrentApplicationPage = ApplicationPage.UserSettingsPage);
            UpdatePasswordCommand = new RelayCommand(() => System.Console.WriteLine("hello"));

            // Init Lists
            UserList = new ObservableCollection<UserModel>(DataBaseHelper.ReadAllDB<UserModel>(DataBaseHelper.UserDatabase));
        }

        #endregion

        #region Methods

        // Updates password for the current user 
        public void UpdatePassword(string oldPassword, string newPassword, string reEnteredNewPassword)
        {


        }
        #endregion
    }
}
