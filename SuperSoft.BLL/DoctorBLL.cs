using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SuperSoft.BLL
{
    /// <summary>
    /// Doctor业务逻辑层，可使用显示事物
    /// </summary>
    public class DoctorBLL : MyClassBase
    {
        DAL.DoctorDAL dal = new DAL.DoctorDAL();

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
        public virtual void Insert(Doctor entity)
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

        ///// <summary>
        ///// 创建对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Insert(SqlTransaction transaction, Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Insert(transaction, entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 创建实体对象集合，内部采用事物整体提交
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Insert(ICollection<Doctor> entitys)
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
        //public virtual void Insert(SqlTransaction transaction, ICollection<Doctor> entitys)
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
        /// 删除对象
        /// </summary>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(Guid id)
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
        ///// 删除对象，使用显示事物
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
        ///// 删除对象
        ///// </summary>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Delete(entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除对象，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entity">一个实体对象</param>
        //public virtual void Delete(SqlTransaction transaction, Doctor entity)
        //{
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (entity != null)
        //        {
        //            dal.Delete(transaction, entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
        //    }
        //}

        ///// <summary>
        ///// 删除实体对象集合
        ///// </summary>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(ICollection<Doctor> entitys)
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
        ///// 删除实体对象集合，使用显示事物
        ///// </summary>
        ///// <param name="transaction">事物对象</param>
        ///// <param name="entitys">实体对象集合</param>
        //public virtual void Delete(SqlTransaction transaction, ICollection<Doctor> entitys)
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
        public virtual void Update(Doctor entity)
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
        //public virtual void Update(SqlTransaction transaction, Doctor entity)
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
        //public virtual void Update(ICollection<Doctor> entitys)
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
        //public virtual void Update(SqlTransaction transaction, ICollection<Doctor> entitys)
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
        public virtual Doctor GetById(Guid id)
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
        public virtual ICollection<Doctor> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (pageIndex > 0 && pageSize > 0 && pageSize <= short.MaxValue)
                {
                    return dal.SelectPaging(pageIndex, pageSize, out recordCount);
                }
                else
                {
                    throw new Exception(ResourceHelper.LoadString(@"PageSizeError"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        ///// <summary>
        ///// 分页查询,使用Id desc排序
        ///// </summary>
        ///// <param name="firstName">firstName</param>
        ///// <param name="pageIndex">页号</param>
        ///// <param name="pageSize">页大小</param>
        ///// <param name="recordCount">记录总数</param>
        ///// <returns></returns>
        //public virtual ICollection<Doctor> SelectByFirstName(string firstName, int pageIndex, int pageSize, out int recordCount)
        //{
        //    recordCount = 0;
        //    if (Disposed)
        //    {
        //        throw new ObjectDisposedException(ToString());
        //    }
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(firstName) && pageIndex > 0 && pageSize > 0)
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
        public virtual ICollection<Doctor> SelectByFirstName(string firstName)
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
        public virtual ICollection<Doctor> SelectByLastName(string lastName)
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