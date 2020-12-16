

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : BasePage
    {
        public SettingsPage(MainWindowViewModel mainWindowViewModel)
        {
            DataContext = new SettingsPageViewModel(mainWindowViewModel);
            InitializeComponent();
        }
    }
}
