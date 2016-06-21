using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SuperSoft.Utility.Windows;

namespace SuperSoft.View.ViewModel
{
    public class ChannelSettingsViewModel : MyViewModelBase
    {
        public ChannelSettingsViewModel()
        {
            TrendChartList = initTrendChartList();
            WaveLineChartList = initWaveLineChartList();

            loadConfigValue();

            ConfirmCommand = new RelayCommand(OnExecuteConfirmCommand);
            CancelCommand = new RelayCommand(OnExecuteCancelCommand);
        }

        private void loadConfigValue()
        {
            //从配置文件加载通道显示设置-趋势图设置

            //ChartType tmpSelectedTrendChartType1 = ChartType.SpO2;
            //Enum.TryParse<ChartType>(Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType1"), true, out tmpSelectedTrendChartType1);
            //SelectedTrendChartType1 = tmpSelectedTrendChartType1;

            var tmpSelectedTrendChartType1 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType1");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType1))
            {
                SelectedTrendChartType1 = ChartType.SpO2;
            }
            else
            {
                SelectedTrendChartType1 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType1, true);
            }
            var tmpSelectedTrendChartType2 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType2");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType2))
            {
                SelectedTrendChartType2 = ChartType.Apnea;
            }
            else
            {
                SelectedTrendChartType2 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType2, true);
            }
            var tmpSelectedTrendChartType3 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType3");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType3))
            {
                SelectedTrendChartType3 = ChartType.RespiratoryRate;
            }
            else
            {
                SelectedTrendChartType3 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType3, true);
            }
            var tmpSelectedTrendChartType4 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType4");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType4))
            {
                SelectedTrendChartType4 = ChartType.SnoreTimes;
            }
            else
            {
                SelectedTrendChartType4 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType4, true);
            }
            var tmpSelectedTrendChartType5 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType5");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType5))
            {
                SelectedTrendChartType5 = ChartType.HeartRate;
            }
            else
            {
                SelectedTrendChartType5 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType5, true);
            }
            var tmpSelectedTrendChartType6 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType6");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType6))
            {
                SelectedTrendChartType6 = ChartType.BPI;
            }
            else
            {
                SelectedTrendChartType6 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType6, true);
            }
            var tmpSelectedTrendChartType7 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType7");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType7))
            {
                SelectedTrendChartType7 = ChartType.EventFlag;
            }
            else
            {
                SelectedTrendChartType7 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType7, true);
            }
            var tmpSelectedTrendChartType8 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType8");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType8))
            {
                SelectedTrendChartType8 = ChartType.Pressure;
            }
            else
            {
                SelectedTrendChartType8 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType8, true);
            }
            var tmpSelectedTrendChartType9 = Utility.ConfigHelper.GetAppSetting(@"SelectedTrendChartType9");
            if (string.IsNullOrWhiteSpace(tmpSelectedTrendChartType9))
            {
                SelectedTrendChartType9 = ChartType.Flow;
            }
            else
            {
                SelectedTrendChartType9 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedTrendChartType9, true);
            }
            //从配置文件加载通道显示设置-波形图图设置
            var tmpSelectedWaveLineChartType1 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType1");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType1))
            {
                SelectedWaveLineChartType1 = ChartType.SpO2;
            }
            else
            {
                SelectedWaveLineChartType1 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType1, true);
            }
            var tmpSelectedWaveLineChartType2 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType2");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType2))
            {
                SelectedWaveLineChartType2 = ChartType.Snore;
            }
            else
            {
                SelectedWaveLineChartType2 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType2, true);
            }
            var tmpSelectedWaveLineChartType3 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType3");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType3))
            {
                SelectedWaveLineChartType3 = ChartType.HeartRate;
            }
            else
            {
                SelectedWaveLineChartType3 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType3, true);
            }
            var tmpSelectedWaveLineChartType4 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType4");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType4))
            {
                SelectedWaveLineChartType4 = ChartType.BPI;
            }
            else
            {
                SelectedWaveLineChartType4 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType4, true);
            }
            var tmpSelectedWaveLineChartType5 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType5");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType5))
            {
                SelectedWaveLineChartType5 = ChartType.OronasalAirflow;
            }
            else
            {
                SelectedWaveLineChartType5 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType5, true);
            }
            var tmpSelectedWaveLineChartType6 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType6");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType6))
            {
                SelectedWaveLineChartType6 = ChartType.ChestBreathing;
            }
            else
            {
                SelectedWaveLineChartType6 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType6, true);
            }
            var tmpSelectedWaveLineChartType7 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType7");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType7))
            {
                SelectedWaveLineChartType7 = ChartType.BellyBreathing;
            }
            else
            {
                SelectedWaveLineChartType7 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType7, true);
            }
            var tmpSelectedWaveLineChartType8 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType8");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType8))
            {
                SelectedWaveLineChartType8 = ChartType.Pressure;
            }
            else
            {
                SelectedWaveLineChartType8 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType8, true);
            }
            var tmpSelectedWaveLineChartType9 = Utility.ConfigHelper.GetAppSetting(@"SelectedWaveLineChartType9");
            if (string.IsNullOrWhiteSpace(tmpSelectedWaveLineChartType9))
            {
                SelectedWaveLineChartType9 = ChartType.Flow;
            }
            else
            {
                SelectedWaveLineChartType9 = (ChartType)Enum.Parse(typeof(ChartType), tmpSelectedWaveLineChartType9, true);
            }
        }

        #region TrendChartList

        private Dictionary<ChartType, string> trendChartList;

        public Dictionary<ChartType, string> TrendChartList
        {
            get { return trendChartList; }
            set { Set(ref trendChartList, value); }
        }

        #endregion

        /// <summary>
        /// 初始化趋势图列表
        /// </summary>
        /// <returns></returns>
        private Dictionary<ChartType, string> initTrendChartList()
        {
            var dictionaryList = new Dictionary<ChartType, string>();
            dictionaryList.Add(ChartType.SpO2, ResourceHelper.LoadString(@"SpO2"));
            dictionaryList.Add(ChartType.Apnea, ResourceHelper.LoadString(@"Apnea"));
            dictionaryList.Add(ChartType.RespiratoryRate, ResourceHelper.LoadString(@"RespiratoryRate"));
            dictionaryList.Add(ChartType.SnoreTimes, ResourceHelper.LoadString(@"SnoreTimes"));
            dictionaryList.Add(ChartType.HeartRate, ResourceHelper.LoadString(@"HeartRate"));
            dictionaryList.Add(ChartType.BPI, ResourceHelper.LoadString(@"BPI"));
            dictionaryList.Add(ChartType.EventFlag, ResourceHelper.LoadString(@"EventFlag"));
            dictionaryList.Add(ChartType.Pressure, ResourceHelper.LoadString(@"Pressure"));
            dictionaryList.Add(ChartType.Flow, ResourceHelper.LoadString(@"Flow"));
            return dictionaryList;
        }

        #region SelectedTrendChartType1

        private ChartType selectedTrendChartType1 = ChartType.SpO2;

        public ChartType SelectedTrendChartType1
        {
            get { return selectedTrendChartType1; }
            set { Set(ref selectedTrendChartType1, value); }
        }

        #endregion

        #region SelectedTrendChartType2

        private ChartType selectedTrendChartType2 = ChartType.Apnea;

        public ChartType SelectedTrendChartType2
        {
            get { return selectedTrendChartType2; }
            set { Set(ref selectedTrendChartType2, value); }
        }

        #endregion

        #region SelectedTrendChartType3

        private ChartType selectedTrendChartType3 = ChartType.RespiratoryRate;

        public ChartType SelectedTrendChartType3
        {
            get { return selectedTrendChartType3; }
            set { Set(ref selectedTrendChartType3, value); }
        }

        #endregion

        #region SelectedTrendChartType4

        private ChartType selectedTrendChartType4 = ChartType.SnoreTimes;

        public ChartType SelectedTrendChartType4
        {
            get { return selectedTrendChartType4; }
            set { Set(ref selectedTrendChartType4, value); }
        }

        #endregion

        #region SelectedTrendChartType5

        private ChartType selectedTrendChartType5 = ChartType.HeartRate;

        public ChartType SelectedTrendChartType5
        {
            get { return selectedTrendChartType5; }
            set { Set(ref selectedTrendChartType5, value); }
        }

        #endregion

        #region SelectedTrendChartType6

        private ChartType selectedTrendChartType6 = ChartType.BPI;

        public ChartType SelectedTrendChartType6
        {
            get { return selectedTrendChartType6; }
            set { Set(ref selectedTrendChartType6, value); }
        }

        #endregion

        #region SelectedTrendChartType7

        private ChartType selectedTrendChartType7 = ChartType.EventFlag;

        public ChartType SelectedTrendChartType7
        {
            get { return selectedTrendChartType7; }
            set { Set(ref selectedTrendChartType7, value); }
        }

        #endregion

        #region SelectedTrendChartType8

        private ChartType selectedTrendChartType8 = ChartType.Pressure;

        public ChartType SelectedTrendChartType8
        {
            get { return selectedTrendChartType8; }
            set { Set(ref selectedTrendChartType8, value); }
        }

        #endregion

        #region SelectedTrendChartType9

        private ChartType selectedTrendChartType9 = ChartType.Flow;

        public ChartType SelectedTrendChartType9
        {
            get { return selectedTrendChartType9; }
            set { Set(ref selectedTrendChartType9, value); }
        }

        #endregion


        #region WaveLineChartList

        private Dictionary<ChartType, string> waveLineChartList;

        public Dictionary<ChartType, string> WaveLineChartList
        {
            get { return waveLineChartList; }
            set { Set(ref waveLineChartList, value); }
        }

        #endregion

        /// <summary>
        /// 初始化时间列表
        /// </summary>
        /// <returns></returns>
        private Dictionary<ChartType, string> initWaveLineChartList()
        {
            var dictionaryList = new Dictionary<ChartType, string>();
            dictionaryList.Add(ChartType.SpO2, ResourceHelper.LoadString(@"SpO2"));
            dictionaryList.Add(ChartType.Snore, ResourceHelper.LoadString(@"Snore"));
            dictionaryList.Add(ChartType.HeartRate, ResourceHelper.LoadString(@"HeartRate"));
            dictionaryList.Add(ChartType.BPI, ResourceHelper.LoadString(@"BPI"));
            dictionaryList.Add(ChartType.OronasalAirflow, ResourceHelper.LoadString(@"OronasalAirflow"));
            dictionaryList.Add(ChartType.ChestBreathing, ResourceHelper.LoadString(@"ChestBreathing"));
            dictionaryList.Add(ChartType.BellyBreathing, ResourceHelper.LoadString(@"BellyBreathing"));
            dictionaryList.Add(ChartType.Pressure, ResourceHelper.LoadString(@"Pressure"));
            dictionaryList.Add(ChartType.Flow, ResourceHelper.LoadString(@"Flow"));
            return dictionaryList;
        }

        #region SelectedWaveLineChartType1

        private ChartType selectedWaveLineChartType1 = ChartType.SpO2;

        public ChartType SelectedWaveLineChartType1
        {
            get { return selectedWaveLineChartType1; }
            set { Set(ref selectedWaveLineChartType1, value); }
        }

        #endregion

        #region SelectedWaveLineChartType2

        private ChartType selectedWaveLineChartType2 = ChartType.Snore;

        public ChartType SelectedWaveLineChartType2
        {
            get { return selectedWaveLineChartType2; }
            set { Set(ref selectedWaveLineChartType2, value); }
        }

        #endregion

        #region SelectedWaveLineChartType3

        private ChartType selectedWaveLineChartType3 = ChartType.HeartRate;

        public ChartType SelectedWaveLineChartType3
        {
            get { return selectedWaveLineChartType3; }
            set { Set(ref selectedWaveLineChartType3, value); }
        }

        #endregion

        #region SelectedWaveLineChartType4

        private ChartType selectedWaveLineChartType4 = ChartType.BPI;

        public ChartType SelectedWaveLineChartType4
        {
            get { return selectedWaveLineChartType4; }
            set { Set(ref selectedWaveLineChartType4, value); }
        }

        #endregion

        #region SelectedWaveLineChartType5

        private ChartType selectedWaveLineChartType5 = ChartType.OronasalAirflow;

        public ChartType SelectedWaveLineChartType5
        {
            get { return selectedWaveLineChartType5; }
            set { Set(ref selectedWaveLineChartType5, value); }
        }

        #endregion

        #region SelectedWaveLineChartType6

        private ChartType selectedWaveLineChartType6 = ChartType.ChestBreathing;

        public ChartType SelectedWaveLineChartType6
        {
            get { return selectedWaveLineChartType6; }
            set { Set(ref selectedWaveLineChartType6, value); }
        }

        #endregion

        #region SelectedWaveLineChartType7

        private ChartType selectedWaveLineChartType7 = ChartType.BellyBreathing;

        public ChartType SelectedWaveLineChartType7
        {
            get { return selectedWaveLineChartType7; }
            set { Set(ref selectedWaveLineChartType7, value); }
        }

        #endregion

        #region SelectedWaveLineChartType8

        private ChartType selectedWaveLineChartType8 = ChartType.Pressure;

        public ChartType SelectedWaveLineChartType8
        {
            get { return selectedWaveLineChartType8; }
            set { Set(ref selectedWaveLineChartType8, value); }
        }

        #endregion

        #region SelectedWaveLineChartType9

        private ChartType selectedWaveLineChartType9 = ChartType.Flow;

        public ChartType SelectedWaveLineChartType9
        {
            get { return selectedWaveLineChartType9; }
            set { Set(ref selectedWaveLineChartType9, value); }
        }

        #endregion

        #region ShortestRespiratoryEvent

        private int shortestRespiratoryEvent = 10;

        public int ShortestRespiratoryEvent
        {
            get { return shortestRespiratoryEvent; }
            set { Set(ref shortestRespiratoryEvent, value); }
        }

        #endregion

        #region ApneaAirflowDecreasedThreshold

        private int apneaAirflowDecreasedThreshold = 20;

        public int ApneaAirflowDecreasedThreshold
        {
            get { return apneaAirflowDecreasedThreshold; }
            set { Set(ref apneaAirflowDecreasedThreshold, value); }
        }

        #endregion

        #region HypopneaAirflowDecreasedThreshold

        private int hypopneaAirflowDecreasedThreshold = 50;

        public int HypopneaAirflowDecreasedThreshold
        {
            get { return hypopneaAirflowDecreasedThreshold; }
            set { Set(ref hypopneaAirflowDecreasedThreshold, value); }
        }

        #endregion

        #region OxygenReductionThreshold

        private int oxygenReductionThreshold = 4;

        public int OxygenReductionThreshold
        {
            get { return oxygenReductionThreshold; }
            set { Set(ref oxygenReductionThreshold, value); }
        }

        #endregion

        #region IsRespiratoryAutoAnalysisWhenOpenFile

        private bool isRespiratoryAutoAnalysisWhenOpenFile = true;

        public bool IsRespiratoryAutoAnalysisWhenOpenFile
        {
            get { return isRespiratoryAutoAnalysisWhenOpenFile; }
            set { Set(ref isRespiratoryAutoAnalysisWhenOpenFile, value); }
        }

        #endregion

        #region SnoreAnalysisSensitivity

        private int snoreAnalysisSensitivity = 60;

        public int SnoreAnalysisSensitivity
        {
            get { return snoreAnalysisSensitivity; }
            set { Set(ref snoreAnalysisSensitivity, value); }
        }

        #endregion

        #region IsSnoreAutoAnalysisWhenOpenFile

        private bool isSnoreAutoAnalysisWhenOpenFile = true;

        public bool IsSnoreAutoAnalysisWhenOpenFile
        {
            get { return isSnoreAutoAnalysisWhenOpenFile; }
            set { Set(ref isSnoreAutoAnalysisWhenOpenFile, value); }
        }

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            try
            {
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType1", SelectedTrendChartType1.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType2", SelectedTrendChartType2.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType3", SelectedTrendChartType3.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType4", SelectedTrendChartType4.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType5", SelectedTrendChartType5.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType6", SelectedTrendChartType6.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType7", SelectedTrendChartType7.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType8", SelectedTrendChartType8.ToString());
                ConfigHelper.AddAppSetting(@"SelectedTrendChartType9", SelectedTrendChartType9.ToString());

                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType1", SelectedWaveLineChartType1.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType2", SelectedWaveLineChartType2.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType3", SelectedWaveLineChartType3.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType4", SelectedWaveLineChartType4.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType5", SelectedWaveLineChartType5.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType6", SelectedWaveLineChartType6.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType7", SelectedWaveLineChartType7.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType8", SelectedWaveLineChartType8.ToString());
                ConfigHelper.AddAppSetting(@"SelectedWaveLineChartType9", SelectedWaveLineChartType9.ToString());
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            }
            catch (Exception e)
            {
                LogHelper.Error(ToString(), e);
            }
        }

        #endregion

        #region CancelCommand

        public ICommand CancelCommand { get; private set; }

        private void OnExecuteCancelCommand()
        {
            loadConfigValue();
            Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        }

        #endregion

    }
}
