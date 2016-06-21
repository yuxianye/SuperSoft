using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SuperSoft.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //Title = Utility.ResourceHelper.LoadString("AppName")
            //+ " "
            //+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major
            //+ "."
            //+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;

            Title = Utility.Windows.ResourceHelper.LoadString(@"AppName")
            + @" "
            + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            mainView.Dispose();
            mainView = null;

        }
    }
}
