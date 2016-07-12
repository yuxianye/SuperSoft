using SuperSoft.Utility.Windows;
using System;
using System.ComponentModel;

namespace SuperSoft.Model
{
    /// <summary>
    /// 医生实体
    /// </summary>
    public class Doctor : EntityBase<Guid>, ICloneable, IDataErrorInfo
    {
        #region 对应数据库字段

        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }

        #endregion

        #region 扩展属性

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName" && string.IsNullOrWhiteSpace(FirstName))
                {
                    return ResourceHelper.LoadString("DoctorAddView_FirstNameValidationRequired");
                }
                if (columnName == "FirstName" && !string.IsNullOrWhiteSpace(FirstName) && FirstName.Length > 16)
                {
                    return ResourceHelper.LoadString("DoctorAddView_FirstNameValidationRequired");
                }
                if (columnName == "LastName" && string.IsNullOrWhiteSpace(LastName))
                {
                    return ResourceHelper.LoadString("DoctorAddView_LastNameValidationRequired");
                }
                if (columnName == "LastName" && !string.IsNullOrWhiteSpace(LastName) && LastName.Length > 16)
                {
                    return ResourceHelper.LoadString("DoctorAddView_LastNameValidationRequired");
                }
                return null;
            }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        #endregion

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            FirstName = null;
            LastName = null;
        }


    }
}
