using ProtoBuf;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{

    /// <summary>
    /// 详细数据实体类,可序列化和反序列化
    /// 0--2	时、分、秒
    /// 3--4	目标压力
    /// 5--6	当前压力
    /// 7--8	当前流量
    /// 9	呼吸事件0，无；1低通气，2，呼吸暂停
    /// 10--18	备用
    /// </summary>
    [ProtoContract]
    public struct DetailedField
    {
        [ProtoMember(1)]
        public DateTime RecoredTime { get; set; }

        [ProtoMember(2)]
        public float TargetPressure { get; set; }

        [ProtoMember(3)]
        public float CurrentPressure { get; set; }

        [ProtoMember(4)]
        public float CurrentFlow { get; set; }

        /// <summary>
        /// 呼吸事件对128求余后：
        /// 0，无；1低通气，2，呼吸暂停 4，鼾声
        /// 主动/被动 ：主动>=128,被动<128;
        /// </summary>
        [ProtoMember(5)]
        public int Events { get; set; }

        ///// <summary>
        /////AI呼吸暂停
        ///// </summary>
        //[ProtoMember(5)]
        //public int AI { get; set; }

        ///// <summary>
        ///// HI低通气
        ///// </summary>
        //[ProtoMember(6)]
        //public int HI { get; set; }

        ///// <summary>
        ///// 鼾声
        ///// </summary>
        //[ProtoMember(7)]
        //public int SnoreIndex { get; set; }


        /// <summary>
        /// 漏气量
        /// </summary>
        [ProtoMember(6)]
        public float Leak { get; set; }

        /// <summary>
        /// 潮气量
        /// </summary>
        [ProtoMember(7)]
        public float TidalVolume { get; set; }

        /// <summary>
        /// 分钟通气量(0-30)L/min
        /// </summary>
        [ProtoMember(8)]
        public int MinuteVentilation { get; set; }

        /// <summary>
        /// 呼吸频率
        /// </summary>
        [ProtoMember(9)]
        public int RespiratoryRate { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        [ProtoMember(10)]
        public int SpO2 { get; set; }

        /// <summary>
        /// 脉率
        /// </summary>
        [ProtoMember(11)]
        public int PulseRate { get; set; }

        /// <summary>
        /// 吸呼比(1-99),1:0.1-9.9
        /// </summary>
        [ProtoMember(12)]
        public float IERatio { get; set; }

        /// <summary>
        /// IPAP
        /// </summary>
        [ProtoMember(13)]
        public float IPAP { get; set; }

        /// <summary>
        /// EPAP
        /// </summary>
        [ProtoMember(14)]
        public float EPAP { get; set; }

        /// <summary>
        /// 触发模式，主动被动 主动>=128,被动<128;
        /// </summary>
        [ProtoMember(15)]
        public int TriggerMode { get; set; }
    }
}
