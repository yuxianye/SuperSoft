using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using SuperSoft.View.ViewModel;
using System.Windows;
using System.Linq;
namespace SuperSoft.View.UserControls
{
    public class ProductInfoViewModel : MyViewModelBase
    {
        #region 当前选中的患者 SelectedPatient

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                Set(ref selectedPatient, value);

                initPatientProductInfo();
            }
        }

        #endregion

        #region 患者的产品信息


        /// <summary>
        /// 初始化患者产品信息
        /// </summary>
        private void initPatientProductInfo()
        {
            BLL.ViewPatientsProductBLL viewPatientProductsBLL = new BLL.ViewPatientsProductBLL();

            var tmp = viewPatientProductsBLL.SelectByPatientId(selectedPatient.Id);
            viewPatientProductsBLL.Dispose();
            viewPatientProductsBLL = null;
            if (tmp != null && tmp.Count > 0)
            {
                ViewPatientsProduct = tmp.FirstOrDefault();
                PatientsProductInfoVisibility = Visibility.Visible;
            }
            else
            {
                PatientsProductInfoVisibility = Visibility.Collapsed;
            }
        }

        private ViewPatientsProduct viewPatientsProduct;

        public ViewPatientsProduct ViewPatientsProduct
        {
            get { return viewPatientsProduct; }
            set { Set(ref viewPatientsProduct, value); }
        }

        private Visibility patientsProductInfoVisibility = Visibility.Collapsed;
        public Visibility PatientsProductInfoVisibility
        {
            get { return patientsProductInfoVisibility; }
            set { Set(ref patientsProductInfoVisibility, value); }
        }

        #endregion


        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            //viewPatientProductsBLL.Dispose();
            //viewPatientProductsBLL = null;
        }
    }
}
