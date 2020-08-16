using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// A Base Value Converter that allows for direct xaml usage 
    /// </summary>
    /// <typeparam name="T">The type of this value converter</typeparam>

    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        // A single static instance of this value converter 
        private static T converter = null;

        // This will try to return an instance of the converter type, if it is not 
        // Currently there it will create a new one 
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new T()); 
        }

        #region Value Converter Methods 
        /// <summary>
        /// The method to convert one type to another 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// The method to convert back 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);


        #endregion
    }
}
