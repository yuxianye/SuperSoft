using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using SuperSoft.View.ViewModel;
using System.Windows;

namespace SuperSoft.View.UserControls

{
    public class ProductConfigInfoViewModel : MyViewModelBase
    {
        /// <summary>
        /// 治疗模式改变，根据模式不同显示不同的内容
        /// </summary>
        private void OnTherapyModeChanged()
        {
            switch (TherapyMode)
            {
                case TherapyMode.ST:
                    IPAPInfoVisibility = Visibility.Visible;
                    EPAPInfoVisibility = Visibility.Visible;
                    RiseTimeInfoVisibility = Visibility.Visible;
                    RespiratoryRateInfoVisibility = Visibility.Visible;
                    InspireTimeInfoVisibility = Visibility.Visible;
                    ITriggerInfoVisibility = Visibility.Visible;
                    ETriggerInfoVisibility = Visibility.Visible;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Collapsed;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
                case TherapyMode.T:
                    IPAPInfoVisibility = Visibility.Visible;
                    EPAPInfoVisibility = Visibility.Visible;
                    RiseTimeInfoVisibility = Visibility.Visible;
                    RespiratoryRateInfoVisibility = Visibility.Visible;
                    InspireTimeInfoVisibility = Visibility.Visible;
                    ITriggerInfoVisibility = Visibility.Collapsed;
                    ETriggerInfoVisibility = Visibility.Collapsed;
                    ExhaleTimeInfoVisibility = Visibility.Visible;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Collapsed;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
                case TherapyMode.S:
                    IPAPInfoVisibility = Visibility.Visible;
                    EPAPInfoVisibility = Visibility.Visible;
                    RiseTimeInfoVisibility = Visibility.Visible;
                    RespiratoryRateInfoVisibility = Visibility.Collapsed;
                    InspireTimeInfoVisibility = Visibility.Collapsed;
                    ITriggerInfoVisibility = Visibility.Visible;
                    ETriggerInfoVisibility = Visibility.Visible;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Collapsed;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
                case TherapyMode.CPAP:
                    IPAPInfoVisibility = Visibility.Collapsed;
                    EPAPInfoVisibility = Visibility.Collapsed;
                    RiseTimeInfoVisibility = Visibility.Collapsed;
                    RespiratoryRateInfoVisibility = Visibility.Collapsed;
                    InspireTimeInfoVisibility = Visibility.Collapsed;
                    ITriggerInfoVisibility = Visibility.Collapsed;
                    ETriggerInfoVisibility = Visibility.Collapsed;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Visible;
                    CFlexInfoVisibility = Visibility.Visible;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
                case TherapyMode.APAP:
                    IPAPInfoVisibility = Visibility.Collapsed;
                    EPAPInfoVisibility = Visibility.Collapsed;
                    RiseTimeInfoVisibility = Visibility.Collapsed;
                    RespiratoryRateInfoVisibility = Visibility.Collapsed;
                    InspireTimeInfoVisibility = Visibility.Collapsed;
                    ITriggerInfoVisibility = Visibility.Collapsed;
                    ETriggerInfoVisibility = Visibility.Collapsed;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Visible;
                    CPAPStartInfoVisibility = Visibility.Visible;
                    CPAPMaxInfoVisibility = Visibility.Visible;
                    CPAPMinInfoVisibility = Visibility.Visible;
                    break;
                case TherapyMode.PCV:
                    IPAPInfoVisibility = Visibility.Visible;
                    EPAPInfoVisibility = Visibility.Visible;
                    RiseTimeInfoVisibility = Visibility.Visible;
                    RespiratoryRateInfoVisibility = Visibility.Visible;
                    InspireTimeInfoVisibility = Visibility.Visible;
                    ITriggerInfoVisibility = Visibility.Visible;
                    ETriggerInfoVisibility = Visibility.Collapsed;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Collapsed;
                    EPAPMinInfoVisibility = Visibility.Collapsed;
                    PSMaxInfoVisibility = Visibility.Collapsed;
                    PSMinInfoVisibility = Visibility.Collapsed;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Collapsed;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
                case TherapyMode.AutoS:
                    IPAPInfoVisibility = Visibility.Collapsed;
                    EPAPInfoVisibility = Visibility.Collapsed;
                    RiseTimeInfoVisibility = Visibility.Visible;
                    RespiratoryRateInfoVisibility = Visibility.Collapsed;
                    InspireTimeInfoVisibility = Visibility.Collapsed;
                    ITriggerInfoVisibility = Visibility.Visible;
                    ETriggerInfoVisibility = Visibility.Visible;
                    ExhaleTimeInfoVisibility = Visibility.Collapsed;
                    IPAPMaxInfoVisibility = Visibility.Visible;
                    EPAPMinInfoVisibility = Visibility.Visible;
                    PSMaxInfoVisibility = Visibility.Visible;
                    PSMinInfoVisibility = Visibility.Visible;
                    CPAPInfoVisibility = Visibility.Collapsed;
                    CFlexInfoVisibility = Visibility.Collapsed;
                    CPAPStartInfoVisibility = Visibility.Collapsed;
                    CPAPMaxInfoVisibility = Visibility.Collapsed;
                    CPAPMinInfoVisibility = Visibility.Collapsed;
                    break;
            }
        }

