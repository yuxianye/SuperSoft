using Respircare.PatientManagementSystem.BLL;
using Respircare.PatientManagementSystem.DAL;
using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// 一共13个通道
    /// </summary>
    public class StackedColumnListViewModel : MyNotifyClassBase
    {
        private double contentHeight = Const.OneStackedColumnHeight();

        private double contentWidth;

        private double horizontalOffset;

        public double ContentWidth
        {
            get { return contentWidth; }
            set
            {
                if (Equals(contentWidth, value)) return;
                contentWidth = value;
                OnPropertyChanged("ContentWidth");
            }
        }

        public double ContentHeight
        {
            get { return contentHeight; }
            set
            {
                if (Equals(contentHeight, value)) return;
                contentHeight = value;
                OnPropertyChanged("ContentHeight");
            }
        }

        public double HorizontalOffset
        {
            get { return horizontalOffset; }
            set
            {
                if (Equals(horizontalOffset, value)) return;
                horizontalOffset = value;
                OnPropertyChanged("HorizontalOffset");
            }
        }

        #region StackedColumnListVisibility

        private Visibility stackedColumnListVisibility = Visibility.Collapsed;

        public Visibility StackedColumnListVisibility
        {
            get
            {
                return stackedColumnListVisibility;
                //System.Diagnostics.Debug.Print("get stackedColumnListVisibility" + stackedColumnListVisibility);
            }
            set
            {
                if (Equals(stackedColumnListVisibility, value)) return;
                stackedColumnListVisibility = value;
                OnPropertyChanged("StackedColumnListVisibility");
                //System.Diagnostics.Debug.Print("OnPropertyChanged StackedColumnListVisibility" + stackedColumnListVisibility);

            }
        }

        #endregion

        #region ViewProductWorkingStatisticsDataList

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
            if (ViewProductWorkingStatisticsDataList != null && ViewProductWorkingStatisticsDataList.Count() > 0)
            {
                StackedColumnListVisibility = Visibility.Visible;

                //总天数乘以每天柱形的宽度，
                ContentWidth =
                    ((ViewProductWorkingStatisticsDataList.Max(a => a.DataTime) -
                      ViewProductWorkingStatisticsDataList.Min(a => a.DataTime)).TotalDays + 1)
                    * Const.OneStackedColumnWidth();

                if (TotalUsageInfoVisibility == Visibility.Visible)
                {
                    TotalUsageDataList = from a in ViewProductWorkingStatisticsDataList
                                         select new StackedColumnDataItem
                                         {
                                             DataTime = a.DataTime,
                                             Value3 = Convert.ToSingle((a.TotalUsage.HasValue ? Convert.ToDouble(a.TotalUsage.Value) / Convert.ToDouble(Const.OneHourTotalMilliseconds()) : 0)),
                                             MaxValue = 24,
                                             MinValue = 0
                                         };
                }

                //使用时间段的数据
                var tmpUsageDataList = new Collection<StackedColumnUsageDataItem>();
                var viewProductWorkingSummaryDataBLL = new ViewProductWorkingSummaryDataBLL();
                foreach (var v in ViewProductWorkingStatisticsDataList)
                {
                    var startTime = v.DataTime.Date.AddHours(12);
                    var endTime = startTime.AddHours(24);
                    var item = new StackedColumnUsageDataItem();
                    Expression<Func<ViewProductWorkingSummaryData, bool>> condition = t =>
                        t.PatientId == StaticDatas.CurrentSelectedPatient.Id && t.TherapyMode == v.TherapyMode &&
                        t.StartTime >= startTime && t.EndTime < endTime;

                    var viewProductWorkingSummaryDataList = viewProductWorkingSummaryDataBLL.GetByCondition(condition);
                    item.DataTime = v.DataTime;
                    item.Items = (from a in viewProductWorkingSummaryDataList
                                  select new KeyValuePair<DateTime, DateTime>(a.StartTime, a.EndTime)).ToList();
                    tmpUsageDataList.Add(item);
                }
                viewProductWorkingSummaryDataBLL.Dispose();
                viewProductWorkingSummaryDataBLL = null;
                UsageDataList = tmpUsageDataList;

                if (AHIInfoVisibility == Visibility.Visible)
                {
                    AHIDataList = null;
                    AHIDataList = (from a in ViewProductWorkingStatisticsDataList
                                   select new StackedColumnDataItem
                                   {
                                       DataTime = a.DataTime,
                                       Value3 = a.CountAHI.HasValue ? a.CountAHI.Value : 0,
                                       Value2 = a.CountAI.HasValue ? a.CountAI.Value : 0,
                                       MaxValue = 40,
                                       MinValue = 0
                                   }).ToList();
                }
                if (PressureInfoVisibility == Visibility.Visible)
                {
                    PressureDataList = null;
                    PressureDataList = (from a in ViewProductWorkingStatisticsDataList
                                        select new StackedColumnDataItem
                                        {
                                            DataTime = a.DataTime,
                                            Value3 = (a.PressureMax.HasValue ? a.PressureMax.Value : 0),
                                            Value2 = (a.PressureP95.HasValue ? a.PressureP95.Value : 0),
                                            Value1 = (a.PressureMedian.HasValue ? a.PressureMedian.Value : 0),
                                            MaxValue = 35,
                                            MinValue = 0
                                        }).ToList();
                }
                if (FlowInfoVisibility == Visibility.Visible)
                {
                    FlowDataList = null;
                    FlowDataList = (from a in ViewProductWorkingStatisticsDataList
                                    select new StackedColumnDataItem
                                    {
                                        DataTime = a.DataTime,
                                        Value3 = (a.FlowMax.HasValue ? a.FlowMax.Value : 0),
                                        Value2 = (a.FlowP95.HasValue ? a.FlowP95.Value : 0),
                                        Value1 = (a.FlowMedian.HasValue ? a.FlowMedian.Value : 0),
                                        MaxValue = 200,
                                        MinValue = -200
                                    }).ToList();
                }
                if (LeakInfoVisibility == Visibility.Visible)
                {
                    LeakDataList = null;
                    LeakDataList = (from a in ViewProductWorkingStatisticsDataList
                                    select new StackedColumnDataItem
                                    {
                                        DataTime = a.DataTime,
                                        Value3 = (a.LeakMax.HasValue ? a.LeakMax.Value : 0),
                                        Value2 = (a.LeakP95.HasValue ? a.LeakP95.Value : 0),
                                        Value1 = (a.LeakMedian.HasValue ? a.LeakMedian.Value : 0),
                                        MaxValue = 100,
                                        MinValue = 0
                                    }).ToList();
                }
                if (TidalVolumeInfoVisibility == Visibility.Visible)
                {
                    TidalVolumeDataList = null;
                    TidalVolumeDataList = (from a in ViewProductWorkingStatisticsDataList
                                           select new StackedColumnDataItem
                                           {
                                               DataTime = a.DataTime,
                                               Value3 = (a.TidalVolumeMax.HasValue ? a.TidalVolumeMax.Value : 0),
                                               Value2 = (a.TidalVolumeP95.HasValue ? a.TidalVolumeP95.Value : 0),
                                               Value1 = (a.TidalVolumeMedian.HasValue ? a.TidalVolumeMedian.Value : 0),
                                               MaxValue = 2500,
                                               MinValue = 0
                                           }).ToList();
                }
                if (RespiratoryRateInfoVisibility == Visibility.Visible)
                {
                    RespiratoryRateDataList = null;
                    RespiratoryRateDataList = (from a in ViewProductWorkingStatisticsDataList
                                               select new StackedColumnDataItem
                                               {
                                                   DataTime = a.DataTime,
                                                   Value3 = (a.RespiratoryRateMax.HasValue ? a.RespiratoryRateMax.Value : 0),
                                                   Value2 = (a.RespiratoryRateP95.HasValue ? a.RespiratoryRateP95.Value : 0),
                                                   Value1 = (a.RespiratoryRateMedian.HasValue ? a.RespiratoryRateMedian.Value : 0),
                                                   MaxValue = 60,
                                                   MinValue = 0
                                               }).ToList();
                }
                if (MinuteVentilationInfoVisibility == Visibility.Visible)
                {
                    MinuteVentilationDataList = null;
                    MinuteVentilationDataList = (from a in ViewProductWorkingStatisticsDataList
                                                 select new StackedColumnDataItem
                                                 {
                                                     DataTime = a.DataTime,
                                                     Value3 =
                                                        (a.MinuteVentilationMax.HasValue ? a.MinuteVentilationMax.Value : 0),
                                                     Value2 =
                                                        (a.MinuteVentilationP95.HasValue ? a.MinuteVentilationP95.Value : 0),
                                                     Value1 =
                                                        (a.MinuteVentilationMedian.HasValue
                                                             ? a.MinuteVentilationMedian.Value
                                                             : 0),
                                                     MaxValue = 30,
                                                     MinValue = 0
                                                 }).ToList();
                }
                if (SpO2InfoVisibility == Visibility.Visible)
                {
                    SpO2DataList = null;
                    SpO2DataList = (from a in ViewProductWorkingStatisticsDataList
                                    select new StackedColumnDataItem
                                    {
                                        DataTime = a.DataTime,
                                        Value3 = (a.SpO2Max.HasValue ? a.SpO2Max.Value : 0),
                                        Value2 = (a.SpO2P95.HasValue ? a.SpO2P95.Value : 0),
                                        Value1 = (a.SpO2Median.HasValue ? a.SpO2Median.Value : 0),
                                        MaxValue = 100,
                                        MinValue = 0
                                    }).ToList();
                }
                if (PulseRateInfoVisibility == Visibility.Visible)
                {
                    PulseRateDataList = null;
                    PulseRateDataList = (from a in ViewProductWorkingStatisticsDataList
                                         select new StackedColumnDataItem
                                         {
                                             DataTime = a.DataTime,
                                             Value3 = (a.PulseRateMax.HasValue ? a.PulseRateMax.Value : 0),
                                             Value2 = (a.PulseRateP95.HasValue ? a.PulseRateP95.Value : 0),
                                             Value1 = (a.PulseRateMedian.HasValue ? a.PulseRateMedian.Value : 0),
                                             MaxValue = 255,
                                             MinValue = 0
                                         }).ToList();
                }
                if (IERatioInfoVisibility == Visibility.Visible)
                {
                    IERatioDataList = null;
                    IERatioDataList = (from a in ViewProductWorkingStatisticsDataList
                                       select new StackedColumnDataItem
                                       {
                                           DataTime = a.DataTime,
                                           Value3 = (a.IERatioMax.HasValue ? a.IERatioMax.Value : 0),
                                           Value2 = (a.IERatioP95.HasValue ? a.IERatioP95.Value : 0),
                                           Value1 = (a.IERatioMedian.HasValue ? a.IERatioMedian.Value : 0),
                                           MaxValue = 10,
                                           MinValue = 0
                                       }).ToList();
                }
                if (IPAPInfoVisibility == Visibility.Visible)
                {
                    IPAPDataList = null;
                    IPAPDataList = (from a in ViewProductWorkingStatisticsDataList
                                    select new StackedColumnDataItem
                                    {
                                        DataTime = a.DataTime,
                                        Value3 = (a.IPAPMax.HasValue ? a.IPAPMax.Value : 4),
                                        Value2 = (a.IPAPP95.HasValue ? a.IPAPP95.Value : 4),
                                        Value1 = (a.IPAPMedian.HasValue ? a.IPAPMedian.Value : 4),
                                        MaxValue = 25,
                                        MinValue = 4
                                    }).ToList();
                }
                if (EPAPInfoVisibility == Visibility.Visible)
                {
                    EPAPDataList = null;
                    EPAPDataList = (from a in ViewProductWorkingStatisticsDataList
                                    select new StackedColumnDataItem
                                    {
                                        DataTime = a.DataTime,
                                        Value3 = a.EPAPMax.HasValue ? a.EPAPMax.Value : 4,
                                        Value2 = a.EPAPP95.HasValue ? a.EPAPP95.Value : 4,
                                        Value1 = a.EPAPMedian.HasValue ? a.EPAPMedian.Value : 4,
                                        MaxValue = 20,
                                        MinValue = 4
                                    }).ToList();
                }
            }
            else
            {
                //都没有数据则隐藏
                StackedColumnListVisibility = Visibility.Collapsed;
            }
        }

        private void ViewProductWorkingStatisticsDataListChangedComplete()
        {
        }

        #endregion

        #region 各个通道的数据源

        #region TotalUsageDataList

        private IEnumerable<StackedColumnDataItem> totalUsageDataList;

        public IEnumerable<StackedColumnDataItem> TotalUsageDataList
        {
            get { return totalUsageDataList; }
            set
            {
                if (Equals(totalUsageDataList, value)) return;
                totalUsageDataList = value;
                OnPropertyChanged("TotalUsageDataList");
            }
        }

        #endregion

        #region UsageDataList

        private IEnumerable<StackedColumnUsageDataItem> usageDataList;

        public IEnumerable<StackedColumnUsageDataItem> UsageDataList
        {
            get { return usageDataList; }
            set
            {
                if (Equals(usageDataList, value)) return;
                usageDataList = value;
                OnPropertyChanged("UsageDataList");
            }
        }

        #endregion

        #region AHIDataList

        private IEnumerable<StackedColumnDataItem> aHIDataList;

        public IEnumerable<StackedColumnDataItem> AHIDataList
        {
            get { return aHIDataList; }
            set
            {
                if (Equals(aHIDataList, value)) return;
                aHIDataList = value;
                OnPropertyChanged("AHIDataList");
            }
        }

        #endregion

        #region PressureDataList

        private IEnumerable<StackedColumnDataItem> pressureDataList;

        public IEnumerable<StackedColumnDataItem> PressureDataList
        {
            get { return pressureDataList; }
            set
            {
                if (Equals(pressureDataList, value)) return;
                pressureDataList = value;
                OnPropertyChanged("PressureDataList");
            }
        }

        #endregion

        #region FlowDataList

        private IEnumerable<StackedColumnDataItem> flowDataList;

        public IEnumerable<StackedColumnDataItem> FlowDataList
        {
            get { return flowDataList; }
            set
            {
                if (Equals(flowDataList, value)) return;
                flowDataList = value;
                OnPropertyChanged("FlowDataList");
            }
        }

        #endregion

        #region LeakDataList

        private IEnumerable<StackedColumnDataItem> leakDataList;

        public IEnumerable<StackedColumnDataItem> LeakDataList
        {
            get { return leakDataList; }
            set
            {
                if (Equals(leakDataList, value)) return;
                leakDataList = value;
                OnPropertyChanged("LeakDataList");
            }
        }

        #endregion

        #region TidalVolumeDataList

        private IEnumerable<StackedColumnDataItem> tidalVolumeDataList;

        public IEnumerable<StackedColumnDataItem> TidalVolumeDataList
        {
            get { return tidalVolumeDataList; }
            set
            {
                if (Equals(tidalVolumeDataList, value)) return;
                tidalVolumeDataList = value;
                OnPropertyChanged("TidalVolumeDataList");
            }
        }

        #endregion

        #region RespiratoryRateDataList

        private IEnumerable<StackedColumnDataItem> respiratoryRateDataList;

        public IEnumerable<StackedColumnDataItem> RespiratoryRateDataList
        {
            get { return respiratoryRateDataList; }
            set
            {
                if (Equals(respiratoryRateDataList, value)) return;
                respiratoryRateDataList = value;
                OnPropertyChanged("RespiratoryRateDataList");
            }
        }

        #endregion

        #region MinuteVentilationDataList

        private IEnumerable<StackedColumnDataItem> minuteVentilationDataList;

        public IEnumerable<StackedColumnDataItem> MinuteVentilationDataList
        {
            get { return minuteVentilationDataList; }
            set
            {
                if (Equals(minuteVentilationDataList, value)) return;
                minuteVentilationDataList = value;
                OnPropertyChanged("MinuteVentilationDataList");
            }
        }

        #endregion

        #region SpO2DataList

        private IEnumerable<StackedColumnDataItem> spO2DataList;

        public IEnumerable<StackedColumnDataItem> SpO2DataList
        {
            get { return spO2DataList; }
            set
            {
                if (Equals(spO2DataList, value)) return;
                spO2DataList = value;
                OnPropertyChanged("SpO2DataList");
            }
        }

        #endregion

        #region PulseRateDataList

        private IEnumerable<StackedColumnDataItem> pulseRateDataList;

        public IEnumerable<StackedColumnDataItem> PulseRateDataList
        {
            get { return pulseRateDataList; }
            set
            {
                if (Equals(pulseRateDataList, value)) return;
                pulseRateDataList = value;
                OnPropertyChanged("PulseRateDataList");
            }
        }

        #endregion

        #region IERatioDataList

        private IEnumerable<StackedColumnDataItem> iERatioDataList;

        public IEnumerable<StackedColumnDataItem> IERatioDataList
        {
            get { return iERatioDataList; }
            set
            {
                if (Equals(iERatioDataList, value)) return;
                iERatioDataList = value;
                OnPropertyChanged("IERatioDataList");
            }
        }

        #endregion

        #region  IPAPDataList

        private IEnumerable<StackedColumnDataItem> iPAPDataList;

        public IEnumerable<StackedColumnDataItem> IPAPDataList
        {
            get { return iPAPDataList; }
            set
            {
                if (Equals(iPAPDataList, value)) return;
                iPAPDataList = value;
                OnPropertyChanged("IPAPDataList");
            }
        }

        #endregion

        #region  EPAPDataList

        private IEnumerable<StackedColumnDataItem> ePAPDataList;

        public IEnumerable<StackedColumnDataItem> EPAPDataList
        {
            get { return ePAPDataList; }
            set
            {
                if (Equals(ePAPDataList, value)) return;
                ePAPDataList = value;
                OnPropertyChanged("EPAPDataList");
            }
        }

        #endregion

        #endregion

        #region 控件的可见性

        private Visibility aHIInfoVisibility;

        public Visibility AHIInfoVisibility
        {
            get { return aHIInfoVisibility; }
            set
            {
                if (Equals(aHIInfoVisibility, value)) return;
                aHIInfoVisibility = value;
                OnPropertyChanged("AHIInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility pressureInfoVisibility;

        public Visibility PressureInfoVisibility
        {
            get { return pressureInfoVisibility; }
            set
            {
                if (Equals(pressureInfoVisibility, value)) return;
                pressureInfoVisibility = value;
                OnPropertyChanged("PressureInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility flowInfoVisibility;

        public Visibility FlowInfoVisibility
        {
            get { return flowInfoVisibility; }
            set
            {
                if (Equals(flowInfoVisibility, value)) return;
                flowInfoVisibility = value;
                OnPropertyChanged("FlowInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility leakInfoVisibility;

        public Visibility LeakInfoVisibility
        {
            get { return leakInfoVisibility; }
            set
            {
                if (Equals(leakInfoVisibility, value)) return;
                leakInfoVisibility = value;
                OnPropertyChanged("LeakInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility tidalVolumeInfoVisibility;

        public Visibility TidalVolumeInfoVisibility
        {
            get { return tidalVolumeInfoVisibility; }
            set
            {
                if (Equals(tidalVolumeInfoVisibility, value)) return;
                tidalVolumeInfoVisibility = value;
                OnPropertyChanged("TidalVolumeInfoVisibility");
                checkIsShowChannel();
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
                checkIsShowChannel();
            }
        }

        private Visibility minuteVentilationInfoVisibility;

        public Visibility MinuteVentilationInfoVisibility
        {
            get { return minuteVentilationInfoVisibility; }
            set
            {
                if (Equals(minuteVentilationInfoVisibility, value)) return;
                minuteVentilationInfoVisibility = value;
                OnPropertyChanged("MinuteVentilationInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility spO2InfoVisibility;

        public Visibility SpO2InfoVisibility
        {
            get { return spO2InfoVisibility; }
            set
            {
                if (Equals(spO2InfoVisibility, value)) return;
                spO2InfoVisibility = value;
                OnPropertyChanged("SpO2InfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility pulseRateInfoVisibility;

        public Visibility PulseRateInfoVisibility
        {
            get { return pulseRateInfoVisibility; }
            set
            {
                if (Equals(pulseRateInfoVisibility, value)) return;
                pulseRateInfoVisibility = value;
                OnPropertyChanged("PulseRateInfoVisibility");
                checkIsShowChannel();
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
                checkIsShowChannel();
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
                checkIsShowChannel();
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
                checkIsShowChannel();
            }
        }

        private Visibility totalUsageInfoVisibility;

        public Visibility TotalUsageInfoVisibility
        {
            get { return totalUsageInfoVisibility; }
            set
            {
                if (Equals(totalUsageInfoVisibility, value)) return;
                totalUsageInfoVisibility = value;
                OnPropertyChanged("TotalUsageInfoVisibility");
                checkIsShowChannel();
            }
        }

        private Visibility usageInfoVisibility;

        public Visibility UsageInfoVisibility
        {
            get { return usageInfoVisibility; }
            set
            {
                if (Equals(usageInfoVisibility, value)) return;
                usageInfoVisibility = value;
                OnPropertyChanged("UsageInfoVisibility");
                checkIsShowChannel();
            }
        }

        private void checkIsShowChannel()
        {
            if (ViewProductWorkingStatisticsDataList != null && ViewProductWorkingStatisticsDataList.Count() > 0)
            {
                if (AHIInfoVisibility == Visibility.Visible
                    || PressureInfoVisibility == Visibility.Visible
                    || FlowInfoVisibility == Visibility.Visible
                    || LeakInfoVisibility == Visibility.Visible
                    || TidalVolumeInfoVisibility == Visibility.Visible
                    || RespiratoryRateInfoVisibility == Visibility.Visible
                    || MinuteVentilationInfoVisibility == Visibility.Visible
                    || SpO2InfoVisibility == Visibility.Visible
                    || PulseRateInfoVisibility == Visibility.Visible
                    || IERatioInfoVisibility == Visibility.Visible
                    || IPAPInfoVisibility == Visibility.Visible
                    || EPAPInfoVisibility == Visibility.Visible
                    || TotalUsageInfoVisibility == Visibility.Visible
                    || UsageInfoVisibility == Visibility.Visible
                    )
                {
                    StackedColumnListVisibility = Visibility.Visible;
                }
                else
                {
                    StackedColumnListVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                StackedColumnListVisibility = Visibility.Collapsed;
            }

            if (!Equals(OnChannelChanged, null))
            {
                OnChannelChanged(this, null);
            }
        }

        public event EventHandler OnChannelChanged;

        #endregion
    }
}