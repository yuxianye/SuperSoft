using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using SuperSoft.Model;
using SuperSoft.Utility;
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
        }

        private void DataGridAllPatientList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataContext = this.DataContext as PatientListViewModel;
            if (!Equals(dataContext, null))
            {
                try
                {
                    StaticDatas.CurrentOpenedPatient = dataContext.SelectedPatient;
                    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientHomeView, ViewType.View, StaticDatas.CurrentOpenedPatient), Model.MessengerToken.Navigate);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ToString(), ex);
                }
            }
        }
    }
}
