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
    public class Patient : EntityBase<Guid>, ICloneable
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public int SBP { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public int DBP { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 值班人员
        /// </summary>
        public string WatchKeeper { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 记录开始时间
        /// </summary>
        public DateTime RecordStartTime { get; set; }

        /// <summary>
        /// 记录结束时间
        /// </summary>
        public DateTime RecordEndTime { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            Number = null;
            Name = null;
            PostalCode = null;
            Address = null;
            PhoneNumber = null;
            WatchKeeper = null;
            Memo = null;
            FileName = null;
        }
    }
}
