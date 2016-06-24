using System;
using System.Linq;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ViewPatientsProduct数据访问层
    /// 只能查询数据
    /// </summary>
    public class ViewPatientsProductDAL : DALBase<ViewPatientsProduct>
    {
        //#region override GetByCondition

        ///// <summary>
        ///// 根据Id查询实体
        ///// </summary>
        ///// <param name="id">实体对象的Id</param>
        ///// <returns>一个实体对象</returns>
        //public override ViewPatientsProduct GetById(Guid id)
        //{
        //    var v = dbEntities.ViewPatientsProducts.Where(a => a.Id == id);
        //    if (v != null && v.Any())
        //    {
        //        return v.ToList().FirstOrDefault();
        //    }
        //    return null;
        //}

        //#endregion
    }
}