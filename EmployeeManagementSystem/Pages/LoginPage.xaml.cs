using ClassLibrary;
using EmployeeManagementSystem.Animations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            SelectedPageAnimation = PageAnimationEnum.SlideFromRight;
            DataContext = new LoginPageViewModel(this);
            InitializeComponent();
        }

        // This check has to be done in the code behind due to password box
        public async void Login_Click(object sender, RoutedEventArgs e)
        {

            if (DataBaseHelper.ValidateUser(UserNameBox.Text, PasswordBox.Password))
            {
                // Sets the page animation
                SelectedPageAnimation = PageAnimationEnum.SlideToLeft;

                // Does the animations 
                await Animate();

                // Delay to allow for the animation to finish 
                await Task.Delay(500);

                // Set the new frame content 
                MainWindow.mainWindow.MainContentFrame.Content = new Dashboard();
            }
            // TODO: Create a new window or pop up that will be initiazlied 
            else
                MessageBox.Show("Incorrect Username / password");
        }
    }
}
