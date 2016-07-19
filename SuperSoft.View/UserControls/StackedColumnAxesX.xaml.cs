using Respircare.PatientManagementSystem.DAL;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// StackedColumnAxesX.xaml 的交互逻辑
    /// </summary>
    public partial class StackedColumnAxesX : UserControl
    {
        private readonly double axesXFontSize = Const.AxesXFontSize();
        private readonly double axesXFontSize1 = Const.AxesXFontSize1();

        public StackedColumnAxesX()
        {
            InitializeComponent();
        }

        #region 内容宽度

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(StackedColumnAxesX),
                new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.None,
                    OnContentWidthPropertyChanged));

        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        private static void OnContentWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnAxesX)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setContentWidth(date);
        }

        private void setContentWidth(double contentWidth)
        {
            RootPanel.Width = contentWidth;
        }

        #endregion

        #region 横向偏移量

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(StackedColumnAxesX),
                new FrameworkPropertyMetadata(default(double),
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHorizontalOffsetPropertyChanged));

        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        private static void OnHorizontalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnAxesX)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setHorizontalOffset(date);
        }

        private void setHorizontalOffset(double horizontalOffset)
        {
            RootPanel.Margin = new Thickness(-horizontalOffset, 0, 0, 0);
        }

        #endregion

        #region  DataSource

        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable<ViewProductWorkingStatisticsData>),
                typeof(StackedColumnAxesX),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnDataSourcePropertyChanged));

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<ViewProductWorkingStatisticsData> DataSource
        {
            get { return (IEnumerable<ViewProductWorkingStatisticsData>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        /// <summary>
        /// 数据源改变事件方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnAxesX)d;
            var data = (IEnumerable<ViewProductWorkingStatisticsData>)e.NewValue;
            if (Equals(data, e.OldValue)) return;
            bar.setDataSource(data);
        }

        private void setDataSource(IEnumerable<ViewProductWorkingStatisticsData> dataSource)
        {
            if (dataSource == null || dataSource.Count() < 1)
            {
                return;
            }
            RootPanel.Children.Clear();
            var minTime = dataSource.Min(m => m.DataTime);
            var maxTime = dataSource.Max(m => m.DataTime);

            for (var i = 0; i <= (maxTime - minTime).TotalDays; i++)
            {
                var dateTime = minTime.AddDays(i);
                var item = new TextBlock();
                item.FontSize = axesXFontSize1;
                item.Width = Const.OneStackedColumnWidth();
                item.VerticalAlignment = VerticalAlignment.Bottom;
                item.TextAlignment = TextAlignment.Center;
                item.Text = ResourceHelper.LoadString("Week" + (int)dateTime.DayOfWeek) + Environment.NewLine +
                            dateTime.Day;
                item.Margin = new Thickness(i * Const.OneStackedColumnWidth(), 12, 0, 0);
                item.ToolTip = dateTime.ToShortDateString() + " (" + ResourceHelper.LoadString("Week" + (int)dateTime.DayOfWeek) + ")";
                RootPanel.Children.Add(item);

                if (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    //星期周末文字颜色
                    item.Foreground = new SolidColorBrush(Colors.Blue);
                }
                if (i == 0 || dateTime.Day == 1)
                {
                    var myLine = new Line();
                    myLine = new Line();
                    myLine.Stroke = FindResource("ControlBorderBrush") as SolidColorBrush;
                    myLine.StrokeThickness = 0.5;
                    myLine.X1 = i * Const.OneStackedColumnWidth();
                    myLine.X2 = myLine.X1;
                    myLine.Y1 = 0;
                    myLine.Y2 = Height;
                    RootPanel.Children.Add(myLine);
                    if (dateTime.GetLastDayOfMonth().Day - dateTime.Day >= 3)
                    {
                        var yearMonth = new TextBlock();
                        yearMonth.FontSize = axesXFontSize;
                        yearMonth.VerticalAlignment = VerticalAlignment.Top;
                        yearMonth.Text = dateTime.GetDateTimeFormats('y')[0];
                        yearMonth.Margin = new Thickness(i * Const.OneStackedColumnWidth() + 5, 0, 0, 0);
                        RootPanel.Children.Add(yearMonth);
                    }
                }
            }
        }

        #endregion
    }
}