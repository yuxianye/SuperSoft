using Respircare.Utility;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// StackedColumnList.xaml 的交互逻辑
    /// </summary>
    public partial class StackedColumnList : UserControl
    {
        private StackedColumnListViewModel stackedColumnListViewModel;

        public StackedColumnList()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (Equals(stackedColumnListViewModel, null))
            {
                stackedColumnListViewModel = DataContext as StackedColumnListViewModel;
                stackedColumnListViewModel.OnChannelChanged += StackedColumnListViewModel_OnChannelChanged;
            }
            if (!Equals(stackedColumnListViewModel, null))
            {
                stackedColumnListViewModel.HorizontalOffset = e.HorizontalOffset;
                var padding = new Thickness(e.HorizontalOffset, 0, 0, 0);
                StackedColumnTotalUsage.Margin = padding;
                StackedColumnUsage.Padding = padding;
                StackedColumnAHI.Padding = padding;
                StackedColumnPressure.Padding = padding;
                StackedColumnFlow.Padding = padding;
                StackedColumnLeak.Padding = padding;
                StackedColumnTidalVolume.Padding = padding;
                StackedColumnRespiratoryRate.Padding = padding;
                StackedColumnMinuteVentilation.Padding = padding;
                StackedColumnSpO2.Padding = padding;
                StackedColumnPulseRate.Padding = padding;
                StackedColumnIERatio.Padding = padding;
                StackedColumnIPAP.Padding = padding;
                StackedColumnEPAP.Padding = padding;
            }
        }

        private void StackedColumnListViewModel_OnChannelChanged(object sender, EventArgs e)
        {
            computerChannelHeight();
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            computerChannelHeight();
        }

        private void computerChannelHeight()
        {
            if (!Equals(stackedColumnListViewModel, null))
            {
                var visibilityChannelCount = 0;

                #region 计算可见通道的数量,一共14个通道

                if (stackedColumnListViewModel.AHIInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.PressureInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.FlowInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.LeakInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.TidalVolumeInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.RespiratoryRateInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.MinuteVentilationInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.SpO2InfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.PulseRateInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.IERatioInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.IPAPInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.EPAPInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.TotalUsageInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }
                if (stackedColumnListViewModel.UsageInfoVisibility == Visibility.Visible)
                {
                    visibilityChannelCount = visibilityChannelCount + 1;
                }

                #endregion

                var computerHeight = ScrollViewer.ActualHeight / visibilityChannelCount;

                if (computerHeight > Const.OneStackedColumnHeight())
                {
                    stackedColumnListViewModel.ContentHeight = computerHeight;
                }
                else
                {
                    stackedColumnListViewModel.ContentHeight = Const.OneStackedColumnHeight();
                }
            }
        }
    }
}