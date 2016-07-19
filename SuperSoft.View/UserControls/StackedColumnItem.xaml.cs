using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// StackedColumnItem.xaml 的交互逻辑
    /// </summary>
    public partial class StackedColumnItem : UserControl
    {
        /// <summary>
        /// 图例颜色1（最下面的图例）,默认亮蓝色
        /// </summary>
        private SolidColorBrush value1Brush = new SolidColorBrush(Color.FromArgb(0xFF, 0x01, 0x72, 0xFF));
        //(0xFF, 0xAD, 0x96, 0xFE));

        /// <summary>
        /// 图例颜色2，默认浅蓝色
        /// </summary>
        private SolidColorBrush value2Brush = new SolidColorBrush(Color.FromArgb(0xFF, 0xAD, 0x96, 0xFE));
        //(0xFF, 0x70, 0x7F, 0xFE));

        /// <summary>
        /// 图例颜色3（最上面的图例），默认红色
        /// </summary>
        private SolidColorBrush value3Brush = new SolidColorBrush(Color.FromArgb(0xFF, 0x70, 0x7F, 0xFE));
        //(0xFF, 0x01, 0x72, 0xFF));

        public StackedColumnItem()
        {
            InitializeComponent();
            Value1Brush = FindResource("Value1ColorBrush") as SolidColorBrush;
            Value2Brush = FindResource("Value2ColorBrush") as SolidColorBrush;
            Value3Brush = FindResource("Value3ColorBrush") as SolidColorBrush;
            Value1Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
            Value2Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
            Value3Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
        }

        /// <summary>
        /// 图例颜色1（最下面的图例）,默认亮蓝色
        /// </summary>
        public SolidColorBrush Value1Brush
        {
            get { return value1Brush; }
            set
            {
                value1Brush = value;
                Value1Rectangle.Background = value1Brush;
            }
        }

        /// <summary>
        /// 图例颜色2，默认浅蓝色
        /// </summary>
        public SolidColorBrush Value2Brush
        {
            get { return value2Brush; }
            set
            {
                value2Brush = value;
                Value2Rectangle.Background = value2Brush;
            }
        }

        /// <summary>
        /// 图例颜色3（最上面的图例），默认红色
        /// </summary>
        public SolidColorBrush Value3Brush
        {
            get { return value3Brush; }
            set
            {
                value3Brush = value;
                Value3Rectangle.Background = value3Brush;
            }
        }

        private void upMargin()
        {
            Value1Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
            Value2Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
            Value3Rectangle.Margin = new Thickness(0, MarginTop, 0, MarginBottom);
        }

        private void upUI()
        {
            if (Height <= 0 || MaxValue <= MinValue)
            {
                return;
            }
            var ratio = (Height - MarginTop - MarginBottom) / (MaxValue - MinValue);
            var maxHeight = Height - MarginBottom - MarginTop;
            Value1Rectangle.Height = ratio * Value1 > maxHeight ? maxHeight : Convert.ToDouble(ratio * Value1);
            Value2Rectangle.Height = ratio * Value2 > maxHeight ? maxHeight : Convert.ToDouble(ratio * Value2);
            Value3Rectangle.Height = ratio * Value3 > maxHeight ? maxHeight : Convert.ToDouble(ratio * Value3);
        }

        private void upValue1UI()
        {
            if (Height <= 0 || MaxValue <= MinValue)
            {
                return;
            }
            var ratio = (Height - MarginTop - MarginBottom) / (MaxValue - MinValue);
            var maxHeight = Height - MarginBottom - MarginTop;
            var actureHeight =
                Convert.ToDouble((ratio.HasValue ? ratio.Value : 0) *
                                 ((Value1.HasValue ? Value1.Value : (MinValue.HasValue ? MinValue.Value : 0)) -
                                  (MinValue.HasValue ? MinValue.Value : 0)));
            Value1Rectangle.Height = actureHeight > maxHeight ? maxHeight : (actureHeight > 0 ? actureHeight : 0);
        }

        private void upValue2UI()
        {
            if (Height <= 0 || MaxValue <= MinValue)
            {
                return;
            }
            var ratio = (Height - MarginTop - MarginBottom) / (MaxValue - MinValue);
            var maxHeight = Height - MarginBottom - MarginTop;
            var actureHeight =
                Convert.ToDouble((ratio.HasValue ? ratio.Value : 0) *
                                 ((Value2.HasValue ? Value2.Value : (MinValue.HasValue ? MinValue.Value : 0)) -
                                  (MinValue.HasValue ? MinValue.Value : 0)));
            Value2Rectangle.Height = actureHeight > maxHeight ? maxHeight : (actureHeight > 0 ? actureHeight : 0);
        }

        private void upValue3UI()
        {
            if (Height <= 0 || MaxValue <= MinValue)
            {
                return;
            }
            var ratio = (Height - MarginTop - MarginBottom) / (MaxValue - MinValue);
            var maxHeight = Height - MarginBottom - MarginTop;
            var actureHeight =
                Convert.ToDouble((ratio.HasValue ? ratio.Value : 0) *
                                 ((Value3.HasValue ? Value3.Value : (MinValue.HasValue ? MinValue.Value : 0)) -
                                  (MinValue.HasValue ? MinValue.Value : 0)));
            Value3Rectangle.Height = actureHeight > maxHeight ? maxHeight : (actureHeight > 0 ? actureHeight : 0);
        }

        #region MarginTop

        private double marginTop = 5;

        public double MarginTop
        {
            get { return marginTop; }
            set
            {
                marginTop = value;
                upMargin();
            }
        }

        #endregion

        #region MarginBottom

        private double mrginBottom = 5;

        public double MarginBottom
        {
            get { return mrginBottom; }
            set
            {
                mrginBottom = value;
                upMargin();
            }
        }

        #endregion

        #region MaxValue

        private double? maxValue;

        public double? MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                upUI();
            }
        }

        #endregion

        #region MinValue

        private double? minValue;

        public double? MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                upUI();
            }
        }

        #endregion

        #region Value1

        private double? value1;

        public double? Value1
        {
            get { return value1; }
            set
            {
                value1 = value;
                upValue1UI();
            }
        }

        #endregion

        #region Value2

        private double? value2;

        public double? Value2
        {
            get { return value2; }
            set
            {
                value2 = value;
                upValue2UI();
            }
        }

        #endregion

        #region Value3

        private double? value3;

        public double? Value3
        {
            get { return value3; }
            set
            {
                value3 = value;
                upValue3UI();
            }
        }

        #endregion
    }
}