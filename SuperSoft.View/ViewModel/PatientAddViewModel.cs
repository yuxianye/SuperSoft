using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SuperSoft.View.ViewModel
{
    public class PatientAddViewModel : MyViewModelBase
    {
        public PatientAddViewModel()
        {
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
            DoctorList = initDoctorList();
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            string sn = base.Parameter as string;
            IsEnabledSerialNumbeControl = false;
            Patient.SerialNumber = sn;
        }

        #region IsEnabledSerialNumbeControl

        private bool isEnabledSerialNumbeControl = true;

        public bool IsEnabledSerialNumbeControl
        {
            get { return isEnabledSerialNumbeControl; }
            set { Set(ref isEnabledSerialNumbeControl, value); }
        }

        #endregion

        #region Patient

        private Patient patient = new Patient();

        public Patient Patient
        {
            get { return patient; }
            set { Set(ref patient, value); }
        }

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            PatientBLL patientBLL = new PatientBLL();
            ProductBLL productBLL = new ProductBLL();
            PatientsProductBLL patientsProductBLL = new PatientsProductBLL();
            try
            {
                Patient.Id = DateTime.Now.ToGuid();

                var product = new Product();
                product.Id = DateTime.Now.ToGuid();
                product.SerialNumber = Patient.SerialNumber;

                var patientsProduct = new PatientsProduct();
                patientsProduct.Id = DateTime.Now.ToGuid();
                patientsProduct.PatientId = Patient.Id;
                patientsProduct.ProductId = product.Id;

                //使用事物提交
                using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString))
                {
                    conn.Open();
                    using (System.Data.SQLite.SQLiteTransaction tran = conn.BeginTransaction())
                    {
                        productBLL.Insert(tran, product);
                        patientBLL.Insert(tran, Patient);
                        patientsProductBLL.Insert(tran, patientsProduct);
                        tran.Commit();
                    }
                    conn.Close();
                }
                StaticDatas.CurrentSelectedPatient = Patient;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                MessageBox.Show(Application.Current.MainWindow, ex.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                clearData();

                if (!Equals(patientBLL, null))
                {
                    patientBLL.Dispose();
                    patientBLL = null;
                }
                if (!Equals(productBLL, null))
                {
                    productBLL.Dispose();
                    productBLL = null;
                }
                if (!Equals(patientsProductBLL, null))
                {
                    patientsProductBLL.Dispose();
                    patientsProductBLL = null;
                }
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);
            }
        }

        private Regex regEMail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        private Regex regSerialNumber = new Regex(@"^\d{18}$");

        private bool OnCanExecuteConfirmCommand()
        {
            if (!Equals(Patient, null)
                && !string.IsNullOrWhiteSpace(Patient.FirstName)
                && !string.IsNullOrWhiteSpace(Patient.LastName)
                && Patient.Weight >= 0 && Patient.Weight <= 255
                && Patient.Height >= 0 && Patient.Height <= 255
                && (string.IsNullOrWhiteSpace(Patient.EMail) || regEMail.IsMatch(Patient.EMail))
                && (regSerialNumber.IsMatch(Patient.SerialNumber ?? string.Empty))
                 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CancelCommand

        public ICommand CancelCommand { get; private set; }
        private void OnExecuteCancelCommand()
        {
            clearData();
            Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        }

        #endregion

        /// <summary>
        /// ViewModel有缓存，执行之后清空
        /// </summary>
        private void clearData()
        {
            this.Patient = new Patient();
            IsEnabledSerialNumbeControl = true;
        }

        #region DoctorList SelectedDoctor

        private Dictionary<Guid, string> doctorList;

        public Dictionary<Guid, string> DoctorList
        {
            get { return doctorList; }
            set { Set(ref doctorList, value); }
        }

        /// <summary>
        /// 初始化医生列表
        /// </summary>
        /// <returns></returns>
        private Dictionary<Guid, string> initDoctorList()
        {
            var dictionaryList = new Dictionary<Guid, string>();
            int count;
            using (var doctorBLL = new DoctorBLL())
            {
                var result = doctorBLL.SelectPaging(1, short.MaxValue, out count);
                if (result != null && result.Count > 0)
                {
                    foreach (var v in result)
                    {
                        dictionaryList.Add(v.Id, v.FirstName + " " + v.LastName);
                    }
                }
            }
            return dictionaryList;
        }

        #endregion

    }
}
