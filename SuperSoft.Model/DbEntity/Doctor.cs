using System;

namespace SuperSoft.Model
{
    /// <summary>
    /// 医生实体
    /// </summary>
    public class Doctor : EntityBase<Guid>
    {
        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            FirstName = null;
            LastName = null;
        }
    }
}
