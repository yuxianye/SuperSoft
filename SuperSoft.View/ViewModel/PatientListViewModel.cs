using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SuperSoft.View.ViewModel
{
    public class PatientListViewModel : MyViewModelBase
    {
        public PatientListViewModel()
        {
            OpenCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteOpenCommand, OnCanExecuteOpenCommand);
            EditCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteEditCommand, OnCanExecuteEditCommand);
            DeleteCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteDeleteCommand, OnCanExecuteDeleteCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);

            BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            Model.Patient patient = new Model.Patient();
            patient.Id = System.Guid.NewGuid();
            // patient.Name = "FirstName";
            patient.FirstName = "FirstName";
            patient.LastName = "LastName";
            patientBLL.Insert(patient);
            // patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            string a = patientBLL.Count() + "ChangeTitleHello MvvmLight";





        }

        #region 取得患者列表

        public void initPatientList()
        {
            //var taskResult = getPatientListAsync();
            //if (!Equals(PatientList, null))
            //{
            //    PatientList.Clear();
            //}

            //PatientList = taskResult.Result;
            //taskResult.Dispose();
            //taskResult = null;

            //if (PatientList != null && PatientList.Count() > 0)//有患者
            //{
            //    if (StaticDatas.CurrentOpenedPatient == null) //默认选择第一个
            //    {
            //        Patient = PatientList.FirstOrDefault();
            //    }
            //    else//选择原有的患者
            //    {
            //        var v = PatientList.Where(a => a.Number == StaticDatas.CurrentOpenedPatient.Number);
            //        if (v != null && v.Count() > 0)
            //        {
            //            Patient = v.FirstOrDefault();
            //        }
            //        else
            //        {
            //            Patient = PatientList.FirstOrDefault();
            //        }
            //    }
            //}
            //else
            //{
            //    Patient = null;
            //}
        }

        private Task<ObservableCollection<Model.Patient>> getPatientListAsync()
        {
            // return Task.Run<ObservableCollection<Model.Patient>>(new Func<ObservableCollection<Model.Patient>>(getPatientList));
            return Task.Run<ObservableCollection<Model.Patient>>(new Func<ObservableCollection<Model.Patient>>(() =>
            {
                var directoryInfo = new DirectoryInfo(Const.AppDatabasePath);
                //得到所有详细数据文件的列表,搜索所有 xxxxxx.dat 的文件和文件件
                var searchedFileInfos = directoryInfo.GetFileSystemInfos(Const.RMSFileExtensionPatientSearch,
                    SearchOption.TopDirectoryOnly);
                directoryInfo = null;
                ObservableCollection<Model.Patient> result = new ObservableCollection<Patient>();

                foreach (var fileInfo in searchedFileInfos.OrderByDescending(a => a.CreationTime))
                {
                    if (fileInfo.Attributes != FileAttributes.Directory)
                    {
                        using (var memoryStream = new MemoryStream(File.ReadAllBytes(fileInfo.FullName)))
                        {
                            //Patient p = Serializer.Deserialize<Model.Patient>(memoryStream);
                            Patient p = new Patient();// Serializer.Deserialize<Model.Patient>(memoryStream);
                            result.Add(p);
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                }
                return result;
            }));

        }

        #endregion

        #region PatientList

        private ObservableCollection<Model.Patient> patientList;

        public ObservableCollection<Model.Patient> PatientList
        {
            get { return patientList; }
            set { Set(ref patientList, value); }
        }

        #endregion

        #region Patient

        private Model.Patient patient = new Model.Patient();

        public Model.Patient Patient
        {
            get { return patient; }
            set { Set(ref patient, value); }
        }

        #endregion

        #region OpenCommand

        public ICommand OpenCommand { get; private set; }

        private void OnExecuteOpenCommand()
        {
            try
            {
                StaticDatas.CurrentOpenedPatient = Patient;
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientHomeView, ViewType.View, Patient), Model.MessengerToken.Navigate);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            }
        }

        private bool OnCanExecuteOpenCommand()
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

        #region EditCommand

        public ICommand EditCommand { get; private set; }

        private void OnExecuteEditCommand()
        {
            try
            {
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientEditView, ViewType.Popup, Patient), Model.MessengerToken.Navigate);
                //编辑之后重新加载
                initPatientList();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            }
        }

        private bool OnCanExecuteEditCommand()
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

        #region DeleteCommand

        public ICommand DeleteCommand { get; private set; }

        private void OnExecuteDeleteCommand()
        {
            //try
            //{
            //    if (MessageBox.Show(ResourceHelper.LoadString(@"DeleteConfirm"), ResourceHelper.LoadString(@"CaseLibrary"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
            //        {
            //            if (!Equals(StaticDatas.CurrentOpenedPatient, null) && Equals(StaticDatas.CurrentOpenedPatient.Number, Patient.Number))
            //            {
            //                MessageBox.Show(ResourceHelper.LoadString(@"PatientFilesUsing"), ResourceHelper.LoadString(@"CaseLibrary"));
            //                return;
            //            }
            //            else
            //            {
            //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionPatient);
            //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionData);
            //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionDataSeializer);
            //                initPatientList();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    LogHelper.Error(ToString(), ex);
            //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            //}
        }

        private bool OnCanExecuteDeleteCommand()
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
            initPatientList();
            Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        }

        #endregion
    }
}
