using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Commands
        public RelayCommand MaxWindowCommand { get; set; }

        public RelayCommand MinWindowCommand { get; set; }

        public RelayCommand CloseWindowCommand { get; set; }
        #endregion



        #region Properties

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

        // Margin to allow for drop shadow
        private int margin = 10;

        public int Margin
        {
            get { return window.WindowState == WindowState.Maximized ? 0 : margin; }
            set { margin = value; OnPropertyChanged(nameof(Margin)); }
        }

        // Caption click and drag height 
        private int captionHeight;

        public int CaptionHeight
        {
            get { return captionHeight; }
            set { captionHeight = value; OnPropertyChanged(nameof(CaptionHeight)); }
        }

        public Page CurrentPage { get; set; } = new Dashboard();

        #endregion

        #region Constructor

        public MainWindowViewModel(Window Window)
        {
            // Get the current window
            window = Window;

            // Commands 
            MaxWindowCommand = new RelayCommand(() => MaximizeWindow());
            MinWindowCommand = new RelayCommand(() => MinimizeWindow());
            CloseWindowCommand = new RelayCommand(() => CloseWindow());

            // Listen Out for window state changes 
            window.StateChanged += (sender, e) =>
            { 
                OnPropertyChanged(nameof(Margin));
                OnPropertyChanged(nameof(CornerRadius));
            };

            // Adds the margin associated with the drop shadow effect 
            CaptionHeight = 30 + Margin;
            
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




        #endregion
    }
}
