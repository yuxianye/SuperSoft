using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 患者实体
    /// </summary>
    public class RespiratoryEvent : EntityBase<Guid>
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Serial { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan DurationTime { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// 最低血氧
        /// </summary>
        public int MinSpO2 { get; set; }

        /// <summary>
        /// 睡眠分期
        /// </summary>
        public string SleepStage { get; set; } = Utility.Windows.ResourceHelper.LoadString(@"Unknow");

        /// <summary>
        /// 体位
        /// </summary>
        public int BPI { get; set; }

        /// <summary>
        /// 最高心率
        /// </summary>
        public int MaxHeartRate { get; set; }

        /// <summary>
        /// 最低心率
        /// </summary>
        public int MinHeartRate { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            SleepStage = null;
        }
    }
}
