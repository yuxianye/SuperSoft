using Respircare.Utility;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Respircare.PatientManagementSystem.BLL
{
    public class PressureUnitValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var tmp = (int)value;
                if (tmp == 0)
                {
                    return ResourceHelper.LoadString("cmH2O");
                }
                if (tmp == 1)
                {
                    return ResourceHelper.LoadString("kPa");
                }
                if (tmp == 2)
                {
                    return ResourceHelper.LoadString("HPa");
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