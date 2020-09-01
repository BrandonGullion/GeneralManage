using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManagementSystem.UserControls.EmployeePageControls
{
    /// <summary>
    /// Interaction logic for EmployeeTabMenuControl.xaml
    /// </summary>
    public partial class EmployeeTabMenuControl : BaseControl
    {
        public EmployeeTabMenuControl()
        {
            SelectedAnimation = ControlAnimationEnum.SlideControlFromRight;
            InitializeComponent();
            this.Loaded += EmployeeTabMenuControl_Loaded;
        }

        private async void EmployeeTabMenuControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
