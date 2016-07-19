using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public class ProductConfigInfoModel : MyNotifyClassBase
    {
        #region IPAP

        private float? iPAP;

        /// <summary>
        /// IPAP
        /// </summary>
        public float? IPAP
        {
            get { return iPAP; }
            set
            {
                if (Equals(iPAP, value)) return;
                iPAP = value;
                OnPropertyChanged("IPAP");
            }
        }

        #endregion

        #region EPAP

        private float? ePAP;

        /// <summary>
        /// EPAP
        /// </summary>
        public float? EPAP
        {
            get { return ePAP; }
            set
            {
                if (Equals(ePAP, value)) return;
                ePAP = value;
                OnPropertyChanged("EPAP");
            }
        }

        #endregion

        #region RsingTime

        private int? rsingTime;

        /// <summary>
        /// 上升时间
        /// </summary>
        public int? RsingTime
        {
            get { return rsingTime; }
            set
            {
                if (Equals(rsingTime, value)) return;
                rsingTime = value;
                OnPropertyChanged("RsingTime");
            }
        }

        #endregion

        #region RespiratoryRate

        private int? respiratoryRate;

        /// <summary>
        /// 呼吸频率
        /// </summary>
        public int? RespiratoryRate
        {
            get { return respiratoryRate; }
            set
            {
                if (Equals(respiratoryRate, value)) return;
                respiratoryRate = value;
                OnPropertyChanged("RespiratoryRate");
            }
        }

        #endregion

        #region InspiratoryTime

        private int? inspiratoryTime;

        /// <summary>
        /// 吸气时间
        /// </summary>
        public int? InspiratoryTime
        {
            get { return inspiratoryTime; }
            set
            {
                if (Equals(inspiratoryTime, value)) return;
                inspiratoryTime = value;
                OnPropertyChanged("InspiratoryTime");
            }
        }

        #endregion

        #region ITrigger

        private int? iTrigger;

        /// <summary>
        /// 吸气灵敏度
        /// </summary>
        public int? ITrigger
        {
            get { return iTrigger; }
            set
            {
                if (Equals(iTrigger, value)) return;
                iTrigger = value;
                OnPropertyChanged("ITrigger");
            }
        }

        #endregion

        #region ETrigger

        private int? eTrigger;

        /// <summary>
        /// 呼气灵敏度
        /// </summary>
        public int? ETrigger
        {
            get { return eTrigger; }
            set
            {
                if (Equals(eTrigger, value)) return;
                eTrigger = value;
                OnPropertyChanged("ETrigger");
            }
        }

        #endregion

        #region Ramp

        private int? ramp;

        /// <summary>
        /// 延时升压
        /// </summary>
        public int? Ramp
        {
            get { return ramp; }
            set
            {
                if (Equals(ramp, value)) return;
                ramp = value;
                OnPropertyChanged("Ramp");
            }
        }

        #endregion

        #region ExhaleTime

        private int? exhaleTime;

        /// <summary>
        /// 呼气时间
        /// </summary>
        public int? ExhaleTime
        {
            get { return exhaleTime; }
            set
            {
                if (Equals(exhaleTime, value)) return;
                exhaleTime = value;
                OnPropertyChanged("ExhaleTime");
            }
        }

        #endregion

        #region IPAPMax

        private float? iPAPMax;


        /// <summary>
        /// IPAPMax
        /// </summary>
        public float? IPAPMax
        {
            get { return iPAPMax; }
            set
            {
                if (Equals(iPAPMax, value)) return;
                iPAPMax = value;
                OnPropertyChanged("IPAPMax");
            }
        }

        #endregion

        #region EPAPMin

        private float? ePAPMin;

        /// <summary>
        /// IPAPMin
        /// </summary>
        public float? EPAPMin
        {
            get { return ePAPMin; }
            set
            {
                if (Equals(ePAPMin, value)) return;
                ePAPMin = value;
                OnPropertyChanged("EPAPMin");
            }
        }

        #endregion

        #region PSMax

        private float? pSMax;


        /// <summary>
        /// IPAPMax
        /// </summary>
        public float? PSMax
        {
            get { return pSMax; }
            set
            {
                if (Equals(pSMax, value)) return;
                pSMax = value;
                OnPropertyChanged("PSMax");
            }
        }

        #endregion

        #region PSMin

        private float? pSMin;

        /// <summary>
        /// IPAPMax
        /// </summary>
        public float? PSMin
        {
            get { return pSMin; }
            set
            {
                if (Equals(pSMin, value)) return;
                pSMin = value;
                OnPropertyChanged("PSMin");
            }
        }

        #endregion

        #region CPAP

        private float? cPAP;

        /// <summary>
        /// CPAP
        /// </summary>
        public float? CPAP
        {
            get { return cPAP; }
            set
            {
                if (Equals(cPAP, value)) return;
                cPAP = value;
                OnPropertyChanged("CPAP");
            }
        }

        #endregion

        #region CFlex

        private int? cFlex;

        /// <summary>
        /// 舒适程度
        /// </summary>
        public int? CFlex
        {
            get { return cFlex; }
            set
            {
                if (Equals(cFlex, value)) return;
                cFlex = value;
                OnPropertyChanged("CFlex");
            }
        }

        #endregion

        #region CPAPStart

        private float? cPAPStart;

        /// <summary>
        /// CPAPStart
        /// </summary>
        public float? CPAPStart
        {
            get { return cPAPStart; }
            set
            {
                if (Equals(cPAPStart, value)) return;
                cPAPStart = value;
                OnPropertyChanged("CPAPStart");
            }
        }

        #endregion

        #region CPAPMax

        private float? cPAPMax;

        /// <summary>
        /// CPAPMax
        /// </summary>
        public float? CPAPMax
        {
            get { return cPAPMax; }
            set
            {
                if (Equals(cPAPMax, value)) return;
                cPAPMax = value;
                OnPropertyChanged("CPAPMax");
            }
        }

        #endregion

        #region CPAPMin

        private float? cPAPMin;

        /// <summary>
        /// CPAPMin
        /// </summary>
        public float? CPAPMin
        {
            get { return cPAPMin; }
            set
            {
                if (Equals(cPAPMin, value)) return;
                cPAPMin = value;
                OnPropertyChanged("CPAPMin");
            }
        }

        #endregion

        #region Alert

        private int? alert;

        /// <summary>
        /// 报警总开关
        /// </summary>
        public int? Alert
        {
            get { return alert; }
            set
            {
                if (Equals(alert, value)) return;
                alert = value;
                OnPropertyChanged("Alert");
            }
        }

        #endregion

        #region Alert_Tube

        private int? alert_Tube;

        /// <summary>
        /// 面罩报警
        /// </summary>
        public int? Alert_Tube
        {
            get { return alert_Tube; }
            set
            {
                if (Equals(alert_Tube, value)) return;
                alert_Tube = value;
                OnPropertyChanged("Alert_Tube");
            }
        }

        #endregion

        #region Alert_Apnea

        private int? alert_Apnea;

        /// <summary>
        /// AI报警
        /// </summary>
        public int? Alert_Apnea
        {
            get { return alert_Apnea; }
            set
            {
                if (Equals(alert_Apnea, value)) return;
                alert_Apnea = value;
                OnPropertyChanged("Alert_Apnea");
            }
        }

        #endregion

        #region Alert_MinuteVentilation

        private int? alert_MinuteVentilation;

        /// <summary>
        /// 分钟通气量报警
        /// </summary>
        public int? Alert_MinuteVentilation
        {
            get { return alert_MinuteVentilation; }
            set
            {
                if (Equals(alert_MinuteVentilation, value)) return;
                alert_MinuteVentilation = value;
                OnPropertyChanged("Alert_MinuteVentilation");
            }
        }

        #endregion

        #region Alert_HRate

        private int? alert_HRate;

        /// <summary>
        /// 高呼吸频率报警
        /// </summary>
        public int? Alert_HRate
        {
            get { return alert_HRate; }
            set
            {
                if (Equals(alert_HRate, value)) return;
                alert_HRate = value;
                OnPropertyChanged("Alert_HRate");
            }
        }

        #endregion

        #region Alert_LRate

        private int? alert_LRate;

        /// <summary>
        /// 低呼吸频率报警
        /// </summary>
        public int? Alert_LRate
        {
            get { return alert_LRate; }
            set
            {
                if (Equals(alert_LRate, value)) return;
                alert_LRate = value;
                OnPropertyChanged("Alert_LRate");
            }
        }

        #endregion

        #region Config_HumidifierLevel

        private int? config_HumidifierLevel;

        /// <summary>
        /// 湿化器档位
        /// </summary>
        public int? Config_HumidifierLevel
        {
            get { return config_HumidifierLevel; }
            set
            {
                if (Equals(config_HumidifierLevel, value)) return;
                config_HumidifierLevel = value;
                OnPropertyChanged("Config_HumidifierLevel");
            }
        }

        #endregion

        #region Config_DataStore

        private int? config_DataStore;

        /// <summary>
        /// 数据保存
        /// </summary>
        public int? Config_DataStore
        {
            get { return config_DataStore; }
            set
            {
                if (Equals(config_DataStore, value)) return;
                config_DataStore = value;
                OnPropertyChanged("Config_DataStore");
            }
        }

        #endregion

        #region Config_SmartStart

        private int? config_SmartStart;

        /// <summary>
        /// 智能启动
        /// </summary>
        public int? Config_SmartStart
        {
            get { return config_SmartStart; }
            set
            {
                if (Equals(config_SmartStart, value)) return;
                config_SmartStart = value;
                OnPropertyChanged("Config_SmartStart");
            }
        }

        #endregion

        #region Config_PressureUnit

        private int? config_PressureUnit;

        /// <summary>
        /// 压力单位
        /// </summary>
        public int? Config_PressureUnit
        {
            get { return config_PressureUnit; }
            set
            {
                if (Equals(config_PressureUnit, value)) return;
                config_PressureUnit = value;
                OnPropertyChanged("Config_PressureUnit");
            }
        }

        #endregion

        #region Config_Language

        private int? config_Language;

        /// <summary>
        /// 界面语言
        /// </summary>
        public int? Config_Language
        {
            get { return config_Language; }
            set
            {
                if (Equals(config_Language, value)) return;
                config_Language = value;
                OnPropertyChanged("Config_Language");
            }
        }

        #endregion

        #region Config_Backlight

        private int? config_Backlight;

        /// <summary>
        /// 背光模式
        /// </summary>
        public int? Config_Backlight
        {
            get { return config_Backlight; }
            set
            {
                if (Equals(config_Backlight, value)) return;
                config_Backlight = value;
                OnPropertyChanged("Config_Backlight");
            }
        }

        #endregion

        #region Config_MaskPressure

        private int? config_MaskPressure;

        /// <summary>
        /// 面罩压力
        /// </summary>
        public int? Config_MaskPressure
        {
            get { return config_MaskPressure; }
            set
            {
                if (Equals(config_MaskPressure, value)) return;
                config_MaskPressure = value;
                OnPropertyChanged("Config_MaskPressure");
            }
        }

        #endregion

        #region Config_ClinicalSet

        private int? config_ClinicalSet;

        /// <summary>
        /// 临床设置
        /// </summary>
        public int? Config_ClinicalSet
        {
            get { return config_ClinicalSet; }
            set
            {
                if (Equals(config_ClinicalSet, value)) return;
                config_ClinicalSet = value;
                OnPropertyChanged("Config_ClinicalSet");
            }
        }

        #endregion
    }
}
