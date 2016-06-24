using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 索引文件【SY_RMS.TXT】的字段
    /// Sy_RMS:302200003311180015,APAP20_V030_131105,20140613171209,000030hrs;
    /// </summary>
    public class IndexFileField : MyClassBase
    {
        /// <summary>
        /// 数据库中Products表中sn对应的Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        public string ProductVersion { get; set; }

        public int ProductModel { get; set; }

        public int TotalWorkingTime { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            ProductId = Guid.Empty;
            SerialNumber = null;
            ProductVersion = null;
        }
    }
}
