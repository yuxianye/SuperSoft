using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
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
    public class PatientAddViewModel : MyViewModelBase
    {
        public PatientAddViewModel()
        {
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        #region Patient

        private Model.Patient patient = new Model.Patient();
        public Model.Patient Patient
        {
            get { return patient; }
            set { Set(ref patient, value); }
        }

        #endregion

        private string fileName;

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            fileName = Parameter.ToString();
        }

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            //try
            //{
            //    Patient.FileName = Patient.Number;
            //    string fileNameTmp = Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionPatient;
            //    if (File.Exists(fileNameTmp))
            //    {//存在同名文件，
            //        //询问是否覆盖
            //        if (MessageBox.Show(ResourceHelper.LoadString(@"FileExistsMessage"), ResourceHelper.LoadString(@"AddPatient"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //        {
            //            //直接覆盖
            //            using (var memoryStream = new MemoryStream())
            //            {
            //                ProtoBuf.Serializer.Serialize(memoryStream, Patient);
            //                System.IO.File.WriteAllBytes(fileNameTmp, memoryStream.ToArray());
            //                System.IO.File.Copy(fileName, Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionData, true);
            //                memoryStream.Close();
            //            }
            //        }
            //        else
            //        {
            //            //不覆盖 则返回
            //            return;
            //        }
            //    }
            //    else
            //    {//不存在直接创建
            //        using (var memoryStream = new MemoryStream())
            //        {
            //            ProtoBuf.Serializer.Serialize(memoryStream, Patient);
            //            System.IO.File.WriteAllBytes(fileNameTmp, memoryStream.ToArray());
            //            System.IO.File.Copy(fileName, Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionData, true);
            //            memoryStream.Close();
            //        }
            //    }
            //    StaticDatas.CurrentOpenedPatient = Patient;
            //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            //    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientHomeView, ViewType.View, Patient), Model.MessengerToken.Navigate);
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
