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
    /// ViewProductWorkingSummaryData业务逻辑层
    /// 只能查询数据
    /// </summary>
    public class ViewProductWorkingSummaryDataBLL : MyClassBase
    {
        DAL.ViewProductWorkingSummaryDataDAL dal = new DAL.ViewProductWorkingSummaryDataDAL();

        #region GetByCondition

        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <returns></returns>
        public virtual IEnumerable<ViewProductWorkingSummaryData> SelectByPatientIdTherapyModeDataTime(Guid patientId, int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (patientId != Guid.Empty)
                {
                    return dal.SelectByPatientIdTherapyModeDataTime(patientId, therapyMode, startTime, endTime);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ResourceHelper.LoadString(@"DataAccessError"), ex);
            }
            return null;
        }

        /// <summary>
        /// 查询最长运行时间的记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="therapyMode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual ViewProductWorkingSummaryData SelectMaxWorkingTimeByPatientIdTherapyModeDataTime(Guid patientId, int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            try
            {
                if (patientId != Guid.Empty)
                {
                    return dal.SelectMaxWorkingTimeByPatientIdTherapyModeDataTime(patientId, therapyMode, startTime, endTime);
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