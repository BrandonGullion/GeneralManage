using EmployeeManagementSystem.Animations;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : BasePage
    {
        public Dashboard()
        {
            SelectedPageAnimation = PageAnimationEnum.FadeIn;
            DataContext = new DashboardViewModel();
            InitializeComponent();
        }
    }
}
