using Respircare.PatientManagementSystem.DAL;
using Respircare.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using Respircare.PatientManagementSystem.Models;
using System.IO;
using ProtoBuf;
using System.Threading.Tasks;
using System.Linq;
namespace Respircare.PatientManagementSystem.Views
{
    /// <summary>
    /// WaveLineList.xaml 的交互逻辑
    /// 根据时间、详细数据通道的菜单项显示各种曲线
    /// </summary>
    public partial class WaveLineList : UserControl
    {
        //private WaveLineListViewModel waveLineListViewModel;
        public WaveLineList()
        {
            InitializeComponent();
            //设置曲线的颜色
            //WaveLineEvent.LineBrush = new SolidBrush(Color.FromArgb(0, 158, 255));
            //WaveLinePressure.LineBrush = new SolidBrush(Color.FromArgb(1, 114, 255));
            //WaveLineFlow.LineBrush = new SolidBrush(Color.FromArgb(0, 150, 0));
            comboBoxTimeList.ItemsSource = initTimeList();
            comboBoxTimeList2.ItemsSource = initTimeList();
            //if (Equals(comboBoxTimeList.ItemsSource, null))
            //{
            //}
            //if (Equals(comboBoxTimeList2.ItemsSource, null))
            //{
            //}
            spLine.Margin = new Thickness((scrollViewer2.ViewportWidth + Const.LengndWidth) / 2, 0, 0, 0);
        }

        #region 界面显示的控件

        #region 所有的曲线控件

        private WaveLine eventWaveLine;
        private WaveLine pressureWaveLine;
        private WaveLine flowWaveLine;
        private WaveLine leakWaveLine;
        private WaveLine tidalVolumeWaveLine;
        private WaveLine spO2WaveLine;
        private WaveLine pulseRateWaveLine;
        private WaveLine respiratoryRateWaveLine;
        private WaveLine minuteVentilationWaveLine;
        private WaveLine iERatioWaveLine;
        private WaveLine iPAPWaveLine;
        private WaveLine ePAPWaveLine;

        private WaveLine eventWaveLine2;
        private WaveLine pressureWaveLine2;
        private WaveLine flowWaveLine2;
        private WaveLine leakWaveLine2;
        private WaveLine tidalVolumeWaveLine2;
        private WaveLine spO2WaveLine2;
        private WaveLine pulseRateWaveLine2;
        private WaveLine respiratoryRateWaveLine2;
        private WaveLine minuteVentilationWaveLine2;
        private WaveLine iERatioWaveLine2;
        private WaveLine iPAPWaveLine2;
        private WaveLine ePAPWaveLine2;

        #endregion

        //#region WaveLineHide占位控件

        //private WaveLineHide waveLineHide = new WaveLineHide();
        //private WaveLineHide waveLineHide2 = new WaveLineHide();

        //#endregion

        #endregion

        #region 公开的属性

        /// <summary>
        /// 绘制曲线的数据源
        /// </summary>
        public IEnumerable<ViewProductWorkingSummaryDetailedData> ViewProductWorkingSummaryDetailedDataList { get; set; }

        /// <summary>
        /// 通道选项 上部的CheckBox按钮
        /// 在PatientDetailedViewModel中动态添加或删除
        /// </summary>
        public List<CheckBox> ChannelOption { get; set; } = new List<CheckBox>();

        /// <summary>
        /// 通道选项 下部的CheckBox按钮
        /// 在PatientDetailedViewModel中动态添加或删除
        /// </summary>
        public List<CheckBox> ChannelOption2 { get; set; } = new List<CheckBox>();

        #region 治疗模式 TherapyMode

        private Models.TherapyMode therapyMode = Models.TherapyMode.Unknown;

        /// <summary>
        /// 治疗模式
        /// </summary>
        public Models.TherapyMode TherapyMode
        {
            get
            {
                return therapyMode;
            }
            set
            {
                therapyMode = value;
                initWillShowWavelineByTherapyMode();
            }
        }

        #endregion

        #region 数据的时间

        private DateTime dataTime;
        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DataTime
        {
            get { return dataTime; }
            set
            {
                dataTime = value;
                dateTimeTextBlock.Text = value.ToShortDateString();
            }
        }

        #endregion

        #endregion

        #region 公开的方法

        /// <summary>
        /// 设置各种变量后更新所有界面，上下曲线和上下的坐标。
        /// </summary>
        public void UpAllUI()
        {
            //根据数据源设置空间是否可见
            if (Equals(ViewProductWorkingSummaryDetailedDataList, null)
                   || ViewProductWorkingSummaryDetailedDataList.Count() < 1
                   || scrollViewer.ViewportWidth <= 0
                   || scrollViewer2.ViewportWidth <= 0
                 )
            {
                scaleYLine.Visibility = Visibility.Hidden;
                gridUp.Visibility = Visibility.Hidden;
                gridDown.Visibility = Visibility.Hidden;
                //WaveLineList.Visibility = Visibility.Hidden;
                //WithoutDataVisibility = Visibility.Visible;
                //return false;
                withoutDataTextBlock.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                //WaveLineList.Visibility = Visibility.Visible;
                //WithoutDataVisibility = Visibility.Hidden;
                scaleYLine.Visibility = Visibility.Visible;
                gridUp.Visibility = Visibility.Visible;
                gridDown.Visibility = Visibility.Visible;
                withoutDataTextBlock.Visibility = Visibility.Hidden;
            }



            //上部x轴
            UpAxesX();
            //下部x轴
            UpAxesX2();

            setWaveLineDataSource();

            //上部曲线
            setStackPanelWidth();
            UpWaveLine();
            //下部曲线
            setStackPanelWidth2();
            UpWaveLine2();

            GC.Collect();
        }

        /// <summary>
        /// 通道选项改变之后更新各个通道的可见性和通道的高度
        /// </summary>
        public void UpChannedUI(CheckBox sender)
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            //System.Diagnostics.Debug.Print("UpChannedUI");

            if (Equals(eventWaveLine.Name, sender.Name))
            {
                eventWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(pressureWaveLine.Name, sender.Name))
            {
                pressureWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(flowWaveLine.Name, sender.Name))
            {
                flowWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(leakWaveLine.Name, sender.Name))
            {
                leakWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(tidalVolumeWaveLine.Name, sender.Name))
            {
                tidalVolumeWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(spO2WaveLine.Name, sender.Name))
            {
                spO2WaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }

            if (Equals(pulseRateWaveLine.Name, sender.Name))
            {
                pulseRateWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight();
                return;
            }


            if (TherapyMode != Models.TherapyMode.APAP && TherapyMode != Models.TherapyMode.CPAP)
            {
                if (Equals(respiratoryRateWaveLine.Name, sender.Name))
                {
                    respiratoryRateWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight();
                    return;
                }

                if (Equals(minuteVentilationWaveLine.Name, sender.Name))
                {
                    minuteVentilationWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight();
                    return;
                }

                if (Equals(iERatioWaveLine.Name, sender.Name))
                {
                    iERatioWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight();
                    return;
                }

                if (Equals(iPAPWaveLine.Name, sender.Name))
                {
                    iPAPWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight();
                    return;
                }

                if (Equals(ePAPWaveLine.Name, sender.Name))
                {
                    ePAPWaveLine.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight();
                    return;
                }

            }

#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpChannedUI 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif


            //foreach (var waveline in willShowWavelineByTherapyMode)
            //{
            //    // waveline.Name .cou ChannelOption.FirstOrDefault (a=>a.Content )
            //    //ChannelOptionsContextMenu.Items

            //    //if (waveline.Visibility != Visibility)
            //    //{



            //    //}
            //}


            //    foreach (var menu in ChannelOptionsContextMenu.Items)
            //{
            //    var m = menu as CheckBox;
            //    if (!Equals(m, null))
            //    {
            //       if ( m.Content 

            //        if (m.IsChecked.Value)
            //        {
            //            willShowWavelineByTherapyMode
            //            //WaveLine w = new WaveLine();

            //        }
            //    }
            //}
        }

        /// <summary>
        /// 通道选项改变之后更新各个通道的可见性和通道的高度
        /// </summary>
        public void UpChannedUI2(CheckBox sender)
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            //System.Diagnostics.Debug.Print("UpChannedUI2");

