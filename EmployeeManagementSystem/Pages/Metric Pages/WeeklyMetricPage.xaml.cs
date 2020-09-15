using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Pages.Metric_Pages
{
    /// <summary>
    /// Interaction logic for WeeklyMetricPage.xaml
    /// </summary>
    public partial class WeeklyMetricPage : BasePage
    {
        public WeeklyMetricPage()
        {
            SelectedPageAnimation = PageAnimationEnum.FadeIn;
            DataContext = new WeeklyMetricViewModel();
            InitializeComponent();
            StartAnimation();
        }

        public async void StartAnimation()
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
