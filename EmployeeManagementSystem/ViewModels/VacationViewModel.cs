using ClassLibrary;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public RelayCommand UpdateCommand { get; set; }

        private MainWindowViewModel mainWindowVM;
        public MainWindowViewModel MainWindowVM
        {
            get { return mainWindowVM; }
            set { mainWindowVM = value; OnPropertyChanged(nameof(MainWindowVM)); }
        }

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
            set 
            { 
                selectedVacation = value; 
                OnPropertyChanged(nameof(SelectedVacation));
                DeleteVacationCommand.RaiseCanExecuteChanged();
                EditVacationCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime vacationStartDate;

        public DateTime VacationStartDate
        {
            get { return vacationStartDate; }
            set 
            {
                if (IsEditing)
                {
                    vacationStartDate = value;
                    IsStartDateSet = true;
                    IsEditing = false; 
                    StartDateBorderVisibility = true;
                    EndDateBorderVisibility = false;
                }
                else
                {
                    vacationStartDate = value; 
                    OnPropertyChanged(nameof(VacationStartDate));
                }
            }
        }

        private DateTime vacationEndDate;

        public DateTime VacationEndDate
        {
            get { return vacationEndDate; }
            set 
            {
                if (IsStartDateSet)
                {
                    EndDateBorderVisibility = true;
                    IsStartDateSet = false;
                    vacationEndDate = value;
                    ReadyToUpdate = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                }
                else
                    vacationEndDate = value;
            }
        }


        // Populates left list view 
        private ObservableCollection<VacationModel> vacationList;

        public ObservableCollection<VacationModel> VacationList
        {
            get { return vacationList; }
            set { vacationList = value; OnPropertyChanged(nameof(VacationList)); }
        }

        public bool IsEditing { get; set; } = false;

        public bool IsStartDateSet { get; set; } = false;

        public bool ReadyToUpdate { get; set; } = false;

        #region Visibilities 

        private bool startDateBorderVisibility;

        public bool StartDateBorderVisibility
        {
            get { return startDateBorderVisibility; }
            set { startDateBorderVisibility = value; OnPropertyChanged(nameof(StartDateBorderVisibility)); }
        }

        private bool endDateBorderVisibility;

        public bool EndDateBorderVisibility
        {
            get { return endDateBorderVisibility; }
            set { endDateBorderVisibility = value; OnPropertyChanged(nameof(EndDateBorderVisibility)); }
        }


        private bool updateButtonVisibility;

        public bool UpdateButtonVisibility
        {
            get { return updateButtonVisibility; }
            set { updateButtonVisibility = value; OnPropertyChanged(nameof(UpdateButtonVisibility)); }
        }

        private bool editButtonVisibility;

        public bool EditButtonVisibility
        {
            get { return editButtonVisibility; }
            set { editButtonVisibility = value; OnPropertyChanged(nameof(EditButtonVisibility)); }
        }

        #endregion


        #endregion

        #region Constructor

        public VacationViewModel(MainWindowViewModel vm)
        {
            MainWindowVM = vm;

            // Commands 
            AddVacationCommand = new RelayCommand(() => AddVacation(),
                () => CheckObject<EmployeeModel>(SelectedEmployeeModel));
            DeleteVacationCommand = new RelayCommand(() => DeleteVacation(SelectedVacation), () => CheckObject<VacationModel>(SelectedVacation));
            EditVacationCommand = new RelayCommand(() => EditVacation(), () => CheckObject<VacationModel>(SelectedVacation));
            ReturnCommand = new RelayCommand(() => MainWindowVM.CurrentPage = ApplicationPage.Dashboard);
            UpdateCommand = new RelayCommand(() => UpdateVacation(SelectedVacation, VacationStartDate, VacationEndDate), () => ReadyToUpdate ? true : false);


            /// TODO :: Figure out how to disable the button when the update is not complete


            // Reads db, returns a list which is then turned into an observable collection 
            EmployeeList = new ObservableCollection<EmployeeModel>(DataBaseHelper.ReadEmployeeDB());
            VacationList = DataBaseHelper.ReadVacatinDB();

            // Beggining Visibility Properties 
            StartDateBorderVisibility = true;
            EndDateBorderVisibility = true;
            UpdateButtonVisibility = true;
            EditButtonVisibility = false;


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

        public void DeleteVacation(VacationModel vacationModel)
        {
            DataBaseHelper.DeleteVacation<VacationModel>(vacationModel);
            VacationList.Remove(vacationModel);
            DeleteVacationCommand.RaiseCanExecuteChanged();
        }

        public void EditVacation()
        {
            IsEditing = true;
            StartDateBorderVisibility = false;
            EditButtonVisibility = true;
            UpdateButtonVisibility = false;
            // TODO :: Make it impossible to click any of the other controls 
        }

        public void AddVacation()
        {
            DataBaseHelper.AddVacation(SelectedEmployeeModel, VacationStartDate, VacationEndDate);
            VacationList = DataBaseHelper.ReadVacatinDB();
        }

        public void UpdateVacation(VacationModel vacationModel, DateTime startDate, DateTime endDate)
        {
            vacationModel.StartDate = startDate.ToString("MMMM dd, yyyy");
            vacationModel.EndDate = endDate.ToString("MMMM dd, yyyy");
            DataBaseHelper.UpdateVacation(vacationModel);
            VacationList = new ObservableCollection<VacationModel>(DataBaseHelper.ReadVacatinDB().OrderBy(v => v.Name).ToList());
        }



        #endregion

    }
}
