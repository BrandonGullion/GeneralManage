using EmployeeManagementSystem.ViewModels;
using System.Windows.Controls;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for VacationPage.xaml
    /// </summary>
    public partial class VacationPage : Page
    {
        public VacationPage(MainWindowViewModel mainWindowViewModel)
        {
            DataContext = new VacationViewModel(mainWindowViewModel);
            InitializeComponent();
        }
    }
}
