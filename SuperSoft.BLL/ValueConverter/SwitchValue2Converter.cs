using SuperSoft.Utility.Windows;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SuperSoft.BLL
{
    /// <summary>
    /// 0:自动  X:m-n
    /// </summary>
    public class SwitchValue2Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var tmp = (int)value;
                if (tmp == 0)
                {
                    return ResourceHelper.LoadString("SwitchAuto");
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