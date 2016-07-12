using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Respircare.PatientManagementSystem.BLL
{
    public class DoctorIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid)
            {
                var doctorBLL = new DoctorBLL();
                var doctor = doctorBLL.GetById((Guid)value);
                doctorBLL.Dispose();
                if (doctor != null)
                {
                    return doctor.FirstName + " " + doctor.LastName;
                }
                return null;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}