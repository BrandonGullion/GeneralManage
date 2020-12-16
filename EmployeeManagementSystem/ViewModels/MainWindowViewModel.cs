using ClassLibrary;
using EmployeeManagementSystem.ValueConverters;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;

namespace EmployeeManagementSystem
{
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// View Model for the entire window itself, including the minus, max, min and close buttons
        /// Logic for maximize and minimize within this view model
        /// 
        /// </summary>


        #region Properties

        // Commands 
        public RelayCommand MaxWindowCommand { get; set; }
        public RelayCommand MinWindowCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand OpenUserInfoCommand { get; set; }

        // Current page that the application begins on 

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = AppEnumToPageConverter.ChangePage(value, this); OnPropertyChanged(nameof(CurrentPage)); }
        }

        // Opacity that will allow for animation of the current user once signed in 
        private int currentUserOpacity;
        public int CurrentUserOpacity
        {
            get { return currentUserOpacity; }
            set { currentUserOpacity = value; OnPropertyChanged(nameof(CurrentUserOpacity)); }
        }

        // Visibility for the CurrentUserInfoVisibility
        private bool currentUserInfoVisibility;
        public bool CurrentUserInfoVisibility
        {
            get { return currentUserInfoVisibility; }
            set { currentUserInfoVisibility = value; OnPropertyChanged(nameof(CurrentUserInfoVisibility)); }
        }

        // Hit test bool for the current user toolbar 
        private bool currentUserHitTestBool;
        public bool CurrentUserHitTestBool
        {
            get { return currentUserHitTestBool; }
            set { currentUserHitTestBool = value; OnPropertyChanged(nameof(CurrentUserHitTestBool)); }
        }

        private UserModel currentUser;
        public UserModel CurrentUser
        {
            get { return currentUser; }
            set 
            { 
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                if(CurrentUser.UserFirstName != null && CurrentUser.UserLastName != null)
                {
                    CurrentUserOpacity = 1;
                    CurrentUserHitTestBool = true;
                }
            }
        }




        // Current window for state checks 
        private Window window { get; set; }

        // Sets the desired resize border range from the edge
        public int ResizeBorder { get; set; } = 6;

        // Gives the view the value as a thickness
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder); } }

        // Corner Radius for entire Window
        private int cornerRadius = 10;
        public int CornerRadius
        {
            get { return window.WindowState == WindowState.Maximized ? 0 : cornerRadius; }
            set { cornerRadius = value; OnPropertyChanged(nameof(CornerRadius)); } 
        }

        // Caption click and drag height 
        private int captionHeight;
        public int CaptionHeight
        {
            get { return captionHeight; }
            set { captionHeight = value; OnPropertyChanged(nameof(CaptionHeight)); }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window Window)
        {
            // Get the current window
            window = Window;

            // Sets the starting page of the entire window 
            CurrentPage = ApplicationPage.Login;

            // Commands 
            MaxWindowCommand = new RelayCommand(() => MaximizeWindow());
            MinWindowCommand = new RelayCommand(() => MinimizeWindow());
            CloseWindowCommand = new RelayCommand(() => CloseWindow());
            OpenUserInfoCommand = new RelayCommand(() => ToggleUserInfoBox());

            // Listen Out for window state changes 
            window.StateChanged += (sender, e) => { OnPropertyChanged(nameof(CornerRadius));};

            // Adds the margin associated with the drop shadow effect 
            CaptionHeight = 40;
            CurrentUserOpacity = 0;
            CurrentUserInfoVisibility = true;
            CurrentUserHitTestBool = false; 
        }
        #endregion

        #region Methods 

        /// <summary>
        /// Check to see if window is maxed, if so max, else return to normal size 
        /// </summary>
        public void MaximizeWindow()
        {
            if(window.WindowState != WindowState.Maximized)
                window.WindowState = WindowState.Maximized;

            else if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
        } 

        public void MinimizeWindow()
        {
            if (window.WindowState != WindowState.Minimized)
                window.WindowState = WindowState.Minimized;
        }

        public void CloseWindow()
        {
            window.Close();
        }

        public void ToggleUserInfoBox()
        {
            CurrentUserInfoVisibility = CurrentUserInfoVisibility == true ? false : true;
        }

        #endregion
    }
}
