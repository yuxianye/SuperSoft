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
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControlBase
    {
        public MainView()
        {
            InitializeComponent();
        }

        #region PatientSearch

        private void PatientSearchConditionContainTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PatientSearchButton.Focus();
            }
        }

        private void PatientSearchButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PatientSearchConditionContainTextBox.Focus();
            }
        }

        #endregion

        #region DoctorSearch

        private void DoctorSearchConditionContainTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorSearchButton.Focus();
            }
        }

        private void DoctorSearchButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorSearchConditionContainTextBox.Focus();
            }
        }

        #endregion

        //private void PatientSearchConditionContainTextBox2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    //if (e.Key == Key.Enter)
        //    //{
        //    //    PatientSearchButton2.Focus();
        //    //}
        //}

        //private void PatientSearchButton2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    //if (e.Key == Key.Enter)
        //    //{
        //    //    PatientSearchConditionContainTextBox2.Focus();
        //    //    // DoctorSearchButton1.IsChecked = true;
        //    //}
        //}

        //private void PatientSearchButton2_Click(object sender, RoutedEventArgs e)
        //{
        //    //PatientSearchConditionContainTextBox2.Focus();
        //    //  DoctorSearchButton1.IsChecked = true;
        //}

    }
}
