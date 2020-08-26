using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem
{
    public class EmployeePageViewModel : BaseViewModel
    {
        #region Properties

        // Commands
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DashboardCommand { get; set; }
        public RelayCommand CancelAddCommand { get; set; }
        public RelayCommand CancelEditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }


        // Regular Properties 
        private List<EmployeeModel> employeeList;

        public List<EmployeeModel> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; OnPropertyChanged(nameof(EmployeeList)); }
        }

        public List<PositionModel> PositionList { get; set; }

        // Search bar text to filter list simultaneously 
        private string searchBarText;
        public string SearchBarText
        {
            get { return searchBarText; }
            set 
            { 
                searchBarText = value; 
                OnPropertyChanged(nameof(SearchBarText));
                UpdateList();
            }
        }

        // Selected Employye within the List View 
        private EmployeeModel selectedEmployee;

        public EmployeeModel SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value; OnPropertyChanged(nameof(SelectedEmployee)); DeleteCommand.RaiseCanExecuteChanged(); } 
        }

        private bool leftControlEnabled;

        public bool LeftControlEnabled
        {
            get { return leftControlEnabled; }
            set { leftControlEnabled = value; OnPropertyChanged(nameof(LeftControlEnabled)); }
        }


        #region Visibility Properties 

        // Bind control visibility for seamless transitions 
        private bool addEmployeeControlVisibility;

        public bool AddEmployeeControlVisibility
        {
            get { return addEmployeeControlVisibility; }
            set { addEmployeeControlVisibility = value; OnPropertyChanged(nameof(AddEmployeeControlVisibility)); }
        }

        // Bind control visibility to allow for easy changing of controls 
        private bool employeeInfoControlVisibility;

        public bool EmployeeInfoControlVisibility
        {
            get { return employeeInfoControlVisibility; }
            set { employeeInfoControlVisibility = value; OnPropertyChanged(nameof(EmployeeInfoControlVisibility)); }
        }

        private bool editEmployeeControlVisibility;

        public bool EditEmployeeControlVisibility
        {
            get { return editEmployeeControlVisibility; }
            set { editEmployeeControlVisibility = value; OnPropertyChanged(nameof(EditEmployeeControlVisibility)); }
        }

        #endregion

        #region Contact Properties 

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(nameof(FirstName)); SaveCommand.RaiseCanExecuteChanged(); }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(nameof(LastName)); SaveCommand.RaiseCanExecuteChanged(); }
        }
        private string position;

        public string Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(nameof(Position)); }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }
        private string wage;

        public string Wage
        {
            get { return wage; }
            set { wage = value; OnPropertyChanged(nameof(Wage)); }
        }

        private string employeeId;

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; OnPropertyChanged(nameof(EmployeeId)); }
        }

        private int authorityLevel;

        public int AuthorityLevel
        {
            get { return authorityLevel; }
            set { authorityLevel = value; }
        }

        private string ext;

        public string Ext
        {
            get { return ext; }
            set { ext = value; OnPropertyChanged(nameof(Ext)); }
        }



        #endregion


        #endregion

        #region Constructor 

        public EmployeePageViewModel()
        {
            // Commands 
            DashboardCommand = new RelayCommand(() => ReturnToDashboard());
            EditCommand = new RelayCommand(() => EditEmployee());
            CancelAddCommand = new RelayCommand(() => CancelAdd());
            CancelEditCommand = new RelayCommand(() => CancelEdit());
            SaveCommand = new RelayCommand(() => Save(), () => CheckBeforeSave());
            AddCommand = new RelayCommand(() => OpenAddControl());
            DeleteCommand = new RelayCommand(() => DeleteEmployee(SelectedEmployee), () => CheckForSelectedEmployee());
            UpdateCommand = new RelayCommand(() => UpdateEmployee());

            // False means not collapsed, true is collapsed 
            EmployeeInfoControlVisibility = false;
            AddEmployeeControlVisibility = true;
            EditEmployeeControlVisibility = true;

            // DB Methods 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
            PositionList = DataBaseHelper.ReadPositionDB();

            LeftControlEnabled = true;
        }
        #endregion

        #region Methods

        // Search feature
        public void UpdateList()
        {
            // Always re read the db to make sure the list is fresh 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();

            // Check the search bar text with every type to reduce the list 
            EmployeeList = EmployeeList.Where(e => e.FirstName.ToLower().Contains((SearchBarText).ToLower())).ToList();

            // Update any ui associated with the Employee List
            OnPropertyChanged(nameof(EmployeeList));
        }

        // Return the content frame to the main dashboard 
        public void ReturnToDashboard()
        {
            MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
        }

        // Makes the add employee control visible 
        public void OpenAddControl()
        {
            EmployeeInfoControlVisibility = true;
            AddEmployeeControlVisibility = false;
        }

        // Makes the edit employee control visible 
        public void EditEmployee()
        {
            EmployeeInfoControlVisibility = true;
            EditEmployeeControlVisibility = false;
            LeftControlEnabled = false;
        }

        // Check if there is a selected employee for the listview 
        public bool CheckIfSelectedEmployee()
        {
            if (SelectedEmployee != null)
                return true;
            else 
                return false;
        }

        // Return to main employee information page from add
        public void CancelAdd()
        {
            EmployeeInfoControlVisibility = false;
            AddEmployeeControlVisibility = true;

            // When collapsing the control, reset all values 
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            AuthorityLevel = 0;
            EmployeeId = string.Empty;
            Position = string.Empty;
            Wage = string.Empty;
            Ext = string.Empty;
        }

        // Return to main employee informatin page from edit 
        public void CancelEdit()
        {
            EmployeeInfoControlVisibility = false;
            EditEmployeeControlVisibility = true;
            LeftControlEnabled = true;
        }

        // Save New Employee

        /// <summary>
        /// 
        /// TODO:: Clear the results and return to the information page with the selected Employee only!
        /// 
        /// </summary>

        public void Save()
        {
            // Saves Employee and updates the Employee List 
            DataBaseHelper.AddEmplyee(FirstName, LastName, AuthorityLevel, EmployeeId, PhoneNumber, Position, Wage);
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
        }

        // Check desired textboxes before saving 
        public bool CheckBeforeSave()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                return true;
            else
                return false;
        }

        // Selected selected employee
        public void DeleteEmployee(EmployeeModel employeeModel)
        {
            DataBaseHelper.DeleteEmployee(employeeModel);
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
        }

        // Check if there is a selected Employee 
        public bool CheckForSelectedEmployee()
        {
            if (SelectedEmployee != null)
                return true;
            else
                return false;
        }

        // Updates the selected employee
        public void UpdateEmployee()
        {
            // Update Employee
            DataBaseHelper.UpdateEmployee(SelectedEmployee);
            // Update the list 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
        }

        #endregion
    }
}
