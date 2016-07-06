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
    public partial class PatientAddView : UserControlBase
    {
        public PatientAddView()
        {
            InitializeComponent();
            txtbox_FirstName.Focus();
            weightTextBox.Text = "0";
            heightTextBox.Text = "0";

        }

        public PatientAddView(string sn)
        {
            InitializeComponent();
            txtbox_FirstName.Focus();
            //DataContext = new PatientAddViewModel(sn);
            weightTextBox.Text = "0";
            heightTextBox.Text = "0";
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)))
            {
                if (e.Key != Key.Tab)
                {
                    e.Handled = true;
                }
            }
        }

        private void weightTextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(weightTextBox.Text))
            {
                weightTextBox.Text = "0";
            }
        }

        private void heightTextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(heightTextBox.Text))
            {
                heightTextBox.Text = "0";
            }
        }

        #region 释放资源

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }

        #endregion
    }
}
