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
    /// 视图-患者产品实体
    /// </summary>
    public partial class ViewPatientsProduct : EntityBase<Guid>
    {

        ///// <summary>
        ///// 
        ///// </summary>
        //public System.Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Guid PatientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> ProductModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> TotalWorkingTime { get; set; }
    }
}
