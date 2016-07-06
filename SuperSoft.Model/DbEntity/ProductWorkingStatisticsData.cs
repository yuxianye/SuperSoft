using System;

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
        public System.Guid ProductId { get; set; }

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
        public long TotalUsage { get; set; }

        /// <summary>
        /// AHI次数
        /// </summary>
        public int CountAHI { get; set; }

        /// <summary>
        /// AI次数
        /// </summary>
        public int CountAI { get; set; }

        /// <summary>
        /// HI次数
        /// </summary>
        public int CountHI { get; set; }

        /// <summary>
        /// 鼾声次数
        /// </summary>
        public int CountSnore { get; set; }

        /// <summary>
        /// 被动呼吸次数
        /// </summary>
        public int CountPassive { get; set; }

        /// <summary>
        /// 最大压力
        /// </summary>
        public float PressureMax { get; set; }

        /// <summary>
        /// 95%th压力
        /// </summary>
        public float PressureP95 { get; set; }

        /// <summary>
        /// 中间压力
        /// </summary>
        public float PressureMedian { get; set; }

        /// <summary>
        /// 最大流速
        /// </summary>
        public float FlowMax { get; set; }

        /// <summary>
        /// 95%th流速
        /// </summary>
        public float FlowP95 { get; set; }

        /// <summary>
        /// 中间流速
        /// </summary>
        public float FlowMedian { get; set; }

        /// <summary>
        /// 最大漏气量
        /// </summary>
        public float LeakMax { get; set; }

        /// <summary>
        /// 95%th漏气量
        /// </summary>
        public float LeakP95 { get; set; }

        /// <summary>
        /// 中间漏气量
        /// </summary>
        public float LeakMedian { get; set; }

        /// <summary>
        /// 最大潮气量
        /// </summary>
        public float TidalVolumeMax { get; set; }

        /// <summary>
        /// 95%th漏气量
        /// </summary>
        public float TidalVolumeP95 { get; set; }

        /// <summary>
        /// 中间漏气量
        /// </summary>
        public float TidalVolumeMedian { get; set; }

        /// <summary>
        /// 最大分钟通气量
        /// </summary>
        public int MinuteVentilationMax { get; set; }

        /// <summary>
        /// 95%th分钟通气量
        /// </summary>
        public int MinuteVentilationP95 { get; set; }

        /// <summary>
        /// 中间分钟通气量
        /// </summary>
        public int MinuteVentilationMedian { get; set; }

        /// <summary>
        /// 最大血氧饱和度
        /// </summary>
        public int SpO2Max { get; set; }

        /// <summary>
        /// 95%th血氧饱和度
        /// </summary>
        public int SpO2P95 { get; set; }

        /// <summary>
        /// 中间血氧饱和度
        /// </summary>
        public int SpO2Median { get; set; }

        /// <summary>
        /// 最大脉率
        /// </summary>
        public int PulseRateMax { get; set; }

        /// <summary>
        /// 95%th脉率
        /// </summary>
        public int PulseRateP95 { get; set; }

        /// <summary>
        /// 中间脉率
        /// </summary>
        public int PulseRateMedian { get; set; }

        /// <summary>
        /// 最大呼吸频率
        /// </summary>
        public int RespiratoryRateMax { get; set; }

        /// <summary>
        /// 95%th呼吸频率
        /// </summary>
        public int RespiratoryRateP95 { get; set; }

        /// <summary>
        /// 中间呼吸频率
        /// </summary>
        public int RespiratoryRateMedian { get; set; }

        /// <summary>
        /// 最大吸呼比
        /// </summary>
        public float IERatioMax { get; set; }

        /// <summary>
        /// 95%th吸呼比
        /// </summary>
        public float IERatioP95 { get; set; }

        /// <summary>
        /// 中间吸呼比
        /// </summary>
        public float IERatioMedian { get; set; }

        /// <summary>
        /// 最大IPAP
        /// </summary>
        public float IPAPMax { get; set; }

        /// <summary>
        /// 95%thIPAP
        /// </summary>
        public float IPAPP95 { get; set; }

        /// <summary>
        /// 中间IPAP
        /// </summary>
        public float IPAPMedian { get; set; }

        /// <summary>
        /// 最大EPAP
        /// </summary>
        public float EPAPMax { get; set; }

        /// <summary>
        /// 95%thEPAP
        /// </summary>
        public float EPAPP95 { get; set; }

        /// <summary>
        /// 中间EPAP
        /// </summary>
        public float EPAPMedian { get; set; }

    }
}
