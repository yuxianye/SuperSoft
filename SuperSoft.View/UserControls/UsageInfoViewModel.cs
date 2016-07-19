using Respircare.PatientManagementSystem.DAL;
using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Respircare.PatientManagementSystem.Views
{
    public class UsageInfoViewModel : MyNotifyClassBase
    {
        private UsageInfoModel usageInfoModel = new UsageInfoModel();

        private Visibility usageInfoVisibility = Visibility.Collapsed;

        private IEnumerable<ViewProductWorkingStatisticsData> viewProductWorkingStatisticsDataList;

        public Visibility UsageInfoVisibility
        {
            get { return usageInfoVisibility; }
            set
            {
                if (Equals(usageInfoVisibility, value)) return;
                usageInfoVisibility = value;
                OnPropertyChanged("UsageInfoVisibility");
            }
        }

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

        public UsageInfoModel UsageInfoModel
        {
            get { return usageInfoModel; }
            set
            {
                if (Equals(usageInfoModel, value)) return;
                usageInfoModel = value;
                OnPropertyChanged("UsageInfoModel");
            }
        }

        private void ViewProductWorkingStatisticsDataListChanged()
        {
            if (ViewProductWorkingStatisticsDataList != null && ViewProductWorkingStatisticsDataList.Count() > 0)
            {
                //ViewProductWorkingStatisticsDataList.
                var totalDays = (int)(EndTime - StartTime).Value.TotalDays + 1;
                var totalUseDays = ViewProductWorkingStatisticsDataList.Count();

                UsageInfoModel.DaysWithProductUsage = totalDays;
                UsageInfoModel.DaysWithoutProductUsage = totalDays - totalUseDays;
                UsageInfoModel.PercentDaysWithProductUsage = totalUseDays / (float)totalDays * 100;
                UsageInfoModel.CumulativeUsage = ViewProductWorkingStatisticsDataList.Sum(a => a.TotalUsage);
                UsageInfoModel.MaximumUsage = ViewProductWorkingStatisticsDataList.Max(a => a.TotalUsage);
                UsageInfoModel.AverageUsageAllDays = (int)(UsageInfoModel.CumulativeUsage / totalDays);
                UsageInfoModel.AverageUsageDaysUsed = (int)(UsageInfoModel.CumulativeUsage / totalUseDays);
                UsageInfoModel.MinimumUsage = ViewProductWorkingStatisticsDataList.Min(a => a.TotalUsage);
                UsageInfoModel.PercentOfDaysWithUsageGreaterThanXHours =
                    ViewProductWorkingStatisticsDataList.Where(a => a.TotalUsage >= Const.ComplianceThreshold).Count() /
                    (float)totalDays * 100;
                UsageInfoModel.PercentOfDaysWithUsageLessThanXHours = 100 -
                                                                      UsageInfoModel
                                                                          .PercentOfDaysWithUsageGreaterThanXHours;

                UsageInfoVisibility = Visibility.Visible;
            }
            else
            {
                UsageInfoVisibility = Visibility.Collapsed;
            }
        }

        private void ViewProductWorkingStatisticsDataListChangedComplete()
        {
        }

        #region StartTime

        private DateTime? startTime;

        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (Equals(startTime, value)) return;
                startTime = value;
                OnPropertyChanged("StartTime");
                UsageInfoModel.StartTime = value;
            }
        }

        #endregion

        #region EndTime

        private DateTime? endTime;

        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (Equals(endTime, value)) return;
                endTime = value;
                OnPropertyChanged("EndTime");
                UsageInfoModel.EndTime = value;
            }
        }

        #endregion
    }
}