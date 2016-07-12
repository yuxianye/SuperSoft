using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Linq;
namespace SuperSoft.View.ViewModel
{
    public class PatientEditViewModel : MyViewModelBase
    {
        public PatientEditViewModel()
        {
            base.IsParameterRepeatChanged = true;
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            this.Patient = StaticDatas.CurrentSelectedPatient.Clone() as Patient;
            initPatientProductSn();
        }

        #region Patient

        private Patient patient;

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
            var patientBLL = new PatientBLL();
            try
            {
                patientBLL.Update(Patient);
                StaticDatas.CurrentSelectedPatient = Patient;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                MessageBox.Show(Application.Current.MainWindow, ex.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (patientBLL != null)
                {
                    patientBLL.Dispose();
                    patientBLL = null;
                }
                clearData();
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

        private void initPatientProductSn()
        {
            if (!Equals(Patient, null))
            {
                using (var viewPatientProductsBLL = new ViewPatientsProductBLL())
                {
                    var v = viewPatientProductsBLL.SelectByPatientId(Patient.Id);
                    if (v != null && v.Count > 0)
                    {
                        Patient.SerialNumber = v.FirstOrDefault().SerialNumber;
                    }
                }
            }
        }

        /// <summary>
        /// ViewModel有缓存，执行之后清空
        /// </summary>
        private void clearData()
        {
            this.Patient = new Model.Patient();
        }

    }
}
