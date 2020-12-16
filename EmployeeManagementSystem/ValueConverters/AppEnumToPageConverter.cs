using ClassLibrary;
using EmployeeManagementSystem.Pages;
using EmployeeManagementSystem.Pages.Metric_Pages;
using EmployeeManagementSystem.Pages.SettingPages;

namespace EmployeeManagementSystem.ValueConverters
{
    /// <summary>
    /// 
    /// Converter that takes enum application pages and returns new pages depending on the enum selected
    /// The Main view model is always passed through so that the frame can be navigated accordingly 
    /// 
    /// </summary>

    public class AppEnumToPageConverter
    {
        #region Properties 

        // User That will be passed in when they log in 
        public UserModel CurrentUser { get; set; }

        #endregion

        #region Methods

        // Takes in the desired application page enum and 
        public static object ChangePage(object applicationPage, MainWindowViewModel mainWindowViewModel = null, UserModel userModel = null)
        {
            switch (applicationPage)
            {
                case ApplicationPage.Login:
                    return new LoginPage(mainWindowViewModel);

                case ApplicationPage.Dashboard:
                    return new DashboardPage(mainWindowViewModel);

                case ApplicationPage.EmployeePage:
                    return new EmployeePage(mainWindowViewModel);

                case ApplicationPage.SchedulePage:
                    return new SchedulePage(mainWindowViewModel);

                case ApplicationPage.VacationPage:
                    return new VacationPage(mainWindowViewModel);

                case ApplicationPage.WeeklyMetricPage:
                    return new WeeklyMetricPage();

                case ApplicationPage.BiWeeklyMetricPage:
                    return new BiWeeklyMetricPage();

                case ApplicationPage.MonthlyMetricPage:
                    return new MonthlyMetricPage();

                case ApplicationPage.EmployeeMetricPage:
                    return new MetricPage(mainWindowViewModel);

                case ApplicationPage.YearlyMetricPage:
                    return new YearlyMetricPage();

                case ApplicationPage.SettingsPage:
                    return new SettingsPage(mainWindowViewModel);

                case ApplicationPage.UserSettingsPage:
                    return new UserPage();

                // If value is null or not found return to dashboard 
                default:
                    return new DashboardPage(mainWindowViewModel);
            }
        }

        #endregion

    }
}