        #region ProductConfigInfoVisibility

        private Visibility productConfigInfoVisibility = Visibility.Collapsed;

        public Visibility ProductConfigInfoVisibility
        {
            get { return productConfigInfoVisibility; }
            set { Set(ref productConfigInfoVisibility, value); }
        }

        #endregion

        #region ViewProductWorkingSummaryData

        private ViewProductWorkingSummaryData viewProductWorkingSummaryData;

        /// <summary>
        /// 配置数据
        /// </summary>
        public ViewProductWorkingSummaryData ViewProductWorkingSummaryData
        {
            get { return viewProductWorkingSummaryData; }
            set
            {
                Set(ref viewProductWorkingSummaryData, value);
                if (Equals(value, null))
                {
                    ProductConfigInfoVisibility = Visibility.Collapsed;
                }
                else
                {
                    ProductConfigInfoVisibility = Visibility.Visible;
                }
            }
        }

        #endregion

        #region ProductConfigInfoModel

        private ProductConfigInfoModel productConfigInfoModel = new ProductConfigInfoModel();

        public ProductConfigInfoModel ProductConfigInfoModel
        {
            get { return productConfigInfoModel; }
            set { Set(ref productConfigInfoModel, value); }
        }

        #endregion

        #region TherapyMode

        private TherapyMode therapyMode;

        public TherapyMode TherapyMode
        {
            get { return therapyMode; }
            set
            {
                Set(ref therapyMode, value);
                OnTherapyModeChanged();
                //TaskAsyncHelper.RunAsync(OnTherapyModeChanged, OnTherapyModeChangedComplete);
            }
        }

        #endregion

        #region 设置项的可见性属性

        #region IPAPInfoVisibility

        private Visibility iPAPInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// IPAPInfoVisibility
        /// </summary>
        public Visibility IPAPInfoVisibility
        {
            get { return iPAPInfoVisibility; }
            set { Set(ref iPAPInfoVisibility, value); }
        }

        #endregion

        #region EPAPInfoVisibility

        /// <summary>
        /// EPAPInfoVisibility
        /// </summary>
        private Visibility ePAPInfoVisibility = Visibility.Collapsed;

        public Visibility EPAPInfoVisibility
        {
            get { return ePAPInfoVisibility; }
            set { Set(ref ePAPInfoVisibility, value); }
        }

        #endregion

        #region RsingTimeInfoVisibility

        private Visibility riseTimeInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// RiseTimeInfoVisibility
        /// </summary>
        public Visibility RiseTimeInfoVisibility
        {
            get { return riseTimeInfoVisibility; }
            set { Set(ref riseTimeInfoVisibility, value); }
        }

        #endregion

        #region RespiratoryRateInfoVisibility

        private Visibility respiratoryRateInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 呼吸频率respiratoryRateInfoVisibility
        /// </summary>
        public Visibility RespiratoryRateInfoVisibility
        {
            get { return respiratoryRateInfoVisibility; }
            set { Set(ref respiratoryRateInfoVisibility, value); }
        }

        #endregion

        #region InspireTimeInfoVisibility

        private Visibility inspireTimeInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 吸气时间InspireTimeInfoVisibility
        /// </summary>
        public Visibility InspireTimeInfoVisibility
        {
            get { return inspireTimeInfoVisibility; }
            set { Set(ref inspireTimeInfoVisibility, value); }
        }

        #endregion

