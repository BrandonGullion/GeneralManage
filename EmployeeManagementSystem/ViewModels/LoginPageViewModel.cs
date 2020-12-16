using ClassLibrary;
using EmployeeManagementSystem.Animations;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Security;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Properties 


        // Relay Commands 
        public RelayCommand LoginCommand { get; set; }

        private MainWindowViewModel mainWindowVM;
        public MainWindowViewModel MainWindowVM
        {
            get { return mainWindowVM; }
            set { mainWindowVM = value; OnPropertyChanged(nameof(MainWindowVM)); }
        }

        private bool passErrorVis;
        public bool PassErrorVis
        {
            get { return passErrorVis; }
            set { passErrorVis = value; OnPropertyChanged(nameof(PassErrorVis)); }
        }


        private object desiredPage;
        public object DesiredPage
        {
            get { return desiredPage; }
            set { desiredPage = value; OnPropertyChanged(nameof(DesiredPage)); }
        }

        // Login page so the animation features can be accessed  
        public LoginPage LoginPage { get; set; }
        public MainWindowViewModel MainWindowViewModel { get; set; }

        // Setting page height in view model 
        public int PageHeight { get; set; } = 200;
        public int PageWidth { get; set; } = 300;

        // Secure String for password login

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = new string('*', value.Length);
                OnPropertyChanged(nameof(Password));
                Console.WriteLine(Password);
            }
        }

        // Username to check against Db
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }


        #endregion


        #region Constructor 

        public LoginPageViewModel(LoginPage loginPage, MainWindowViewModel VM)
        {
            MainWindowVM = VM;

            // Relay Commands
            LoginCommand = new RelayCommand(() => Login());
            LoginPage = loginPage;

            // Initial Prop Values 
            PassErrorVis = true;

        }

        #endregion


        #region Methods

        /// <summary>
        /// Sets the selected animation of the login page and init animate method
        /// to complete transfer to main dashboard 
        /// </summary>
        public async void Login()
        {
            // TODO :: Implement Login protocols with security measures  

            // Sets the visibility within the main window view model 
            MainWindowVM.CurrentUserHitTestBool = true;
            MainWindowVM.CurrentUserOpacity = 1;

            LoginPage.SelectedPageAnimation = PageAnimationEnum.SlideToLeft;
            await LoginPage.Animate();

            await Task.Delay(500);

            MainWindowVM.CurrentPage = ApplicationPage.Dashboard;
        }
        #endregion
    }
}
