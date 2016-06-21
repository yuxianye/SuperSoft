using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SuperSoft.Utility;

namespace SuperSoft.DAL
{
    /// <summary>
    /// 数据访问抽象泛型基类,不能实例化
    /// SaveChanges()手动保存
    /// 事物使用方法
    /// using (TransactionScope transactionScope = new TransactionScope())
    /// {
    /// transactionScope.Complete();
    /// Transaction.Current.Rollback();
    /// }
    /// </summary>
    /// <typeparam name="T">数据层的实体类</typeparam>
    public abstract class DALBase<T> : MyClassBase where T : Model.EntityBase<Guid>
    {
        /// <summary>
        /// 数据访问的实体数据模型
        /// </summary>
        private DbEntities dbEntities = new DbEntities();

        #region Save

        /// <summary>
        /// 保存修改(增删改之后统一调用，有事物机制)
        /// </summary>
        /// <returns>生效的行数</returns>
        public virtual int SaveChanges()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (dbEntities.ChangeTracker.HasChanges())
            {
                return dbEntities.SaveChanges();
            }
            return 0;
        }

        #endregion

        #region Count

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            return dbEntities.Set<T>().Count();
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建
        /// </summary>
        /// <param Name="entity">一个实体对象</param>
        /// <returns></returns>
        public virtual void Insert(T entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                dbEntities.Set<T>().Add(entity);
            }
        }

        /// <summary>
        /// 创建实体对象集合
        /// </summary>
        /// <param Name="entitys">实体对象集合</param>
        public virtual void Insert(IEnumerable<T> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null)
            {
                dbEntities.Set<T>().AddRange(entitys);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                var v = dbEntities.Set<T>().AsEnumerable().FirstOrDefault(m => m.Id == id);
                dbEntities.Entry(v).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(T entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                Delete(entity.Id);
            }
        }

        /// <summary>
        /// 删除实体对象集合
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(IEnumerable<T> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null)
            {
                dbEntities.Set<T>().RemoveRange(entitys);
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
        public virtual void Delete(Expression<Func<T, bool>> condition)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            dbEntities.Set<T>().RemoveRange(dbEntities.Set<T>().Where(condition));
        }

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public virtual void Update(T entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                dbEntities.Set<T>().Attach(entity);
                dbEntities.Entry(entity).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// 编辑实体对象集合
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(IEnumerable<T> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                //var o = new object();
                //Parallel.ForEach(entitys, entity =>
                //{
                //    lock (o)
                //    {
                //        dbEntities.Set<T>().Attach(entity);
                //        dbEntities.Entry(entity).State = EntityState.Modified;
                //    }
                //});
                //o = null;
                foreach (var entity in entitys)
                {
                    dbEntities.Set<T>().Attach(entity);
                    dbEntities.Entry(entity).State = EntityState.Modified;
                }

            }
        }

        #endregion

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public virtual T GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                return dbEntities.Set<T>().AsEnumerable().SingleOrDefault<T>(m => m.Id == id);
            }
            return default(T);
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
        public virtual IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            var sortCondition = new PropertySortCondition("Id", ListSortDirection.Ascending);
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
        public virtual IEnumerable<T> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<T, bool>> predicate)
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
        public virtual IEnumerable<T> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<T, bool>> predicate,
            int pageIndex,
            int pageSize)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            return dbEntities.Set<T>().Where(predicate).OrderBy(sortCondition).Skip(((pageIndex < 1 ? 1 : pageIndex) - 1) * pageSize).Take(pageSize).ToArray();
        }

        #endregion

        #region 直接执行SQL语句命令
        /// <summary>
        /// 执行SQL语句命令,直接对数据库操作并更新到数据库。
        /// </summary>
        /// <returns></returns>
        public virtual int ExecuteSqlCommand(string sqlString)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (!string.IsNullOrWhiteSpace(sqlString))
            {
                return dbEntities.Database.ExecuteSqlCommand(sqlString);
            }
            return 0;
        }

        /// <summary>
        /// 执行SQL语句命令,直接对数据库操作并更新到数据库。
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public virtual int ExecuteSqlCommand(string sqlString, object[] parameters)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (!string.IsNullOrWhiteSpace(sqlString))
            {
                return dbEntities.Database.ExecuteSqlCommand(sqlString, parameters);
            }
            return 0;
        }

        /// <summary>
        /// 异步执行SQL语句命令,直接对数据库操作并更新到数据库。
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlCommandAsync(string sqlString)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (!string.IsNullOrWhiteSpace(sqlString))
            {
                return dbEntities.Database.ExecuteSqlCommandAsync(sqlString);
            }
            return null;
        }
        /// <summary>
        /// 异步执行SQL语句命令,直接对数据库操作并更新到数据库。
        /// </summary>
        /// <param name="sqlString">SQL 命令字符串</param>
        /// <returns></returns>
        public virtual Task<int> ExecuteSqlCommandAsync(string sqlString, object[] parameters)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (!string.IsNullOrWhiteSpace(sqlString))
            {
                return dbEntities.Database.ExecuteSqlCommandAsync(sqlString, parameters);
            }
            return null;
        }
        #endregion

        #region Dispose 

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (!Equals(dbEntities, null))
            {
                dbEntities.Dispose();
                dbEntities = null;
            }
        }

        #endregion
    }
}
