using Respircare.Utility;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Respircare.PatientManagementSystem.BLL
{
    public class BacklightValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var tmp = (int)value;
                if (tmp == 0)
                {
                    return ResourceHelper.LoadString("Bright");
                }
                if (tmp == 1)
                {
                    return ResourceHelper.LoadString("Darken");
                }
                return ResourceHelper.LoadString("Unknown");
            }
            return ResourceHelper.LoadString("Unknown");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}