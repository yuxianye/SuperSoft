using SuperSoft.Utility.Windows;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SuperSoft.Model
{
    /// <summary>
    /// 患者实体
    /// </summary>
    public class Patient : EntityBase<Guid>, ICloneable, IDataErrorInfo
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

        private System.DateTime dateOfBirth = DateTime.Now;
        /// <summary>
        /// 出生日期
        /// </summary>
        public System.DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
                OnPropertyChanged("Age");
            }
        }

        private int weight;
        /// <summary>
        /// 体重(kg)
        /// </summary>
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
                OnPropertyChanged("BMI");
            }
        }

        private int height;
        /// <summary>
        /// 身高(cm)
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
                OnPropertyChanged("BMI");
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string TelephoneNumbers { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 病症
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 医生Id
        /// </summary>
        public Nullable<System.Guid> DoctorId { get; set; }

        #endregion

        #region 扩展属性

        public int Age
        {
            get
            {
                if (DateOfBirth == DateTime.MinValue)
                {
                    return 0;
                }
                if (DateTime.Now.Year - DateOfBirth.Year < 0)
                {
                    return 0;
                }
                return DateTime.Now.Year - DateOfBirth.Year;
            }
            set { OnPropertyChanged("Age"); }
        }

        /// <summary>
        /// BMI＝体重（kg）÷身高（m）÷身长（m）
        /// </summary>
        public float BMI
        {
            get
            {
                if ((Height == 1) || double.IsNaN((double)Weight) || Weight < 1 || Height < 1)
                {
                    return 0;
                }
                var v = (float)Height / 100;
                var v2 = v * v;
                return (float)Weight / ((float)Height / 100 * ((float)Height / 100));
            }
            set { OnPropertyChanged("BMI"); }
        }

        private string serialNumber;
        /// <summary>
        /// 产品序列号
        /// </summary>
        public string SerialNumber
        {
            get { return serialNumber; }
            set
            {
                if (serialNumber == value) return;
                serialNumber = value;
                OnPropertyChanged("SerialNumber");
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName" && string.IsNullOrWhiteSpace(FirstName))
                {
                    return ResourceHelper.LoadString("FirstNameValidationRequired");
                }
                //if (columnName == "FirstName" && !string.IsNullOrWhiteSpace(FirstName) && FirstName.Length > 32)
                //{
                //    return ResourceHelper.LoadString("FirstNameValidationLength");
                //}
                if (columnName == "LastName" && string.IsNullOrWhiteSpace(LastName))
                {
                    return ResourceHelper.LoadString("LastNameValidationRequired");
                }
                //if (columnName == "LastName" && !string.IsNullOrWhiteSpace(LastName) && LastName.Length > 32)
                //{
                //    return ResourceHelper.LoadString("LastNameValidationLength");
                //}
                if (columnName == "Weight" && (Weight < 0 || Weight > 255))
                {
                    return ResourceHelper.LoadString("WeightValidation");
                }
                if (columnName == "Height" && (Height < 0 || Height > 255))
                {
                    return ResourceHelper.LoadString("HeightValidation");
                }
                //if (columnName == "TelephoneNumbers" && !string.IsNullOrWhiteSpace(TelephoneNumbers) &&
                //    TelephoneNumbers.Length > 32)
                //{
                //    return ResourceHelper.LoadString("TelephoneNumbersValidation");
                //}
                //if (columnName == "EMail" && !string.IsNullOrWhiteSpace(EMail) && EMail.Length > 32)
                //{
                //    return ResourceHelper.LoadString("EMailValidationLength");
                //}
                if (columnName == "EMail" && !string.IsNullOrWhiteSpace(EMail))
                {
                    var reg =
                        new Regex(
                            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                    if (!reg.IsMatch(EMail))
                    {
                        return ResourceHelper.LoadString("EMailValidationFormat");
                    }
                }
                //if (columnName == "PostalCode" && !string.IsNullOrWhiteSpace(PostalCode) && PostalCode.Length > 32)
                //{
                //    return ResourceHelper.LoadString("PostalCodeValidationLength");
                //}
                //if (columnName == "Address" && !string.IsNullOrWhiteSpace(Address) && Address.Length > 32)
                //{
                //    return ResourceHelper.LoadString("AddressValidationLength");
                //}
                //if (columnName == "Diagnosis" && !string.IsNullOrWhiteSpace(Diagnosis) && Diagnosis.Length > 32)
                //{
                //    return ResourceHelper.LoadString("DiagnosisValidationLength");
                //}
                //if (columnName == "Diagnosis" && DoctorId == Guid.Empty)
                //{
                //    return ResourceHelper.LoadString("DiagnosisValidation");
                //}
                if (columnName == "SerialNumber")
                {
                    if (string.IsNullOrWhiteSpace(SerialNumber))
                    {
                        return ResourceHelper.LoadString("SerialNumberValidation");
                    }
                    var reg = new Regex(@"^\d{18}$");
                    if (!reg.IsMatch(SerialNumber))
                    {
                        return ResourceHelper.LoadString("SerialNumberValidation");
                    }
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
            Photo = null;
            EMail = null;
            TelephoneNumbers = null;
            PostalCode = null;
            Address = null;
            Diagnosis = null;
        }
    }
}
