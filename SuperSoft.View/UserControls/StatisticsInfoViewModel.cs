using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using SuperSoft.View.ViewModel;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using SuperSoft.Utility;
using System;

namespace SuperSoft.View.UserControls
{
    public class StatisticsInfoViewModel : MyNotifyClassBase
    {
        #region 治疗模式不同，显示的统计信息不同

        /// <summary>
        /// 选择治疗模式
        /// </summary>
        private TherapyMode therapyMode;

        /// <summary>
        /// 选择的治疗模式
        /// </summary>
        public TherapyMode TherapyMode
        {
            get { return therapyMode; }
            set
            {
                //if (Equals(therapyMode, value)) return;
                therapyMode = value;
                OnPropertyChanged("TherapyMode");
                setControlIsVisibility();
            }
        }

        private void setControlIsVisibility()
        {
            //CPAP 和APAP模式 和其他的模式不同
            if (TherapyMode == TherapyMode.APAP || TherapyMode == TherapyMode.CPAP)
            {
                RespiratoryRateInfoVisibility = Visibility.Collapsed;
                IERatioInfoVisibility = Visibility.Collapsed;
                IPAPInfoVisibility = Visibility.Collapsed;
                EPAPInfoVisibility = Visibility.Collapsed;
            }
            else
            {
                RespiratoryRateInfoVisibility = Visibility.Visible;
                IERatioInfoVisibility = Visibility.Visible;
                IPAPInfoVisibility = Visibility.Visible;
                EPAPInfoVisibility = Visibility.Visible;
            }
        }

        #endregion

        #region 统计信息

        private StatisticsInfoModel statisticsInfoModel = new StatisticsInfoModel();

        public StatisticsInfoModel StatisticsInfoModel
        {
            get { return statisticsInfoModel; }
            set
            {
                if (Equals(statisticsInfoModel, value)) return;

                statisticsInfoModel = value;
                OnPropertyChanged("StatisticsInfoModel");
            }
        }

        private IEnumerable<ViewProductWorkingStatisticsData> viewProductWorkingStatisticsDataList;

        public IEnumerable<ViewProductWorkingStatisticsData> ViewProductWorkingStatisticsDataList
        {
            get { return viewProductWorkingStatisticsDataList; }
            set
            {
                if (Equals(viewProductWorkingStatisticsDataList, value)) return;
                viewProductWorkingStatisticsDataList = value;
                OnPropertyChanged("ViewProductWorkingStatisticsDataList");
                TaskAsyncHelper.RunAsync(ViewProductWorkingStatisticsDataListChanged,
                    ViewProductWorkingStatisticsDataListChangedComplete);
            }
        }

