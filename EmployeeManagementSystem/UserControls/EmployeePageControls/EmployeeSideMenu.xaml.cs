using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeSideMenu.xaml
    /// </summary>
    public partial class EmployeeSideMenu : BaseControl
    {
        public EmployeeSideMenu()
        {
            SelectedAnimation = ControlAnimationEnum.SlideControlFromLeft;
            Loaded += EmployeeSideMenu_Loaded;
            InitializeComponent();
        }

        private async void EmployeeSideMenu_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
