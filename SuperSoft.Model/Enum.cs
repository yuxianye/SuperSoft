using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 治疗模式(数据库中存储对应的byte值)
    /// var values=    Enum.GetValues(typeof (TherapyMode ));
    /// var names = Enum.GetNames (typeof(TherapyMode));
    /// </summary>
    public enum TherapyMode
    {
        [Description("S/T")]
        ST = 0x00,

        [Description("T")]
        T = 0x01,

        [Description("S")]
        S = 0x02,

        [Description("CPAP")]
        CPAP = 0x03,

        [Description("APAP")]
        APAP = 0x04,

        [Description("PCV")]
        PCV = 0x05,

        [Description("AutoS")]
        AutoS = 0x06,

        [Description("Unknown")]
        Unknown = 0xFF
    }

    /// <summary>
    /// 产品型号(数据库中存储对应的byte值)
    /// </summary>
    public enum ProductModels
    {
        [Description("BPAP30")]
        BPAP30 = 0x00,

        [Description("BPAP25")]
        BPAP25 = 0x01,

        [Description("BPAP20")]
        BPAP20 = 0x02,

        [Description("CPAP20")]
        CPAP20 = 0x03,

        [Description("APAP20")]
        APAP20 = 0x04,

        [Description("APAP20E")]
        APAP20E = 0x05,

        [Description("BPAP20S")]
        BPAP20S = 0x06,

        [Description("BPAP25MPlus")]
        BPAP25MPlus = 0x07,

        [Description("BPAP30MPlus")]
        BPAP30MPlus = 0x08,

        [Description("BPAP20Plus")]
        BPAP20Plus = 0x09,

        [Description("BPAP25Plus")]
        BPAP25Plus = 0x0A,

        [Description("BPAP30Plus")]
        BPAP30Plus = 0x0B,

        [Description("BPAP20Auto")]
        BPAP20Auto = 0x0C,

        [Description("BPAP25Auto")]
        BPAP25Auto = 0x0D,

        [Description("BPAP25Pro")]
        BPAP25Pro = 0x0E,

        [Description("BPAP30Pro")]
        BPAP30Pro = 0x0F,

        [Description("BPAP20F")]
        BPAP20F = 0x10,

        [Description("BPAP30F")]
        BPAP30F = 0x11,

        [Description("Unknown")]
        Unknown = 0xFF
    }

    /// <summary>
    /// 呼吸触发模式
    /// </summary>
    public enum TriggerMode
    {
        /// <summary>
        /// 被动：时间模式
        /// </summary>
        Timing = 0,

        /// <summary>
        /// 主动：自主模式
        /// </summary>
        Automatic = 1
    }



    public enum ChartType
    {

        /// <summary>
        /// 未知
        /// </summary>
        Unknown,

        #region 直接从原始数据获取，不用分析

        /// <summary>
        /// 体位
        /// </summary>
        BPI,

        /// <summary>
        /// 口鼻气流
        /// </summary>
        OronasalAirflow,

        /// <summary>
        /// 胸呼吸
        /// </summary>
        ChestBreathing,

        /// <summary>
        /// 腹呼吸
        /// </summary>
        BellyBreathing,

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        SpO2,

        /// <summary>
        /// 心率(脉率)
        /// </summary>
        HeartRate,

        /// <summary>
        /// 鼾声
        /// </summary>
        Snore,

        /// <summary>
        /// 呼吸机压力
        /// </summary>
        Pressure,

        /// <summary>
        /// 呼吸机流速
        /// </summary>
        Flow,

        /// <summary>
        /// 呼吸机事件
        /// </summary>
        Event,

        #endregion

        /// <summary>
        /// 呼吸暂停
        /// </summary>
        Apnea,

        /// <summary>
        /// 呼吸频率
        /// </summary>
        RespiratoryRate,

        /// <summary>
        /// 鼾声次数
        /// </summary>
        SnoreTimes,

        /// <summary>
        /// 事件标志
        /// </summary>
        EventFlag,

    }
}
