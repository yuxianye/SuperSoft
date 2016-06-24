using SuperSoft.DAL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.BLL
{
    public class DoctorBLL : MyClassBase
    {
        /// <summary>
        /// 医生的数据库访问对象
        /// </summary>
        private DoctorDAL dAL = new DoctorDAL();

        #region 记录数

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
                return dAL.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        #endregion

        #region Dispose

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (!Equals(dAL, null))
            {
                dAL.Dispose();
                dAL = null;
            }
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建
        /// </summary>
        /// <param Name="entity">一个实体对象</param>
        /// <returns></returns>
        public void Insert(Doctor entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Insert(entity);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMsg = new StringBuilder();
                foreach (var v in ex.EntityValidationErrors)
                {
                    foreach (var vv in v.ValidationErrors)
                    {
                        errorMsg.AppendLine(vv.ErrorMessage);
                    }
                }
                throw new Exception(errorMsg.ToString());
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ResourceHelper.LoadString("DbUpdateException"), ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 创建实体对象集合
        /// </summary>
        /// <param Name="entitys">实体对象集合</param>
        public void Insert(IEnumerable<Doctor> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Insert(entitys);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMsg = new StringBuilder();
                foreach (var v in ex.EntityValidationErrors)
                {
                    foreach (var vv in v.ValidationErrors)
                    {
                        errorMsg.AppendLine(vv.ErrorMessage);
                    }
                }
                throw new Exception(errorMsg.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        public void Delete(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public void Delete(Doctor entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 删除实体对象集合
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public void Delete(IEnumerable<Doctor> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Delete(entitys);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 根据条件删除
        /// 示例（大括号换成尖括号）
        /// System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 && v.Height == 100;
        /// </summary>
        /// <param name="condition">
        /// 删除条件 System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 &&
        /// v.Height == 100;
        /// </param>
        public void Delete(Expression<Func<Doctor, bool>> condition)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Delete(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public void Update(Doctor entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Update(entity);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMsg = new StringBuilder();
                foreach (var v in ex.EntityValidationErrors)
                {
                    foreach (var vv in v.ValidationErrors)
                    {
                        errorMsg.AppendLine(vv.ErrorMessage);
                    }
                }
                throw new Exception(errorMsg.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 编辑实体对象集合
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public void Update(IEnumerable<Doctor> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                dAL.Update(entitys);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMsg = new StringBuilder();
                foreach (var v in ex.EntityValidationErrors)
                {
                    foreach (var vv in v.ValidationErrors)
                    {
                        errorMsg.AppendLine(vv.ErrorMessage);
                    }
                }
                throw new Exception(errorMsg.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        #endregion

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public Doctor GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                return dAL.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// 示例（大括号换成尖括号）：
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 && v.Height == 100;
        /// GetByCondition( predicate);
        /// </summary>
        /// <param name="predicate">
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) Foo = v =) v.Id ==0 && v.Height ==
        /// 100;
        /// </param>
        /// <returns>实体对象集合</returns>
        public IEnumerable<Doctor> GetByCondition(Expression<Func<Doctor, bool>> predicate)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                return dAL.GetByCondition(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// 示例（大括号换成尖括号）：
        /// 排序条件 Common.PropertySortCondition sortCondition = new Common.PropertySortCondition("Id",
        /// ListSortDirection.Descending);
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 && v.Height == 100;
        /// GetByCondition(sortCondition, predicate);
        /// </summary>
        /// <param name="sortCondition">
        /// 排序条件 Common.PropertySortCondition SortCondition = new Common.PropertySortCondition("Id",
        /// ListSortDirection.Descending);
        /// </param>
        /// <param name="predicate">
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) Foo = v =) v.Id ==0 && v.Height ==
        /// 100;
        /// </param>
        /// <returns>实体对象集合</returns>
        public IEnumerable<Doctor> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<Doctor, bool>> predicate)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                return dAL.GetByCondition(sortCondition, predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// 示例（大括号换成尖括号）：
        /// 排序条件 Common.PropertySortCondition sortCondition = new Common.PropertySortCondition("Id",
        /// ListSortDirection.Descending);
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 && v.Height == 100;
        /// GetByCondition(sortCondition, predicate, 10, 10);
        /// </summary>
        /// <param name="sortCondition">
        /// 排序条件 Common.PropertySortCondition SortCondition = new Common.PropertySortCondition("Id",
        /// ListSortDirection.Descending);
        /// </param>
        /// <param name="predicate">
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) Foo = v =) v.Id ==0 && v.Height ==
        /// 100;
        /// </param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>实体对象集合</returns>
        public IEnumerable<Doctor> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<Doctor, bool>> predicate, int pageIndex, int pageSize)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                return dAL.GetByCondition(sortCondition, predicate, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString("DataAccessError"), ex);
            }
        }

        #endregion
    }
}