using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Respircare.PatientManagementSystem.BLL
{
    /// <summary>
    /// Product Model Converter 
    /// </summary>
    public class ProductModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return value.CastTo(ProductModels.Unknown);
            }
            return ProductModels.Unknown;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}