﻿using System;
using System.Linq;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// PatientsProductDAL
    /// </summary>
    public class PatientsProductDAL : DALBase<PatientsProduct>
    {
        //#region override GetByCondition

        ///// <summary>
        ///// 根据Id查询实体
        ///// </summary>
        ///// <param name="id">实体对象的Id</param>
        ///// <returns>一个实体对象</returns>
        //public override PatientsProduct GetById(Guid id)
        //{
        //    var v =base. dbEntities.PatientsProducts.Where(a => a.Id == id);
        //    if (v != null && v.Any())
        //    {
        //        return v.ToList().FirstOrDefault();
        //    }
        //    return null;
        //}

        //#endregion
    }
}