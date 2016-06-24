using SuperSoft.Model;
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
    /// PatientListView.xaml 的交互逻辑
    /// </summary>
    public partial class PatientListView : UserControlBase
    {
        public PatientListView()
        {
            InitializeComponent();
            DataContext = new PatientListViewModel();
        }

        /// <summary>
        /// 搜索条件的构造
        /// </summary>
        /// <param name="condition"></param>
        public PatientListView(Expression<Func<Patient, bool>> condition)
        {
            InitializeComponent();
            //DataContext = new PatientListViewModel(condition);
        }

        private void DataGridAllPatientList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //var dataContext = this.DataContext as PatientListViewModel;
            //if (!Equals(dataContext, null) && StaticDatas.IsCurrentSelectedPatientHaveProduct)
            //{
            //    dataContext.ViewManagement.Navigate(ViewNames.PatientHomeView);
            //}
            //else
            //{
            //    MessageBox.Show(Application.Current.MainWindow, ResourceHelper.LoadString("NoProductData"),
            //        dataContext.SelectedPatient.FirstName + " " + dataContext.SelectedPatient.LastName,
            //        MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}
