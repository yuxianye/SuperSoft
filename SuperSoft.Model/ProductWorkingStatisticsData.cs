using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 产品运行统计数据实体
    /// </summary>
    public class ProductWorkingStatisticsData : EntityBase<Guid>
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Nullable<System.Guid> ProductId { get; set; }

        /// <summary>
        /// 治疗模式，具体值参看数据格式定义
        /// </summary>
        public int TherapyMode { get; set; }

        /// <summary>
        /// 数据时间
        /// </summary>
        public System.DateTime DataTime { get; set; }

        /// <summary>
        /// 总是用时间（毫秒）
        /// </summary>
        public Nullable<long> TotalUsage { get; set; }

        /// <summary>
        /// AHI次数
        /// </summary>
        public Nullable<int> CountAHI { get; set; }

        /// <summary>
        /// AI次数
        /// </summary>
        public Nullable<int> CountAI { get; set; }

        /// <summary>
        /// HI次数
        /// </summary>
        public Nullable<int> CountHI { get; set; }

        /// <summary>
        /// 鼾声次数
        /// </summary>
        public Nullable<int> CountSnore { get; set; }

        /// <summary>
        /// 被动呼吸次数
        /// </summary>
        public Nullable<int> CountPassive { get; set; }

        /// <summary>
        /// 最大压力
        /// </summary>
        public Nullable<float> PressureMax { get; set; }

        /// <summary>
        /// 95%th压力
        /// </summary>
        public Nullable<float> PressureP95 { get; set; }

        /// <summary>
        /// 中间压力
        /// </summary>
        public Nullable<float> PressureMedian { get; set; }

        /// <summary>
        /// 最大流速
        /// </summary>
        public Nullable<float> FlowMax { get; set; }

        /// <summary>
        /// 95%th流速
        /// </summary>
        public Nullable<float> FlowP95 { get; set; }

        /// <summary>
        /// 中间流速
        /// </summary>
        public Nullable<float> FlowMedian { get; set; }

        /// <summary>
        /// 最大漏气量
        /// </summary>
        public Nullable<float> LeakMax { get; set; }

        /// <summary>
        /// 95%th漏气量
        /// </summary>
        public Nullable<float> LeakP95 { get; set; }

        /// <summary>
        /// 中间漏气量
        /// </summary>
        public Nullable<float> LeakMedian { get; set; }

        /// <summary>
        /// 最大潮气量
        /// </summary>
        public Nullable<float> TidalVolumeMax { get; set; }

        /// <summary>
        /// 95%th漏气量
        /// </summary>
        public Nullable<float> TidalVolumeP95 { get; set; }

        /// <summary>
        /// 中间漏气量
        /// </summary>
        public Nullable<float> TidalVolumeMedian { get; set; }

        /// <summary>
        /// 最大分钟通气量
        /// </summary>
        public Nullable<int> MinuteVentilationMax { get; set; }

        /// <summary>
        /// 95%th分钟通气量
        /// </summary>
        public Nullable<int> MinuteVentilationP95 { get; set; }

        /// <summary>
        /// 中间分钟通气量
        /// </summary>
        public Nullable<int> MinuteVentilationMedian { get; set; }

        /// <summary>
        /// 最大血氧饱和度
        /// </summary>
        public Nullable<int> SpO2Max { get; set; }

        /// <summary>
        /// 95%th血氧饱和度
        /// </summary>
        public Nullable<int> SpO2P95 { get; set; }

        /// <summary>
        /// 中间血氧饱和度
        /// </summary>
        public Nullable<int> SpO2Median { get; set; }

        /// <summary>
        /// 最大脉率
        /// </summary>
        public Nullable<int> PulseRateMax { get; set; }

        /// <summary>
        /// 95%th脉率
        /// </summary>
        public Nullable<int> PulseRateP95 { get; set; }

        /// <summary>
        /// 中间脉率
        /// </summary>
        public Nullable<int> PulseRateMedian { get; set; }

        /// <summary>
        /// 最大呼吸频率
        /// </summary>
        public Nullable<int> RespiratoryRateMax { get; set; }

        /// <summary>
        /// 95%th呼吸频率
        /// </summary>
        public Nullable<int> RespiratoryRateP95 { get; set; }

        /// <summary>
        /// 中间呼吸频率
        /// </summary>
        public Nullable<int> RespiratoryRateMedian { get; set; }

        /// <summary>
        /// 最大吸呼比
        /// </summary>
        public Nullable<float> IERatioMax { get; set; }

        /// <summary>
        /// 95%th吸呼比
        /// </summary>
        public Nullable<float> IERatioP95 { get; set; }

        /// <summary>
        /// 中间吸呼比
        /// </summary>
        public Nullable<float> IERatioMedian { get; set; }

        /// <summary>
        /// 最大IPAP
        /// </summary>
        public Nullable<float> IPAPMax { get; set; }

        /// <summary>
        /// 95%thIPAP
        /// </summary>
        public Nullable<float> IPAPP95 { get; set; }

        /// <summary>
        /// 中间IPAP
        /// </summary>
        public Nullable<float> IPAPMedian { get; set; }

        /// <summary>
        /// 最大EPAP
        /// </summary>
        public Nullable<float> EPAPMax { get; set; }

        /// <summary>
        /// 95%thEPAP
        /// </summary>
        public Nullable<float> EPAPP95 { get; set; }

        /// <summary>
        /// 中间EPAP
        /// </summary>
        public Nullable<float> EPAPMedian { get; set; }


        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            //Content = null;
        }
    }
}
