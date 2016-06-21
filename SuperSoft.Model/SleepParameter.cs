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
    public class SleepParameter : EntityBase<Guid>
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 记录长度
        /// </summary>
        public TimeSpan RecordLength { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public int SpO2 { get; set; } = 100;

        /// <summary>
        /// 醒时血氧
        /// </summary>
        public int WakeSpO2 { get; set; }

        /// <summary>
        /// 最低血氧
        /// </summary>
        public int MinSpO2 { get; set; }

        /// <summary>
        /// 最高血氧
        /// </summary>
        public int MaxSpO2 { get; set; }

        /// <summary>
        /// 平均血氧
        /// </summary>
        public int AvgSpO2 { get; set; }

        /// <summary>
        /// 呼吸暂停
        /// </summary>
        public TimeSpan Apnea { get; set; }

        /// <summary>
        /// 最长呼吸暂停
        /// </summary>
        public TimeSpan MaxApnea { get; set; }

        /// <summary>
        /// 最长呼吸暂停-发生时间
        /// </summary>
        public DateTime MaxApneaHappenTime { get; set; }

        /// <summary>
        /// 最长低通气
        /// </summary>
        public TimeSpan MaxHYP { get; set; }

        /// <summary>
        /// 最长低通气-发生时间
        /// </summary>
        public DateTime MaxHYPHappenTime { get; set; }

        /// <summary>
        /// 呼吸暂停指数
        /// </summary>
        public float AI { get; set; }

        /// <summary>
        /// 低通气指数
        /// </summary>
        public float HI { get; set; }

        /// <summary>
        /// 呼吸紊乱指数
        /// </summary>
        public float AHI { get; set; }

        /// <summary>
        /// 呼吸频率
        /// </summary>
        public int RespiratoryRate { get; set; }

        /// <summary>
        /// 鼾声次数
        /// </summary>
        public int SnoreTimes { get; set; }

        /// <summary>
        /// 总鼾声次数
        /// </summary>
        public int TotalSnoreTimes { get; set; }

        /// <summary>
        /// 鼾声指数
        /// </summary>
        public float SnoreIndex { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        public int HeartRate { get; set; }

        /// <summary>
        /// 最高心率
        /// </summary>
        public int MaxHeartRate { get; set; }

        /// <summary>
        /// 最低心率
        /// </summary>
        public int MinHeartRate { get; set; }

        /// <summary>
        /// 平均心率
        /// </summary>
        public int AvgHeartRate { get; set; }

        /// <summary>
        /// 体位
        /// </summary>
        public int BPI { get; set; } = 0;

        /// <summary>
        /// 睡眠分期
        /// </summary>
        public string SleepStage { get; set; } = Utility.Windows.ResourceHelper.LoadString(@"Unknow");

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            SleepStage = null;
        }
    }
}
