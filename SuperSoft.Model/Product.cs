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
    /// 产品实体
    /// </summary>
    public class Product : EntityBase<Guid>//, ICloneable
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
        public Nullable<int> ProductModel { get; set; }

        /// <summary>
        /// 总运行时间
        /// </summary>
        public Nullable<int> TotalWorkingTime { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            SerialNumber = null;
            ProductVersion = null;
            ProductModel = null;
            TotalWorkingTime = null;

        }
    }
}