        private void ViewProductWorkingStatisticsDataListChanged()
        {
            var tmp = ViewProductWorkingStatisticsDataList;

            if (tmp != null && tmp.Count() > 0)
            {
                StatisticsInfoModel.PressureMax = tmp.Average(a => a.PressureMax);
                StatisticsInfoModel.PressureMedian = tmp.Average(a => a.PressureMedian);
                StatisticsInfoModel.PressureP95 = tmp.Average(a => a.PressureP95);

                StatisticsInfoModel.FlowMax = tmp.Average(a => a.FlowMax);
                StatisticsInfoModel.FlowMedian = tmp.Average(a => a.FlowMedian);
                StatisticsInfoModel.FlowP95 = tmp.Average(a => a.FlowP95);

                StatisticsInfoModel.CountAI = Convert.ToSingle(tmp.Average(a => a.CountAI));
                StatisticsInfoModel.CountHI = Convert.ToSingle(tmp.Average(a => a.CountHI));
                StatisticsInfoModel.CountAHI = Convert.ToSingle(tmp.Average(a => a.CountAHI));


                StatisticsInfoModel.LeakMax = tmp.Average(a => a.LeakMax);
                StatisticsInfoModel.LeakMedian = tmp.Average(a => a.LeakMedian);
                StatisticsInfoModel.LeakP95 = tmp.Average(a => a.LeakP95);

                StatisticsInfoModel.TidalVolumeMax = tmp.Average(a => a.TidalVolumeMax);
                StatisticsInfoModel.TidalVolumeMedian = tmp.Average(a => a.TidalVolumeMedian);
                StatisticsInfoModel.TidalVolumeP95 = tmp.Average(a => a.TidalVolumeP95);

                StatisticsInfoModel.RespiratoryRateMax = Convert.ToSingle(tmp.Average(a => a.RespiratoryRateMax));
                StatisticsInfoModel.RespiratoryRateMedian = Convert.ToSingle(tmp.Average(a => a.RespiratoryRateMedian));
                StatisticsInfoModel.RespiratoryRateP95 = Convert.ToSingle(tmp.Average(a => a.RespiratoryRateP95));

                StatisticsInfoModel.MinuteVentilationMax = Convert.ToSingle(tmp.Average(a => a.MinuteVentilationMax));
                StatisticsInfoModel.MinuteVentilationMedian = Convert.ToSingle(tmp.Average(a => a.MinuteVentilationMedian));
                StatisticsInfoModel.MinuteVentilationP95 = Convert.ToSingle(tmp.Average(a => a.MinuteVentilationP95));

                StatisticsInfoModel.SpO2Max = Convert.ToSingle(tmp.Average(a => a.SpO2Max));
                StatisticsInfoModel.SpO2Median = Convert.ToSingle(tmp.Average(a => a.SpO2Median));
                StatisticsInfoModel.SpO2P95 = Convert.ToSingle(tmp.Average(a => a.SpO2P95));

                StatisticsInfoModel.PulseRateMax = Convert.ToSingle(tmp.Average(a => a.PulseRateMax));
                StatisticsInfoModel.PulseRateMedian = Convert.ToSingle(tmp.Average(a => a.PulseRateMedian));
                StatisticsInfoModel.PulseRateP95 = Convert.ToSingle(tmp.Average(a => a.PulseRateP95));

                StatisticsInfoModel.IERatioMax = tmp.Average(a => a.IERatioMax);
                StatisticsInfoModel.IERatioMedian = tmp.Average(a => a.IERatioMedian);
                StatisticsInfoModel.IERatioP95 = tmp.Average(a => a.IERatioP95);

                StatisticsInfoModel.IPAPMax = tmp.Average(a => a.IPAPMax);
                StatisticsInfoModel.IPAPMedian = tmp.Average(a => a.IPAPMedian);
                StatisticsInfoModel.IPAPP95 = tmp.Average(a => a.IPAPP95);

                StatisticsInfoModel.EPAPMax = tmp.Average(a => a.EPAPMax);
                StatisticsInfoModel.EPAPMedian = tmp.Average(a => a.EPAPMedian);
                StatisticsInfoModel.EPAPP95 = tmp.Average(a => a.EPAPP95);

                StatisticsInfoVisibility = Visibility.Visible;
            }
            else
            {
                StatisticsInfoVisibility = Visibility.Collapsed;
            }
        }

        private void ViewProductWorkingStatisticsDataListChangedComplete()
        {
        }

        #endregion

        #region 控件的可见性

        private Visibility statisticsInfoVisibility = Visibility.Collapsed;

        public Visibility StatisticsInfoVisibility
        {
            get { return statisticsInfoVisibility; }

            set
            {
                if (Equals(statisticsInfoVisibility, value)) return;

                statisticsInfoVisibility = value;
                OnPropertyChanged("StatisticsInfoVisibility");
            }
        }

        private Visibility respiratoryRateInfoVisibility;

        public Visibility RespiratoryRateInfoVisibility
        {
            get { return respiratoryRateInfoVisibility; }

            set
            {
                if (Equals(respiratoryRateInfoVisibility, value)) return;

                respiratoryRateInfoVisibility = value;
                OnPropertyChanged("RespiratoryRateInfoVisibility");
            }
        }

        private Visibility iERatioInfoVisibility;

        public Visibility IERatioInfoVisibility
        {
            get { return iERatioInfoVisibility; }

            set
            {
                if (Equals(iERatioInfoVisibility, value)) return;

                iERatioInfoVisibility = value;
                OnPropertyChanged("IERatioInfoVisibility");
            }
        }

        private Visibility iPAPInfoVisibility;

        public Visibility IPAPInfoVisibility
        {
            get { return iPAPInfoVisibility; }

            set
            {
                if (Equals(iPAPInfoVisibility, value)) return;

                iPAPInfoVisibility = value;
                OnPropertyChanged("IPAPInfoVisibility");
            }
        }

        private Visibility ePAPInfoVisibility;

        public Visibility EPAPInfoVisibility
        {
            get { return ePAPInfoVisibility; }

            set
            {
                if (Equals(ePAPInfoVisibility, value)) return;

                ePAPInfoVisibility = value;
                OnPropertyChanged("EPAPInfoVisibility");
            }
        }

        #endregion
    }
}