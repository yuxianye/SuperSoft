using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;

namespace SuperSoft.BLL.DownloadData
{
    /// <summary>
    /// 内部类
    /// </summary>
    internal class SummaryAndDetailed : MyClassBase
    {
        public SummaryAndDetailed(ProductWorkingSummaryData productWorkingSummaryData, IList<byte[]> detailedList)
        {
            ProductWorkingSummaryData = productWorkingSummaryData;
            DetailedList = detailedList;
        }

        public ProductWorkingSummaryData ProductWorkingSummaryData { get; set; }
        public IList<byte[]> DetailedList { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            ProductWorkingSummaryData = null;
            DetailedList = null;
        }
    }

    internal class NeedUpDateTherapyMode : MyClassBase
    {
        public NeedUpDateTherapyMode(DateTime dataTime, int therapyMode)
        {
            DataTime = dataTime;
            TherapyMode = therapyMode;
        }

        /// <summary>
        /// 数据的时间
        /// </summary>
        public DateTime DataTime { get; set; }

        /// <summary>
        /// 治疗模式
        /// </summary>
        public int TherapyMode { get; set; }
    }
}