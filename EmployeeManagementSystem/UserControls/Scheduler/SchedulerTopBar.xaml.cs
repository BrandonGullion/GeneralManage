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
    /// Interaction logic for SchedulerTopBar.xaml
    /// </summary>
    public partial class SchedulerTopBar : BaseControl
    {
        public SchedulerTopBar()
        {
            SelectedAnimation = ControlAnimationEnum.SmallFadeIn;
            this.Loaded += SchedulerTopBar_Loaded;
            InitializeComponent();
        }

        private async void SchedulerTopBar_Loaded(object sender, RoutedEventArgs e)
        {
            await Animate();
            await Task.Delay(1000);
        }
    }
}
