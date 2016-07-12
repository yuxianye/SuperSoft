using GalaSoft.MvvmLight.Messaging;
using SuperSoft.View;
using SuperSoft.View.ViewModel;
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

namespace SuperSoft.View.View
{
    public partial class DoctorAddView : UserControlBase
    {
        public DoctorAddView()
        {
            InitializeComponent();
            txtbox_FirstName.Focus();
        }

        //public DoctorAddView(string sn)
        //{
        //    InitializeComponent();
        //    txtbox_FirstName.Focus();
        //    DataContext = new DoctorAddViewModel(sn);

        //}

        #region 释放资源

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }

        #endregion
    }
}
