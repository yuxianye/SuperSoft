using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SuperSoft.BLL
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if (value.GetBool())
                {
                    return ResourceHelper.LoadString("Male");
                }
                return ResourceHelper.LoadString("Female");
            }
            return ResourceHelper.LoadString("Female");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}