using Respircare.Utility;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Respircare.PatientManagementSystem.BLL
{
    /// <summary>
    /// 0:关  X:m-n
    /// </summary>
    public class SwitchValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var tmp = (int)value;
                if (tmp == 0)
                {
                    return ResourceHelper.LoadString("SwitchClose");
                }
                return value;
            }
            return ResourceHelper.LoadString("Unknown");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}