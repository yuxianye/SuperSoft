using System;
using System.Linq;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ProductWorkingStatisticsData数据访问层
    /// </summary>
    public class ProductWorkingStatisticsDataDAL : DALBase<ProductWorkingStatisticsData>
    {
        //#region override GetByCondition

        ///// <summary>
        ///// 根据Id查询实体
        ///// </summary>
        ///// <param name="id">实体对象的Id</param>
        ///// <returns>一个实体对象</returns>
        //public override ProductWorkingStatisticsData GetById(Guid id)
        //{
        //    var v = dbEntities.ProductWorkingStatisticsDatas.Where(a => a.Id == id);
        //    if (v != null && v.Any())
        //    {
        //        return v.ToList().FirstOrDefault();
        //    }
        //    return null;
        //}

        //#endregion
    }
}