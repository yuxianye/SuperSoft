using SuperSoft.DAL;
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
    public class ViewProductWorkingSummaryDetailedDataBLL : MyClassBase
    {
        /// <summary>
        /// View-数据库访问对象
        /// </summary>
        private ViewProductWorkingSummaryDetailedDataDAL dAL = new ViewProductWorkingSummaryDetailedDataDAL();

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

        #region GetByCondition

        /// <summary>
        /// 根据Id查询实体
        /// </summary>
        /// <param name="id">实体对象的Id</param>
        /// <returns>一个实体对象</returns>
        public ViewProductWorkingSummaryDetailedData GetById(Guid id)
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
        public IEnumerable<ViewProductWorkingSummaryDetailedData> GetByCondition(
            Expression<Func<ViewProductWorkingSummaryDetailedData, bool>> predicate)
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
        public IEnumerable<ViewProductWorkingSummaryDetailedData> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<ViewProductWorkingSummaryDetailedData, bool>> predicate)
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
        public IEnumerable<ViewProductWorkingSummaryDetailedData> GetByCondition(PropertySortCondition sortCondition,
            Expression<Func<ViewProductWorkingSummaryDetailedData, bool>> predicate, int pageIndex, int pageSize)
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