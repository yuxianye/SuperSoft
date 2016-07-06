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
    /// <summary>
    /// ViewPatientsProduct业务逻辑层
    /// 只能查询数据
    /// </summary>
    public class ViewPatientsProductBLL : MyClassBase
    {
        DAL.ViewPatientsProductDAL dal = new DAL.ViewPatientsProductDAL();

        #region GetByCondition

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <returns></returns>
        public virtual IEnumerable<ViewPatientsProduct> SelectByPatientId(Guid patientId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (patientId != Guid.Empty)
                {
                    return dal.SelectByPatientId(patientId);
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
        /// <param name="serialNumber">serialNumber</param>
        /// <returns></returns>
        public virtual IEnumerable<ViewPatientsProduct> SelectBySerialNumber(string serialNumber)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(serialNumber))
                {
                    return dal.SelectBySerialNumber(serialNumber);
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