            if (Equals(eventWaveLine2.Name, sender.Name))
            {
                eventWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(pressureWaveLine2.Name, sender.Name))
            {
                pressureWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(flowWaveLine2.Name, sender.Name))
            {
                flowWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(leakWaveLine2.Name, sender.Name))
            {
                leakWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(tidalVolumeWaveLine2.Name, sender.Name))
            {
                tidalVolumeWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(spO2WaveLine2.Name, sender.Name))
            {
                spO2WaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }

            if (Equals(pulseRateWaveLine2.Name, sender.Name))
            {
                pulseRateWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                setVisibilityWaveLineHeight2();
                return;
            }


            if (TherapyMode != Models.TherapyMode.APAP && TherapyMode != Models.TherapyMode.CPAP)
            {
                if (Equals(respiratoryRateWaveLine2.Name, sender.Name))
                {
                    respiratoryRateWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight2();
                    return;
                }

                if (Equals(minuteVentilationWaveLine2.Name, sender.Name))
                {
                    minuteVentilationWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight2();
                    return;
                }

                if (Equals(iERatioWaveLine2.Name, sender.Name))
                {
                    iERatioWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight2();
                    return;
                }

                if (Equals(iPAPWaveLine2.Name, sender.Name))
                {
                    iPAPWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight2();
                    return;
                }

                if (Equals(ePAPWaveLine2.Name, sender.Name))
                {
                    ePAPWaveLine2.Visibility = sender.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                    setVisibilityWaveLineHeight2();
                    return;
                }

            }
#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpChannedUI2 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif
        }

        #endregion

        #region 内部方法和变量

        /// <summary>
        /// 更新上部的时间坐标
        /// </summary>
        private void UpAxesX()
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            waveLineAxesX.DataTime = DataTime;
            waveLineAxesX.HorizontalOffset = scrollViewer.HorizontalOffset;
            //waveLineAxesX.ViewportWidth = getScrollViewerViewportWidth(scrollViewer);// scrollViewer.ViewportWidth;
            waveLineAxesX.ViewportWidth = scrollViewer.ViewportWidth;
            waveLineAxesX.ViewTime = this.comboBoxTimeList.SelectedValue.GetInt();
            waveLineAxesX.DrawAxesX();
            //System.Diagnostics.Debug.Print("UpAxesX:" + scrollViewer.ViewportWidth);
#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpAxesX 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif
        }

        private double getScrollViewerViewportWidth(ScrollViewer sv)
        {//应为控件大小改变时，ScrollViewer.ViewportWidth的值还没有变化，所以手动计算

            double d = 0;

            if (sv.ComputedVerticalScrollBarVisibility == Visibility.Visible)
            {
                //计算得出的值
                d = sv.ActualWidth - 14;
            }
            else
            {
                d = sv.ActualWidth;
            }


            //  scrollViewer.Width -scrollViewer.ComputedVerticalScrollBarVisibility

            return d <= 0 ? 0 : d;
        }


        /// <summary>
        /// 更新下部的时间坐标
        /// </summary>
        private void UpAxesX2()
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            waveLineAxesX2.DataTime = DataTime;
            waveLineAxesX2.HorizontalOffset = scrollViewer2.HorizontalOffset;
            //waveLineAxesX2.ViewportWidth = getScrollViewerViewportWidth(scrollViewer);//  scrollViewer2.ViewportWidth;
            waveLineAxesX2.ViewportWidth = scrollViewer2.ViewportWidth;
            waveLineAxesX2.ViewTime = this.comboBoxTimeList2.SelectedValue.GetInt();
            waveLineAxesX2.DrawAxesX();
            //System.Diagnostics.Debug.Print("UpAxesX2:" + scrollViewer.ViewportWidth);
