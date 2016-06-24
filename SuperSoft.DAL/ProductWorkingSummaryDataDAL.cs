using System;
using System.Linq;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ProductWorkingSummaryData数据访问层
    /// </summary>
    public class ProductWorkingSummaryDataDAL : DALBase<ProductWorkingSummaryData>
    {
        //#region override GetByCondition

        ///// <summary>
        ///// 根据Id查询实体
        ///// </summary>
        ///// <param name="id">实体对象的Id</param>
        ///// <returns>一个实体对象</returns>
        //public override ProductWorkingSummaryData GetById(Guid id)
        //{
        //    var v = dbEntities.ProductWorkingSummaryDatas.Where(a => a.Id == id);
        //    if (v != null && v.Any())
        //    {
        //        return v.ToList().FirstOrDefault();
        //    }
        //    return null;
        //}

        //#endregion
    }
}