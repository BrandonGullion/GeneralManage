using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class EmployeePageVisibilityController : PropChanged
    {
        #region Properties 

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

        private bool wageVisibility;

        public bool WageVisibility
        {
            get { return wageVisibility; }
            set { wageVisibility = value; OnPropertyChanged(nameof(WageVisibility)); }
        }


        #endregion

        #region Constructor 


        public EmployeePageVisibilityController()
        {
            // False means not collapsed, true is collapsed 
            EmployeeInfoControlVisibility = false;
            AddEmployeeControlVisibility = true;
            EditEmployeeControlVisibility = true;
            WageVisibility = false;
        }

        #endregion

    }
}
