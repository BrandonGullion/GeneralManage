﻿using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

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
        public RelayCommand TestCommand { get; set; }
        public RelayCommand<object> AvailabilityCheck { get; set; }
        public RelayCommand UpdateWageControlCommand { get; set; }
        public RelayCommand ClearAllCommand { get; set; }
        public RelayCommand SelectAllCommand { get; set; }


        // Regular Properties 


        // Used to check if there are any changes to the Availability Control 
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; OnPropertyChanged(nameof(IsDirty)); }
        }



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
            set { 
                selectedEmployee = value; 
                OnPropertyChanged(nameof(SelectedEmployee)); 
                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                if (SelectedEmployee != null)
                {
                    CurrentAvailabilityModel = DataBaseHelper.ReadAvailability(SelectedEmployee);
                    UpdateAvailability();
                    IsDirty = false;
                }
            } 
        }

        // Availability Model Used to populate the Availability Menu 
        private AvailabilityModel currentAvailabilityModel;

        public AvailabilityModel CurrentAvailabilityModel
        {
            get 
            { 
                IsDirty = true;
                if (IsDirty)
                    UpdateWageControlCommand.RaiseCanExecuteChanged();
                return currentAvailabilityModel;
            }
            set 
            {
                currentAvailabilityModel = value; 
                OnPropertyChanged(nameof(CurrentAvailabilityModel));
            }
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

        private EmployeeModel addEmployeeModel;
        public EmployeeModel AddEmployeeModel
        {
            get { return addEmployeeModel; }
            set { addEmployeeModel = value; OnPropertyChanged(nameof(AddEmployeeModel)); }
        }


        #endregion


        #endregion

        #region Constructor 

        public EmployeePageViewModel()
        {
            // Commands 
            DashboardCommand = new RelayCommand(() => ReturnToDashboard());
            EditCommand = new RelayCommand(() => EditEmployee(), () => CheckSelected<EmployeeModel>(SelectedEmployee));
            CancelAddCommand = new RelayCommand(() => CancelAdd());
            CancelEditCommand = new RelayCommand(() => CancelEdit());
            SaveCommand = new RelayCommand(() => Save());
            AddCommand = new RelayCommand(() => OpenAddControl());
            DeleteCommand = new RelayCommand(() => DeleteEmployee(SelectedEmployee), () => CheckSelected<EmployeeModel>(SelectedEmployee));
            ClearAllCommand = new RelayCommand(() => ClearAvailability());
            SelectAllCommand = new RelayCommand(() => SelectAllAvailability());

            UpdateCommand = new RelayCommand(() => UpdateEmployee());
            UpdateWageControlCommand = new RelayCommand(() => UpdateAll(CurrentAvailabilityModel), () => IsDirty ? true : false); 

            // Commands used for testing 

            // False means not collapsed, true is collapsed 
            EmployeeInfoControlVisibility = false;
            AddEmployeeControlVisibility = true;
            EditEmployeeControlVisibility = true;

            // DB Methods 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
            PositionList = DataBaseHelper.ReadPositionDB();

            // Sets instance to allow for binding 
            CurrentAvailabilityModel = new AvailabilityModel();
            AddEmployeeModel = new EmployeeModel();

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
        public bool CheckSelected<T>(object value)
        {
            return (T)value != null ? true : false;
        }

        // Return to main employee information page from add
        public void CancelAdd()
        {
            EmployeeInfoControlVisibility = false;
            AddEmployeeControlVisibility = true;

            // When collapsing the control, reset all values 
            AddEmployeeModel.FirstName = string.Empty;
            AddEmployeeModel.LastName = string.Empty;
            AddEmployeeModel.PhoneNumber = string.Empty;
            AddEmployeeModel.AuthorityLevel = 0;
            AddEmployeeModel.Position = string.Empty;
            AddEmployeeModel.Ext = string.Empty;
        }

        // Return to main employee informatin page from edit 
        public void CancelEdit()
        {
            EmployeeInfoControlVisibility = false;
            EditEmployeeControlVisibility = true;
            LeftControlEnabled = true;
        }

        // Save New Employee, update selected and change vis
        public void Save()
        {
            // Saves Employee, updates selectedEmployee and updates the Employee List 
            SelectedEmployee = DataBaseHelper.AddEmplyee(AddEmployeeModel);
            EmployeeList = DataBaseHelper.ReadEmployeeDB();

            // Clears the Added Employee
            AddEmployeeModel = new EmployeeModel();

            // Updates Visiblity of Controls 
            AddEmployeeControlVisibility = true;
            EmployeeInfoControlVisibility = false;
        }

        // Check desired textboxes before saving 
        public bool CheckBeforeSave()
        {
            return !string.IsNullOrEmpty(AddEmployeeModel.FirstName) && 
                !string.IsNullOrWhiteSpace(AddEmployeeModel.LastName) &&
                !string.IsNullOrWhiteSpace(AddEmployeeModel.DOB)? true : false;
        }

        // Selected selected employee
        public void DeleteEmployee(EmployeeModel employeeModel)
        {
            DataBaseHelper.DeleteEmployee(employeeModel);
            EmployeeList = DataBaseHelper.ReadEmployeeDB();
        }

        // Updates the selected employee
        public void UpdateEmployee()
        {
            // Update Employee
            SelectedEmployee = DataBaseHelper.UpdateEmployee(SelectedEmployee);

            // Update the list 
            EmployeeList = DataBaseHelper.ReadEmployeeDB();

            // Return Visibilities to normal 
            LeftControlEnabled = true;
            EditEmployeeControlVisibility = true;
            EmployeeInfoControlVisibility = false;
        }

        // Update the availability whenever 
        public void UpdateAvailability()
        {
            CurrentAvailabilityModel = DataBaseHelper.ReadAvailability(SelectedEmployee);

            IsDirty = false;
            UpdateWageControlCommand.RaiseCanExecuteChanged();
        }

        // This is used to save the Availability Model To DB
        public void UpdateAll(AvailabilityModel availabilityModel)
        {
            DataBaseHelper.UpdateAvailability(availabilityModel);
            IsDirty = false;
            UpdateWageControlCommand.RaiseCanExecuteChanged();
        }

        // Makes all bool values in availability false
        public void ClearAvailability()
        {
            // Gets all properties from a class and puts them into an array 
            PropertyInfo[] propertyInfos = typeof(AvailabilityModel).GetProperties();

            // Iterates over the list and updates the values to false 
            foreach (var property in propertyInfos)
            {
                // Checks if the property is a boolean and then changes the values of the 
                // Given object 
                if(property.PropertyType == typeof(bool))
                    property.SetValue(CurrentAvailabilityModel, false);
            }
        
            OnPropertyChanged(nameof(CurrentAvailabilityModel)) ;
        }

        // Makes all bool values in availability true 
        public void SelectAllAvailability()
        {
            PropertyInfo[] propertyInfos = typeof(AvailabilityModel).GetProperties();

            foreach (var property in propertyInfos)
            {
                if (property.PropertyType == typeof(bool))
                    property.SetValue(CurrentAvailabilityModel, true);
            }

            OnPropertyChanged(nameof(CurrentAvailabilityModel));
        }
        #endregion
    }
}
