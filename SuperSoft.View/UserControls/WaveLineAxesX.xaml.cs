using Respircare.PatientManagementSystem.BLL;
using Respircare.Utility;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// WaveLineAxesX.xaml 的交互逻辑
    /// </summary>
    public partial class WaveLineAxesX : UserControl
    {
        private readonly WaveLineScaleXImg scaleXimg1 = new WaveLineScaleXImg();

        public WaveLineAxesX()
        {
            InitializeComponent();
            scaleXimg1.ImgVisualHeight = Convert.ToDouble(ResourceHelper.FindResource("AxesXHeight2"));
            scaleXimg1.ImgHeight = scaleXimg1.ImgVisualHeight;
            scaleXimg1.OnImageSourceChanged += scaleXimg1_OnImageSourceChanged;
        }

        //#region 可见视图的宽度

        //public static readonly DependencyProperty ViewportWidthProperty =
        //    DependencyProperty.Register("ViewportWidth", typeof(double), typeof(WaveLineAxesX),
        //        new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.None,
        //            OnViewportWidthPropertyChanged));

        ///// <summary>
        ///// 可见视图的宽度
        ///// </summary>
        //public double ViewportWidth
        //{
        //    get { return (double)GetValue(ViewportWidthProperty); }
        //    set { SetValue(ViewportWidthProperty, value); }
        //}

        //private static void OnViewportWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLineAxesX)d;
        //    var data = (double)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setViewportWidth(data);
        //}

        //private void setViewportWidth(double data)
        //{
        //    Width = data;
        //    scaleXimg1.ImgVisualWidth = ViewportWidth - Const.LengndWidth;
        //    drawAxesX();
        //}

        private double viewportWidth;
        public double ViewportWidth
        {
            get
            {
                return viewportWidth;
            }
            set
            {
                viewportWidth = value;
                Width = viewportWidth;
                scaleXimg1.ImgVisualWidth = viewportWidth - Const.LengndWidth;
            }
        }


        //#endregion

        //#region  时间视图

        //public static readonly DependencyProperty ViewTimeProperty =
        //    DependencyProperty.Register("ViewTime", typeof(int), typeof(WaveLineAxesX),
        //        new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.None,
        //            OnViewTimePropertyChanged));

        //public int ViewTime
        //{
        //    get { return (int)GetValue(ViewTimeProperty); }
        //    set { SetValue(ViewTimeProperty, value); }
        //}

        //private static void OnViewTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLineAxesX)d;
        //    var data = (int)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setViewTime(data);
        //}

        //private void setViewTime(int data)
        //{
        //    scaleXimg1.ImgWidth = scaleXimg1.ImgVisualWidth *
        //                          (Convert.ToDouble(Const.MilliSecFor24Hour) / Convert.ToDouble(data));
        //    System.Diagnostics.Debug.Print("scaleXimg1.ImgWidth" + scaleXimg1.ImgWidth);
        //    drawAxesX();
        //}
        private int viewTime;
        /// <summary>
        /// ViewportWidth 在设置ViewTime
        /// </summary>
        public int ViewTime
        {
            get
            {

                return viewTime;
            }
            set
            {
                viewTime = value;
                scaleXimg1.ImgWidth = scaleXimg1.ImgVisualWidth *
                                 (Convert.ToDouble(Const.MilliSecFor24Hour) / Convert.ToDouble(viewTime));
            }
        }
        //#endregion

        //#region 横向偏移量

        //public static readonly DependencyProperty HorizontalOffsetProperty =
        //    DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(WaveLineAxesX),
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
        //    var bar = (WaveLineAxesX)d;
        //    var data = (double)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setHorizontalOffset(data);
        //}

        //private void setHorizontalOffset(double horizontalOffset)
        //{
        //    if (!Equals(scaleXimg1, null))
        //    {
        //        scaleXimg1.ScrollImgH = Convert.ToSingle(horizontalOffset);
        //        drawAxesX();
        //    }
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
                scaleXimg1.ScrollImgH = Convert.ToSingle(horizontalOffset);
            }
        }
        //#endregion

        //#region DataTime

        ///// <summary>
        ///// 数据时间
        ///// </summary>
        //public static readonly DependencyProperty DataTimeProperty =
        //    DependencyProperty.Register("DataTime", typeof(DateTime), typeof(WaveLineAxesX),
        //        new FrameworkPropertyMetadata(default(DateTime), FrameworkPropertyMetadataOptions.None,
        //            OnDataTimePropertyChanged));

        ///// <summary>
        ///// 数据时间
        ///// </summary>
        //public DateTime DataTime
        //{
        //    get { return (DateTime)GetValue(DataTimeProperty); }
        //    set { SetValue(DataTimeProperty, value); }
        //}

        ///// <summary>
        ///// 数据时间改变事件方法
        ///// </summary>
        ///// <param name="d"></param>
        ///// <param name="e"></param>
        //private static void OnDataTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var bar = (WaveLineAxesX)d;
        //    var data = (DateTime)e.NewValue;
        //    if (Equals(data, e.OldValue)) return;
        //    bar.setDataSource(data);
        //}

        //private void setDataSource(DateTime dataTime)
        //{
        //    scaleXimg1.MinValueX = DataTime.AddHours(12);
        //    scaleXimg1.MaxValueX = DataTime.AddHours(36);
        //    drawAxesX();
        //}
        private DateTime dataTime;
        public DateTime DataTime
        {
            get
            {
                return dataTime;
            }
            set
            {
                dataTime = value;
                scaleXimg1.MinValueX = DataTime.AddHours(12);
                scaleXimg1.MaxValueX = DataTime.AddHours(36);
            }
        }

        //#endregion

        //#region 时间或者视图大小改变之后，重新绘制时间坐标

        public void DrawAxesX()
        {
            if (DataTime == null || DataTime == DateTime.MinValue)
            {
                return;
            }
            if (ViewTime <= 0 || ViewportWidth <= 0)
            {
                return;
            }

            //Width = ViewportWidth;
            //scaleXimg1.ImgVisualWidth = ViewportWidth - Const.LengndWidth;
            //scaleXimg1.ImgWidth = scaleXimg1.ImgVisualWidth *
            //                      (Convert.ToDouble(Const.MilliSecFor24Hour) / Convert.ToDouble(ViewTime));

            //scaleXimg1.ScrollImgH = Convert.ToSingle(HorizontalOffset);

            scaleXimg1.StartCreateImage();
        }

        public double RatioX
        {
            get { return scaleXimg1.RatioX; }
        }

        private void scaleXimg1_OnImageSourceChanged(object sender, EventArgs e)
        {
            if (ScaleXImg1.Source != null)
            {
                ScaleXImg1.Source = null;
            }
            ScaleXImg1.Source = sender as ImageSource;
            sender = null;
        }

        //#endregion
    }
}