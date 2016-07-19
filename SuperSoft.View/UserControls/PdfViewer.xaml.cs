using Respircare.Utility;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// PdfViewer.xaml 的交互逻辑
    /// </summary>
    public partial class PdfViewer : UserControl
    {
        /// <summary>
        /// PDF的文件路径
        /// </summary>
        public static readonly DependencyProperty PdfFileNameProperty =
            DependencyProperty.Register("PdfFileName", typeof(string), typeof(PdfViewer),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnPdfFileNamePropertyChanged));

        /// <summary>
        /// 是否显示
        /// </summary>
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(PdfViewer),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, OnIsShowPropertyChanged));

        public PdfViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// PDF的文件路径
        /// </summary>
        public string PdfFileName
        {
            get { return (string)GetValue(PdfFileNameProperty); }
            set { SetValue(PdfFileNameProperty, value); }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow
        {
            get { return (bool)GetValue(PdfFileNameProperty); }
            set { SetValue(PdfFileNameProperty, value); }
        }

        /// <summary>
        /// PDF的文件路径改变事件方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPdfFileNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfView = (PdfViewer)d;
            var n = e.NewValue;
            var o = e.OldValue;
            if (Equals(n, o)) return;
            if (Equals(n, null)) return;
            pdfView.changeFile(n.ToString());
        }

        private void changeFile(string pdfFileName)
        {
            if (File.Exists(pdfFileName))
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
            pdfView.OpenFile(pdfFileName);
            pdfView.ZoomToWidth();
        }

        /// <summary>
        /// 是否显示 改变事件方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnIsShowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfView = (PdfViewer)d;
            var n = e.NewValue;
            var o = e.OldValue;
            if (Equals(n, o)) return;
            if (Equals(n, null)) return;
            pdfView.changeIsShow(n.CastTo<bool>());
        }

        private void changeIsShow(bool isShow)
        {
            if (isShow)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}