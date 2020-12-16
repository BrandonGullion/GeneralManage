
namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for MetricPage.xaml
    /// </summary>
    public partial class MetricPage : BasePage
    {
        public MetricPage(MainWindowViewModel mainWindowViewModel)
        {
            DataContext = new MetricsViewModel(mainWindowViewModel);
            InitializeComponent();
        }
    }
}
