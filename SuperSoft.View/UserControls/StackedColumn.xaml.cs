using Respircare.Log;
using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// StackedColumn.xaml 的交互逻辑
    /// </summary>
    public partial class StackedColumn : UserControl
    {
        private double interval;
        private double maxY;
        private double minY;

        private Point oldPoint;

        public StackedColumn()
        {
            InitializeComponent();
        }

        #region MarginTop

        public double MarginTop { get; set; } = 5;

        #endregion

        #region MarginBottom

        public double MarginBottom { get; set; } = 5;

        #endregion

        private void RootAxesY_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition((Canvas)sender);
                //改变坐标的大小和图像的大小
                if (Equals(oldPoint, null) || oldPoint.X == 0)
                {
                    oldPoint = point;
                }
                else
                {
                    if (point.Y - oldPoint.Y > 10) //缩小
                    {
                        oldPoint = point; //new System.Windows.Point();
                        MoveDown();
                        oldPoint = new Point();
                    }
                    else if (oldPoint.Y - point.Y > 10) //放大
                    {
                        oldPoint = point; // new System.Windows.Point();
                        MoveUp();
                        oldPoint = new Point();
                    }
                }
            }
        }

        private void MoveDown()
        {
            maxY = maxY + interval;
            var vv = DataSource.FirstOrDefault().MinValue;
            if (maxY > 10000)
            {
                return;
            }
            drawAxesY();
            drawColoum();
        }

        private void MoveUp()
        {
            maxY = maxY - interval;
            if (maxY <= minY)
            {
                return;
            }
            drawAxesY();
            drawColoum();
        }

        #region 内容宽度

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.None,
                    OnContentWidthPropertyChanged));

        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        private static void OnContentWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
            var date = (double)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setContentWidth(date);
        }

        private void setContentWidth(double contentWidth)
        {
            RootPanel.Width = contentWidth;
        }

        #endregion

        #region 内容高度

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(double), typeof(StackedColumn),
                new FrameworkPropertyMetadata(Const.OneStackedColumnHeight(), FrameworkPropertyMetadataOptions.None,
                    OnContentHeightPropertyChanged));

        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        private static void OnContentHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
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
            DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(StackedColumn),
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
            var bar = (StackedColumn)d;
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
            DependencyProperty.Register("Title", typeof(string), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnTitlePropertyChanged));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
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
            DependencyProperty.Register("Unit", typeof(string), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnUnitPropertyChanged));

        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        private static void OnUnitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setUnit(date);
        }

        private void setUnit(string unit)
        {
            this.unit.Text = unit;
        }

        #endregion

        #region Value1Text

        public static readonly DependencyProperty Value1TextProperty =
            DependencyProperty.Register("Value1Text", typeof(string), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnValue1TextPropertyChanged));

        public string Value1Text
        {
            get { return (string)GetValue(Value1TextProperty); }
            set { SetValue(Value1TextProperty, value); }
        }

        private static void OnValue1TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setValue1Text(date);
        }

        private void setValue1Text(string value1Text)
        {
            this.value1Text.Text = value1Text;
            if (string.IsNullOrWhiteSpace(value1Text))
            {
                value1Brush.Visibility = Visibility.Collapsed;
                this.value1Text.Visibility = Visibility.Collapsed;
            }
            else
            {
                value1Brush.Visibility = Visibility.Visible;
                this.value1Text.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Value2Text

        public static readonly DependencyProperty Value2TextProperty =
            DependencyProperty.Register("Value2Text", typeof(string), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnValue2TextPropertyChanged));

        public string Value2Text
        {
            get { return (string)GetValue(Value2TextProperty); }
            set { SetValue(Value2TextProperty, value); }
        }

        private static void OnValue2TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
            var date = (string)e.NewValue;
            if (Equals(date, e.OldValue)) return;
            bar.setValue2Text(date);
        }

        private void setValue2Text(string value2Text)
        {
            this.value2Text.Text = value2Text;


            if (string.IsNullOrWhiteSpace(value2Text))
            {
                value2Brush.Visibility = Visibility.Collapsed;
                this.value2Text.Visibility = Visibility.Collapsed;
            }
            else
            {
                value2Brush.Visibility = Visibility.Visible;
                this.value2Text.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Value3Text

        public static readonly DependencyProperty Value3TextProperty =
            DependencyProperty.Register("Value3Text", typeof(string), typeof(StackedColumn),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
                    OnValue3TextPropertyChanged));

        public string Value3Text
        {
            get { return (string)GetValue(Value3TextProperty); }
            set { SetValue(Value3TextProperty, value); }
        }

        private static void OnValue3TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
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
            DependencyProperty.Register("DataSource", typeof(IEnumerable<StackedColumnDataItem>),
                typeof(StackedColumn),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnDataSourcePropertyChanged));

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable<StackedColumnDataItem> DataSource
        {
            get { return (IEnumerable<StackedColumnDataItem>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        /// <summary>
        /// 数据源改变事件方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (StackedColumn)d;
            var data = (IEnumerable<StackedColumnDataItem>)e.NewValue;
            if (Equals(data, e.OldValue)) return;
            bar.setDataSource();
        }

        private void setDataSource()
        {
            RootPanel.Children.Clear();

            if (DataSource == null || DataSource.Count() < 1)
            {
                return;
            }
            if (Visibility != Visibility.Visible)
            {
                return;
            }

            maxY = DataSource.FirstOrDefault().MaxValue;
            minY = DataSource.FirstOrDefault().MinValue;
            drawAxesY();
            drawColoum();
        }

        private void drawAxesY()
        {
            //生成Y轴刻度和文字
            RootAxesY.Children.Clear();
            RootPanelLine.Children.Clear();

            var axisManager = new AxisManager(maxY, minY);
            axisManager.AxisMaximumValue = maxY;
            axisManager.AxisMinimumValue = minY;
            axisManager.Calculate();

            var ratio = (Height - MarginTop - MarginBottom) / (maxY - minY);

            interval = axisManager.Interval;
            var intervalRange = ratio * axisManager.Interval;

            var loopNo = (int)((Height - MarginBottom) / intervalRange);
            for (var i = 0; i <= loopNo; i++)
            {
                var offsetY = Height - MarginBottom - intervalRange * i;

                var scaleItem = new TextBlock();
                scaleItem.Width = 27;
                scaleItem.TextAlignment = TextAlignment.Right;
                scaleItem.FontSize = 9;
                scaleItem.Text = (minY + axisManager.Interval * i).ToString();
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
                //Log.LogHelper.Info("this.Width:" + this.Width + "  this.ActualWidth:" + this.ActualWidth + "  this.ContentWidth:" + this.ContentWidth);
            }
            axisManager = null;
        }

        private void drawColoum()
        {
            RootPanel.Children.Clear();
            //生成柱形
            var minTime = DataSource.Min(m => m.DataTime);
            foreach (var v in DataSource)
            {
                var item = new StackedColumnItem();
                item.Width = Const.OneStackedColumnWidth();
                item.Height = Height;
                item.MaxValue = maxY; // v.MaxValue;
                item.MinValue = minY; // v.MinValue;
                item.Value3 = v.Value3;
                item.Value2 = v.Value2;
                item.Value1 = v.Value1;
                var offsetDay = (int)(v.DataTime - minTime).Value.TotalDays;
                item.Margin = new Thickness(offsetDay * Const.OneStackedColumnWidth(), 0, 0, 0);
                RootPanel.Children.Add(item);

                var sb = new StringBuilder();
                if (v.DataTime.HasValue)
                {
                    sb.Append(v.DataTime.Value.ToShortDateString());
                    sb.AppendLine();
                    sb.AppendLine();
                }
                if (v.Value3.HasValue)
                {
                    sb.Append(v.Value3);
                    sb.AppendLine();
                }
                if (v.Value2.HasValue)
                {
                    sb.Append(v.Value2);
                    sb.AppendLine();
                }
                if (v.Value1.HasValue)
                {
                    sb.Append(v.Value1);
                }

                item.ToolTip = sb.ToString().TrimEnd(Environment.NewLine.ToCharArray());
                sb.Clear();
                sb = null;
            }
        }

        #endregion
    }
}