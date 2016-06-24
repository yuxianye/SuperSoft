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
    /// 产品运行详细数据实体
    /// </summary>
    public class ProductWorkingDetailedData : EntityBase<Guid>
    {
        /// <summary>
        /// 产品运行概要数据Id
        /// </summary>
        public Nullable<System.Guid> ProductWorkingSummaryDataId { get; set; }

        /// <summary>
        /// 序列化（ProtoBuf）后的二进制内容
        /// </summary>
        public byte[] Content { get; set; }


        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            Content = null;
        }
    }
}
