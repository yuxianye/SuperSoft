using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

            test();
        }

        //public PatientAddViewModel(string sn)
        //{
        //    ConfirmCommand = new RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
        //    CancelCommand = new RelayCommand(OnExecuteCancelCommand);
        //    DoctorList = initDoctorList();
        //    // PatientModel.PatientProductSn = sn;
        //    NotHaveCondition = false;
        //}


        //#region CurrentDate

        //private DateTime currentDate = DateTime.Now;

        //public DateTime CurrentDate
        //{
        //    get { return currentDate; }
        //    set { Set(ref currentDate, value); }
        //}

        //#endregion

        //private bool notHaveCondition = true;

        //public bool NotHaveCondition
        //{
        //    get { return notHaveCondition; }
        //    set { Set(ref notHaveCondition, value); }
        //}

        #region PatientModel

        private PatientModel patientModel = new PatientModel();

        public PatientModel PatientModel
        {
            get { return patientModel; }
            set { Set(ref patientModel, value); }
        }

        #endregion

        #region ProductModel

        private Product productModel = new Product();

        public Product ProductModel
        {
            get { return productModel; }
            set { Set(ref productModel, value); }
        }

        #endregion


        //#region Patient

        //private Model.Patient patient = new Model.Patient();
        //public Model.Patient Patient
        //{
        //    get { return patient; }
        //    set { Set(ref patient, value); }
        //}

        //#endregion

        //private string fileName;

        //protected override void OnParameterChanged()
        //{
        //    base.OnParameterChanged();
        //    // fileName = Parameter.ToString();
        //}


        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            PatientBLL patientBLL = null;
            ProductBLL productBLL = null;
            PatientsProductBLL patientsProductBLL = null;
            try
            {
                var patient = new Patient();
                patient.Id = DateTime.Now.ToGuid();
                patient.FirstName = PatientModel.FirstName;
                patient.LastName = PatientModel.LastName;
                //patient.DateOfBirth = PatientModel.DateOfBirth;
                patient.DateOfBirth = DateTime.Now;
                patient.Weight = PatientModel.Weight;
                patient.Height = PatientModel.Height;
                patient.Gender = PatientModel.Gender;
                patient.Photo = PatientModel.Photo = new byte[] { 12, 12, 12, 12, 12, 12, 12 };
                patient.EMail = PatientModel.EMail;
                patient.TelephoneNumbers = PatientModel.TelephoneNumbers;
                patient.PostalCode = PatientModel.PostalCode;
                patient.Address = PatientModel.Address;
                patient.Diagnosis = PatientModel.Diagnosis;
                patient.PostalCode = PatientModel.PostalCode;
                patient.DoctorId = PatientModel.DoctorId;
                patientBLL = new PatientBLL();
                patientBLL.Insert(patient);

                var count = patientBLL.Count();




                var product = new Product();
                product.Id = DateTime.Now.ToGuid();
                product.SerialNumber = PatientModel.SerialNumber;
                productBLL = new ProductBLL();
                productBLL.Insert(product);

                var patientsProduct = new PatientsProduct();
                patientsProduct.Id = DateTime.Now.ToGuid();
                patientsProduct.PatientId = patient.Id;
                patientsProduct.ProductId = product.Id;
                patientsProductBLL = new PatientsProductBLL();
                patientsProductBLL.Insert(patientsProduct);

                //product.Dispose();
                //product = null;
                var vv = patientBLL.GetById(patient.Id);
                //patientsProduct.Dispose();
                //patientsProduct = null;
                StaticDatas.CurrentSelectedPatient = patient;

            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                MessageBox.Show(Application.Current.MainWindow, ex.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (!Equals(patientBLL, null))
                {
                    patientBLL.Dispose();
                    patientBLL = null;
                }
                //if (!Equals(productBLL, null))
                //{
                //    productBLL.Dispose();
                //    productBLL = null;
                //}
                //if (!Equals(patientsProductBLL, null))
                //{
                //    patientsProductBLL.Dispose();
                //    patientsProductBLL = null;
                //}
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            }
        }

        private bool OnCanExecuteConfirmCommand()
        {
            if (!Equals(PatientModel, null)
                && !string.IsNullOrWhiteSpace(PatientModel.FirstName)
                && !string.IsNullOrWhiteSpace(PatientModel.LastName)
                && !Equals(ProductModel, null)
                && !string.IsNullOrWhiteSpace(ProductModel.SerialNumber)
                && ProductModel.SerialNumber.Length == 18)
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
            // this.Patient = new Model.Patient();
        }

        #region DoctorList SelectedDoctor initDoctorList

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
            var doctorBLL = new DoctorBLL();
            Expression<Func<Doctor, bool>> condition = t => t.FirstName != null;
            //var result = doctorBLL.GetByCondition(condition);
            //doctorBLL.Dispose();
            //doctorBLL = null;
            var dictionaryList = new Dictionary<Guid, string>();
            //foreach (var v in result)
            //{
            //    dictionaryList.Add(v.Id, v.FirstName + " " + v.LastName);
            //}
            return dictionaryList;
        }

        #endregion DoctorList SelectedDoctor




        private void test()
        {
            var patient = new Patient();
            patient.Id = DateTime.Now.ToGuid();
            patient.FirstName = "FirstName";
            patient.LastName = "LastName";
            patient.DateOfBirth = DateTime.Now;
            patient.Weight = 65;
            patient.Height = 175;
            patient.Gender = true;
            //patient.Photo = new byte[] { 12, 12, 12, 12, 12, 12, 12 };
            patient.EMail = "test@163.com";
            patient.TelephoneNumbers = "13800138000";
            patient.PostalCode = "110024";
            patient.Address = "沈阳市浑南新区创新路153-5号U11（迈思医疗）";
            patient.Diagnosis = "COPD";
            patient.DoctorId = Guid.Empty;
            PatientBLL patientBLL = new PatientBLL();
            patientBLL.Insert(patient);

            var p = patientBLL.GetById(patient.Id);
            if (p.FirstName == patient.FirstName
                 && p.LastName == patient.LastName
                 && p.DateOfBirth == patient.DateOfBirth
                 && p.Weight == patient.Weight
                 && p.Height == patient.Height
                 && p.Gender == patient.Gender
                 && p.Photo == patient.Photo//引用类型
                 && p.EMail == patient.EMail
                 && p.TelephoneNumbers == patient.TelephoneNumbers
                 && p.PostalCode == patient.PostalCode
                 && p.Address == patient.Address
                 && p.Diagnosis == patient.Diagnosis
                 && p.DoctorId == patient.DoctorId
                 )
            {
                LogHelper.Info(" patientBLL.Insert Success");
            }
            else
            {
                LogHelper.Info(" patientBLL.Insert Fail");
            }




            //patientBLL.Insert ()



        }



    }
}
