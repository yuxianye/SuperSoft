using System.Windows;

namespace SuperSoft.Utility.Windows
{
    public class ResourceHelper
    {
        public static string LoadString(string key)
        {
            return Application.Current.TryFindResource(key) as string;
        }

        public static object FindResource(string key)
        {
            return Application.Current.TryFindResource(key);
        }
    }
}