#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpAxesX2 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif
        }

        private void setWaveLineDataSource()
        {

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            WaveLineDataModel eventDataListTmp = null;
            WaveLineDataModel pressureDataListTmp = null;
            WaveLineDataModel flowDataListTmp = null;
            WaveLineDataModel leakDataListTmp = null;
            WaveLineDataModel tidalVolumeDataListTmp = null;
            WaveLineDataModel minuteVentilationDataListTmp = null;
            WaveLineDataModel respiratoryRateDataListTmp = null;
            WaveLineDataModel spO2DataListTmp = null;
            WaveLineDataModel pulseRateDataListTmp = null;
            WaveLineDataModel iERatioDataListTmp = null;
            WaveLineDataModel iPAPDataListTmp = null;
            WaveLineDataModel ePAPDataListTmp = null;

            foreach (var v in ViewProductWorkingSummaryDetailedDataList)
            {
                #region 反序列化解析数据并查询每个通道波形的数据源

                using (var fs = new MemoryStream(v.Content))
                {
                    var all = Serializer.Deserialize<ICollection<DetailedField>>(fs);
                    if (!Equals(eventWaveLine, null) && !Equals(eventWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, a.Events);
                        if (Equals(eventDataListTmp, null))
                        {
                            eventDataListTmp = new WaveLineDataModel();
                            eventDataListTmp.DataTime = DataTime;
                        }
                        eventDataListTmp.Items.Add(list);
                    }
                    if (!Equals(pressureWaveLine, null) && !Equals(pressureWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.TargetPressure));
                        if (Equals(pressureDataListTmp, null))
                        {
                            pressureDataListTmp = new WaveLineDataModel();
                            pressureDataListTmp.DataTime = DataTime;
                        }
                        pressureDataListTmp.Items.Add(list);
                    }
                    if (!Equals(flowWaveLine, null) && !Equals(flowWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.CurrentFlow));
                        if (Equals(flowDataListTmp, null))
                        {
                            flowDataListTmp = new WaveLineDataModel();
                            flowDataListTmp.DataTime = DataTime;
                        }
                        float minY = list.Min(a => a.Value);
                        if (minY < flowDataListTmp.MinValueY)
                        {
                            flowDataListTmp.MinValueY = minY;
                        }
                        flowDataListTmp.Items.Add(list);
                    }
                    if (!Equals(leakWaveLine, null) && !Equals(leakWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.Leak));
                        if (Equals(leakDataListTmp, null))
                        {
                            leakDataListTmp = new WaveLineDataModel();
                            leakDataListTmp.DataTime = DataTime;
                        }
                        leakDataListTmp.Items.Add(list);
                    }
                    if (!Equals(tidalVolumeWaveLine, null) && !Equals(tidalVolumeWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.TidalVolume));
                        if (Equals(tidalVolumeDataListTmp, null))
                        {
                            tidalVolumeDataListTmp = new WaveLineDataModel();
                            tidalVolumeDataListTmp.DataTime = DataTime;
                        }
                        tidalVolumeDataListTmp.Items.Add(list);
                    }
                    if (!Equals(minuteVentilationWaveLine, null) && !Equals(minuteVentilationWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.MinuteVentilation));
                        if (Equals(minuteVentilationDataListTmp, null))
                        {
                            minuteVentilationDataListTmp = new WaveLineDataModel();
                            minuteVentilationDataListTmp.DataTime = DataTime;
                        }
                        minuteVentilationDataListTmp.Items.Add(list);
                    }

                    if (!Equals(respiratoryRateWaveLine, null) && !Equals(respiratoryRateWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.RespiratoryRate));
                        if (Equals(respiratoryRateDataListTmp, null))
                        {
                            respiratoryRateDataListTmp = new WaveLineDataModel();
                            respiratoryRateDataListTmp.DataTime = DataTime;
                        }
                        respiratoryRateDataListTmp.Items.Add(list);
                    }

                    if (!Equals(spO2WaveLine, null) && !Equals(spO2WaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.SpO2));
                        if (Equals(spO2DataListTmp, null))
                        {
                            spO2DataListTmp = new WaveLineDataModel();
                            spO2DataListTmp.DataTime = DataTime;
                        }
                        spO2DataListTmp.Items.Add(list);
                    }
                    if (!Equals(pulseRateWaveLine, null) && !Equals(pulseRateWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.PulseRate));
                        if (Equals(pulseRateDataListTmp, null))
                        {
                            pulseRateDataListTmp = new WaveLineDataModel();
                            pulseRateDataListTmp.DataTime = DataTime;
                        }
                        pulseRateDataListTmp.Items.Add(list);
                    }
                    if (!Equals(iERatioWaveLine, null) && !Equals(iERatioWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.IERatio));
                        if (Equals(iERatioDataListTmp, null))
                        {
                            iERatioDataListTmp = new WaveLineDataModel();
                            iERatioDataListTmp.DataTime = DataTime;
                        }
                        iERatioDataListTmp.Items.Add(list);
                    }
                    if (!Equals(iPAPWaveLine, null) && !Equals(iPAPWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.IPAP));
                        if (Equals(iPAPDataListTmp, null))
                        {
                            iPAPDataListTmp = new WaveLineDataModel();
                            iPAPDataListTmp.DataTime = DataTime;
                        }
                        iPAPDataListTmp.Items.Add(list);
                    }
                    if (!Equals(ePAPWaveLine, null) && !Equals(ePAPWaveLine2, null))
                    {
                        var list = from a in all
                                   orderby a.RecoredTime
                                   select new WaveLineDataItem(a.RecoredTime, Convert.ToSingle(a.EPAP));
                        if (Equals(ePAPDataListTmp, null))
                        {
                            ePAPDataListTmp = new WaveLineDataModel();
                            ePAPDataListTmp.DataTime = DataTime;
                        }
                        ePAPDataListTmp.Items.Add(list);
                    }
                } //endusing

                #endregion
            } //endforeach

            if (!Equals(eventDataListTmp, null))
            {
                eventDataListTmp.MaxValueY = 4;
                eventDataListTmp.MinValueY = 0;
                eventWaveLine.DataSource = null;
                eventWaveLine2.DataSource = null;
                eventWaveLine.DataSource = eventDataListTmp;
                eventWaveLine2.DataSource = eventDataListTmp;
                //eventWaveLine.DrawLine();
                //eventWaveLine2.DrawLine();
            }
            if (!Equals(pressureDataListTmp, null))
            {
                pressureDataListTmp.MaxValueY = 35;
                pressureDataListTmp.MinValueY = 0;
                pressureWaveLine.DataSource = null;
                pressureWaveLine2.DataSource = null;
                pressureWaveLine.DataSource = pressureDataListTmp;
                pressureWaveLine2.DataSource = pressureDataListTmp;
                //pressureWaveLine.DrawLine();
                //pressureWaveLine2.DrawLine();
            }
            if (!Equals(flowDataListTmp, null))
            {

                flowDataListTmp.MaxValueY = 200;
                //FlowDataList.MinValueY = -200;//最小值取决于数据中的最小值
                flowWaveLine.DataSource = null;
                flowWaveLine2.DataSource = null;
                flowWaveLine.DataSource = flowDataListTmp;
                flowWaveLine2.DataSource = flowDataListTmp;
                //flowWaveLine.DrawLine();
                //flowWaveLine2.DrawLine();
            }
            if (!Equals(leakDataListTmp, null))
            {

                leakDataListTmp.MaxValueY = 100;
                leakDataListTmp.MinValueY = 0;
                leakWaveLine.DataSource = null;
                leakWaveLine2.DataSource = null;
                leakWaveLine.DataSource = leakDataListTmp;
                leakWaveLine2.DataSource = leakDataListTmp;
                //leakWaveLine.DrawLine();
                //leakWaveLine2.DrawLine();
            }
            if (!Equals(tidalVolumeDataListTmp, null))
            {
                tidalVolumeDataListTmp.MaxValueY = 2500;
                tidalVolumeDataListTmp.MinValueY = 0;
                tidalVolumeWaveLine.DataSource = null;
                tidalVolumeWaveLine2.DataSource = null;
                tidalVolumeWaveLine.DataSource = tidalVolumeDataListTmp;
                tidalVolumeWaveLine2.DataSource = tidalVolumeDataListTmp;
                //tidalVolumeWaveLine.DrawLine();
                //tidalVolumeWaveLine2.DrawLine();
            }
            if (!Equals(minuteVentilationDataListTmp, null))
            {
                minuteVentilationDataListTmp.MaxValueY = 30;
                minuteVentilationDataListTmp.MinValueY = 0;
                minuteVentilationWaveLine.DataSource = null;
                minuteVentilationWaveLine2.DataSource = null;
                minuteVentilationWaveLine.DataSource = minuteVentilationDataListTmp;
                minuteVentilationWaveLine2.DataSource = minuteVentilationDataListTmp;
                //minuteVentilationWaveLine.DrawLine();
                //minuteVentilationWaveLine2.DrawLine();
            }
            if (!Equals(respiratoryRateDataListTmp, null))
            {
                respiratoryRateDataListTmp.MaxValueY = 60;
                respiratoryRateDataListTmp.MinValueY = 0;
                respiratoryRateWaveLine.DataSource = null;
                respiratoryRateWaveLine2.DataSource = null;
                respiratoryRateWaveLine.DataSource = respiratoryRateDataListTmp;
                respiratoryRateWaveLine2.DataSource = respiratoryRateDataListTmp;
                //respiratoryRateWaveLine.DrawLine();
                //respiratoryRateWaveLine2.DrawLine();
            }
            if (!Equals(spO2DataListTmp, null))
            {
                spO2DataListTmp.MaxValueY = 100;
                spO2DataListTmp.MinValueY = 0;
                spO2WaveLine.DataSource = null;
                spO2WaveLine2.DataSource = null;
                spO2WaveLine.DataSource = spO2DataListTmp;
                spO2WaveLine2.DataSource = spO2DataListTmp;
                //spO2WaveLine.DrawLine();
                //spO2WaveLine2.DrawLine();
            }

            if (!Equals(pulseRateDataListTmp, null))
            {
                pulseRateDataListTmp.MaxValueY = 255;
                pulseRateDataListTmp.MinValueY = 0;
                pulseRateWaveLine.DataSource = null;
                pulseRateWaveLine2.DataSource = null;
                pulseRateWaveLine.DataSource = pulseRateDataListTmp;
                pulseRateWaveLine2.DataSource = pulseRateDataListTmp;
                //pulseRateWaveLine.DrawLine();
                //pulseRateWaveLine2.DrawLine();
            }
            if (!Equals(iERatioDataListTmp, null))
            {
                iERatioDataListTmp.MaxValueY = 10;
                iERatioDataListTmp.MinValueY = 0;
                iERatioWaveLine.DataSource = null;
                iERatioWaveLine2.DataSource = null;
                iERatioWaveLine.DataSource = iERatioDataListTmp;
                iERatioWaveLine2.DataSource = iERatioDataListTmp;
                //iERatioWaveLine.DrawLine();
                //iERatioWaveLine2.DrawLine();
            }
            if (!Equals(iPAPDataListTmp, null))
            {
                iPAPDataListTmp.MaxValueY = 25;
                iPAPDataListTmp.MinValueY = 4;
                iPAPWaveLine.DataSource = null;
                iPAPWaveLine2.DataSource = null;
                iPAPWaveLine.DataSource = iPAPDataListTmp;
                iPAPWaveLine2.DataSource = iPAPDataListTmp;
                //iPAPWaveLine.DrawLine();
                //iPAPWaveLine2.DrawLine();
            }
            if (!Equals(ePAPDataListTmp, null))
            {
                ePAPDataListTmp.MaxValueY = 20;
                ePAPDataListTmp.MinValueY = 4;
                ePAPWaveLine.DataSource = null;
                ePAPWaveLine2.DataSource = null;
                ePAPWaveLine.DataSource = ePAPDataListTmp;
                ePAPWaveLine2.DataSource = ePAPDataListTmp;
                //ePAPWaveLine.DrawLine();
                //ePAPWaveLine2.DrawLine();
            }
            //foreach (var v in willShowWavelineByTherapyMode)
            //{


            //    //v.DataSource =


            //}


