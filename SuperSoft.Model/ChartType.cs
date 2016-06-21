using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
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
