using EmployeeManagementSystem.Animations;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : BasePage
    {
        public Dashboard()
        {
            SelectedPageAnimation = PageAnimationEnum.FadeIn;
            DataContext = new DashboardViewModel();
            InitializeComponent();
        }

        private void DashboardControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private async void DashboardControl_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedPageAnimation = PageAnimationEnum.FadeOut;
            await Animate();

            await Task.Delay(700);
            MainWindow.mainWindow.MainContentFrame.Content = new EmployeePage();
        }

        private void DashboardControl_MouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DashboardControl_MouseDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DashboardControl_MouseDown_4(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DashboardControl_MouseDown_5(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
