using ClassLibrary;
using EmployeeManagementSystem.Animations;
using System;
using System.Windows;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage
    {
        public MainWindowViewModel mainWindowVM { get; set; }
        public LoginPageViewModel loginVM { get; set; }
        public LoginPage(MainWindowViewModel mainWindowViewModel)
        {
            SelectedPageAnimation = PageAnimationEnum.SlideFromRight;
            mainWindowVM = mainWindowViewModel;
            loginVM = new LoginPageViewModel(this, mainWindowViewModel);
            DataContext = loginVM;
            InitializeComponent();
        }

        /// <summary>
        /// Handles user login and logs message details 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            // Create return message to log information 
            string returnMessage;

            // Method focused variable for checking if login was successful 
            bool loginComplete;

            // Check DB against inputted username and password 
            mainWindowVM.CurrentUser = DataBaseHelper.GetUserModel(UserNameBox.Text, passwordBox.Password, out returnMessage, out loginComplete);
            
            // **In future add log feature**
            Console.WriteLine(returnMessage);

            // Initiate animation to dashboard if login successfully completed, else display 
            if (loginComplete)
                mainWindowVM.CurrentPage = ApplicationPage.Dashboard;
            else
                loginVM.PassErrorVis = false;
        }
    }
}
