using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagementSystem.UserControls.VacationControls
{
    /// <summary>
    /// Interaction logic for EmployeeSideMenu.xaml
    /// </summary>
    public partial class VacationSideMenu : BaseControl
    {
        public VacationSideMenu()
        {
            SelectedAnimation = ControlAnimationEnum.SlideControlFromLeft;
            Loaded += VacationSideMenu_Loaded;
            InitializeComponent();
        }

        private async void VacationSideMenu_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
