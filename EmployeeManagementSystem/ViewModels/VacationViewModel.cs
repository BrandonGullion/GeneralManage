using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmployeeManagementSystem.ViewModels
{
    public class VacationViewModel : BaseViewModel
    {

        #region Properties

        // Commands
        public RelayCommand AddVacationCommand { get; set; }
        public RelayCommand DeleteVacationCommand { get; set; }
        public RelayCommand EditVacationCommand { get; set; }

        public RelayCommand ReturnCommand { get; set; }

        // Search bar text used to filter results 
        private string searchBarText;

        public string SearchBarText
        {
            get { return searchBarText; }
            set
            { 
                searchBarText = value; 
                OnPropertyChanged(nameof(SearchBarText)); 
                UpdateLists(); 
            }
        }

        // Employee list view selection 
        private EmployeeModel selectedEmployeeModel;

        public EmployeeModel SelectedEmployeeModel
        {
            get { return selectedEmployeeModel; }
            set 
            { 
                selectedEmployeeModel = value;
                OnPropertyChanged(nameof(SelectedEmployeeModel)); 
                AddVacationCommand.RaiseCanExecuteChanged();
                DeleteVacationCommand.RaiseCanExecuteChanged();
                EditVacationCommand.RaiseCanExecuteChanged();
            }
        }

        // List for the employee list view 
        private ObservableCollection<EmployeeModel> employeeList;

        public ObservableCollection<EmployeeModel> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; OnPropertyChanged(nameof(EmployeeList)); }
        }

        // Selection by the vacation list view 
        private VacationModel selectedVacation;

        public VacationModel SelectedVacation
        {
            get  { return selectedVacation; }
            set { selectedVacation = value; OnPropertyChanged(nameof(SelectedVacation)); DeleteVacationCommand.RaiseCanExecuteChanged(); }
        }

        // Populates left list view 
        private ObservableCollection<VacationModel> vacationList;

        public ObservableCollection<VacationModel> VacationList
        {
            get { return vacationList; }
            set { vacationList = value; OnPropertyChanged(nameof(VacationList)); }
        }

        private DateTime vacationStartDate;

        public DateTime VacationStartDate
        {
            get { return vacationStartDate; }
            set { vacationStartDate = value; OnPropertyChanged(nameof(VacationStartDate)); }
        }

        private DateTime vacationEndDate;

        public DateTime VacationEndDate
        {
            get { return vacationEndDate; }
            set { vacationEndDate = value; OnPropertyChanged(nameof(VacationEndDate)); }
        }

        #endregion

        #region Constructor

        public VacationViewModel()
        {
            // Commands 
            AddVacationCommand = new RelayCommand(() => AddVacation(),
                () => CheckObject<EmployeeModel>(SelectedEmployeeModel));
            DeleteVacationCommand = new RelayCommand(() => DeleteVacation(), () => CheckObject<VacationModel>(SelectedVacation));
            EditVacationCommand = new RelayCommand(() => EditVacation());
            ReturnCommand = new RelayCommand(() => Return());


            // Reads db, returns a list which is then turned into an observable collection 
            EmployeeList = new ObservableCollection<EmployeeModel>(DataBaseHelper.ReadEmployeeDB());
            VacationList = DataBaseHelper.ReadVacatinDB();

            // Sets the vacation dates to the current day 
            VacationStartDate = DateTime.Now;
            VacationEndDate = DateTime.Now;
        }


        #endregion

        #region Methods

        // Checks the search bar text and updates depending on the inputted data 
        public void UpdateLists()
        {
            // Updates the list, particularly if the search text gets removed
            EmployeeList = new ObservableCollection<EmployeeModel>(DataBaseHelper.ReadEmployeeDB());
            VacationList = DataBaseHelper.ReadVacatinDB();

            // Gets the employee list and iterates through checking the information pertaining to first name 
            EmployeeList = new ObservableCollection<EmployeeModel>(EmployeeList.Where(e => e.FirstName.ToLower().Contains(SearchBarText.ToLower())).ToList());
            VacationList = new ObservableCollection<VacationModel>(VacationList.Where(v => v.Name.ToLower().Contains(SearchBarText.ToLower())).ToList());

            OnPropertyChanged(nameof(EmployeeList));
            OnPropertyChanged(nameof(VacationList));
        }

        // Generic check for any desired types, with ability to check multiple values 
        public bool CheckObject<T>(object value)
        {
            // Smaller Version of using if statements, if value != null return true, else false        
            return (T)value != null ? true : false;
        }

        public void PopulateVacationList()
        {
            VacationList = DataBaseHelper.ReadVacatinDB();
        }

        public void DeleteVacation()
        {
            DataBaseHelper.DeleteVacation<VacationModel>(SelectedVacation);
            VacationList.Remove(SelectedVacation);
            DeleteVacationCommand.RaiseCanExecuteChanged();
        }

        public void EditVacation()
        {

        }

        public void AddVacation()
        {
            DataBaseHelper.AddVacation(SelectedEmployeeModel, VacationStartDate, VacationEndDate);
            VacationList = DataBaseHelper.ReadVacatinDB();
        }

        public void Return()
        {
            MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
        }

        #endregion

    }
}
