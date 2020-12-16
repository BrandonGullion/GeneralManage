

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : BasePage
    {
        public SchedulePage(MainWindowViewModel mainWindowViewModel)
        {
            DataContext = new ScheduleViewModel(mainWindowViewModel);
            InitializeComponent();
        }
    }
}
