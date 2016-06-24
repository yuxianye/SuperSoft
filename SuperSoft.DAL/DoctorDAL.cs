using System;
using System.Linq;
using SuperSoft.Model;

namespace SuperSoft.DAL
{
    /// <summary>
    /// Doctor数据访问层
    /// </summary>
    public class DoctorDAL : DALBase<Doctor>
    {
        //#region override Delete

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id">一个实体对象的Id</param>
        //public override void Delete(Guid id)
        //{
        //    var entity = dbEntities.Doctors.FirstOrDefault(a => a.Id == id);
        //    base.Delete(entity);
        //}

        //#endregion

        //#region override GetByCondition

        ///// <summary>
        ///// 根据Id查询实体
        ///// </summary>
        ///// <param name="id">实体对象的Id</param>
        ///// <returns>一个实体对象</returns>
        //public override Doctor GetById(Guid id)
        //{
        //    var v = dbEntities.Doctors.Where(a => a.Id == id);
        //    if (v != null && v.Any())
        //    {
        //        return v.ToList().FirstOrDefault();
        //    }
        //    return null;
        //}

        //#endregion
    }
}