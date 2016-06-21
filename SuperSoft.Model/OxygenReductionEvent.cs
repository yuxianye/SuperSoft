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
    public class OxygenReductionEvent : EntityBase<Guid>
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
        /// 下降前血氧
        /// </summary>
        public int PreviousSpO2 { get; set; }

        /// <summary>
        /// 最低血氧
        /// </summary>
        public int MinSpO2 { get; set; }

        /// <summary>
        /// 恢复后血氧
        /// </summary>
        public int NextSpO2 { get; set; }

        /// <summary>
        /// 睡眠分期
        /// </summary>
        public string SleepStage { get; set; } = SuperSoft.Utility.Windows.ResourceHelper.LoadString(@"Unknow");

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            SleepStage = null;
        }
    }
}
