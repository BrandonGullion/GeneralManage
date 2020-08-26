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

namespace EmployeeManagementSystem.UserControls.Scheduler
{
    /// <summary>
    /// Interaction logic for SchedulerControl.xaml
    /// </summary>
    public partial class SchedulerControl : BaseControl
    {
        public SchedulerControl()
        {
            SelectedAnimation = ControlAnimationEnum.SmallFadeIn;
            this.Loaded += SchedulerControl_Loaded;
            InitializeComponent();
        }

        private async void SchedulerControl_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
