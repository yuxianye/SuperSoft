using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SuperSoft.Utility;

namespace SuperSoft.View.ViewModel
{
    public class AutoAnalysisSettingsViewModel : MyViewModelBase
    {
        public AutoAnalysisSettingsViewModel()
        {
            loadConfigValue();
            ConfirmCommand = new RelayCommand(OnExecuteConfirmCommand);
            CancelCommand = new RelayCommand(OnExecuteCancelCommand);
        }

        private void loadConfigValue()
        {
            //从配置文件加载呼吸分析阈值设置
            var tmpShortestRespiratoryEvent = Utility.ConfigHelper.GetAppSetting(@"ShortestRespiratoryEvent");
            if (!Equals(tmpShortestRespiratoryEvent, null))
            {
                ShortestRespiratoryEvent = tmpShortestRespiratoryEvent.GetInt(10);
            }
            var tmpApneaAirflowDecreasedThreshold = Utility.ConfigHelper.GetAppSetting(@"ApneaAirflowDecreasedThreshold");
            if (!Equals(tmpShortestRespiratoryEvent, null))
            {
                ApneaAirflowDecreasedThreshold = tmpApneaAirflowDecreasedThreshold.GetInt(20);
            }
            var tmpHypopneaAirflowDecreasedThreshold = Utility.ConfigHelper.GetAppSetting(@"HypopneaAirflowDecreasedThreshold");
            if (!Equals(tmpShortestRespiratoryEvent, null))
            {
                HypopneaAirflowDecreasedThreshold = tmpHypopneaAirflowDecreasedThreshold.GetInt(50);
            }
            var tmpOxygenReductionThreshold = Utility.ConfigHelper.GetAppSetting(@"OxygenReductionThreshold");
            if (!Equals(tmpShortestRespiratoryEvent, null))
            {
                OxygenReductionThreshold = tmpOxygenReductionThreshold.GetInt(4);
            }
            var tmpIsRespiratoryAutoAnalysisWhenOpenFile = Utility.ConfigHelper.GetAppSetting(@"IsRespiratoryAutoAnalysisWhenOpenFile");
            if (!Equals(tmpIsRespiratoryAutoAnalysisWhenOpenFile, null))
            {
                IsRespiratoryAutoAnalysisWhenOpenFile = tmpIsRespiratoryAutoAnalysisWhenOpenFile.GetBool(true);
            }
            //从配置文件加载鼾声分析灵敏度设置
            var tmpSnoreAnalysisSensitivity = Utility.ConfigHelper.GetAppSetting(@"SnoreAnalysisSensitivity");
            if (!Equals(tmpSnoreAnalysisSensitivity, null))
            {
                SnoreAnalysisSensitivity = tmpSnoreAnalysisSensitivity.GetInt(60);
            }
            var tmpIsSnoreAutoAnalysisWhenOpenFile = Utility.ConfigHelper.GetAppSetting(@"IsSnoreAutoAnalysisWhenOpenFile");
            if (!Equals(tmpIsSnoreAutoAnalysisWhenOpenFile, null))
            {
                IsSnoreAutoAnalysisWhenOpenFile = tmpIsSnoreAutoAnalysisWhenOpenFile.GetBool(true);
            }
        }

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
                ConfigHelper.AddAppSetting(@"ShortestRespiratoryEvent", ShortestRespiratoryEvent.ToString());
                ConfigHelper.AddAppSetting(@"ApneaAirflowDecreasedThreshold", ApneaAirflowDecreasedThreshold.ToString());
                ConfigHelper.AddAppSetting(@"HypopneaAirflowDecreasedThreshold", HypopneaAirflowDecreasedThreshold.ToString());
                ConfigHelper.AddAppSetting(@"OxygenReductionThreshold", OxygenReductionThreshold.ToString());
                ConfigHelper.AddAppSetting(@"IsRespiratoryAutoAnalysisWhenOpenFile", IsRespiratoryAutoAnalysisWhenOpenFile.ToString());

                ConfigHelper.AddAppSetting(@"SnoreAnalysisSensitivity", SnoreAnalysisSensitivity.ToString());
                ConfigHelper.AddAppSetting(@"IsSnoreAutoAnalysisWhenOpenFile", IsSnoreAutoAnalysisWhenOpenFile.ToString());
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
