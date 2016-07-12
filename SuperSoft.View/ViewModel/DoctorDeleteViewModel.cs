using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace SuperSoft.View.ViewModel
{
    public class DoctorDeleteViewModel : MyViewModelBase
    {
        public DoctorDeleteViewModel()
        {
            base.IsParameterRepeatChanged = true;
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            this.Doctor = StaticDatas.CurrentSelectedDoctor;
        }

        #region Doctor

        private Doctor doctor;

        public Doctor Doctor
        {
            get { return doctor; }
            set { Set(ref doctor, value); }
        }

        #endregion

        #region DoctorDeleteMessageVisibility

        private Visibility doctorDeleteMessageVisibility = Visibility.Collapsed;

        public Visibility DoctorDeleteMessageVisibility
        {
            get { return doctorDeleteMessageVisibility; }
            set { Set(ref doctorDeleteMessageVisibility, value); }
        }

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            var doctorBLL = new DoctorBLL();
            try
            {
                doctorBLL.Delete(Doctor.Id);
                StaticDatas.CurrentSelectedDoctor = null;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                MessageBox.Show(Application.Current.MainWindow, ex.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (doctorBLL != null)
                {
                    doctorBLL.Dispose();
                    doctorBLL = null;
                }
                clearData();
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorListView, ViewType.View), Model.MessengerToken.Navigate);
            }
        }

        private PatientBLL patientBLL = new PatientBLL();

        private bool OnCanExecuteConfirmCommand()
        {
            if (Doctor == null || Doctor.Id == Guid.Empty)
            {
                return false;
            }

            var v = patientBLL.SelectByDoctorId(Doctor.Id);

            if (v == null)
            {
                DoctorDeleteMessageVisibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                DoctorDeleteMessageVisibility = Visibility.Visible;
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
            this.Doctor = new Model.Doctor();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            patientBLL.Dispose();
            patientBLL = null;
        }

    }
}
