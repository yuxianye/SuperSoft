using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SuperSoft.BLL
{
    /// <summary>
    /// Patient业务逻辑层，可使用显示事物
    /// </summary>
    public class PatientBLL : Utility.MyClassBase
    {
        DAL.PatientDAL dal = new DAL.PatientDAL();

        #region Count

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                return dal.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        /// <returns></returns>
        public void Insert(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entity != null)
                {
                    dal.Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public void Insert(SqlTransaction transaction, Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entity != null)
                {
                    dal.Insert(transaction, entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        ///// <summary>
        ///// 创建实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public void Insert(ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Insert(entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 创建实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public void Insert(SqlTransaction transaction, ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Insert(transaction, entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        #endregion

        #region Delete

        /// <summary>
        /// 删除对象,调用数据库存储过程,会删除所有患者相关的数据和产品运行信息等数据
        /// </summary>
        /// <param name="id">一个实体对象的Id</param>
        public void Delete(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (id != Guid.Empty)
                {
                    dal.Delete(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        ///// <summary>
        ///// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="id">一个实体对象的Id</param>
        //public virtual void Delete(SqlTransaction transaction, Guid id)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (id != Guid.Empty)
        //        {
        //            dal.Delete(transaction, id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除对象,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="entity">一个实体对象</param>
        //public void Delete(Patient entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Delete(entity.Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除对象，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(SqlTransaction transaction, Patient entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Delete(transaction, entity.Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除实体对象集合,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public void Delete(ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Delete(entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除实体对象集合，使用显示事物,Patients表有触发器,会删除所有患者相关的数据和产品运行信息等数据
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(SqlTransaction transaction, ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Delete(transaction, entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public void Update(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entity != null)
                {
                    dal.Update(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        ///// <summary>
        ///// 更新对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Update(SqlTransaction transaction, Patient entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Update(transaction, entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 更新实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">将要编辑的实体对象集合</param>
        //public void Update(ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Update(entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 更新实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Update(SqlTransaction transaction, ICollection<Patient> entitys)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entitys != null && entitys.Count() > 0)
        //        {
        //            dal.Update(transaction, entitys);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        #endregion

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public Patient GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (id != Guid.Empty)
                {
                    return dal.GetById(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (pageIndex > 0 && pageSize > 0)
                {
                    return dal.SelectPaging(pageIndex, pageSize, out recordCount);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        ///// <summary>
        ///// 分页查询,使用Id desc排序
        ///// </summary>
        ///// <param name="firstName">firstName</param>
        ///// <param name="pageIndex">页号</param>
        ///// <param name="pageSize">页大小</param>
        ///// <param name="recordCount">记录总数</param>
        ///// <returns></returns>
        //public virtual ICollection<Patient> SelectByFirstName(string firstName, int pageIndex, int pageSize, out int recordCount)
        //{
        //    recordCount = 0;
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (pageIndex > 0 && pageSize > 0)
        //        {
        //            return dal.SelectByFirstName(firstName, pageIndex, pageSize, out recordCount);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //    return null;
        //}

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="firstName">firstName</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByFirstName(string firstName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    return dal.SelectByFirstName(firstName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="lastName">lastName</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByLastName(string lastName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    return dal.SelectByLastName(lastName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="dateOfBirth">dateOfBirth</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDateOfBirth(DateTime dateOfBirth)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (DateTime.MinValue != dateOfBirth && DateTime.MaxValue != dateOfBirth)
                {
                    return dal.SelectByDateOfBirth(dateOfBirth);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="weight">weight</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByWeight(int weight)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (weight >= 0 && weight <= 255)
                {
                    return dal.SelectByWeight(weight);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="height">height</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByHeight(int height)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (height >= 0 && height <= 255)
                {
                    return dal.SelectByHeight(height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="gender">gender</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByGender(bool gender)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                //if (gender )
                //{
                return dal.SelectByGender(gender);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            //return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="eMail">eMail</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByEMail(string eMail)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(eMail))
                {
                    return dal.SelectByEMail(eMail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="telephoneNumbers">telephoneNumbers</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SselectByTelephoneNumbers(string telephoneNumbers)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(telephoneNumbers))
                {
                    return dal.SselectByTelephoneNumbers(telephoneNumbers);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="postalCode">postalCode</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByPostalCode(string postalCode)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(postalCode))
                {
                    return dal.SelectByPostalCode(postalCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="address">address</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByAddress(string address)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(address))
                {
                    return dal.SelectByAddress(address);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="diagnosis">diagnosis</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDiagnosis(string diagnosis)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(diagnosis))
                {
                    return dal.SelectByDiagnosis(diagnosis);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="doctorId">doctorId</param>
        /// <returns></returns>
        public virtual ICollection<Patient> SelectByDoctorId(Guid doctorId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (doctorId != Guid.Empty)
                {
                    return dal.SelectByDoctorId(doctorId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        #endregion

        #region Dispose 

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (!Equals(dal, null))
            {
                dal.Dispose();
                dal = null;
            }
        }

        #endregion
    }
}
