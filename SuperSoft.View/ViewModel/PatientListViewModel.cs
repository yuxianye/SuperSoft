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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SuperSoft.View.ViewModel
{
    public class PatientListViewModel : MyViewModelBase
    {
        //public PatientListViewModel()
        //{
        //    //OpenCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteOpenCommand, OnCanExecuteOpenCommand);
        //    //EditCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteEditCommand, OnCanExecuteEditCommand);
        //    //DeleteCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteDeleteCommand, OnCanExecuteDeleteCommand);
        //    //CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);

        //    //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
        //    //Model.Patient patient = new Model.Patient();
        //    //patient.Id = System.Guid.NewGuid();
        //    //// patient.Name = "FirstName";
        //    //patient.FirstName = "FirstName";
        //    //patient.LastName = "LastName";
        //    //patientBLL.Insert(patient);
        //    //// patientBLL.SaveChanges();
        //    ////patientBLL.ExecuteSqlCommand("delete from Patients");

        //    //string a = patientBLL.Count() + "ChangeTitleHello MvvmLight";
        //}

        //#region 取得患者列表

        //public void initPatientList()
        //{
        //    //var taskResult = getPatientListAsync();
        //    //if (!Equals(PatientList, null))
        //    //{
        //    //    PatientList.Clear();
        //    //}

        //    //PatientList = taskResult.Result;
        //    //taskResult.Dispose();
        //    //taskResult = null;

        //    //if (PatientList != null && PatientList.Count() > 0)//有患者
        //    //{
        //    //    if (StaticDatas.CurrentOpenedPatient == null) //默认选择第一个
        //    //    {
        //    //        Patient = PatientList.FirstOrDefault();
        //    //    }
        //    //    else//选择原有的患者
        //    //    {
        //    //        var v = PatientList.Where(a => a.Number == StaticDatas.CurrentOpenedPatient.Number);
        //    //        if (v != null && v.Count() > 0)
        //    //        {
        //    //            Patient = v.FirstOrDefault();
        //    //        }
        //    //        else
        //    //        {
        //    //            Patient = PatientList.FirstOrDefault();
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Patient = null;
        //    //}
        //}

        ////private Task<ObservableCollection<Model.Patient>> getPatientListAsync()
        ////{
        ////    // return Task.Run<ObservableCollection<Model.Patient>>(new Func<ObservableCollection<Model.Patient>>(getPatientList));
        ////    return Task.Run<ObservableCollection<Model.Patient>>(new Func<ObservableCollection<Model.Patient>>(() =>
        ////    {
        ////        var directoryInfo = new DirectoryInfo(Const.AppDatabasePath);
        ////        //得到所有详细数据文件的列表,搜索所有 xxxxxx.dat 的文件和文件件
        ////        var searchedFileInfos = directoryInfo.GetFileSystemInfos(Const.RMSFileExtensionPatientSearch,
        ////            SearchOption.TopDirectoryOnly);
        ////        directoryInfo = null;
        ////        ObservableCollection<Model.Patient> result = new ObservableCollection<Patient>();

        ////        foreach (var fileInfo in searchedFileInfos.OrderByDescending(a => a.CreationTime))
        ////        {
        ////            if (fileInfo.Attributes != FileAttributes.Directory)
        ////            {
        ////                using (var memoryStream = new MemoryStream(File.ReadAllBytes(fileInfo.FullName)))
        ////                {
        ////                    //Patient p = Serializer.Deserialize<Model.Patient>(memoryStream);
        ////                    Patient p = new Patient();// Serializer.Deserialize<Model.Patient>(memoryStream);
        ////                    result.Add(p);
        ////                    memoryStream.Close();
        ////                    memoryStream.Dispose();
        ////                }
        ////            }
        ////        }
        ////        return result;
        ////    }));

        ////}

        //#endregion

        //#region PatientList

        //public ObservableCollection<Model.Patient> PatientList { get; set; }

        //#endregion


        //#region OpenCommand

        //public ICommand OpenCommand { get; private set; }

        //private void OnExecuteOpenCommand()
        //{
        //    try
        //    {
        //        // StaticDatas.CurrentOpenedPatient = Patient;
        //        Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        //        //  Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientHomeView, ViewType.View, Patient), Model.MessengerToken.Navigate);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ToString(), ex);
        //        Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        //    }
        //}

        //private bool OnCanExecuteOpenCommand()
        //{
        //    //if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    return true;

        //}

        //#endregion

        //#region EditCommand

        //public ICommand EditCommand { get; private set; }

        //private void OnExecuteEditCommand()
        //{
        //    try
        //    {
        //        //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientEditView, ViewType.Popup, Patient), Model.MessengerToken.Navigate);
        //        //编辑之后重新加载
        //        initPatientList();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ToString(), ex);
        //        Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        //    }
        //}

        //private bool OnCanExecuteEditCommand()
        //{
        //    //if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    return true;

        //}

        //#endregion

        //#region DeleteCommand

        //public ICommand DeleteCommand { get; private set; }

        //private void OnExecuteDeleteCommand()
        //{
        //    //try
        //    //{
        //    //    if (MessageBox.Show(ResourceHelper.LoadString(@"DeleteConfirm"), ResourceHelper.LoadString(@"CaseLibrary"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        //    //    {
        //    //        if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
        //    //        {
        //    //            if (!Equals(StaticDatas.CurrentOpenedPatient, null) && Equals(StaticDatas.CurrentOpenedPatient.Number, Patient.Number))
        //    //            {
        //    //                MessageBox.Show(ResourceHelper.LoadString(@"PatientFilesUsing"), ResourceHelper.LoadString(@"CaseLibrary"));
        //    //                return;
        //    //            }
        //    //            else
        //    //            {
        //    //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionPatient);
        //    //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionData);
        //    //                File.Delete(Const.AppDatabasePath + Patient.FileName + Const.RMSFileExtensionDataSeializer);
        //    //                initPatientList();
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    LogHelper.Error(ToString(), ex);
        //    //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        //    //}
        //}

        //private bool OnCanExecuteDeleteCommand()
        //{
        //    //if (!Equals(Patient, null) && !string.IsNullOrWhiteSpace(Patient.Number) && !string.IsNullOrWhiteSpace(Patient.Name))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    return true;

        //}

        //#endregion

        //#region CancelCommand

        //public ICommand CancelCommand { get; private set; }

        //private void OnExecuteCancelCommand()
        //{
        //    initPatientList();
        //    Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        //}

        //#endregion


        private readonly bool isHaveCondition;
        private readonly PropertySortCondition sortCondition = new PropertySortCondition("Id", ListSortDirection.Descending);
        public PatientListViewModel() //: this(null)
        {
        }

        //public PatientListViewModel(Expression<Func<Patient, bool>> condition)
        //{
            //            #region init DEBUG
            //#if DEBUG
            //            {

            //                if (StaticDatas.CurrentSelectedPatient == null)
            //                {
            //                    //医生
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.DoctorBLL doctorBLL = new DoctorBLL();
            //                    ICollection<DAL.Doctor> tmp = new Collection<DAL.Doctor>();
            //                    for (int i = 0; i < 3; i++)
            //                    {
            //                        DAL.Doctor doctor = new DAL.Doctor();
            //                        doctor.Id = GuidExtensions.MyGuid();
            //                        //doctor.FirstName = "医医医医医医医医医医医医医医医医医医医医医医医医医医医医医医医医";
            //                        //doctor.LastName =  "生生生生生生生生生生生生生生生生生生生生生生生生生生生生生生生" + i;
            //                        doctor.FirstName = "医";
            //                        doctor.LastName = "生" + i;
            //                        tmp.Add(doctor);
            //                    }
            //                    doctorBLL.Insert(tmp);
            //                    doctorBLL.Dispose();

            //                    //患者
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.PatientBLL patientBLL = new PatientBLL();
            //                    ICollection<DAL.Patient> tmp2 = new Collection<DAL.Patient>();
            //                    for (int i = 0; i < 3; i++)
            //                    {
            //                        DAL.Patient patient = new DAL.Patient();
            //                        patient.Id = GuidExtensions.MyGuid();
            //                        //patient.FirstName = "患患患患患患患患患患患患患患患患患患患患患患患患患患患患患患患好";
            //                        patient.FirstName = "患";
            //                        patient.LastName = "者" + i;
            //                        patient.Address = "辽宁省沈阳市浑南新区创新路153-5号U11";
            //                        patient.PostalCode = "110024";
            //                        patient.TelephoneNumbers = "13800138000";
            //                        patient.DateOfBirth = DateTime.Now.AddYears(-56);
            //                        patient.Diagnosis = "COPDII";
            //                        patient.DoctorId = tmp.FirstOrDefault().Id;
            //                        patient.EMail = "test@respircare.com";
            //                        patient.Gender = true;
            //                        patient.Height = 182;
            //                        patient.Weight = 81;
            //                        tmp2.Add(patient);
            //                    }
            //                    patientBLL.Insert(tmp2);
            //                    patientBLL.Dispose();

            //                    //产品
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.ProductBLL productBLL = new ProductBLL();
            //                    DAL.Product product = new DAL.Product();
            //                    product.Id = GuidExtensions.MyGuid();
            //                    product.SerialNumber = "123456789012345678";
            //                    product.ProductModel = 2;
            //                    product.ProductVersion = "APAP20_V030_150117";
            //                    product.TotalWorkingTime = 522;
            //                    productBLL.Insert(product);
            //                    productBLL.Dispose();

            //                    //患者产品
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.PatientsProductBLL patientsProductBLL = new PatientsProductBLL();

            //                    DAL.PatientsProduct patientsProduct = new DAL.PatientsProduct();
            //                    patientsProduct.Id = GuidExtensions.MyGuid(); ;
            //                    patientsProduct.PatientId = tmp2.FirstOrDefault().Id;
            //                    patientsProduct.ProductId = product.Id;
            //                    patientsProductBLL.Insert(patientsProduct);
            //                    patientsProductBLL.Dispose();

            //                    //产品分析
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.ProductWorkingStatisticsDataBLL productWorkingStatisticsDataBLL = new ProductWorkingStatisticsDataBLL();
            //                    ICollection<DAL.ProductWorkingStatisticsData> listProductWorkingStatisticsDataBLL = new Collection<DAL.ProductWorkingStatisticsData>();
            //                    Random random = new Random();
            //                    for (byte i = 0; i < 7; i++)
            //                    //for (byte i = 0; i < 2; i++)
            //                    {
            //                        for (int j = 0; j < 10; j++)
            //                        {
            //                            if (j % 7 == 3)
            //                            {
            //                                continue;
            //                            }
            //                            DAL.ProductWorkingStatisticsData productWorkingStatisticsData = new DAL.ProductWorkingStatisticsData();

            //                            productWorkingStatisticsData.Id = GuidExtensions.MyGuid();
            //                            productWorkingStatisticsData.ProductId = product.Id;
            //                            productWorkingStatisticsData.DataTime = DateTime.Now.AddDays(-j);
            //                            productWorkingStatisticsData.TherapyMode = i;

            //                            productWorkingStatisticsData.PressureMax = random.Next(0, 35);
            //                            productWorkingStatisticsData.PressureP95 = (byte)random.Next(0, productWorkingStatisticsData.PressureMax.HasValue ? (int)productWorkingStatisticsData.PressureMax : 0);
            //                            productWorkingStatisticsData.PressureMedian = (byte)random.Next(0, productWorkingStatisticsData.PressureP95.HasValue ? (int)productWorkingStatisticsData.PressureP95 : 0);

            //                            productWorkingStatisticsData.FlowMax = random.Next(-200, 200);
            //                            productWorkingStatisticsData.FlowP95 = (byte)random.Next(-200, productWorkingStatisticsData.FlowMax.HasValue ? (int)productWorkingStatisticsData.FlowMax : 0);
            //                            productWorkingStatisticsData.FlowMedian = (byte)random.Next(-200, productWorkingStatisticsData.FlowP95.HasValue ? (int)productWorkingStatisticsData.FlowP95 : 0);

            //                            productWorkingStatisticsData.CountAI = random.Next(0, 20);
            //                            productWorkingStatisticsData.CountHI = random.Next(0, 20);
            //                            productWorkingStatisticsData.CountAHI = productWorkingStatisticsData.CountAI + productWorkingStatisticsData.CountHI;

            //                            productWorkingStatisticsData.LeakMax = (byte)random.Next(0, 99);
            //                            productWorkingStatisticsData.LeakP95 = (byte)random.Next(0, productWorkingStatisticsData.LeakMax.HasValue ? (int)productWorkingStatisticsData.LeakMax : 0);
            //                            productWorkingStatisticsData.LeakMedian = (byte)random.Next(0, productWorkingStatisticsData.LeakP95.HasValue ? (int)productWorkingStatisticsData.LeakP95 : 0);

            //                            productWorkingStatisticsData.TidalVolumeMax = random.Next(0, 2500);
            //                            productWorkingStatisticsData.TidalVolumeP95 = (byte)random.Next(0, productWorkingStatisticsData.TidalVolumeMax.HasValue ? (int)productWorkingStatisticsData.TidalVolumeMax : 0);
            //                            productWorkingStatisticsData.TidalVolumeMedian = (byte)random.Next(0, productWorkingStatisticsData.TidalVolumeP95.HasValue ? (int)productWorkingStatisticsData.TidalVolumeP95 : 0);


            //                            productWorkingStatisticsData.MinuteVentilationMax = (byte)random.Next(0, 30);
            //                            productWorkingStatisticsData.MinuteVentilationP95 = (byte)random.Next(0, productWorkingStatisticsData.MinuteVentilationMax.HasValue ? (int)productWorkingStatisticsData.MinuteVentilationMax : 0);
            //                            productWorkingStatisticsData.MinuteVentilationMedian = (byte)random.Next(0, productWorkingStatisticsData.MinuteVentilationP95.HasValue ? (int)productWorkingStatisticsData.MinuteVentilationP95 : 0);

            //                            productWorkingStatisticsData.SpO2Max = (byte)random.Next(85, 100);
            //                            productWorkingStatisticsData.SpO2P95 = (byte)random.Next(0, productWorkingStatisticsData.SpO2Max.HasValue ? (int)productWorkingStatisticsData.SpO2Max : 0);
            //                            productWorkingStatisticsData.SpO2Median = (byte)random.Next(0, productWorkingStatisticsData.SpO2P95.HasValue ? (int)productWorkingStatisticsData.SpO2P95 : 0);

            //                            productWorkingStatisticsData.PulseRateMax = (byte)random.Next(0, 255);
            //                            productWorkingStatisticsData.PulseRateP95 = (byte)random.Next(0, productWorkingStatisticsData.PulseRateMax.HasValue ? (int)productWorkingStatisticsData.PulseRateMax : 0);
            //                            productWorkingStatisticsData.PulseRateMedian = (byte)random.Next(0, productWorkingStatisticsData.PulseRateP95.HasValue ? (int)productWorkingStatisticsData.PulseRateP95 : 0);

            //                            productWorkingStatisticsData.RespiratoryRateMax = (byte)random.Next(0, 60);
            //                            productWorkingStatisticsData.RespiratoryRateP95 = (byte)random.Next(0, productWorkingStatisticsData.RespiratoryRateMax.HasValue ? (int)productWorkingStatisticsData.RespiratoryRateMax : 0);
            //                            productWorkingStatisticsData.RespiratoryRateMedian = (byte)random.Next(0, productWorkingStatisticsData.RespiratoryRateP95.HasValue ? (int)productWorkingStatisticsData.RespiratoryRateP95 : 0);

            //                            productWorkingStatisticsData.IERatioMax = (byte)random.Next(0, 100);
            //                            productWorkingStatisticsData.IERatioP95 = (byte)random.Next(0, productWorkingStatisticsData.IERatioMax.HasValue ? (int)productWorkingStatisticsData.IERatioMax : 0);
            //                            productWorkingStatisticsData.IERatioMedian = (byte)random.Next(0, productWorkingStatisticsData.IERatioP95.HasValue ? (int)productWorkingStatisticsData.IERatioP95 : 0);

            //                            productWorkingStatisticsData.IPAPMax = (byte)random.Next(4, 25);
            //                            productWorkingStatisticsData.IPAPP95 = (byte)random.Next(4, productWorkingStatisticsData.IPAPMax.HasValue ? (int)productWorkingStatisticsData.IPAPMax : 0);
            //                            productWorkingStatisticsData.IPAPMedian = (byte)random.Next(4, productWorkingStatisticsData.IPAPP95.HasValue ? (int)productWorkingStatisticsData.IPAPP95 : 0);

            //                            productWorkingStatisticsData.EPAPMax = (byte)random.Next(4, 20);
            //                            productWorkingStatisticsData.EPAPP95 = (byte)random.Next(4, productWorkingStatisticsData.EPAPMax.HasValue ? (int)productWorkingStatisticsData.EPAPMax : 0);
            //                            productWorkingStatisticsData.EPAPMedian = (byte)random.Next(4, productWorkingStatisticsData.EPAPP95.HasValue ? (int)productWorkingStatisticsData.EPAPP95 : 0);

            //                            productWorkingStatisticsData.TotalUsage = random.Next(0, 86400000);

            //                            listProductWorkingStatisticsDataBLL.Add(productWorkingStatisticsData);
            //                        }
            //                    }
            //                    productWorkingStatisticsDataBLL.Insert(listProductWorkingStatisticsDataBLL);
            //                    productWorkingStatisticsDataBLL.Dispose();

            //                    //产品概要
            //                    ////////////////////////////////////////////////////////////////////////////////////
            //                    BLL.ProductWorkingSummaryDataBLL productWorkingSummaryDataBLL = new ProductWorkingSummaryDataBLL();
            //                    ICollection<DAL.ProductWorkingSummaryData> listProductWorkingSummaryData = new Collection<DAL.ProductWorkingSummaryData>();

            //                    BLL.ProductWorkingDetailedDataBLL productWorkingDetailedDataBLL = new ProductWorkingDetailedDataBLL();
            //                    ICollection<DAL.ProductWorkingDetailedData> listProductWorkingDetailedData = new Collection<DAL.ProductWorkingDetailedData>();

            //                    for (byte i = 0; i < 7; i++)
            //                    {
            //                        for (int j = 0; j < 10; j++)
            //                        {
            //                            DateTime time = DateTime.Now.AddDays(-j).Date.AddHours(12);
            //                            //每天的多段数据
            //                            for (int loop = 0; loop < random.Next(1, 6); loop++)
            //                            {
            //                                time = time.AddHours(loop);
            //                                DAL.ProductWorkingSummaryData productWorkingSummaryData = new DAL.ProductWorkingSummaryData();

            //                                productWorkingSummaryData.Id = GuidExtensions.MyGuid();
            //                                productWorkingSummaryData.ProductId = product.Id;
            //                                productWorkingSummaryData.FileName = "1506\\204056.dat";
            //                                productWorkingSummaryData.StartTime = time;
            //                                productWorkingSummaryData.EndTime = productWorkingSummaryData.StartTime.AddHours(loop);
            //                                productWorkingSummaryData.ProductVersion = "APAP20_V030_140107";
            //                                productWorkingSummaryData.ProductModel = (byte)random.Next(0, 17);
            //                                productWorkingSummaryData.WorkingTime = random.Next(0, (int)(productWorkingSummaryData.EndTime - productWorkingSummaryData.StartTime).TotalMilliseconds);
            //                                productWorkingSummaryData.TherapyMode = i;
            //                                productWorkingSummaryData.CurrentTime = time;
            //                                productWorkingSummaryData.IPAP = (byte)random.Next(4, 25);
            //                                productWorkingSummaryData.EPAP = (byte)random.Next(4, 20);
            //                                productWorkingSummaryData.RiseTime = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.RespiratoryRate = (byte)random.Next(0, 60);
            //                                productWorkingSummaryData.InspireTime = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.ITrigger = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.ETrigger = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.Ramp = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.ExhaleTime = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.IPAPMax = (byte)random.Next(4, 25);
            //                                productWorkingSummaryData.EPAPMin = (byte)random.Next(4, 20);
            //                                productWorkingSummaryData.PSMax = (byte)random.Next(4, 255);
            //                                productWorkingSummaryData.PSMin = (byte)random.Next(4, 255);
            //                                productWorkingSummaryData.CPAP = random.Next(0, 255);
            //                                productWorkingSummaryData.CFlex = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.CPAPStart = random.Next(0, 255);
            //                                productWorkingSummaryData.CPAPMax = random.Next(0, 255);
            //                                productWorkingSummaryData.CPAPMin = random.Next(0, 255);
            //                                productWorkingSummaryData.Alert = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Alert_Tube = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Alert_Apnea = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Alert_MinuteVentilation = (byte)random.Next(0, 255);
            //                                productWorkingSummaryData.Alert_HRate = (byte)random.Next(0, 0x16);
            //                                productWorkingSummaryData.Alert_LRate = (byte)random.Next(0, 0x16);

            //                                productWorkingSummaryData.Config_HumidifierLevel = (byte)random.Next(0, 6);
            //                                productWorkingSummaryData.Config_DataStore = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Config_SmartStart = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Config_PressureUnit = (byte)random.Next(0, 2);
            //                                productWorkingSummaryData.Config_Language = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Config_Backlight = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Config_MaskPressure = (byte)random.Next(0, 1);
            //                                productWorkingSummaryData.Config_ClinicalSet = (byte)random.Next(0, 1);
            //                                listProductWorkingSummaryData.Add(productWorkingSummaryData);

            //                                DAL.ProductWorkingDetailedData productWorkingDetailedData = new DAL.ProductWorkingDetailedData();
            //                                productWorkingDetailedData.Id = GuidExtensions.MyGuid();
            //                                //productWorkingDetailedData.Content = new byte[] { 0x00, 0x00 };
            //                                productWorkingDetailedData.ProductWorkingSummaryDataId = productWorkingSummaryData.Id;
            //                                listProductWorkingDetailedData.Add(productWorkingDetailedData);
            //                            }
            //                        }
            //                    }
            //                    productWorkingSummaryDataBLL.Insert(listProductWorkingSummaryData);
            //                    productWorkingSummaryDataBLL.Dispose();

            //                    productWorkingDetailedDataBLL.Insert(listProductWorkingDetailedData);
            //                    productWorkingDetailedDataBLL.Dispose();
            //                }
            //            }
            //#endif
            //            #endregion init DEBUG

            //CursorsHelp.SetAppCursorWait();
            //取得所有患者
        //    if (condition == null)
        //    {
        //        condition = t => t.FirstName != null;
        //        isHaveCondition = false;
        //    }
        //    else
        //    {
        //        isHaveCondition = true;
        //    }
        //    //AllPatientList = getAllPatient(condition);

        //    if (AllPatientList != null && AllPatientList.Count() > 0)//有患者
        //    {
        //        if (StaticDatas.CurrentSelectedPatient == null) //默认选择第一个
        //        {
        //            SelectedPatient = AllPatientList.FirstOrDefault();
        //        }
        //        else//选择原有的患者
        //        {
        //            var v = AllPatientList.Where(a => a.Id == StaticDatas.CurrentSelectedPatient.Id);
        //            if (v != null && v.Count() > 0)
        //            {
        //                SelectedPatient = v.FirstOrDefault();
        //            }
        //            else
        //            {
        //                SelectedPatient = AllPatientList.FirstOrDefault();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        SelectedPatient = null;
        //    }
        //    if (AllPatientList == null || AllPatientList.Count() < 1)
        //    {
        //        AllPatientListVisibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        AllPatientListVisibility = Visibility.Visible;
        //        PatientCount = ResourceHelper.LoadString("PatientCount") + AllPatientList.Count();
        //    }

        //    //CursorsHelp.SetAppCursorNormal();
        //}

        //public IViewManagement ViewManagement { get; set; }

        //public object Params { get; set; }

        //public event EventHandler<MenuName> OnMenuSelectChange;

        //public void InitMenuSelectChange()
        //{
        //    if (!Equals(OnMenuSelectChange, null))
        //    {
        //        if (isHaveCondition)
        //        {
        //            OnMenuSelectChange(this, MenuName.PatientListSearchMenu);
        //        }
        //        else
        //        {
        //            OnMenuSelectChange(this, MenuName.PatientListMenu);
        //        }
        //    }
        //}

        #region 取得所有患者 getAllPatient 

        ///// <summary>
        ///// 取得所有患者
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerable<Patient> getAllPatient(Expression<Func<Patient, bool>> condition)
        //{
        //    var patientBLL = new PatientBLL();
        //    var tmpList = patientBLL.GetByCondition(sortCondition, condition);
        //    patientBLL.Dispose();
        //    patientBLL = null;
        //    StaticDatas.AllPatientList = tmpList;
        //    return StaticDatas.AllPatientList;
        //}


        //private Task<ObservableCollection<Patient>> getPatientListAsync()
        //{
        //    // return Task.Run<ObservableCollection<Models.Patient>>(new Func<ObservableCollection<Models.Patient>>(getPatientList));
        //    return Task.Run<ObservableCollection<Patient>>(new Func<ObservableCollection<Patient>>(() =>
        //    {
        //        ObservableCollection<Patient> result = new ObservableCollection<Patient>();

        //        var patientBLL = new PatientBLL();
        //        var tmpList = patientBLL.GetByCondition(sortCondition, condition);
        //        patientBLL.Dispose();
        //        patientBLL = null;
        //        //StaticDatas.AllPatientList = tmpList;
        //        //return StaticDatas.AllPatientList;
        //        return result;
        //    }));

        //}

        #endregion

        //private void initData()
        //{
        //    //改变子ViewModel的数据
        //    PatientInfoViewModel.SelectedPatient = SelectedPatient;
        //    ProductInfoViewModel.SelectedPatient = SelectedPatient;
        //    StatisticsInfoViewModel.TherapyMode = TherapyMode.Key;
        //    initViewProductWorkingStatisticsDataList();
        //}

        //private void initDataComplete()
        //{
        //}

        //private void initViewProductWorkingStatisticsDataList()
        //{
        //    var viewProductWorkingStatisticsDataBLL = new ViewProductWorkingStatisticsDataBLL();
        //    Expression<Func<ViewProductWorkingStatisticsData, bool>> condition =
        //        t => t.PatientId == SelectedPatient.Id && t.TherapyMode == (byte)TherapyMode.Key;
        //    var tmp = viewProductWorkingStatisticsDataBLL.GetByCondition(condition);
        //    StatisticsInfoViewModel.ViewProductWorkingStatisticsDataList = tmp;
        //    viewProductWorkingStatisticsDataBLL.Dispose();
        //    viewProductWorkingStatisticsDataBLL = null;
        //}

        #region AllPatientListVisibility

        /// <summary>
        /// </summary>
        public IEnumerable<Patient> AllPatientList { get; set; }

        private Visibility allPatientListVisibility = Visibility.Collapsed;

        public Visibility AllPatientListVisibility
        {
            get { return allPatientListVisibility; }
            set
            {
                Set(ref allPatientListVisibility, value);
            }
        }

        #endregion

        #region 当前医生看护患者的数量

        private string patientCount;

        /// <summary>
        /// PatientCount
        /// </summary>
        public string PatientCount
        {
            get { return patientCount; }
            set
            {
                Set(ref patientCount, value);

            }
        }

        #endregion

        #region 当前选中的患者 SelectedPatient ,根据当前选择的患者 加载治疗模式数据，然后根据模式加载其他的数据

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                Set(ref selectedPatient, value);

                //StaticDatas.CurrentSelectedPatient = value;
                //CommandManager.InvalidateRequerySuggested();
                //if (!Equals(selectedPatient, null))
                //{
                //   // initTherapyModeList();
                //}
            }
        }

        #endregion

        //#region 治疗模式 没有治疗模式则 后面的统计信息等不显示，由治疗模式改变出发加载数据

        ///// <summary>
        ///// 初始化治疗模式列表，根据产品的运行数据，加载不同的治疗模式，未使用的模式不列出。
        ///// 查询视图ViewProductWorkingStatisticsData中的治疗模式种类的数量
        ///// </summary>
        //private void initTherapyModeList()
        //{
        //var tmpTherapyModeList = new Collection<KeyValuePair<TherapyMode, string>>();
        //var viewProductWorkingStatisticsDataBLL = new ViewProductWorkingStatisticsDataBLL();
        //Expression<Func<ViewProductWorkingStatisticsData, bool>> condition = t => t.PatientId == SelectedPatient.Id;
        //var tmp = viewProductWorkingStatisticsDataBLL.GetByCondition(condition).GroupBy(a => a.TherapyMode);
        //viewProductWorkingStatisticsDataBLL.Dispose();
        //viewProductWorkingStatisticsDataBLL = null;
        //if (tmp != null && tmp.Count() > 0)
        //{
        //    foreach (var v in tmp)
        //    {
        //        var tm = (TherapyMode)v.Key;
        //        tmpTherapyModeList.Add(new KeyValuePair<TherapyMode, string>(tm, tm.ToDescription()));
        //    }
        //    StaticDatas.IsCurrentSelectedPatientHaveProduct = true;
        //    TherapyModeList = tmpTherapyModeList.OrderBy(a => a.Key).ToArray();
        //    TherapyMode = TherapyModeList.FirstOrDefault();
        //    TherapyModelInfoVisibility = Visibility.Visible;
        //}
        //else
        //{
        //    StaticDatas.IsCurrentSelectedPatientHaveProduct = false;
        //    //没有治疗模式不显示统计信息
        //    TherapyMode = default(KeyValuePair<TherapyMode, string>);
        //    TherapyModelInfoVisibility = Visibility.Collapsed;
        //}
        //CommandManager.InvalidateRequerySuggested();
        //}

        //private IEnumerable<KeyValuePair<TherapyMode, string>> therapyModeList;

        //public IEnumerable<KeyValuePair<TherapyMode, string>> TherapyModeList
        //{
        //    get { return therapyModeList; }
        //    set
        //    {
        //        Set(ref therapyModeList, value);

        //    }
        //}

        ///// <summary>
        ///// 选择治疗模式
        ///// </summary>
        //private KeyValuePair<TherapyMode, string> therapyMode;

        ///// <summary>
        ///// 选择的治疗模式
        ///// </summary>
        //public KeyValuePair<TherapyMode, string> TherapyMode
        //{
        //    get { return therapyMode; }
        //    set
        //    {
        //        Set(ref therapyMode, value);
        //        //if (Equals(therapyMode, value)) return;
        //        //therapyMode = value;
        //        //OnPropertyChanged("TherapyMode");
        //        TaskAsyncHelper.RunAsync(initData, initDataComplete);
        //    }
        //}

        //#endregion

        //#region PatientInfoViewModel

        //private PatientInfoViewModel patientInfoViewModel = new PatientInfoViewModel();

        //public PatientInfoViewModel PatientInfoViewModel
        //{
        //    get { return patientInfoViewModel; }
        //    set
        //    {
        //        if (Equals(patientInfoViewModel, value)) return;
        //        patientInfoViewModel = value;
        //        OnPropertyChanged("PatientInfoViewModel");
        //    }
        //}

        //#endregion

        //#region ProductInfoViewModel

        //private ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel();

        //public ProductInfoViewModel ProductInfoViewModel
        //{
        //    get { return productInfoViewModel; }
        //    set
        //    {
        //        if (Equals(productInfoViewModel, value)) return;
        //        productInfoViewModel = value;
        //        OnPropertyChanged("ProductInfoViewModel");
        //    }
        //}

        //#endregion

        //#region StatisticsInfoViewModel

        //private StatisticsInfoViewModel statisticsInfoViewModel = new StatisticsInfoViewModel();

        //public StatisticsInfoViewModel StatisticsInfoViewModel
        //{
        //    get { return statisticsInfoViewModel; }
        //    set
        //    {
        //        if (Equals(statisticsInfoViewModel, value)) return;
        //        statisticsInfoViewModel = value;
        //        OnPropertyChanged("StatisticsInfoViewModel");
        //    }
        //}

        //#endregion

        //#region TherapyModelInfoVisibility

        //private Visibility therapyModelInfoVisibility = Visibility.Collapsed;

        //public Visibility TherapyModelInfoVisibility
        //{
        //    get { return therapyModelInfoVisibility; }
        //    set { Set(ref therapyModelInfoVisibility, value); }
        //}

        //#endregion





    }
}
