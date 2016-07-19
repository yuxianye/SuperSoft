using Respircare.Log;
using Respircare.PatientManagementSystem.BLL;
using Respircare.PatientManagementSystem.Models;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Brush = System.Drawing.Brush;
using Point = System.Windows.Point;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// WaveLine.xaml 的交互逻辑
    /// </summary>
    public partial class WaveLine : UserControl
    {
        public WaveLine()
        {
            InitializeComponent();
            ScaleYBrush = FindResource("AccentColorBrush") as SolidColorBrush;
            waveLineXImg.MarginTop = 5;
            waveLineXImg.MarginBottom = 5;
            waveLineXImg.ImgHeight = Convert.ToSingle(Height);
            waveLineXImg.ImgVisualHeight = waveLineXImg.ImgHeight;
            waveLineXImg.OnImageSourceChanged += waveLineXImg_OnImageSourceChanged;
        }

        #region 私有变量

        /// <summary>
        /// 最小y值，用于垂直缩放和绘图
        /// </summary>
        private float minY;
        /// <summary>
        /// 最大y值，用于垂直缩放和绘图
        /// </summary>
        private float maxY;

        /// <summary>
        /// 刻度的间隔值，用于垂直缩放和绘制，刻度数值的变化幅度
        /// </summary>
        private float interval;

        /// <summary>
        /// 旧位置，用于垂直缩放时，鼠标移动的参考
        /// </summary>
        private Point oldPoint;

        /// <summary>
        /// y轴刻度的颜色
        /// </summary>
        private readonly SolidColorBrush ScaleYBrush;

        /// <summary>
        /// 曲线绘制对象
        /// </summary>
        private readonly WaveLineXImg waveLineXImg = new WaveLineXImg();

        #endregion

        #region y轴放大缩小

        /// <summary>
        /// Y轴鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            var vv = DataSource.MinValueY;
            if (maxY > 10000)
            {
                return;
            }
            //drawScaleY();
            DrawLine();
        }

        private void MoveUp()
        {
            maxY = maxY - interval;
            if (maxY <= minY)
            {
                return;
            }
            //drawScaleY();
            DrawLine();
        }

        #endregion

        #region 曲线的颜色

        //public static readonly DependencyProperty LineBrushProperty =
        //    DependencyProperty.Register("LineBrush", typeof(SolidBrush), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(SolidBrush), FrameworkPropertyMetadataOptions.None,
        //            OnLineBrushPropertyChanged));

        ///// <summary>
        ///// 曲线的颜色
        ///// </summary>
        //public Brush LineBrush
        //{
        //    get { return (Brush)GetValue(LineBrushProperty); }
        //    set { SetValue(LineBrushProperty, value); }
        //}

        //private static void OnLineBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (SolidBrush)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setLineBrush(data);
        //}

        //private void setLineBrush(SolidBrush data)
        //{
        //    waveLineXImg.GraphicsForeground = data;
        //    drawLine();
        //}
        private SolidBrush lineBrush = default(SolidBrush);

        /// <summary>
        /// 曲线的颜色
        /// </summary>
        public SolidBrush LineBrush
        {
            get
            {
                return lineBrush;
            }
            set
            {
                lineBrush = value;
                waveLineXImg.GraphicsForeground = lineBrush;
            }
        }


        #endregion

        #region 可见视图的宽度 

        //public static readonly DependencyProperty ViewportWidthProperty =
        //    DependencyProperty.Register("ViewportWidth", typeof(float), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(float), FrameworkPropertyMetadataOptions.None,
        //            OnViewportWidthPropertyChanged));

        ///// <summary>
        ///// 可见视图的宽度
        ///// </summary>
        //public float ViewportWidth
        //{
        //    get { return (float)GetValue(ViewportWidthProperty); }
        //    set { SetValue(ViewportWidthProperty, value); }
        //}

        //private static void OnViewportWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (float)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setViewportWidth(data);
        //}

        //private void setViewportWidth(float data)
        //{
        //    Width = data;
        //    waveLineXImg.ImgVisualWidth = data - Const.LengndWidth;
        //    waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth *
        //                           (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(data));
        //    drawLine();
        //    drawScaleY();
        //}

        private float viewportWidth;
        /// <summary>
        /// 可见视图的宽度
        /// </summary>
        public float ViewportWidth
        {
            get { return viewportWidth; }
            set
            {
                viewportWidth = value;
                Width = value;
                waveLineXImg.ImgVisualWidth = value - Const.LengndWidth;
                waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth * (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(ViewTime));
                //System.Diagnostics.Debug.Print("waveLineXImg.ImgWidth :" + waveLineXImg.ImgWidth);
            }
        }


        #endregion

        #region  时间视图

        //public static readonly DependencyProperty ViewTimeProperty =
        //    DependencyProperty.Register("ViewTime", typeof(int), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.None,
        //            OnViewTimePropertyChanged));

        //public int ViewTime
        //{
        //    get { return (int)GetValue(ViewTimeProperty); }
        //    set { SetValue(ViewTimeProperty, value); }
        //}

        //private static void OnViewTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (int)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setViewTime(data);
        //}

        //private void setViewTime(int data)
        //{
        //    waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth *
        //                            (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(data));
        //    drawLine();
        //}
        private int viewTime;
        public int ViewTime
        {
            get
            {
                return viewTime;
            }
            set
            {
                viewTime = value;
                waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth * (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(value));
            }
        }
        #endregion

        #region 内容高度

        //public static readonly DependencyProperty ContentHeightProperty =
        //    DependencyProperty.Register("ContentHeight", typeof(double), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(Const.OneStackedColumnHeight(), FrameworkPropertyMetadataOptions.None,
        //            OnContentHeightPropertyChanged));

        //public double ContentHeight
        //{
        //    get { return (double)GetValue(ContentHeightProperty); }
        //    set { SetValue(ContentHeightProperty, value); }
        //}

        //private static void OnContentHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (double)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setContentHeight(data);
        //}

        //private void setContentHeight(double data)
        //{
        //    Height = data;
        //    waveLineXImg.ImgHeight = data;
        //    waveLineXImg.ImgVisualHeight = waveLineXImg.ImgHeight;
        //    RootAxesY.Height = data;
        //    drawLine();
        //    drawScaleY();
        //}
        private float contentHeight;
        public float ContentHeight
        {
            get
            {
                return contentHeight;
            }
            set
            {
                contentHeight = value;
                Height = value;
                waveLineXImg.ImgHeight = value;
                waveLineXImg.ImgVisualHeight = waveLineXImg.ImgHeight;
                RootAxesY.Height = value;
            }
        }
        #endregion

        #region 横向偏移量

        //public static readonly DependencyProperty HorizontalOffsetProperty =
        //    DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(double),
        //            FrameworkPropertyMetadataOptions.AffectsArrange |
        //            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHorizontalOffsetPropertyChanged));

        //public double HorizontalOffset
        //{
        //    get { return (double)GetValue(HorizontalOffsetProperty); }
        //    set { SetValue(HorizontalOffsetProperty, value); }
        //}

        //private static void OnHorizontalOffsetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (double)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setHorizontalOffset(data);
        //}

        //private void setHorizontalOffset(double data)
        //{
        //    waveLineXImg.ScrollImgH = Convert.ToSingle(data);
        //    drawLine();
        //}
        private double horizontalOffset;
        public double HorizontalOffset
        {
            get
            {

                return horizontalOffset;
            }
            set
            {
                horizontalOffset = value;
                waveLineXImg.ScrollImgH = Convert.ToSingle(value);

            }
        }

        #endregion

        #region 标题

        //public static readonly DependencyProperty TitleProperty =
        //    DependencyProperty.Register("Title", typeof(string), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
        //            OnTitlePropertyChanged));

        //public string Title
        //{
        //    get { return (string)GetValue(TitleProperty); }
        //    set { SetValue(TitleProperty, value); }
        //}

        //private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var date = (string)e.NewValue;
        //    if (Equals(date, e.OldValue)) return;
        //    bar.setTitle(date);
        //}

        //private void setTitle(string title)
        //{
        //    this.title.Text = title;
        //}
        //private string title;

        //public string Title
        //{
        //    get { return title.Text; }
        //    set
        //    {

        //        this.title.Text = value;
        //    }
        //}

        #endregion

        #region 单位

        //public static readonly DependencyProperty UnitProperty =
        //    DependencyProperty.Register("Unit", typeof(string), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.None,
        //            OnUnitPropertyChanged));

        //public string Unit
        //{
        //    get { return (string)GetValue(UnitProperty); }
        //    set { SetValue(UnitProperty, value); }
        //}

        //private static void OnUnitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var date = (string)e.NewValue;
        //    if (Equals(date, e.OldValue)) return;
        //    bar.setUnit(date);
        //}

        //private void setUnit(string unit)
        //{
        //    this.unit.Text = unit;
        //}
        //public string Unit
        //{
        //    get { return unit.Text; }
        //    set
        //    {
        //        this.unit.Text = value;
        //    }
        //}


        #endregion

        #region Y轴刻度的文字

        //public static readonly DependencyProperty ScaleYDictionaryProperty =
        //    DependencyProperty.Register("ScaleYDictionary", typeof(Dictionary<double, string>), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(default(Dictionary<double, string>), FrameworkPropertyMetadataOptions.None,
        //            OnScaleYDictionaryPropertyChanged));

        //public Dictionary<double, string> ScaleYDictionary
        //{
        //    get { return (Dictionary<double, string>)GetValue(ScaleYDictionaryProperty); }
        //    set { SetValue(ScaleYDictionaryProperty, value); }
        //}

        //private static void OnScaleYDictionaryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var date = (Dictionary<double, string>)e.NewValue;
        //    if (Equals(date, e.OldValue)) return;
        //    bar.setScaleYDictionary(date);
        //}

        //private void setScaleYDictionary(Dictionary<double, string> unit)
        //{
        //    drawScaleY();
        //}


        public Dictionary<double, string> ScaleYDictionary { get; set; }

        #endregion

        #region  DataSource

        ///// <summary>
        ///// 数据源
        ///// </summary>
        //public static readonly DependencyProperty DataSourceProperty =
        //    DependencyProperty.Register("DataSource", typeof(WaveLineDataModel), typeof(WaveLine),
        //        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnDataSourcePropertyChanged));

        ///// <summary>
        ///// 数据源
        ///// </summary>
        //public WaveLineDataModel DataSource
        //{
        //    get { return (WaveLineDataModel)GetValue(DataSourceProperty); }
        //    set { SetValue(DataSourceProperty, value); }
        //}

        ///// <summary>
        ///// 数据源改变事件方法
        ///// </summary>
        ///// <param name="d"></param>
        ///// <param name="e"></param>
        //private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLine)d;
        //    var data = (WaveLineDataModel)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setDataSource();
        //}

        //private void setDataSource()
        //{
        //    if (DataSource == null || DataSource.Items == null || DataSource.Items.Count() < 1)
        //    {
        //        return;
        //    }
        //    waveLineXImg.MinValueX = DataSource.DataTime.Date.AddHours(12);
        //    waveLineXImg.MaxValueX = waveLineXImg.MinValueX.AddHours(24);

        //    minY = Convert.ToSingle(DataSource.MinValueY);
        //    maxY = Convert.ToSingle(DataSource.MaxValueY);
        //    waveLineXImg.DataSource = DataSource.Items;
        //    drawScaleY();
        //    drawLine();
        //}

        private WaveLineDataModel dataSource;

        public WaveLineDataModel DataSource
        {
            get
            {
                return dataSource;

            }
            set
            {
                dataSource = value;
                if (!Equals(value, null))
                {
                    waveLineXImg.MinValueX = DataSource.DataTime.Date.AddHours(12);
                    waveLineXImg.MaxValueX = waveLineXImg.MinValueX.AddHours(24);
                    minY = Convert.ToSingle(DataSource.MinValueY);
                    maxY = Convert.ToSingle(DataSource.MaxValueY);
                    waveLineXImg.DataSource = DataSource.Items;
                }

            }
        }
        #endregion

        #region 时间或者视图大小改变之后，重新绘制时间坐标和曲线

        public void DrawScaleY()
        {
            RootAxesY.Children.Clear();
            waveLineImgLine.Children.Clear();
            if (ViewTime <= 0 || Width <= 0)
            {
                return;
            }
            if (DataSource == null || DataSource.Items == null || DataSource.Items.Count() < 1)
            {
                return;
            }
            try
            {
                //生成Y轴刻度和文字 如果指定了y轴的文字和值，则用指定的显示
                if (ScaleYDictionary != null && ScaleYDictionary.Count() > 0)
                {
                    //使用指定的刻度值
                    double min = ScaleYDictionary.First().Key, max = ScaleYDictionary.Last().Key;
                    if (min == max)
                    {
                        max = min + 1;
                    }
                    RootAxesY.MouseMove -= RootAxesY_MouseMove;
                    RootAxesY.Cursor = Cursors.Arrow;
                    var axisManager = new AxisManager(DataSource.MaxValueY, DataSource.MinValueY);
                    axisManager.AxisMaximumValue = DataSource.MaxValueY;
                    axisManager.AxisMinimumValue = DataSource.MinValueY;
                    axisManager.Calculate();
                    var ratio = waveLineXImg.MainBoardHeight / (max - min);
                    var IntervalRnage = ratio * axisManager.Interval;
                    var loopNo = (int)(waveLineXImg.MainBoardHeight / IntervalRnage);
                    for (var i = 0; i <= loopNo; i++)
                    {
                        var offsetY = Height - waveLineXImg.MarginBottom - IntervalRnage * i;
                        var scaleItem = new TextBlock();
                        scaleItem.Width = 27;
                        scaleItem.TextAlignment = TextAlignment.Right;
                        scaleItem.FontSize = 9;
                        scaleItem.Text = string.IsNullOrWhiteSpace(ScaleYDictionary[i]) ? null : ScaleYDictionary[i];
                        scaleItem.Margin = new Thickness(0, offsetY - 7, 0, 0);
                        RootAxesY.Children.Add(scaleItem);
                        var myLine = new Line();
                        myLine = new Line();
                        myLine.Stroke = ScaleYBrush;
                        myLine.StrokeThickness = 0.2;
                        myLine.X1 = 0;
                        myLine.X2 = Width;
                        myLine.Y1 = offsetY;
                        myLine.Y2 = offsetY;
                        waveLineImgLine.Children.Add(myLine);
                    }
                }
                else
                {
                    //用数值显示
                    waveLineXImg.DataSource = DataSource.Items;
                    waveLineXImg.MinValueX = DataSource.DataTime.Date.AddHours(12);
                    waveLineXImg.MaxValueX = waveLineXImg.MinValueX.AddHours(24);
                    double min = minY, max = maxY;
                    var axisManager = new AxisManager(maxY, minY);
                    axisManager.Calculate();
                    if (min == max)
                    {
                        max = max + axisManager.Interval;
                        min = min - axisManager.Interval;
                    }
                    var ratio = (Height - waveLineXImg.MarginTop - waveLineXImg.MarginBottom) / (maxY - minY);
                    var IntervalRnage = ratio * axisManager.Interval;
                    interval = Convert.ToSingle(axisManager.Interval);
                    var loopNo = (int)((Height - waveLineXImg.MarginBottom) / IntervalRnage);
                    for (var i = 0; i <= loopNo; i++)
                    {
                        var offsetY = Height - waveLineXImg.MarginBottom - IntervalRnage * i;

                        var scaleItem = new TextBlock();
                        scaleItem.Width = 27;
                        scaleItem.TextAlignment = TextAlignment.Right;
                        scaleItem.FontSize = 9;
                        scaleItem.Text = (min + axisManager.Interval * i).ToString();
                        scaleItem.Margin = new Thickness(0, offsetY - 7, 0, 0);
                        RootAxesY.Children.Add(scaleItem);
                        var myLine = new Line();
                        myLine = new Line();
                        myLine.Stroke = ScaleYBrush;
                        myLine.StrokeThickness = 0.2;
                        myLine.X1 = 0;
                        myLine.X2 = Width;
                        myLine.Y1 = offsetY;
                        myLine.Y2 = offsetY;
                        waveLineImgLine.Children.Add(myLine);
                    } //end for
                    axisManager = null;
                } //end if
            }
            catch (Exception e)
            {
                //LogHelper.Error(ToString(), e);
                //MessageBox.Show(Application.Current.MainWindow,e.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DrawLine()
        {
            if (this.Visibility != Visibility.Visible)
            {
                return;
            }

            if (Width <= 0
                || double.IsInfinity(Width) || double.IsNaN(Width) ||
                double.IsNegativeInfinity(Width) || double.IsPositiveInfinity(Width)
                || double.IsInfinity(Height) || double.IsNaN(Height) ||
                double.IsNegativeInfinity(Height) || double.IsPositiveInfinity(Height)
                || double.IsInfinity(HorizontalOffset) || double.IsNaN(HorizontalOffset) ||
                double.IsNegativeInfinity(HorizontalOffset) || double.IsPositiveInfinity(HorizontalOffset)
                )
            {
                return;
                //throw new Exception("draw image variable error");
            }
            if (ViewTime <= 0 || Width <= 0)
            {
                return;
            }
            if (DataSource == null || DataSource.Items == null || DataSource.Items.Count() < 1)
            {
                return;
            }
            try
            {

                //waveLineXImg.MinValueX = DataSource.DataTime.Date.AddHours(12);
                //waveLineXImg.MaxValueX = waveLineXImg.MinValueX.AddHours(24);

                ////minY = Convert.ToSingle(DataSource.MinValueY);
                ////maxY = Convert.ToSingle(DataSource.MaxValueY);
                //waveLineXImg.DataSource = DataSource.Items;
                DrawScaleY();
                //drawLine();



                //生成曲线
                //waveLineXImg.ImgVisualWidth = Width - Const.LengndWidth;
                //waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth *
                //                       (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(Width));
                //waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth *
                //                      (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(ViewTime));
                //waveLineXImg.ImgVisualHeight = Height;
                //waveLineXImg.ScrollImgH = Convert.ToSingle(HorizontalOffset);
                waveLineXImg.MinValueY = minY;
                waveLineXImg.MaxValueY = maxY;

                //#if DEBUG
                //                Stopwatch sw = new Stopwatch();
                //                sw.Start();
                //#endif
                waveLineXImg.StartCreateImage();
                //System.Diagnostics.Debug.Print("waveLineXImg.Height:" + waveLineXImg.ImgHeight
                //             + "ImgVisualHeight:" + waveLineXImg.ImgVisualHeight
                //            + "ImgWidth:" + waveLineXImg.ImgWidth
                //              + "ImgVisualWidth:" + waveLineXImg.ImgVisualWidth

                //            );
                //#if DEBUG
                //                sw.Stop();
                //                TimeSpan ts2 = sw.Elapsed;
                //                Console.WriteLine(this.Title.Text + "StartCreateImage 总共花费{0}ms.", ts2.TotalMilliseconds);
                //                Console.WriteLine("waveLineXImg.MinValueY:" + waveLineXImg.MinValueY
                //                    + " waveLineXImg.MaxValueY:" + waveLineXImg.MaxValueY
                //                    + " waveLineXImg.ImgWidth:" + waveLineXImg.ImgWidth
                //                    + " waveLineXImg.ImgHeight:" + waveLineXImg.ImgHeight
                //                    + " waveLineXImg.ImgVisualWidth:" + waveLineXImg.ImgVisualWidth
                //                    + " waveLineXImg.ImgVisualHeight:" + waveLineXImg.ImgVisualHeight
                //                    + " waveLineXImg.DataSource.Count :" + waveLineXImg.DataSource.Count

                //                    );

                //#endif
            }
            catch (Exception e)
            {
                //LogHelper.Error(ToString(), e);
                //MessageBox.Show(Application.Current.MainWindow,e.Message, ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void waveLineXImg_OnImageSourceChanged(object sender, EventArgs e)
        {

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            if (!Equals(waveLineImg, null))
            {
                if (waveLineImg.Source != null)
                {
                    waveLineImg.Source = null;
                }
                waveLineImg.Source = sender as ImageSource;
                //System.Diagnostics.Debug.Print("waveLineImg.Source .Height:" + waveLineImg.Source.Height
                //            + "Width:" + waveLineImg.Source.Width

                //           );
            }
#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("waveLineXImg_OnImageSourceChanged{0}ms.", ts2.TotalMilliseconds);
#endif
        }

        #endregion

        private void waveLineImg_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(waveLineImg);
            if (waveLineXImg != null && waveLineXImg.PointList != null && waveLineXImg.PointList.Count() > 0)
            {
                var tmp = waveLineXImg.PointList.Where(a => Math.Round(a.PointF.X, 0) == Math.Round(point.X, 0));
                if (tmp != null && tmp.Count() > 0)
                {
                    tooltip.Margin = new Thickness(point.X + 20, point.Y + 20, waveLineImg.ActualWidth - point.X - tooltip.ActualWidth - 20, waveLineImg.ActualHeight - point.Y - tooltip.ActualHeight - 20);
                    tooltip.Visibility = System.Windows.Visibility.Visible;
                    if (Equals(ScaleYDictionary, null))
                    {
                        tooltip.Text = tmp.Max(a => a.Value).ToString();
                    }
                    else
                    {
                        tooltip.Text = ScaleYDictionary[Convert.ToDouble(tmp.Max(a => a.Value))];
                    }
                }
            }
        }

        private void waveLineImg_MouseLeave(object sender, MouseEventArgs e)
        {
            tooltip.Visibility = Visibility.Hidden;
        }

        //private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    waveLineXImg.ImgHeight = Height;

        //    waveLineXImg.ImgVisualWidth = Width - Const.LengndWidth;
        //    waveLineXImg.ImgWidth = waveLineXImg.ImgVisualWidth *
        //                           (Convert.ToSingle(Const.MilliSecFor24Hour) / Convert.ToSingle(Width));
        //    // waveLineXImg.ImgVisualWidth=Width 

        //}
    }
}