#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("setWaveLineDataSource 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif

        }
        //bool isFirstLoad = true;

        /// <summary>
        /// 更新上部的曲线
        /// </summary>
        private void UpWaveLine()
        {
            //foreach (var checkBox in ChannelOption.Where(a => a.IsChecked.Value == true))
            //{
            //    //if (Equals(eventWaveLine.Name, checkBox.Name))
            //    //{
            //    //    if (checkBox.IsChecked.Value)
            //    //    {
            //    //        eventWaveLine.Visibility = Visibility.Visible;
            //    //    } 
            //    //    //eventWaveLine.DataSource


            //    //}

            //    if (checkBox.IsChecked.Value)
            //    {
            //        eventWaveLine.Visibility = Visibility.Visible;
            //    }

            //}

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif

            //if (scrollViewer.ViewportWidth <= 0)
            //{


            //    return;
            //}

            var padding = new Thickness(scrollViewer.HorizontalOffset, 0, 0, 0);
            foreach (WaveLine waveLine in willShowWavelineByTherapyMode.Where(a => a.Visibility == Visibility.Visible))
            {
                waveLine.ContentHeight = contentHeight;
                waveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                waveLine.ViewTime = comboBoxTimeList.SelectedValue.GetInt();
                waveLine.HorizontalOffset = scrollViewer.HorizontalOffset;
                waveLine.Margin = padding;// new Thickness(scrollViewer.HorizontalOffset, 0, 0, 0);
                waveLine.DrawLine();
                //System.Diagnostics.Debug.Print("waveLine.width:" + waveLine.Width + " waveLine.Height:" + waveLine.Height);
            }


            //if (isFirstLoad)
            //{
            //    foreach (WaveLine waveLine in willShowWavelineByTherapyMode.Where(a => a.Visibility == Visibility.Visible))
            //    {
            //        //waveLine.ContentHeight = contentHeight;
            //        waveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
            //        //waveLine.ViewTime = comboBoxTimeList.SelectedValue.GetInt();
            //        //waveLine.HorizontalOffset = scrollViewer.HorizontalOffset;
            //        //waveLine.Margin = padding;// new Thickness(scrollViewer.HorizontalOffset, 0, 0, 0);
            //        waveLine.DrawLine();
            //    }
            //    isFirstLoad = false;
            //}

            //Parallel.ForEach(willShowWavelineByTherapyMode, waveLine =>
            //{
            //    try
            //    {
            //        waveLine.ContentHeight = contentHeight;
            //        waveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);

            //        waveLine.ViewTime = comboBoxTimeList.SelectedValue.GetInt();
            //        waveLine.HorizontalOffset = scrollViewer.HorizontalOffset;
            //        waveLine.Margin = padding;// new Thickness(scrollViewer.HorizontalOffset, 0, 0, 0);
            //        waveLine.DrawLine();
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.LogHelper.Error(this.ToString(), ex);
            //    }
            //});















#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpWaveLine 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif

        }

        /// <summary>
        /// 更新下部的曲线
        /// </summary>
        private void UpWaveLine2()
        {

            //if (scrollViewer2.ViewportWidth <= 0)
            //{
            //    return;
            //}
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            var padding = new Thickness(scrollViewer2.HorizontalOffset, 0, 0, 0);
            float viewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
            foreach (WaveLine waveLine in willShowWavelineByTherapyMode2.Where(a => a.Visibility == Visibility.Visible))
            {
                waveLine.ContentHeight = contentHeight;
                waveLine.ViewportWidth = viewportWidth;
                waveLine.ViewTime = comboBoxTimeList2.SelectedValue.GetInt();
                waveLine.HorizontalOffset = scrollViewer2.HorizontalOffset;
                waveLine.Margin = padding;// new Thickness(scrollViewer2.HorizontalOffset, 0, 0, 0);
                waveLine.DrawLine();
            }

            //System.Diagnostics.Debug.Print("UpWaveLine2:" + scrollViewer2.ViewportWidth);

#if DEBUG
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("UpWaveLine2 总共花费{0}ms.", ts2.TotalMilliseconds);
#endif
        }

        #region combobox数据源

        /// <summary>
        /// 初始化时间列表
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, string> initTimeList()
        {
            var dictionaryList = new Dictionary<int, string>();
            dictionaryList.Add(Const.MilliSecFor24Hour, ResourceHelper.LoadString("MilliSecFor24Hour"));
            dictionaryList.Add(Const.MilliSecFor12Hour, ResourceHelper.LoadString("MilliSecFor12Hour"));
            dictionaryList.Add(Const.MilliSecFor10Hour, ResourceHelper.LoadString("MilliSecFor10Hour"));
            dictionaryList.Add(Const.MilliSecFor8Hour, ResourceHelper.LoadString("MilliSecFor8Hour"));
            dictionaryList.Add(Const.MilliSecFor5Hour, ResourceHelper.LoadString("MilliSecFor5Hour"));
            dictionaryList.Add(Const.MilliSecFor2Hour, ResourceHelper.LoadString("MilliSecFor2Hour"));
            dictionaryList.Add(Const.MilliSecForOneHour, ResourceHelper.LoadString("MilliSecForOneHour"));
            dictionaryList.Add(Const.MilliSecForHalfHour, ResourceHelper.LoadString("MilliSecForHalfHour"));
            dictionaryList.Add(Const.MilliSecFor10Minutes, ResourceHelper.LoadString("MilliSecFor10Minutes"));
            dictionaryList.Add(Const.MilliSecFor5Minutes, ResourceHelper.LoadString("MilliSecFor5Minutes"));
            dictionaryList.Add(Const.MilliSecForOneMinutes, ResourceHelper.LoadString("MilliSecForOneMinutes"));
            dictionaryList.Add(Const.MilliSecForHalfMinutes, ResourceHelper.LoadString("MilliSecForHalfMinutes"));
            dictionaryList.Add(Const.MilliSecFor10Sec, ResourceHelper.LoadString("MilliSecFor10Sec"));
            return dictionaryList;
        }

        #endregion

        #region 事件的Y轴文字

        private Dictionary<double, string> initEventScaleYDictionary()
        {
            var tmpList = new Dictionary<double, string>();
            tmpList.Add(0, null);
            tmpList.Add(1, ResourceHelper.LoadString("HI"));
            tmpList.Add(2, ResourceHelper.LoadString("AI"));
            tmpList.Add(3, null);
            tmpList.Add(4, ResourceHelper.LoadString("Snore"));
            return tmpList;
        }
        #endregion

        #region  通道选项可见性改变后 设置可见通道控件的高度

        private float contentHeight = (float)Const.OneStackedColumnHeight();
        private float contentHeight2 = (float)Const.OneStackedColumnHeight();

        private void setVisibilityWaveLineHeight()
        {
            //return;
            int VisibilityChannelCount = ChannelOption.Count(a => a.IsChecked.Value == true);
            //如果没有可见通道，那么上部的x轴和曲线 不可见
            if (VisibilityChannelCount < 1)
            {
                gridUpRow.Height = GridLength.Auto;
                BorderUp.Visibility = Visibility.Collapsed;
                waveLineAxesX.Visibility = Visibility.Collapsed;
                comboBoxTimeList.Visibility = Visibility.Collapsed;
                //setVisibilityWaveLineHeight2();//上部部不可见之后设置下部的高度，可能无限循环。
            }
            else//上部通道可见
            {
                gridUpRow.Height = new GridLength(1, GridUnitType.Star);
                BorderUp.Visibility = Visibility.Visible;
                waveLineAxesX.Visibility = Visibility.Visible;
                comboBoxTimeList.Visibility = Visibility.Visible;
                setStackPanelWidth();
                ////gridUp.Visibility = Visibility.Visible;

                ////gridMain.Visibility = Visibility.Visible;
                ////控件可见的时候设置通道的高度
                ////上下拉伸
                //if (VisibilityChannelCount * contentHeight <= this.scrollViewer.ViewportHeight)
                //{//如果可见控件的总高度之和小于滚动视图的高度,那么设置可见控件的高度为滚动视图高度/VisibilityChannelCount
                // //也就是剩下的可见通道扑满整个滚动视图
                //    contentHeight = this.scrollViewer.ViewportHeight / VisibilityChannelCount;
                //    foreach (var waveLine in willShowWavelineByTherapyMode.Where(a => a.Visibility == Visibility.Visible))
                //    {
                //        if (!Equals(waveLine.Height, contentHeight))
                //        {

                //            System.Diagnostics.Debug.Print("waveLine.Height:" + waveLine.Height
                //               + "contentHeight:" + contentHeight
                //              + "scrollViewer.ViewportHeight:" + scrollViewer.ViewportHeight);

                //            waveLine.ContentHeight = contentHeight;
                //            waveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ActualWidth);
                //            //waveLine.DrawScaleY();
                //            waveLine.DrawLine();

                //            System.Diagnostics.Debug.Print("waveLine.Height:" + waveLine.Height
                //               + "contentHeight:" + contentHeight
                //              + "Width:" + waveLine.Width
                //                + "ViewportWidth:" + waveLine.ViewportWidth
                //              );
                //            setStackPanelWidth();
                //        }
                //    }
                //}
                //else
                //{//默认高度，不拉伸

                //    //if (VisibilityChannelCount == 1)
                //    //{
                //    //    contentHeight = this.UpWave.ActualHeight / VisibilityChannelCount;
                //    //}
                //    //else
                //    //{
                //    //    contentHeight = Const.OneStackedColumnHeight();
                //    //}
                //    contentHeight = Const.OneStackedColumnHeight();

                //    foreach (var waveLine in willShowWavelineByTherapyMode.Where(a => a.Visibility == Visibility.Visible))
                //    {
                //        if (!Equals(waveLine.Height, contentHeight))
                //        {
                //            //System.Diagnostics.Debug.Print("waveLine.Height:" + waveLine.Height
                //            //   + "contentHeight:" + contentHeight
                //            //  + "scrollViewer.ViewportHeight:" + scrollViewer.ViewportHeight);

                //            waveLine.ContentHeight = contentHeight;
                //            waveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                //            waveLine.DrawLine();
                //            //waveLine.drawLine;
                //        }
                //    }
                //}//end if
            }//end if
        }

        private void setVisibilityWaveLineHeight2()
        {
            //return;
            int VisibilityChannelCount = ChannelOption2.Count(a => a.IsChecked.Value == true);
            //如果没有可见通道，那么上部的x轴和曲线 不可见
            if (VisibilityChannelCount < 1)
            {
                //gridMain.Visibility = Visibility.Collapsed;
                gridDown.Visibility = Visibility.Collapsed;
                gridDownRow.Height = GridLength.Auto;
                BorderDown.Visibility = Visibility.Collapsed;
                waveLineAxesX2.Visibility = Visibility.Collapsed;
                comboBoxTimeList2.Visibility = Visibility.Collapsed;
                //setVisibilityWaveLineHeight();//下部不可见之后设置上部的高度，可能无限循环。
            }
            else//上部通道可见
            {
                gridDown.Visibility = Visibility.Visible;
                gridDownRow.Height = new GridLength(1, GridUnitType.Star);
                BorderDown.Visibility = Visibility.Visible;
                waveLineAxesX2.Visibility = Visibility.Visible;
                comboBoxTimeList2.Visibility = Visibility.Visible;
                setStackPanelWidth2();
                ////gridMain.Visibility = Visibility.Visible;
                ////控件可见的时候设置通道的高度
                //if (VisibilityChannelCount * contentHeight2 < this.scrollViewer2.ViewportHeight)
                //{//如果可见控件的总高度之和小于滚动视图的高度,那么设置可见控件的高度为滚动视图高度/VisibilityChannelCount
                // //也就是剩下的可见通道扑满整个滚动视图
                //    contentHeight2 = this.scrollViewer2.ViewportHeight / VisibilityChannelCount;
                //    foreach (var waveLine in willShowWavelineByTherapyMode2.Where(a => a.Visibility == Visibility.Visible))
                //    {
                //        waveLine.ContentHeight = contentHeight2;
                //        waveLine.ViewportWidth = Convert.ToSingle(scrollViewer2.ActualWidth);
                //        //waveLine.DrawScaleY();
                //        waveLine.DrawLine();
                //        setStackPanelWidth2();
                //    }
                //}
                //else
                //{//默认高度，不拉伸
                //    contentHeight2 = Const.OneStackedColumnHeight();
                //    foreach (var waveLine in willShowWavelineByTherapyMode2.Where(a => a.Visibility == Visibility.Visible))
                //    {
                //        waveLine.ContentHeight = contentHeight2;
                //        waveLine.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                //        waveLine.DrawLine();
                //        //waveLine.drawLine;
                //    }
                //}//end if
            }//end if
        }
        #endregion

        #region 治疗模式改变后，根据治疗模式初始化要显示的曲线 willShowWavelineByTherapyMode willShowWavelineByTherapyMode2

        private List<WaveLine> willShowWavelineByTherapyMode = new List<WaveLine>();
        private List<WaveLine> willShowWavelineByTherapyMode2 = new List<WaveLine>();

        /// <summary>
        /// 治疗模式改变后，根据治疗模式初始化要显示的曲线，
        /// 控件的名称需要和PatientDetailedViewModel中checkbox的名称一致
        /// </summary>
        private void initWillShowWavelineByTherapyMode()
        {
            //清空
            willShowWavelineByTherapyMode.Clear();
            willShowWavelineByTherapyMode2.Clear();

            #region 上下两部 保持顺序

            if (Equals(eventWaveLine, null))
            {
                eventWaveLine = new WaveLine();
                eventWaveLine.Name = "Event";
                eventWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                eventWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                eventWaveLine.Title.Text = ResourceHelper.LoadString("Event");
                eventWaveLine.Unit.Text = ResourceHelper.LoadString("Event");
                eventWaveLine.ScaleYDictionary = initEventScaleYDictionary();
            }
            willShowWavelineByTherapyMode.Add(eventWaveLine);
            if (Equals(eventWaveLine2, null))
            {
                eventWaveLine2 = new WaveLine();
                eventWaveLine2.Name = "Event_2";
                eventWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                eventWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                eventWaveLine2.Title.Text = ResourceHelper.LoadString("Event");
                eventWaveLine2.Unit.Text = ResourceHelper.LoadString("Event");
                eventWaveLine2.ScaleYDictionary = initEventScaleYDictionary();
            }
            willShowWavelineByTherapyMode2.Add(eventWaveLine2);

            if (Equals(pressureWaveLine, null))
            {
                pressureWaveLine = new WaveLine();
                pressureWaveLine.Name = "Pressure";
                pressureWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                pressureWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                pressureWaveLine.Title.Text = ResourceHelper.LoadString("Pressure");
                pressureWaveLine.Unit.Text = ResourceHelper.LoadString("PressureUnit");
            }
            willShowWavelineByTherapyMode.Add(pressureWaveLine);
            if (Equals(pressureWaveLine2, null))
            {
                pressureWaveLine2 = new WaveLine();
                pressureWaveLine2.Name = "Pressure_2";
                pressureWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                pressureWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                pressureWaveLine2.Title.Text = ResourceHelper.LoadString("Pressure");
                pressureWaveLine2.Unit.Text = ResourceHelper.LoadString("PressureUnit");
            }
            willShowWavelineByTherapyMode2.Add(pressureWaveLine2);

            if (Equals(flowWaveLine, null))
            {
                flowWaveLine = new WaveLine();
                flowWaveLine.Name = "Flow";
                flowWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                flowWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                flowWaveLine.Title.Text = ResourceHelper.LoadString("Flow");
                flowWaveLine.Unit.Text = ResourceHelper.LoadString("FlowUnit");
            }
            willShowWavelineByTherapyMode.Add(flowWaveLine);
            if (Equals(flowWaveLine2, null))
            {
                flowWaveLine2 = new WaveLine();
                flowWaveLine2.Name = "Flow_2";
                flowWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                flowWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                flowWaveLine2.Title.Text = ResourceHelper.LoadString("Flow");
                flowWaveLine2.Unit.Text = ResourceHelper.LoadString("FlowUnit");
            }
            willShowWavelineByTherapyMode2.Add(flowWaveLine2);

            //CPAP 和APAP模式 和其他的模式不同
            if (TherapyMode != Models.TherapyMode.APAP && TherapyMode != Models.TherapyMode.CPAP)
            {
                if (Equals(minuteVentilationWaveLine, null))
                {
                    minuteVentilationWaveLine = new WaveLine();
                    minuteVentilationWaveLine.Name = "MinuteVentilation";
                    minuteVentilationWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                    minuteVentilationWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                    minuteVentilationWaveLine.Title.Text = ResourceHelper.LoadString("MinuteVentilation");
                    minuteVentilationWaveLine.Unit.Text = ResourceHelper.LoadString("MinuteVentilationUnit");
                }
                willShowWavelineByTherapyMode.Add(minuteVentilationWaveLine);
                if (Equals(minuteVentilationWaveLine2, null))
                {
                    minuteVentilationWaveLine2 = new WaveLine();
                    minuteVentilationWaveLine2.Name = "MinuteVentilation_2";
                    minuteVentilationWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                    minuteVentilationWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                    minuteVentilationWaveLine2.Title.Text = ResourceHelper.LoadString("MinuteVentilation");
                    minuteVentilationWaveLine2.Unit.Text = ResourceHelper.LoadString("MinuteVentilationUnit");
                }
                willShowWavelineByTherapyMode2.Add(minuteVentilationWaveLine2);
            }

            if (Equals(leakWaveLine, null))
            {
                leakWaveLine = new WaveLine();
                leakWaveLine.Name = "Leak";
                leakWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                leakWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                leakWaveLine.Title.Text = ResourceHelper.LoadString("Leak");
                leakWaveLine.Unit.Text = ResourceHelper.LoadString("LeakUnit");
            }
            willShowWavelineByTherapyMode.Add(leakWaveLine);
            if (Equals(leakWaveLine2, null))
            {
                leakWaveLine2 = new WaveLine();
                leakWaveLine2.Name = "Leak_2";
                leakWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                leakWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                leakWaveLine2.Title.Text = ResourceHelper.LoadString("Leak");
                leakWaveLine2.Unit.Text = ResourceHelper.LoadString("LeakUnit");
            }
            willShowWavelineByTherapyMode2.Add(leakWaveLine2);

            if (Equals(tidalVolumeWaveLine, null))
            {
                tidalVolumeWaveLine = new WaveLine();
                tidalVolumeWaveLine.Name = "TidalVolume";
                tidalVolumeWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                tidalVolumeWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                tidalVolumeWaveLine.Title.Text = ResourceHelper.LoadString("TidalVolume");
                tidalVolumeWaveLine.Unit.Text = ResourceHelper.LoadString("TidalVolumeUnit");
            }
            willShowWavelineByTherapyMode.Add(tidalVolumeWaveLine);
            if (Equals(tidalVolumeWaveLine2, null))
            {
                tidalVolumeWaveLine2 = new WaveLine();
                tidalVolumeWaveLine2.Name = "TidalVolume_2";
                tidalVolumeWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                tidalVolumeWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                tidalVolumeWaveLine2.Title.Text = ResourceHelper.LoadString("TidalVolume");
                tidalVolumeWaveLine2.Unit.Text = ResourceHelper.LoadString("TidalVolumeUnit");
            }
            willShowWavelineByTherapyMode2.Add(tidalVolumeWaveLine2);

            //CPAP 和APAP模式 和其他的模式不同
            if (TherapyMode != Models.TherapyMode.APAP && TherapyMode != Models.TherapyMode.CPAP)
            {
                //上部
                if (Equals(respiratoryRateWaveLine, null))
                {
                    respiratoryRateWaveLine = new WaveLine();
                    respiratoryRateWaveLine.Name = "RespiratoryRate";
                    respiratoryRateWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                    respiratoryRateWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                    respiratoryRateWaveLine.Title.Text = ResourceHelper.LoadString("RespiratoryRate");
                    respiratoryRateWaveLine.Unit.Text = ResourceHelper.LoadString("RespiratoryRateUnit");
                }
                willShowWavelineByTherapyMode.Add(respiratoryRateWaveLine);
                if (Equals(respiratoryRateWaveLine2, null))
                {
                    respiratoryRateWaveLine2 = new WaveLine();
                    respiratoryRateWaveLine2.Name = "RespiratoryRate_2";
                    respiratoryRateWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                    respiratoryRateWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                    respiratoryRateWaveLine2.Title.Text = ResourceHelper.LoadString("RespiratoryRate");
                    respiratoryRateWaveLine2.Unit.Text = ResourceHelper.LoadString("RespiratoryRateUnit");
                }
                willShowWavelineByTherapyMode2.Add(respiratoryRateWaveLine2);

                if (Equals(iERatioWaveLine, null))
                {
                    iERatioWaveLine = new WaveLine();
                    iERatioWaveLine.Name = "IERatio";
                    iERatioWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                    iERatioWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                    iERatioWaveLine.Title.Text = ResourceHelper.LoadString("IERatio");
                    iERatioWaveLine.Unit.Text = ResourceHelper.LoadString("IERatioUnit");
                }
                willShowWavelineByTherapyMode.Add(iERatioWaveLine);
                if (Equals(iERatioWaveLine2, null))
                {
                    iERatioWaveLine2 = new WaveLine();
                    iERatioWaveLine2.Name = "IERatio_2";
                    iERatioWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                    iERatioWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                    iERatioWaveLine2.Title.Text = ResourceHelper.LoadString("IERatio");
                    iERatioWaveLine2.Unit.Text = ResourceHelper.LoadString("IERatioUnit");
                }
                willShowWavelineByTherapyMode2.Add(iERatioWaveLine2);

                if (Equals(ePAPWaveLine, null))
                {
                    ePAPWaveLine = new WaveLine();
                    ePAPWaveLine.Name = "EPAP";
                    ePAPWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                    ePAPWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                    ePAPWaveLine.Title.Text = ResourceHelper.LoadString("EPAP");
                    ePAPWaveLine.Unit.Text = ResourceHelper.LoadString("EPAPUnit");
                }
                willShowWavelineByTherapyMode.Add(ePAPWaveLine);
                if (Equals(ePAPWaveLine2, null))
                {
                    ePAPWaveLine2 = new WaveLine();
                    ePAPWaveLine2.Name = "EPAP_2";
                    ePAPWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                    ePAPWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                    ePAPWaveLine2.Title.Text = ResourceHelper.LoadString("EPAP");
                    ePAPWaveLine2.Unit.Text = ResourceHelper.LoadString("EPAPUnit");
                }
                willShowWavelineByTherapyMode2.Add(ePAPWaveLine2);

                if (Equals(iPAPWaveLine, null))
                {
                    iPAPWaveLine = new WaveLine();
                    iPAPWaveLine.Name = "IPAP";
                    iPAPWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                    iPAPWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                    iPAPWaveLine.Title.Text = ResourceHelper.LoadString("IPAP");
                    iPAPWaveLine.Unit.Text = ResourceHelper.LoadString("IPAPnit");
                }
                willShowWavelineByTherapyMode.Add(iPAPWaveLine);
                if (Equals(iPAPWaveLine2, null))
                {
                    iPAPWaveLine2 = new WaveLine();
                    iPAPWaveLine2.Name = "IPAP_2";
                    iPAPWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                    iPAPWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                    iPAPWaveLine2.Title.Text = ResourceHelper.LoadString("IPAP");
                    iPAPWaveLine2.Unit.Text = ResourceHelper.LoadString("IPAPnit");
                }
                willShowWavelineByTherapyMode2.Add(iPAPWaveLine2);
            }

            if (Equals(spO2WaveLine, null))
            {
                spO2WaveLine = new WaveLine();
                spO2WaveLine.Name = "SpO2";
                spO2WaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                spO2WaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                spO2WaveLine.Title.Text = ResourceHelper.LoadString("SpO2");
                spO2WaveLine.Unit.Text = ResourceHelper.LoadString("SpO2Unit");

            }
            willShowWavelineByTherapyMode.Add(spO2WaveLine);
            if (Equals(spO2WaveLine2, null))
            {
                spO2WaveLine2 = new WaveLine();
                spO2WaveLine2.Name = "SpO2_2";
                spO2WaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                spO2WaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                spO2WaveLine2.Title.Text = ResourceHelper.LoadString("SpO2");
                spO2WaveLine2.Unit.Text = ResourceHelper.LoadString("SpO2Unit");
            }
            willShowWavelineByTherapyMode2.Add(spO2WaveLine2);

            if (Equals(pulseRateWaveLine, null))
            {
                pulseRateWaveLine = new WaveLine();
                pulseRateWaveLine.Name = "PulseRate";
                pulseRateWaveLine.HorizontalAlignment = HorizontalAlignment.Left;
                pulseRateWaveLine.ViewportWidth = Convert.ToSingle(scrollViewer.ViewportWidth);
                pulseRateWaveLine.Title.Text = ResourceHelper.LoadString("PulseRate");
                pulseRateWaveLine.Unit.Text = ResourceHelper.LoadString("PulseRateUnit");
            }
            willShowWavelineByTherapyMode.Add(pulseRateWaveLine);
            if (Equals(pulseRateWaveLine2, null))
            {
                pulseRateWaveLine2 = new WaveLine();
                pulseRateWaveLine2.Name = "PulseRate_2";
                pulseRateWaveLine2.HorizontalAlignment = HorizontalAlignment.Left;
                pulseRateWaveLine2.ViewportWidth = Convert.ToSingle(scrollViewer2.ViewportWidth);
                pulseRateWaveLine2.Title.Text = ResourceHelper.LoadString("PulseRate");
                pulseRateWaveLine2.Unit.Text = ResourceHelper.LoadString("PulseRateUnit");
            }
            willShowWavelineByTherapyMode2.Add(pulseRateWaveLine2);

            #endregion

            //清空

            stackPanel.Children.Clear();
            stackPanel2.Children.Clear();

            //设置stack控件宽度
            setStackPanelWidth();
            setStackPanelWidth2();

            ////添加占位隐藏控件
            //stackPanel.Children.Add(waveLineHide);
            //stackPanel2.Children.Add(waveLineHide2);

            //添加曲线控件到界面
            foreach (var v in willShowWavelineByTherapyMode)
            {
                stackPanel.Children.Add(v);
            }
            foreach (var v in willShowWavelineByTherapyMode2)
            {
                stackPanel2.Children.Add(v);
            }
        }

        #endregion

        #endregion

        #region 界面控件的关联事件

        #region comboBox时间选择改变事件 ，需要更新坐标和 可见的曲线

        private void comboBoxTimeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpAxesX();
            setStackPanelWidth();
            //setWaveLineHideWidth();
            UpWaveLine();
        }

        /// <summary>
        /// 根据选择的时间段和当前scrollViewer.ViewportWidth的宽度
        /// 设置stackPanel容器的宽度，以便撑开滚动条
        /// </summary>
        private void setStackPanelWidth()
        {
            //waveLineHide.ViewTime = comboBoxTimeList.SelectedValue.GetInt();
            //waveLineHide.ViewportWidth = scrollViewer.ViewportWidth;
            //waveLineHide.SetWidth();
            //stackPanel.Width = waveLineHide.Width + Const.LengndWidth;

            stackPanel.Width = (Const.OneDayTotalMilliseconds() / comboBoxTimeList.SelectedValue.GetInt() * (scrollViewer.ViewportWidth - Const.LengndWidth))
              + Const.LengndWidth;

        }

        /// <summary>
        /// 根据选择的时间段和当前scrollViewer2.ViewportWidth的宽度
        /// 设置stackPanel容器的宽度，以便撑开滚动条
        /// </summary>
        private void setStackPanelWidth2()
        {
            //waveLineHide2.ViewTime = comboBoxTimeList2.SelectedValue.GetInt();
            //waveLineHide2.ViewportWidth = scrollViewer2.ViewportWidth;
            //waveLineHide2.SetWidth();

            stackPanel2.Width = (Const.OneDayTotalMilliseconds() / comboBoxTimeList2.SelectedValue.GetInt() * (scrollViewer2.ViewportWidth - Const.LengndWidth))
                + Const.LengndWidth;
            //Width = Const.OneDayTotalMilliseconds() / ViewTime * (ViewportWidth - Const.LengndWidth);
        }

        private void comboBoxTimeList2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpAxesX2();
            //setWaveLineHideWidth2();
            setStackPanelWidth2();
            UpWaveLine2();
        }

        #endregion

        #region 滚动视图水平滚动事件，需要更新坐标和可见的曲线
        private double horizontalOffsetOld;
        private double horizontalOffsetOld2;

        /// <summary>
        /// 上部 滚动视图左右改变事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalOffset == horizontalOffsetOld)
            {
                return;
            }
            horizontalOffsetOld = e.HorizontalOffset;
            //UpWaveLineUI 
            UpAxesX();
            UpWaveLine();
            //if (Equals(waveLineListViewModel, null))
            //{
            //    waveLineListViewModel = DataContext as WaveLineListViewModel;
            //    waveLineListViewModel.OnChannelChanged += WaveLineListViewModel_OnChannelChanged;
            //    waveLineListViewModel.OnChannelChanged2 += WaveLineListViewModel_OnChannelChanged2;
            //}
            //if (!Equals(waveLineListViewModel, null))
            //{
            //    waveLineListViewModel.HorizontalOffset = e.HorizontalOffset;
            //    var padding = new Thickness(e.HorizontalOffset, 0, 0, 0);
            //    if (WaveLineEvent.Visibility == Visibility.Visible) WaveLineEvent.Margin = padding;
            //    if (WaveLinePressure.Visibility == Visibility.Visible) WaveLinePressure.Margin = padding;
            //    if (WaveLineFlow.Visibility == Visibility.Visible) WaveLineFlow.Margin = padding;
            //    if (WaveLineLeak.Visibility == Visibility.Visible) WaveLineLeak.Margin = padding;
            //    if (WaveLineTidalVolume.Visibility == Visibility.Visible) WaveLineTidalVolume.Margin = padding;
            //    if (WaveLineRespiratoryRate.Visibility == Visibility.Visible) WaveLineRespiratoryRate.Margin = padding;
            //    if (WaveLineMinuteVentilation.Visibility == Visibility.Visible) WaveLineMinuteVentilation.Margin = padding;
            //    if (WaveLineSpO2.Visibility == Visibility.Visible) WaveLineSpO2.Margin = padding;
            //    if (WaveLinePulseRate.Visibility == Visibility.Visible) WaveLinePulseRate.Margin = padding;
            //    if (WaveLineIERatio.Visibility == Visibility.Visible) WaveLineIERatio.Margin = padding;
            //    if (WaveLineIPAP.Visibility == Visibility.Visible) WaveLineIPAP.Margin = padding;
            //    if (WaveLineEPAP.Visibility == Visibility.Visible) WaveLineEPAP.Margin = padding;
            //}

        }

        /// <summary>
        /// 下部 滚动视图左右改变事件函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollViewer2_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalOffset == horizontalOffsetOld2)
            {
                return;
            }
            horizontalOffsetOld2 = e.HorizontalOffset;
            UpAxesX2();
            UpWaveLine2();
            //UpAxesX2();
            //UpWaveLineUI();

            //if (Equals(waveLineListViewModel, null))
            //{
            //    waveLineListViewModel = DataContext as WaveLineListViewModel;
            //    waveLineListViewModel.OnChannelChanged += WaveLineListViewModel_OnChannelChanged;
            //    waveLineListViewModel.OnChannelChanged2 += WaveLineListViewModel_OnChannelChanged2;
            //}
            //if (!Equals(waveLineListViewModel, null))
            //{
            //    waveLineListViewModel.HorizontalOffset2 = e.HorizontalOffset;
            //    var padding = new Thickness(e.HorizontalOffset, 0, 0, 0);
            //    if (WaveLineEvent2.Visibility == Visibility.Visible) WaveLineEvent2.Margin = padding;
            //    if (WaveLinePressure2.Visibility == Visibility.Visible) WaveLinePressure2.Margin = padding;
            //    if (WaveLineFlow2.Visibility == Visibility.Visible) WaveLineFlow2.Margin = padding;
            //    if (WaveLineLeak2.Visibility == Visibility.Visible) WaveLineLeak2.Margin = padding;
            //    if (WaveLineTidalVolume2.Visibility == Visibility.Visible) WaveLineTidalVolume2.Margin = padding;
            //    if (WaveLineRespiratoryRate2.Visibility == Visibility.Visible) WaveLineRespiratoryRate2.Margin = padding;
            //    if (WaveLineMinuteVentilation2.Visibility == Visibility.Visible) WaveLineMinuteVentilation2.Margin = padding;
            //    if (WaveLineSpO22.Visibility == Visibility.Visible) WaveLineSpO22.Margin = padding;
            //    if (WaveLinePulseRate2.Visibility == Visibility.Visible) WaveLinePulseRate2.Margin = padding;
            //    if (WaveLineIERatio2.Visibility == Visibility.Visible) WaveLineIERatio2.Margin = padding;
            //    if (WaveLineIPAP2.Visibility == Visibility.Visible) WaveLineIPAP2.Margin = padding;
            //    if (WaveLineEPAP2.Visibility == Visibility.Visible) WaveLineEPAP2.Margin = padding;
            //}
        }

        #endregion

        //#region 滚动控件大小改变事件 需要更新坐标和可见的曲线

        //private bool isfirstLoadscrollViewer = true, isfirstLoadscrollViewer2 = true;

        //private void scrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    //waveLineAxesX.ViewTime = waveLineListViewModel.SelectedTimeValue;
        //    //WaveLineListViewModel_OnChannelChanged(null, null);
        //    if (isfirstLoadscrollViewer)
        //    {
        //        isfirstLoadscrollViewer = false;
        //        return;
        //    }
        //    System.Diagnostics.Debug.Print("WaveLineList2 scrollViewer_SizeChanged" + e.NewSize.ToString() + " scrollViewer.ViewportWidth:" + scrollViewer.ViewportWidth + scrollViewer.ComputedVerticalScrollBarVisibility);
        //    //UpAxesX();
        //    //UpWaveLine();
        //    //UpAllUI();
        //}

        //private void scrollViewer2_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (isfirstLoadscrollViewer2)
        //    {
        //        isfirstLoadscrollViewer2 = false;
        //        return;
        //    }
        //    //UpAxesX2();
        //    //UpWaveLine2();
        //    //spLine.Margin = new Thickness((scrollViewer2.ActualWidth + Const.LengndWidth) / 2, 0, 0, 0);
        //    //WaveLineListViewModel_OnChannelChanged2(null, null);
        //    //waveLineAxesX2.ViewTime = waveLineListViewModel.SelectedTime2Value;
        //    System.Diagnostics.Debug.Print("WaveLineList2 scrollViewer2_SizeChanged" + e.NewSize.ToString() + " scrollViewer2.ViewportWidth:" + scrollViewer2.ViewportWidth);

        //}

        //#endregion

        #endregion

        #region 辅助功能，参考线、上下联动功能等

        private void stackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            line.Visibility = Visibility.Collapsed;
            var X = e.GetPosition(stackPanel).X - Const.LengndWidth;
            var ViewTime = waveLineAxesX.ViewTime;
            var vv = waveLineAxesX.HorizontalOffset;
            var v = X + scrollViewer.HorizontalOffset * waveLineAxesX.RatioX;
            var mills = v / waveLineAxesX.RatioX;
            scrollViewer2.ScrollToHorizontalOffset(scrollViewer2.ScrollableWidth * (mills / Const.MilliSecFor24Hour));

            var x = e.GetPosition(stackPanel).X;
            if (x > Const.LengndWidth)
            {
                line.Visibility = Visibility.Visible;
                if (line.X1 == Convert.ToInt32(x))
                {
                    return;
                }
                line.X1 = line.X2 = Convert.ToInt32(x);
            }
        }

        private void stackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //var x = e.GetPosition(stackPanel).X;
            //if (x < Const.LengndWidth)
            //{
            //    line.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    line.Visibility = Visibility.Visible;
            //    if (line.X1 == Convert.ToInt32(x))
            //    {
            //        return;
            //    }
            //    line.X1 = line.X2 = Convert.ToInt32(x) + 2;
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpAllUI();
        }

        private void stackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //line.Visibility = Visibility.Collapsed;
        }


        #endregion

        ///// <summary>
        ///// 上部：解决时间选择之后滚动条值不变的问题
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void WaveLineHide_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    //解决时间选择之后滚动条值不变的问题
        //    //stackPanel.Width = waveLineHide.Width + Const.LengndWidth;
        //}

        ///// <summary>
        ///// 下部：解决时间选择之后滚动条值不变的问题
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void WaveLineHide2_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    //解决时间选择之后滚动条值不变的问题
        //    //stackPanel2.Width = waveLineHide2.Width + Const.LengndWidth;
        //}

        ///// <summary>
        ///// 通道选项菜单
        ///// </summary>
        // public ContextMenu ChannelOptionsContextMenu { get; set; }





        //private int VisibleControlCountChannel = 0;
        //private int VisibleControlCountChannel2 = 0;

        ///// <summary>
        ///// WaveLineListViewModel上部通道改变事件OnChannelChanged
        ///// TODO：最后一个通道有问题
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void WaveLineListViewModel_OnChannelChanged(object sender, EventArgs e)
        //{
        //    if (Equals(waveLineListViewModel, null))
        //    {
        //        waveLineListViewModel = DataContext as WaveLineListViewModel;
        //        waveLineListViewModel.OnChannelChanged += WaveLineListViewModel_OnChannelChanged;
        //        waveLineListViewModel.OnChannelChanged2 += WaveLineListViewModel_OnChannelChanged2;
        //    }

        //    if (!Equals(waveLineListViewModel, null))
        //    {
        //        VisibleControlCountChannel = 0;
        //        foreach (UIElement v in stackPanel.Children)
        //        {
        //            if (v.Visibility == Visibility.Visible)
        //            {
        //                VisibleControlCountChannel = VisibleControlCountChannel + 1;
        //            }
        //        }
        //        if (VisibleControlCountChannel < 1)
        //        {
        //            VisibleControlCountChannel2 = 0;
        //            foreach (UIElement v in stackPanel2.Children)
        //            {
        //                if (v.Visibility == Visibility.Visible)
        //                {
        //                    VisibleControlCountChannel2 = VisibleControlCountChannel2 + 1;
        //                }
        //            }
        //            if (VisibleControlCountChannel2 > 0)
        //            {
        //                WaveLineListViewModel_OnChannelChanged2(null, null);
        //            }
        //            else
        //            {
        //                return;
        //            }
        //            return;
        //        }
        //        //scrollViewer的大小未变，最后一个通道无变化
        //        var height = scrollViewer.ViewportHeight / VisibleControlCountChannel;

        //        if (height > contentHeight)
        //        {
        //            waveLineListViewModel.ContentHeight = height;
        //        }
        //        else
        //        {
        //            waveLineListViewModel.ContentHeight = contentHeight;
        //        }
        //    }
        //}

        ///// <summary>
        ///// WaveLineListViewModel下部通道改变事件OnChannelChanged
        ///// TODO：最后一个通道有问题
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void WaveLineListViewModel_OnChannelChanged2(object sender, EventArgs e)
        //{
        //    if (Equals(waveLineListViewModel, null))
        //    {
        //        waveLineListViewModel = DataContext as WaveLineListViewModel;
        //        //waveLineListViewModel.OnChannelChanged += WaveLineListViewModel_OnChannelChanged;
        //        //waveLineListViewModel.OnChannelChanged2 += WaveLineListViewModel_OnChannelChanged2;
        //    }
        //    if (!Equals(waveLineListViewModel, null))
        //    {
        //        VisibleControlCountChannel2 = 0;
        //        foreach (UIElement v in stackPanel2.Children)
        //        {
        //            if (v.Visibility == Visibility.Visible)
        //            {
        //                VisibleControlCountChannel2 = VisibleControlCountChannel2 + 1;
        //            }
        //        }
        //        if (VisibleControlCountChannel2 < 1)
        //        {
        //            VisibleControlCountChannel = 0;
        //            foreach (UIElement v in stackPanel.Children)
        //            {
        //                if (v.Visibility == Visibility.Visible)
        //                {
        //                    VisibleControlCountChannel = VisibleControlCountChannel + 1;
        //                }
        //            }
        //            if (VisibleControlCountChannel > 0)
        //            {
        //                WaveLineListViewModel_OnChannelChanged(null, null);
        //            }
        //            else
        //            {
        //                return;
        //            }
        //            return;
        //        }
        //        //scrollViewer2的大小未变，最后一个通道无变化
        //        var height = scrollViewer2.ViewportHeight / VisibleControlCountChannel2;
        //        if (height > Const.OneStackedColumnHeight())
        //        {
        //            waveLineListViewModel.ContentHeight2 = height;
        //        }
        //        else
        //        {
        //            waveLineListViewModel.ContentHeight2 = Const.OneStackedColumnHeight();
        //        }
        //    }
        //}
    }
}