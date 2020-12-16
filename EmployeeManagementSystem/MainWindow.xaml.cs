using System.Windows;
using System.Windows.Interactivity;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow { get; set; }

        public MainWindow()
        {
            mainWindow = this;
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }
    }
}
