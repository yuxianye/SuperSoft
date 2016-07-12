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
    public class DoctorAddViewModel : MyViewModelBase
    {
        public DoctorAddViewModel()
        {
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        #region Doctor

        private Doctor doctor = new Doctor();

        public Doctor Doctor
        {
            get { return doctor; }
            set { Set(ref doctor, value); }
        }

        #endregion


        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            var doctorBLL = new DoctorBLL();
            try
            {
                Doctor.Id = DateTime.Now.ToGuid();
                doctorBLL.Insert(Doctor);
                StaticDatas.CurrentSelectedDoctor = Doctor;
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

        private bool OnCanExecuteConfirmCommand()
        {
            if (!Equals(Doctor, null) && !string.IsNullOrWhiteSpace(Doctor.FirstName) && !string.IsNullOrWhiteSpace(Doctor.LastName))
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
            this.Doctor = new Model.Doctor();
        }

    }
}
