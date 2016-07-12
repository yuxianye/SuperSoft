using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace SuperSoft.View.ViewModel
{
    public class PatientDeleteViewModel : MyViewModelBase
    {
        public PatientDeleteViewModel()
        {
            base.IsParameterRepeatChanged = true;
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            this.Patient = StaticDatas.CurrentSelectedPatient;
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
            PatientBLL patientBLL = new PatientBLL();

            try
            {
                //删除患者所有信息包括产品运行信息等数据
                patientBLL.Delete(Patient.Id);

                StaticDatas.CurrentSelectedPatient = null;
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
