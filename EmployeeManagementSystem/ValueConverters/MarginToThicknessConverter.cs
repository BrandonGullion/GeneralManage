using ClassLibrary;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeManagementSystem
{
    public class MarginToThicknessConverter : BaseValueConverter<MarginToThicknessConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intValue = (int)value;
            return new Thickness((double)intValue, 0, 0, 0);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
