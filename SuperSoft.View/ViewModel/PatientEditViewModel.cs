using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SuperSoft.View.ViewModel
{
    public class PatientEditViewModel : MyViewModelBase
    {
        public PatientEditViewModel()
        {
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        #region Patient

        private Model.Patient patient;
        public Model.Patient Patient
        {
            get { return patient; }
            set { Set(ref patient, value); }
        }

        #endregion


        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            // Patient = (Parameter as Model.Patient).Clone() as Model.Patient;
        }

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            //try
            //{
            //    string fileNameTmp = Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionPatient;

            //    //直接覆盖
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        ProtoBuf.Serializer.Serialize(memoryStream, Patient);
            //        System.IO.File.WriteAllBytes(fileNameTmp, memoryStream.ToArray());
            //        memoryStream.Close();
            //    }
            //    StaticDatas.CurrentOpenedPatient = Patient;
            //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.Error(ToString(), ex);
            //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            //}
            //finally
            //{
            //    clearData();
            //}
        }

        private bool OnCanExecuteConfirmCommand()
        {
            //if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;

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
            this.Patient = new Model.Patient();
        }

    }
}
