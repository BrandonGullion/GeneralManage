using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardPage : BasePage
    {
        public DashboardPage(MainWindowViewModel vm)
        {
            SelectedPageAnimation = PageAnimationEnum.FadeIn;
            DataContext = new DashboardViewModel(this, vm);
            InitializeComponent();
        }
    }
}
