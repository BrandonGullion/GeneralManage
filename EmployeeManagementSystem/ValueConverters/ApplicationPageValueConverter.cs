using EmployeeManagementSystem.Pages;
using EmployeeManagementSystem.Pages.Metric_Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ValueConverters
{
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                // Returns a login page
                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.WeeklyMetricPage:
                    return new WeeklyMetricPage();

                case ApplicationPage.BiWeeklyMetricPage:
                    return new BiWeeklyMetricPage();

                case ApplicationPage.MonthlyMetricPage:
                    return new MonthlyMetricPage();

                case ApplicationPage.EmployeeMetricPage:
                    return new EmployeeMetricPage();

                // Default if a value is not thrown 
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
