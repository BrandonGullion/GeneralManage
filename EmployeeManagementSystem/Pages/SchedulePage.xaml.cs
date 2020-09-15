

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : BasePage
    {
        public SchedulePage()
        {
            DataContext = new ScheduleViewModel();
            InitializeComponent();
        }
    }
}
