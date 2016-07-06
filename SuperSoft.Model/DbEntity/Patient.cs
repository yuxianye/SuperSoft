﻿using System;

namespace SuperSoft.Model
{
    /// <summary>
    /// 患者实体
    /// </summary>
    public class Patient : EntityBase<Guid>, ICloneable
    {
        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public System.DateTime DateOfBirth { get; set; }

        /// <summary>
        /// 体重(kg)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 身高(cm)
        /// </summary>
        public int Height { get; set; }

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
        public System.Guid DoctorId { get; set; }

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