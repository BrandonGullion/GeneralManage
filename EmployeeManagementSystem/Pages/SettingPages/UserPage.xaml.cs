using System.Windows.Controls;
using ClassLibrary;
using System;

namespace EmployeeManagementSystem.Pages.SettingPages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        #region Properties 

        public MainWindowViewModel vm { get; set; }
        public UserSettingsViewModel UserSettingsVM { get; set; }

        #endregion

        #region Constructor

        public UserPage()
        {
            InitializeComponent();
            DataContext = new UserSettingsViewModel();
            UserSettingsVM = (UserSettingsViewModel)DataContext;
            vm = (MainWindowViewModel)MainWindow.mainWindow.DataContext;
        }

        #endregion

        #region Methods

        #endregion
        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string loggedMessage;
            DataBaseHelper.UpdatePassword(vm.CurrentUser, NewPasswordBox.Password, ReNewPasswordBox.Password, out loggedMessage);
            Console.WriteLine(loggedMessage);
        }

        private void AddUserSaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string loggedMessage = "There was an error, are both passwords the same?";
            var salt = SaltGenerator.GenerateSalt(32);
            if (HashGenerator.GenerateHashSHA256Hash(AddUserPasswordBox.Password, salt) == HashGenerator.GenerateHashSHA256Hash(ReAddUserPasswordBox.Password, salt))
            {

                DataBaseHelper.InsertUserModel(AddUserNameTextBox.Text, AddUserPasswordBox.Password, FirstNameTextBox.Text, LastNameTextBox.Text,
                    vm.CurrentUser.AuthorityLevel, out loggedMessage, (int)AuthorityLevelComboBox.SelectedItem);
                Console.WriteLine(loggedMessage);

                UserSettingsVM.UserModels = DataBaseHelper.ReadAllDB<UserModel>(DataBaseHelper.UserDatabase);
                UserSettingsVM.OnPropertyChanged(nameof(UserSettingsVM.UserModels));
            }

            else
                Console.WriteLine(loggedMessage);
        }
    }
}
