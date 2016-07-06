using System;

namespace SuperSoft.Model
{
    /// <summary>
    /// 产品实体
    /// </summary>
    public class Product : EntityBase<Guid>
    {
        /// <summary>
        /// 序列号18位数字
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 产品版本
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public int ProductModel { get; set; }

        /// <summary>
        /// 总运行时间
        /// </summary>
        public int TotalWorkingTime { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            SerialNumber = null;
            ProductVersion = null;
        }
    }
}