        #region ITriggerInfoVisibility

        private Visibility iTriggerInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 吸气灵敏度iTriggerInfoVisibility
        /// </summary>
        public Visibility ITriggerInfoVisibility
        {
            get { return iTriggerInfoVisibility; }
            set { Set(ref iTriggerInfoVisibility, value); }
        }

        #endregion

        #region ETriggerInfoVisibility

        private Visibility eTriggerInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 呼气灵敏度eTriggerInfoVisibility
        /// </summary>
        public Visibility ETriggerInfoVisibility
        {
            get { return eTriggerInfoVisibility; }
            set { Set(ref eTriggerInfoVisibility, value); }
        }

        #endregion

        #region ExhaleTimeInfoVisibility

        private Visibility exhaleTimeInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 呼气时间ExhaleTimeInfoVisibility
        /// </summary>
        public Visibility ExhaleTimeInfoVisibility
        {
            get { return exhaleTimeInfoVisibility; }
            set { Set(ref exhaleTimeInfoVisibility, value); }
        }

        #endregion

        #region IPAPMaxInfoVisibility

        private Visibility iPAPMaxInfoVisibility = Visibility.Collapsed;


        /// <summary>
        /// IPAPMaxInfoVisibility
        /// </summary>
        public Visibility IPAPMaxInfoVisibility
        {
            get { return iPAPMaxInfoVisibility; }
            set { Set(ref iPAPMaxInfoVisibility, value); }
        }

        #endregion

        #region EPAPMinInfoVisibility

        private Visibility ePAPMinInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// EPAPMinInfoVisibility
        /// </summary>
        public Visibility EPAPMinInfoVisibility
        {
            get { return ePAPMinInfoVisibility; }
            set { Set(ref ePAPMinInfoVisibility, value); }
        }

        #endregion

        #region PSMaxInfoVisibility

        private Visibility pSMaxInfoVisibility = Visibility.Collapsed;


        /// <summary>
        /// PSMaxInfoVisibility
        /// </summary>
        public Visibility PSMaxInfoVisibility
        {
            get { return pSMaxInfoVisibility; }
            set { Set(ref pSMaxInfoVisibility, value); }
        }

        #endregion

        #region PSMinInfoVisibility

        private Visibility pSMinInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// PSMinInfoVisibility
        /// </summary>
        public Visibility PSMinInfoVisibility
        {
            get { return pSMinInfoVisibility; }
            set { Set(ref pSMinInfoVisibility, value); }
        }

        #endregion

        #region CPAPInfoVisibility

        private Visibility cPAPInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// CPAPInfoVisibility
        /// </summary>
        public Visibility CPAPInfoVisibility
        {
            get { return cPAPInfoVisibility; }
            set { Set(ref cPAPInfoVisibility, value); }
        }

        #endregion

        #region CFlexInfoVisibility

        private Visibility cFlexInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// 舒适程度CFlexInfoVisibility
        /// </summary>
        public Visibility CFlexInfoVisibility
        {
            get { return cFlexInfoVisibility; }
            set { Set(ref cFlexInfoVisibility, value); }
        }

        #endregion

        #region CPAPStartInfoVisibility

        private Visibility cPAPStartInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// CPAPStartInfoVisibility
        /// </summary>
        public Visibility CPAPStartInfoVisibility
        {
            get { return cPAPStartInfoVisibility; }
            set { Set(ref cPAPStartInfoVisibility, value); }
        }

        #endregion

        #region CPAPMaxInfoVisibility

        private Visibility cPAPMaxInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// CPAPMaxInfoVisibility
        /// </summary>
        public Visibility CPAPMaxInfoVisibility
        {
            get { return cPAPMaxInfoVisibility; }
            set { Set(ref cPAPMaxInfoVisibility, value); }
        }

        #endregion

        #region CPAPMinInfoVisibility

        private Visibility cPAPMinInfoVisibility = Visibility.Collapsed;

        /// <summary>
        /// CPAPMinInfoVisibility
        /// </summary>
        public Visibility CPAPMinInfoVisibility
        {
            get { return cPAPMinInfoVisibility; }
            set { Set(ref cPAPMinInfoVisibility, value); }
        }

        #endregion

        #endregion

        #region IsExpanded

        private bool isExpanded = true;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { Set(ref isExpanded, value); }
        }

        #endregion
    }
}