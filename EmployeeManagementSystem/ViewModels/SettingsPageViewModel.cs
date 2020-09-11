using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    public class SettingsPageViewModel : BaseViewModel
    {
        #region Properties 

        // Relay Commands 

        public RelayCommand ReturnDashboardCommand { get; set; }

        #endregion

        #region Constructor 

        public SettingsPageViewModel()
        {
            ReturnDashboardCommand = new RelayCommand(() => ReturnToDashboard());
        }

        #endregion

        #region Methods
        public void ReturnToDashboard()
        {
            MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
        }


        #endregion
    }
}
