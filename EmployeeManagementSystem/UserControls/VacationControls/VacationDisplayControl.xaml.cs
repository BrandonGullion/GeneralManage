using EmployeeManagementSystem.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagementSystem.UserControls.VacationControls
{
    /// <summary>
    /// Interaction logic for VacationDisplayControl.xaml
    /// </summary>
    public partial class VacationDisplayControl : BaseControl
    {
        public VacationDisplayControl()
        {
            SelectedAnimation = ControlAnimationEnum.SmallFadeIn;
            InitializeComponent();
            this.Loaded += VacationDisplayControl_Loaded;
        }

        private async void VacationDisplayControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
