using GalaSoft.MvvmLight.Messaging;
using SuperSoft.View;
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
    public partial class SwitchLanguageView : UserControlBase
    {
        public SwitchLanguageView()
        {
            InitializeComponent();
        }

        #region 释放资源

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }

        #endregion
    }
}
