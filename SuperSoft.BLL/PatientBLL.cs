using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.BLL
{
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
        /// 创建
        /// </summary>
        /// <param Name="entity">一个实体对象</param>
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
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 创建实体对象集合
        /// </summary>
        /// <param Name="entitys">实体对象集合</param>
        public void Insert(IEnumerable<Patient> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entitys != null && entitys.Count() > 0)
                {
                    dal.Insert(entitys);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
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
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public void Delete(Patient entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entity != null)
                {
                    dal.Delete(entity.Id);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 删除实体对象集合
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public void Delete(IEnumerable<Patient> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entitys != null && entitys.Count() > 0)
                {
                    dal.Delete(entitys);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
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
        public void Delete(Expression<Func<Patient, bool>> condition)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (condition != null)
                {
                    dal.Delete(condition);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

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
                    dal.Delete(entity);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 编辑实体对象集合
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public void Update(IEnumerable<Patient> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (entitys != null && entitys.Count() > 0)
                {
                    dal.Delete(entitys);
                    dal.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

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
                return default(Patient);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 根据条件查询
        /// 示例（大括号换成尖括号）：
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) predicate = v =) v.Id ==0 && v.Height == 100;
        /// GetByCondition( predicate);
        /// </summary>
        /// <param name="predicate">
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) Foo = v =) v.Id ==0 && v.Height == 100;
        /// </param>
        /// <returns>实体对象集合</returns>
        public IEnumerable<Patient> GetByCondition(Expression<Func<Patient, bool>> predicate)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            var sortCondition = new PropertySortCondition(@"Id", ListSortDirection.Ascending);
            return GetByCondition(sortCondition, predicate);
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
        ///  排序条件 Common.PropertySortCondition SortCondition = new Common.PropertySortCondition("Id",
        ///  ListSortDirection.Descending);
        /// </param>
        /// <param name="predicate">
        /// 筛选条件 System.Linq.Expressions.Expression(Func(Patient, bool)) Foo = v =) v.Id ==0 && v.Height ==
        /// 100;
        /// </param>
        /// <returns>实体对象集合</returns>
        public IEnumerable<Patient> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<Patient, bool>> predicate)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            return GetByCondition(sortCondition, predicate, 1, int.MaxValue);
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
        public IEnumerable<Patient> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<Patient, bool>> predicate,
            int pageIndex,
            int pageSize)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (sortCondition != null && predicate != null)
                {
                    return dal.GetByCondition(sortCondition, predicate, pageIndex, pageSize);
                }
                return default(IEnumerable<Patient>);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        #endregion

        #region 直接执行SQL语句命令

        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sqlString)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(sqlString))
                {
                    return dal.ExecuteSqlCommand(sqlString);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sqlString, object[] parameters)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(sqlString))
                {
                    return dal.ExecuteSqlCommand(sqlString, parameters);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 异步执行SQL语句命令
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public Task<int> ExecuteSqlCommandAsync(string sqlString)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(sqlString))
                {
                    return dal.ExecuteSqlCommandAsync(sqlString);
                }
                return default(Task<int>);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
        }

        /// <summary>
        /// 异步执行SQL语句命令
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public Task<int> ExecuteSqlCommandAsync(string sqlString, object[] parameters)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(sqlString))
                {
                    return dal.ExecuteSqlCommandAsync(sqlString, parameters);
                }
                return default(Task<int>);
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
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
