using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.View.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace SuperSoft.View.ViewModel
{
    public class PatientHomeViewModel : MyViewModelBase
    {
        public PatientHomeViewModel()
        {
            registerMessenger();
#if DEBUG
            for (int i = 1; i < 10; i++)
            {
                var v1 = new Model.RespiratoryEvent();
                v1.Serial = i;
                var v2 = new Model.OxygenReductionEvent();
                v2.Serial = i;
                RespiratoryEventList.Add(v1);
                OxygenReductionEventList.Add(v2);
            }
#endif
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            Patient = Parameter as Model.Patient;
            loadData();
        }

        #region Patient

        private Model.Patient patient = new Model.Patient();

        public Model.Patient Patient
        {
            get { return patient; }
            set { Set(ref patient, value); }
        }

        #endregion

        #region SleepParameter

        private Model.SleepParameter sleepParameter = new Model.SleepParameter();

        public Model.SleepParameter SleepParameter
        {
            get { return sleepParameter; }
            set { Set(ref sleepParameter, value); }
        }

        #endregion

        #region RespiratoryEventList

        private ObservableCollection<Model.RespiratoryEvent> respiratoryEventList = new ObservableCollection<Model.RespiratoryEvent>();
        public ObservableCollection<Model.RespiratoryEvent> RespiratoryEventList
        {
            get { return respiratoryEventList; }
            set { Set(ref respiratoryEventList, value); }
        }

        #endregion

        #region RespiratoryEvent

        private Model.RespiratoryEvent respiratoryEvent = new Model.RespiratoryEvent();
        public Model.RespiratoryEvent RespiratoryEvent
        {
            get { return respiratoryEvent; }
            set { Set(ref respiratoryEvent, value); }
        }

        #endregion

        #region OxygenReductionEventList

        private ObservableCollection<Model.OxygenReductionEvent> oxygenReductionEventList = new ObservableCollection<Model.OxygenReductionEvent>();
        public ObservableCollection<Model.OxygenReductionEvent> OxygenReductionEventList
        {
            get { return oxygenReductionEventList; }
            set { Set(ref oxygenReductionEventList, value); }
        }

        #endregion

        #region RespiratoryEvent

        private Model.OxygenReductionEvent oxygenReductionEvent = new Model.OxygenReductionEvent();
        public Model.OxygenReductionEvent OxygenReductionEvent
        {
            get { return oxygenReductionEvent; }
            set { Set(ref oxygenReductionEvent, value); }
        }

        #endregion

        private void loadData()
        {
            //var fileName = Utility.Const.AppDatabasePath + Patient.FileName + Utility.Const.RMSFileExtensionData;

            //if (System.IO.File.Exists(fileName))
            //{
            //    UnPackageData unPackageData = new UnPackageData(fileName);
            //    //赋值给静态变量，其他业务需要使用数据时，直接访问
            //    StaticDatas.DataStruct = unPackageData.DataStruct;
            //    unPackageData.Dispose();
            //    unPackageData = null;
            //}
            //else
            //{
            //    //未找到患者文件
            //    System.Windows.MessageBox.Show(Utility.Windows.ResourceHelper.LoadString(@"PatientFilesNotFound"), Utility.Windows.ResourceHelper.LoadString(@"Error"), System.Windows.MessageBoxButton.OK);
            //}
        }

        #region 显示波形图时，上一个下一个事件可用，改变xaml中控件顺序时需要改变此处代码的值

        private int selectTabIndex;

        public int SelectTabIndex
        {
            get { return selectTabIndex; }
            set
            {
                Set(ref selectTabIndex, value);

                //选中波形
                if (selectTabIndex == 1)//x:Name="WaveLineChartTabItem"
                {
                    Messenger.Default.Send<MenuInfo>(new MenuInfo(MenuName.PreviousEventsMenu, true), Model.MessengerToken.SetMenuStatus);
                    Messenger.Default.Send<MenuInfo>(new MenuInfo(MenuName.NextEventsMenu, true), Model.MessengerToken.SetMenuStatus);
                }
                else
                {
                    Messenger.Default.Send<MenuInfo>(new MenuInfo(MenuName.PreviousEventsMenu, false), Model.MessengerToken.SetMenuStatus);
                    Messenger.Default.Send<MenuInfo>(new MenuInfo(MenuName.NextEventsMenu, false), Model.MessengerToken.SetMenuStatus);
                }
            }
        }

        #endregion


        #region 消息初始化 分析呼吸事件、鼾声事件函数

        /// <summary>
        /// 注册MVVMLight消息
        /// </summary>
        private void registerMessenger()
        {
            Messenger.Default.Register<object>(this, Model.MessengerToken.EventAnalysis, EventAnalysis);
            Messenger.Default.Register<object>(this, Model.MessengerToken.SnoreAnalysis, SnoreAnalysis);
        }

        /// <summary>
        /// 取消注册MVVMlight消息
        /// </summary>
        private void unRegisterMessenger()
        {
            Messenger.Default.Unregister<object>(this, Model.MessengerToken.EventAnalysis, EventAnalysis);
            Messenger.Default.Unregister<object>(this, Model.MessengerToken.SnoreAnalysis, SnoreAnalysis);
        }

        private void EventAnalysis(object obj)
        {
            //StaticDatas.DataStruct = BLL.AutoAnalysis.EventAnalysis(StaticDatas.DataStruct);
            //Utility.TaskAsyncHelper.RunAsync(saveAnalysisFile, saveAnalysisFileComplete);
            ////通知各个Chart更新分析曲线
            //Messenger.Default.Send<object>(null, Model.MessengerToken.UpEventAnalysisChart);
        }

        private void SnoreAnalysis(object obj)
        {
            //StaticDatas.DataStruct = BLL.AutoAnalysis.SnoreAnalysis(StaticDatas.DataStruct);
            //Utility.TaskAsyncHelper.RunAsync(saveAnalysisFile, saveAnalysisFileComplete);
            ////通知鼾声Chart更新图形
            //Messenger.Default.Send<object>(null, Model.MessengerToken.UpSnoreAnalysissChart);
        }

        private void saveAnalysisFile()
        {
            ////序列化
            //using (var memoryStream = new System.IO.MemoryStream())
            //{
            //    var serFileName = Utility.Const.AppDatabasePath + Patient.FileName + Utility.Const.RMSFileExtensionDataSeializer;
            //    ProtoBuf.Serializer.Serialize(memoryStream, StaticDatas.DataStruct);
            //    System.IO.File.WriteAllBytes(serFileName, memoryStream.ToArray());
            //    memoryStream.Close();
            //    memoryStream.Dispose();
            //    serFileName = null;
            //}
        }

        private void saveAnalysisFileComplete()
        {
        }

        #endregion

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unRegisterMessenger();
        }
    }
}
