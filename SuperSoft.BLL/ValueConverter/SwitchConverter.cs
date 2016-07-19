using SuperSoft.Utility.Windows;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SuperSoft.BLL
{
    /// <summary>
    /// 0:关 1:开
    /// </summary>
    public class SwitchConverter : IValueConverter
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
                if (tmp == 1)
                {
                    return ResourceHelper.LoadString("SwitchOpen");
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