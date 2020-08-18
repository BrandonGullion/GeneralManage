using EmployeeManagementSystem.Animations;
using EmployeeManagementSystem.Pages;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace EmployeeManagementSystem
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Properties 

        public MainWindowViewModel MainWindowViewModel { get; set; }

        private ApplicationPage applicationPage;

        public ApplicationPage ApplicationPage
        {
            get { return applicationPage; }
            set { applicationPage = value; OnPropertyChanged(nameof(ApplicationPage)); }
        }


        /// <summary>
        /// This will be used to set an authority level that must be passed throughout the 
        /// application to allow for differing controls to be visible, particularly the 
        /// settings control that will allow for addition or removal of managers 
        /// </summary>
        public int AuthorityLevel { get; set; }

        // Login page so the animation features can be accessed  
        public LoginPage Page { get; set; }

        // Setting page height in view model 
        public int PageHeight { get; set; } = 200;
        public int PageWidth { get; set; } = 300;

        // Secure String for password login

        private SecureString password;

        public SecureString Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        // Username to check against Db

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }


        #endregion

        #region Commands

        #endregion

        #region Constructor 

        public LoginPageViewModel(LoginPage page)
        {
            Page = page;
        }

        #endregion

    }
}
