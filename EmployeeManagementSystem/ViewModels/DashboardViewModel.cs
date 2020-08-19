using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EmployeeManagementSystem
{

    public class DashboardViewModel
    {
        public RelayCommand OpenCommand { get; set; }

        public DashboardViewModel()
        {
            OpenCommand = new RelayCommand(() => Open());
        }

        public void Open()
        {

        }
    }
}
