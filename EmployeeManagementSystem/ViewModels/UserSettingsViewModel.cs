using ClassLibrary;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace EmployeeManagementSystem
{

    /// <summary>
    /// Datacontext for the user settings page 
    /// </summary>
    public class UserSettingsViewModel : BaseViewModel
    {
        #region Properties 

        public int[] AuthorityLevels { get; set; }
        public ObservableCollection<UserModel> UserModels { get; set; }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }
        public RelayCommand DeleteUserCommand { get; set; }


        #endregion


        #region Constructor 

        public UserSettingsViewModel()
        {
            AuthorityLevels = new int[] { 1, 2, 3 };
            UserModels = DataBaseHelper.ReadAllDB<UserModel>(DataBaseHelper.UserDatabase);
            DeleteUserCommand = new RelayCommand(() => DeleteSelectedUser(SelectedUser));
        }

        #endregion


        #region Methods

        public void DeleteSelectedUser(UserModel userModel)
        {
            if(userModel != null)
            {
                UserModels.Remove(userModel);
                DataBaseHelper.DeleteModel<UserModel>(userModel, DataBaseHelper.UserDatabase);
                System.Console.WriteLine("Successfully Deleted User");
            }
            else
                System.Console.WriteLine("There was an error deleting the desired user");
        }

        #endregion
    }
}
