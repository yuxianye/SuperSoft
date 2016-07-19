using System.Windows;
using System.Windows.Media;

namespace SuperSoft.Utility.Windows
{
    public class MyDependencyObject
    {
        public static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }
    }
}
