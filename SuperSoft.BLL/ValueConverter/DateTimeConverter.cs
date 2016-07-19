using SuperSoft.Utility.Windows;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SuperSoft.BLL
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long)
            {
                var ts = TimeSpan.FromMilliseconds((long)value);
                if (ts.Days < 1)
                {
                    if (ts.Hours < 1)
                    {
                        if (ts.Minutes < 1)
                        {
                            return string.Format(ResourceHelper.LoadString("TimeSpanFromatSecond"), ts.Seconds);
                        }
                        return string.Format(ResourceHelper.LoadString("TimeSpanFromatMinute"), ts.Minutes, ts.Seconds);
                    }
                    return string.Format(ResourceHelper.LoadString("TimeSpanFromatHour"), ts.Hours, ts.Minutes,
                        ts.Seconds);
                }
                return string.Format(ResourceHelper.LoadString("TimeSpanFromatFull"), ts.Days, ts.Hours, ts.Minutes,
                    ts.Seconds);
            }
            if (value is int)
            {
                var ts = TimeSpan.FromMilliseconds((int)value);
                if (ts.Days < 1)
                {
                    if (ts.Hours < 1)
                    {
                        if (ts.Minutes < 1)
                        {
                            return string.Format(ResourceHelper.LoadString("TimeSpanFromatSecond"), ts.Seconds);
                        }
                        return string.Format(ResourceHelper.LoadString("TimeSpanFromatMinute"), ts.Minutes, ts.Seconds);
                    }
                    return string.Format(ResourceHelper.LoadString("TimeSpanFromatHour"), ts.Hours, ts.Minutes,
                        ts.Seconds);
                }
                return string.Format(ResourceHelper.LoadString("TimeSpanFromatFull"), ts.Days, ts.Hours, ts.Minutes,
                    ts.Seconds);
            }
            return ResourceHelper.LoadString("Unknown");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}