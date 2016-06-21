using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    /// <summary>
    /// 报表数据源
    /// </summary>
    public class ReportStatisticsContent : Utility.MyClassBase
    {
        public string ImagePath { get; set; }


        //public Patient Patient { get; set; }
        //public Patient Patient { get; set; }


        //public string Doctor { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            ImagePath = null;
        }

    }

    /// <summary>
    /// 报表医嘱建议
    /// </summary>
    public class ReportAdvise : Utility.MyClassBase
    {
        /// <summary>
        /// 医嘱建议
        /// </summary>
        public string Advise { get; set; }




        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            Advise = null;
        }
    }

    /// <summary>
    /// 报表静态文字，多语言时重新加载
    /// </summary>
    public class ReportStaticString : Utility.MyClassBase
    {

        #region 报表普通字符串

        public string ReportTitle { get; set; }

        public string ReportCreateTime { get; set; }

        #endregion

        #region 患者信息相关字符串

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
        public string Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Male { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Female { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string SBP { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string DBP { get; set; }

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


        #endregion



        #region 医嘱建议

        public string Advise { get; set; }

        #endregion

        ///// <summary>
        ///// 记录开始时间
        ///// </summary>
        //public string RecordStartTime { get; set; }

        ///// <summary>
        ///// 记录结束时间
        ///// </summary>
        //public string RecordEndTime { get; set; }

        ///// <summary>
        ///// 当前时间
        ///// </summary>
        //public string CurrentTime { get; set; }

        ///// <summary>
        ///// 文件名
        ///// </summary>
        //public string FileName { get; set; }


        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            ReportTitle = null;
            ReportCreateTime = null;
            Number = null;
            Name = null;
            Age = null;
            Gender = null;
            Male = null;
            Female = null;
            Height = null;
            Weight = null;
            SBP = null;
            DBP = null;
            PostalCode = null;
            Address = null;
            PhoneNumber = null;
            WatchKeeper = null;
            Memo = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;
            Advise = null;

            Advise = null;
        }
    }


}
