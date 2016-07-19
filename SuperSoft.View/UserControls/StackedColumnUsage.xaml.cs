using Respircare.PatientManagementSystem.BLL;
using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// StackedColumnUsage.xaml 的交互逻辑
    /// </summary>
    public partial class StackedColumnUsage : UserControl
    {
        public StackedColumnUsage()
        {
            InitializeComponent();
            stackedColumnBrush = FindResource("Value1ColorBrush") as SolidColorBrush;
        }

        #region 内容宽度

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.None,
                    OnContentWidthPropertyChanged));

        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        private static void OnContentWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setContentWidth(date);
        }

        private void setContentWidth(double contentWidth)
        {
            RootPanel.Width = contentWidth;
        }

        #endregion

        #region MarginTop

        public double MarginTop { get; set; } = 5;

        #endregion

        #region MarginBottom

        public double MarginBottom { get; set; } = 5;

        #endregion

        #region 内容高度

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(double), typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(Const.OneStackedColumnHeight(), FrameworkPropertyMetadataOptions.None,
                    OnContentHeightPropertyChanged));

        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        private static void OnContentHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setContentHeight(date);
        }

        private void setContentHeight(double contentHeight)
        {
            if (double.IsNaN(contentHeight)
                || double.IsInfinity(contentHeight)
                || double.IsNegativeInfinity(contentHeight)
                || double.IsPositiveInfinity(contentHeight)
                )
            {
                return;
            }
            Height = contentHeight;
            setDataSource();
        }

        #endregion

        #region 横向偏移量

        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(StackedColumnUsage),
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
            var bar = (StackedColumnUsage)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setHorizontalOffset(date);
        }

        private void setHorizontalOffset(double horizontalOffset)
        {
            RootPanel.Margin = new Thickness(-horizontalOffset, 0, 0, 0);
        }

        #endregion

        #region 标题

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnTitlePropertyChanged));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setTitle(date);
        }

        private void setTitle(string title)
        {
            this.title.Text = title;
        }

        #endregion

        #region 单位

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnUnitPropertyChanged));

        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        private static void OnUnitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setUnit(date);
        }

        private void setUnit(string unit)
        {
            this.unit.Text = unit;
        }

        #endregion

        #region Value3Text

        public static readonly DependencyProperty Value3TextProperty =
            DependencyProperty.Register("Value3Text", typeof(string), typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnValue3TextPropertyChanged));

        public string Value3Text
        {
            get { return (string)GetValue(Value3TextProperty); }
            set { SetValue(Value3TextProperty, value); }
        }

        private static void OnValue3TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setValue3Text(date);
        }

        private void setValue3Text(string value3Text)
        {
            this.value3Text.Text = value3Text;

            if (string.IsNullOrWhiteSpace(value3Text))
            {
                value3Brush.Visibility = Visibility.Collapsed;
                this.value3Text.Visibility = Visibility.Collapsed;
            }
            else
            {
                value3Brush.Visibility = Visibility.Visible;
                this.value3Text.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region  DataSource

        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable<StackedColumnUsageDataItem>),
                typeof(StackedColumnUsage),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnDataSourcePropertyChanged));

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<StackedColumnUsageDataItem> DataSource
        {
            get { return (IEnumerable<StackedColumnUsageDataItem>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        /// <summary>
        /// 数据源改变事件方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumnUsage)d;
            var data = (IEnumerable<StackedColumnUsageDataItem>)e.NewValue;
            if (Equals(data, e.OldValue)) return;
            bar.setDataSource();
        }

        private readonly IList<string> axesYtext = new Collection<string>
        {
            "12:00",
            "15:00",
            "18:00",
            "21:00",
            "00:00",
            "03:00",
            "06:00",
            "09:00",
            "12:00"
        };

        //from StackedColumnItem RootGrid  Margin="3,5,3,5"
        private Thickness thickness = new Thickness(3, 5, 3, 5);
        private readonly DateTimeConverter dateTimeConverter = new DateTimeConverter();

        private readonly SolidColorBrush stackedColumnBrush;

        private void setDataSource()
        {
            RootPanel.Children.Clear();
            RootPanelLine.Children.Clear();

            if (DataSource == null || DataSource.Count() < 1)
            {
                return;
            }
            if (Visibility != Visibility.Visible)
            {
                return;
            }
            var ratio = (Height - MarginTop - MarginBottom) / (Const.OneDayTotalMilliseconds() - 0);
            var IntervalRnage = ratio * (Const.OneDayTotalMilliseconds() / (axesYtext.Count - 1));
            var loopNo = (int)((Height - MarginBottom) / IntervalRnage);

            //生成Y轴刻度和文字
            RootAxesY.Children.Clear();
            for (var i = 0; i <= loopNo; i++)
            {
                var offsetY = Height - MarginBottom - IntervalRnage * i;

                var scaleItem = new TextBlock();
                scaleItem.Width = 27;
                scaleItem.TextAlignment = TextAlignment.Right;
                scaleItem.FontSize = 9;
                scaleItem.Text = axesYtext[i];
                scaleItem.Margin = new Thickness(0, offsetY - 7, 0, 0);
                RootAxesY.Children.Add(scaleItem);
                var myLine = new Line();
                myLine = new Line();
                myLine.Stroke = FindResource("ControlBorderBrush") as SolidColorBrush;
                myLine.StrokeThickness = 0.3;
                myLine.X1 = 0;
                myLine.X2 = ContentWidth;
                myLine.Y1 = offsetY;
                myLine.Y2 = offsetY;
                RootPanelLine.Children.Add(myLine);
            }

            var minTime = DataSource.Min(m => m.DataTime).Date;

            foreach (var v in DataSource)
            {
                if (v.Items.Count() < 1)
                {
                    continue;
                }
                var toolTipStr = new StringBuilder();
                toolTipStr.Append(v.DataTime.ToShortDateString());
                toolTipStr.AppendLine();
                toolTipStr.AppendLine();
                var canvas = new Grid();
                canvas.Width = Const.OneStackedColumnWidth();
                canvas.Height = Height;
                foreach (var rangeData in v.Items.OrderByDescending(a => a.Key))
                {
                    toolTipStr.Append(rangeData.Key);
                    toolTipStr.Append("-");
                    toolTipStr.Append(rangeData.Value);
                    toolTipStr.Append("\t");
                    toolTipStr.Append(
                        dateTimeConverter.Convert(Convert.ToInt64((rangeData.Value - rangeData.Key).TotalMilliseconds),
                            null, null, Thread.CurrentThread.CurrentUICulture));
                    toolTipStr.AppendLine();
                    var rangeCanvas = new Canvas();
                    rangeCanvas.Background = stackedColumnBrush;
                    rangeCanvas.Height = ratio * (rangeData.Value - rangeData.Key).TotalMilliseconds;
                    rangeCanvas.Width = 15; //21-3-3
                    var tmpMarginBottom = ratio * (rangeData.Key - v.DataTime.Date.AddHours(12)).TotalMilliseconds;
                    rangeCanvas.Margin = new Thickness(0, 0, 0, tmpMarginBottom + MarginBottom);
                    rangeCanvas.VerticalAlignment = VerticalAlignment.Bottom;
                    canvas.Children.Add(rangeCanvas);
                }
                canvas.ToolTip = toolTipStr.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                toolTipStr.Clear();
                toolTipStr = null;
                var offsetDay = (int)(v.DataTime - minTime).TotalDays;
                canvas.Margin = new Thickness(offsetDay * Const.OneStackedColumnWidth(), 0, 0, 0);
                RootPanel.Children.Add(canvas);
            }
        }

        #endregion
    }
}