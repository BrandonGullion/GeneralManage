using System.Windows;
using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeeInformationSideMenuControl.xaml
    /// </summary>
    public partial class EmployeeInformationSideMenuControl: BaseControl
    {
       
        public EmployeeInformationSideMenuControl()
        {
            SelectedAnimation = ControlAnimationEnum.SlideControlFromRight; 
            this.Loaded += EmployeeInformationSideMenuControl_Loaded;
            InitializeComponent();
        }

        private async void EmployeeInformationSideMenuControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
