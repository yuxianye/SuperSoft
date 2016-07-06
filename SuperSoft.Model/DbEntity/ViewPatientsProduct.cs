using System;

namespace SuperSoft.Model
{
    /// <summary>
    /// 视图-患者产品实体
    /// </summary>
    public partial class ViewPatientsProduct : EntityBase<Guid>
    {

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
        public int ProductModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalWorkingTime { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            FirstName = null;
            LastName = null;
            SerialNumber = null;
            ProductVersion = null;
        }
    }
}
