using Microsoft.Practices.ServiceLocation;
using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using SuperSoft.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace SuperSoft.View.View
{
    /// <summary>
    /// PatientHomeView.xaml 的交互逻辑
    /// </summary>
    public partial class PatientHomeView : UserControlBase
    {
        public PatientHomeView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 树形右键选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem =
                MyDependencyObject.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //System.Diagnostics.Debug.Print("TreeView_SelectedItemChanged" + e.NewValue);
            //dataContext.TreeView_SelectedItemChanged(sender, e);

        }
    }
}
