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
    /// 患者产品实体
    /// </summary>
    public class PatientsProduct : EntityBase<Guid>
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        public System.Guid PatientId { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public System.Guid ProductId { get; set; }
    }
}
