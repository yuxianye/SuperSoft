using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ProductWorkingStatisticsData数据访问层，可使用显示事物
    /// </summary>
    public class ProductWorkingStatisticsDataDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ProductWorkingStatisticsDataDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //43个字段
        private const string selectCount = "SELECT COUNT(*) FROM ProductWorkingStatisticsDatas";
        private const string insert = @"INSERT INTO ProductWorkingStatisticsDatas(
Id,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,
TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,
RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian) 
VALUES(@Id,@ProductId,@TherapyMode,@DataTime,@TotalUsage,@CountAHI,@CountAI,@CountHI,@CountSnore,@CountPassive,@PressureMax,@PressureP95,@PressureMedian,@FlowMax,@FlowP95,
@FlowMedian,@LeakMax,@LeakP95,@LeakMedian,@TidalVolumeMax,@TidalVolumeP95,@TidalVolumeMedian,@MinuteVentilationMax,@MinuteVentilationP95,@MinuteVentilationMedian,@SpO2Max,
@SpO2P95,@SpO2Median,@PulseRateMax,@PulseRateP95,@PulseRateMedian,@RespiratoryRateMax,@RespiratoryRateP95,@RespiratoryRateMedian,@IERatioMax,@IERatioP95,@IERatioMedian,
@IPAPMax,@IPAPP95,@IPAPMedian,@EPAPMax,@EPAPP95,@EPAPMedian)";
        private const string deleteById = "DELETE FROM ProductWorkingStatisticsDatas WHERE Id=@Id";
        private const string deleteByIds = "DELETE FROM ProductWorkingStatisticsDatas WHERE Id IN (@Ids)";

        private const string updateById = @"UPDATE ProductWorkingStatisticsDatas SET Id=@Id,ProductId=@ProductId,TherapyMode=@TherapyMode,DataTime=@DataTime,
TotalUsage=@TotalUsage,CountAHI=@CountAHI,CountAI=@CountAI,CountHI=@CountHI,CountSnore=@CountSnore,CountPassive=@CountPassive,PressureMax=@PressureMax,PressureP95=@PressureP95,
PressureMedian=@PressureMedian,FlowMax=@FlowMax,FlowP95=@FlowP95,FlowMedian=@FlowMedian,LeakMax=@LeakMax,LeakP95=@LeakP95,LeakMedian=@LeakMedian,TidalVolumeMax=@TidalVolumeMax,
TidalVolumeP95=@TidalVolumeP95,TidalVolumeMedian=@TidalVolumeMedian,MinuteVentilationMax=@MinuteVentilationMax,MinuteVentilationP95=@MinuteVentilationP95,
MinuteVentilationMedian=@MinuteVentilationMedian,SpO2Max=@SpO2Max,SpO2P95=@SpO2P95,SpO2Median=@SpO2Median,PulseRateMax=@PulseRateMax,PulseRateP95=@PulseRateP95,
PulseRateMedian=@PulseRateMedian,RespiratoryRateMax=@RespiratoryRateMax,RespiratoryRateP95=@RespiratoryRateP95,RespiratoryRateMedian=@RespiratoryRateMedian,IERatioMax=@IERatioMax,
IERatioP95=@IERatioP95,IERatioMedian=@IERatioMedian,IPAPMax=@IPAPMax,IPAPP95=@IPAPP95,IPAPMedian=@IPAPMedian,EPAPMax=@EPAPMax,EPAPP95=@EPAPP95,EPAPMedian=@EPAPMedian 
WHERE Id =@Id";

        private const string selectById = @"SELECT Id,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ProductWorkingStatisticsDatas WHERE Id =@Id";

        private const string selectPaging = @"SELECT Id,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ProductWorkingStatisticsDatas 
ORDER BY Id DESC 
LIMIT @PageSize OFFSET @OffictCount";
        private const string selectByProductIdTherapyModeDataTime = @"SELECT Id,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ProductWorkingStatisticsDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND DataTime>=@StartTime AND DataTime<@EndTime 
ORDER BY Id DESC 
LIMIT @PageSize OFFSET @OffictCount";
        private const string selectByProductIdTherapyModeDataTimeCount = @"SELECT COUNT(*) FROM ProductWorkingStatisticsDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND DataTime>=@StartTime AND DataTime<@EndTime";

        private const string selectByProductIdTherapyModeDataTime2 = @"SELECT Id,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ProductWorkingStatisticsDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND DataTime>=@StartTime AND DataTime<@EndTime 
ORDER BY Id DESC";
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
            return SQLiteHelper.ExecuteScalar(sQLiteConnection, System.Data.CommandType.Text, selectCount).GetInt();
        }

        #endregion

        #region Insert

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(ProductWorkingStatisticsData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, insert,
                    new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@DataTime", entity.DataTime),
                    new SQLiteParameter("@TotalUsage", entity.TotalUsage),
                    new SQLiteParameter("@CountAHI", entity.CountAHI),
                    new SQLiteParameter("@CountAI", entity.CountAI),
                    new SQLiteParameter("@CountHI", entity.CountHI),
                    new SQLiteParameter("@CountSnore", entity.CountSnore),
                    new SQLiteParameter("@CountPassive", entity.CountPassive),
                    new SQLiteParameter("@PressureMax", entity.PressureMax),
                    new SQLiteParameter("@PressureP95", entity.PressureP95),
                    new SQLiteParameter("@PressureMedian", entity.PressureMedian),
                    new SQLiteParameter("@FlowMax", entity.FlowMax),
                    new SQLiteParameter("@FlowP95", entity.FlowP95),
                    new SQLiteParameter("@FlowMedian", entity.FlowMedian),
                    new SQLiteParameter("@LeakMax", entity.LeakMax),
                    new SQLiteParameter("@LeakP95", entity.LeakP95),
                    new SQLiteParameter("@LeakMedian", entity.LeakMedian),
                    new SQLiteParameter("@TidalVolumeMax", entity.TidalVolumeMax),
                    new SQLiteParameter("@TidalVolumeP95", entity.TidalVolumeP95),
                    new SQLiteParameter("@TidalVolumeMedian", entity.TidalVolumeMedian),
                    new SQLiteParameter("@MinuteVentilationMax", entity.MinuteVentilationMax),
                    new SQLiteParameter("@MinuteVentilationP95", entity.MinuteVentilationP95),
                    new SQLiteParameter("@MinuteVentilationMedian", entity.MinuteVentilationMedian),
                    new SQLiteParameter("@SpO2Max", entity.SpO2Max),
                    new SQLiteParameter("@SpO2P95", entity.SpO2P95),
                    new SQLiteParameter("@SpO2Median", entity.SpO2Median),
                    new SQLiteParameter("@PulseRateMax", entity.PulseRateMax),
                    new SQLiteParameter("@PulseRateP95", entity.PulseRateP95),
                    new SQLiteParameter("@PulseRateMedian", entity.PulseRateMedian),
                    new SQLiteParameter("@RespiratoryRateMax", entity.RespiratoryRateMax),
                    new SQLiteParameter("@RespiratoryRateP95", entity.RespiratoryRateP95),
                    new SQLiteParameter("@RespiratoryRateMedian", entity.RespiratoryRateMedian),
                    new SQLiteParameter("@IERatioMax", entity.IERatioMax),
                    new SQLiteParameter("@IERatioP95", entity.IERatioP95),
                    new SQLiteParameter("@IERatioMedian", entity.IERatioMedian),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@IPAPP95", entity.IPAPP95),
                    new SQLiteParameter("@IPAPMedian", entity.IPAPMedian),
                    new SQLiteParameter("@EPAPMax", entity.EPAPMax),
                    new SQLiteParameter("@EPAPP95", entity.EPAPP95),
                    new SQLiteParameter("@EPAPMedian", entity.EPAPMedian)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SQLiteTransaction transaction, ProductWorkingStatisticsData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
                    new SQLiteParameter("@Id", entity.Id),
                    new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@DataTime", entity.DataTime),
                    new SQLiteParameter("@TotalUsage", entity.TotalUsage),
                    new SQLiteParameter("@CountAHI", entity.CountAHI),
                    new SQLiteParameter("@CountAI", entity.CountAI),
                    new SQLiteParameter("@CountHI", entity.CountHI),
                    new SQLiteParameter("@CountSnore", entity.CountSnore),
                    new SQLiteParameter("@CountPassive", entity.CountPassive),
                    new SQLiteParameter("@PressureMax", entity.PressureMax),
                    new SQLiteParameter("@PressureP95", entity.PressureP95),
                    new SQLiteParameter("@PressureMedian", entity.PressureMedian),
                    new SQLiteParameter("@FlowMax", entity.FlowMax),
                    new SQLiteParameter("@FlowP95", entity.FlowP95),
                    new SQLiteParameter("@FlowMedian", entity.FlowMedian),
                    new SQLiteParameter("@LeakMax", entity.LeakMax),
                    new SQLiteParameter("@LeakP95", entity.LeakP95),
                    new SQLiteParameter("@LeakMedian", entity.LeakMedian),
                    new SQLiteParameter("@TidalVolumeMax", entity.TidalVolumeMax),
                    new SQLiteParameter("@TidalVolumeP95", entity.TidalVolumeP95),
                    new SQLiteParameter("@TidalVolumeMedian", entity.TidalVolumeMedian),
                    new SQLiteParameter("@MinuteVentilationMax", entity.MinuteVentilationMax),
                    new SQLiteParameter("@MinuteVentilationP95", entity.MinuteVentilationP95),
                    new SQLiteParameter("@MinuteVentilationMedian", entity.MinuteVentilationMedian),
                    new SQLiteParameter("@SpO2Max", entity.SpO2Max),
                    new SQLiteParameter("@SpO2P95", entity.SpO2P95),
                    new SQLiteParameter("@SpO2Median", entity.SpO2Median),
                    new SQLiteParameter("@PulseRateMax", entity.PulseRateMax),
                    new SQLiteParameter("@PulseRateP95", entity.PulseRateP95),
                    new SQLiteParameter("@PulseRateMedian", entity.PulseRateMedian),
                    new SQLiteParameter("@RespiratoryRateMax", entity.RespiratoryRateMax),
                    new SQLiteParameter("@RespiratoryRateP95", entity.RespiratoryRateP95),
                    new SQLiteParameter("@RespiratoryRateMedian", entity.RespiratoryRateMedian),
                    new SQLiteParameter("@IERatioMax", entity.IERatioMax),
                    new SQLiteParameter("@IERatioP95", entity.IERatioP95),
                    new SQLiteParameter("@IERatioMedian", entity.IERatioMedian),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@IPAPP95", entity.IPAPP95),
                    new SQLiteParameter("@IPAPMedian", entity.IPAPMedian),
                    new SQLiteParameter("@EPAPMax", entity.EPAPMax),
                    new SQLiteParameter("@EPAPP95", entity.EPAPP95),
                    new SQLiteParameter("@EPAPMedian", entity.EPAPMedian)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Insert(IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SQLiteTransaction tran = sQLiteConnection.BeginTransaction();
                try
                {
                    foreach (var v in entitys)
                    {
                        Insert(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 创建实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Insert(SQLiteTransaction transaction, IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    foreach (var v in entitys)
                    {
                        Insert(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除对象
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
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, deleteById,
                   new SQLiteParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(SQLiteTransaction transaction, Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteById,
                   new SQLiteParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(ProductWorkingStatisticsData entity)
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
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Delete(SQLiteTransaction transaction, ProductWorkingStatisticsData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    Delete(entity);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 删除实体对象集合
        /// </summary>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var v in entitys)
                {
                    sb.Append(v.Id);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 2, 1);
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, deleteByIds, new SQLiteParameter("@Ids", sb.ToString()));
                sb.Clear();
                sb = null;
            }
        }

        /// <summary>
        /// 删除实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(SQLiteTransaction transaction, IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                foreach (var v in entitys)
                {
                    Delete(transaction, v);
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="entity">将要编辑的一个对象</param>
        public virtual void Update(ProductWorkingStatisticsData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@DataTime", entity.DataTime),
                    new SQLiteParameter("@TotalUsage", entity.TotalUsage),
                    new SQLiteParameter("@CountAHI", entity.CountAHI),
                    new SQLiteParameter("@CountAI", entity.CountAI),
                    new SQLiteParameter("@CountHI", entity.CountHI),
                    new SQLiteParameter("@CountSnore", entity.CountSnore),
                    new SQLiteParameter("@CountPassive", entity.CountPassive),
                    new SQLiteParameter("@PressureMax", entity.PressureMax),
                    new SQLiteParameter("@PressureP95", entity.PressureP95),
                    new SQLiteParameter("@PressureMedian", entity.PressureMedian),
                    new SQLiteParameter("@FlowMax", entity.FlowMax),
                    new SQLiteParameter("@FlowP95", entity.FlowP95),
                    new SQLiteParameter("@FlowMedian", entity.FlowMedian),
                    new SQLiteParameter("@LeakMax", entity.LeakMax),
                    new SQLiteParameter("@LeakP95", entity.LeakP95),
                    new SQLiteParameter("@LeakMedian", entity.LeakMedian),
                    new SQLiteParameter("@TidalVolumeMax", entity.TidalVolumeMax),
                    new SQLiteParameter("@TidalVolumeP95", entity.TidalVolumeP95),
                    new SQLiteParameter("@TidalVolumeMedian", entity.TidalVolumeMedian),
                    new SQLiteParameter("@MinuteVentilationMax", entity.MinuteVentilationMax),
                    new SQLiteParameter("@MinuteVentilationP95", entity.MinuteVentilationP95),
                    new SQLiteParameter("@MinuteVentilationMedian", entity.MinuteVentilationMedian),
                    new SQLiteParameter("@SpO2Max", entity.SpO2Max),
                    new SQLiteParameter("@SpO2P95", entity.SpO2P95),
                    new SQLiteParameter("@SpO2Median", entity.SpO2Median),
                    new SQLiteParameter("@PulseRateMax", entity.PulseRateMax),
                    new SQLiteParameter("@PulseRateP95", entity.PulseRateP95),
                    new SQLiteParameter("@PulseRateMedian", entity.PulseRateMedian),
                    new SQLiteParameter("@RespiratoryRateMax", entity.RespiratoryRateMax),
                    new SQLiteParameter("@RespiratoryRateP95", entity.RespiratoryRateP95),
                    new SQLiteParameter("@RespiratoryRateMedian", entity.RespiratoryRateMedian),
                    new SQLiteParameter("@IERatioMax", entity.IERatioMax),
                    new SQLiteParameter("@IERatioP95", entity.IERatioP95),
                    new SQLiteParameter("@IERatioMedian", entity.IERatioMedian),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@IPAPP95", entity.IPAPP95),
                    new SQLiteParameter("@IPAPMedian", entity.IPAPMedian),
                    new SQLiteParameter("@EPAPMax", entity.EPAPMax),
                    new SQLiteParameter("@EPAPP95", entity.EPAPP95),
                    new SQLiteParameter("@EPAPMedian", entity.EPAPMedian),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SQLiteTransaction transaction, ProductWorkingStatisticsData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@DataTime", entity.DataTime),
                    new SQLiteParameter("@TotalUsage", entity.TotalUsage),
                    new SQLiteParameter("@CountAHI", entity.CountAHI),
                    new SQLiteParameter("@CountAI", entity.CountAI),
                    new SQLiteParameter("@CountHI", entity.CountHI),
                    new SQLiteParameter("@CountSnore", entity.CountSnore),
                    new SQLiteParameter("@CountPassive", entity.CountPassive),
                    new SQLiteParameter("@PressureMax", entity.PressureMax),
                    new SQLiteParameter("@PressureP95", entity.PressureP95),
                    new SQLiteParameter("@PressureMedian", entity.PressureMedian),
                    new SQLiteParameter("@FlowMax", entity.FlowMax),
                    new SQLiteParameter("@FlowP95", entity.FlowP95),
                    new SQLiteParameter("@FlowMedian", entity.FlowMedian),
                    new SQLiteParameter("@LeakMax", entity.LeakMax),
                    new SQLiteParameter("@LeakP95", entity.LeakP95),
                    new SQLiteParameter("@LeakMedian", entity.LeakMedian),
                    new SQLiteParameter("@TidalVolumeMax", entity.TidalVolumeMax),
                    new SQLiteParameter("@TidalVolumeP95", entity.TidalVolumeP95),
                    new SQLiteParameter("@TidalVolumeMedian", entity.TidalVolumeMedian),
                    new SQLiteParameter("@MinuteVentilationMax", entity.MinuteVentilationMax),
                    new SQLiteParameter("@MinuteVentilationP95", entity.MinuteVentilationP95),
                    new SQLiteParameter("@MinuteVentilationMedian", entity.MinuteVentilationMedian),
                    new SQLiteParameter("@SpO2Max", entity.SpO2Max),
                    new SQLiteParameter("@SpO2P95", entity.SpO2P95),
                    new SQLiteParameter("@SpO2Median", entity.SpO2Median),
                    new SQLiteParameter("@PulseRateMax", entity.PulseRateMax),
                    new SQLiteParameter("@PulseRateP95", entity.PulseRateP95),
                    new SQLiteParameter("@PulseRateMedian", entity.PulseRateMedian),
                    new SQLiteParameter("@RespiratoryRateMax", entity.RespiratoryRateMax),
                    new SQLiteParameter("@RespiratoryRateP95", entity.RespiratoryRateP95),
                    new SQLiteParameter("@RespiratoryRateMedian", entity.RespiratoryRateMedian),
                    new SQLiteParameter("@IERatioMax", entity.IERatioMax),
                    new SQLiteParameter("@IERatioP95", entity.IERatioP95),
                    new SQLiteParameter("@IERatioMedian", entity.IERatioMedian),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@IPAPP95", entity.IPAPP95),
                    new SQLiteParameter("@IPAPMedian", entity.IPAPMedian),
                    new SQLiteParameter("@EPAPMax", entity.EPAPMax),
                    new SQLiteParameter("@EPAPP95", entity.EPAPP95),
                    new SQLiteParameter("@EPAPMedian", entity.EPAPMedian),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SQLiteTransaction tran = sQLiteConnection.BeginTransaction();
                try
                {
                    foreach (var v in entitys)
                    {
                        Update(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                }
            }
        }

        /// <summary>
        /// 更新实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Update(SQLiteTransaction transaction, IEnumerable<ProductWorkingStatisticsData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SQLiteTransaction tran = transaction;
                try
                {
                    foreach (var v in entitys)
                    {
                        Update(tran, v);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
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
        public virtual ProductWorkingStatisticsData GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                ProductWorkingStatisticsData result = new ProductWorkingStatisticsData();
                using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectById,
                      new SQLiteParameter("@Id", id)
                      ))
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.ProductId = reader.GetGuid(1);
                        result.TherapyMode = reader.GetInt32(2);
                        result.DataTime = reader.GetDateTime(3);
                        result.TotalUsage = reader.GetInt64(4);
                        result.CountAHI = reader.GetInt32(5);
                        result.CountAI = reader.GetInt32(6);
                        result.CountHI = reader.GetInt32(7);
                        result.CountSnore = reader.GetInt32(8);
                        result.CountPassive = reader.GetInt32(9);
                        result.PressureMax = reader.GetFloat(10);
                        result.PressureP95 = reader.GetFloat(11);
                        result.PressureMedian = reader.GetFloat(12);
                        result.FlowMax = reader.GetFloat(13);
                        result.FlowP95 = reader.GetFloat(14);
                        result.FlowMedian = reader.GetFloat(15);
                        result.LeakMax = reader.GetFloat(16);
                        result.LeakP95 = reader.GetFloat(17);
                        result.LeakMedian = reader.GetFloat(18);
                        result.TidalVolumeMax = reader.GetFloat(19);
                        result.TidalVolumeP95 = reader.GetFloat(20);
                        result.TidalVolumeMedian = reader.GetFloat(21);
                        result.MinuteVentilationMax = reader.GetInt32(22);
                        result.MinuteVentilationP95 = reader.GetInt32(23);
                        result.MinuteVentilationMedian = reader.GetInt32(24);
                        result.SpO2Max = reader.GetInt32(25);
                        result.SpO2P95 = reader.GetInt32(26);
                        result.SpO2Median = reader.GetInt32(27);
                        result.PulseRateMax = reader.GetInt32(28);
                        result.PulseRateP95 = reader.GetInt32(29);
                        result.PulseRateMedian = reader.GetInt32(30);
                        result.RespiratoryRateMax = reader.GetInt32(31);
                        result.RespiratoryRateP95 = reader.GetInt32(32);
                        result.RespiratoryRateMedian = reader.GetInt32(33);
                        result.IERatioMax = reader.GetFloat(34);
                        result.IERatioP95 = reader.GetFloat(35);
                        result.IERatioMedian = reader.GetFloat(36);
                        result.IPAPMax = reader.GetFloat(37);
                        result.IPAPP95 = reader.GetFloat(38);
                        result.IPAPMedian = reader.GetFloat(39);
                        result.EPAPMax = reader.GetFloat(40);
                        result.EPAPP95 = reader.GetFloat(41);
                        result.EPAPMedian = reader.GetFloat(42);
                    }
                    reader.Close();
                }
                return result;
            }
            return default(ProductWorkingStatisticsData);
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual IEnumerable<ProductWorkingStatisticsData> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingStatisticsData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingStatisticsData>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectPaging,
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
                ))
            {
                while (reader.Read())
                {
                    ProductWorkingStatisticsData result = new ProductWorkingStatisticsData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.TherapyMode = reader.GetInt32(2);
                    result.DataTime = reader.GetDateTime(3);
                    result.TotalUsage = reader.GetInt64(4);
                    result.CountAHI = reader.GetInt32(5);
                    result.CountAI = reader.GetInt32(6);
                    result.CountHI = reader.GetInt32(7);
                    result.CountSnore = reader.GetInt32(8);
                    result.CountPassive = reader.GetInt32(9);
                    result.PressureMax = reader.GetFloat(10);
                    result.PressureP95 = reader.GetFloat(11);
                    result.PressureMedian = reader.GetFloat(12);
                    result.FlowMax = reader.GetFloat(13);
                    result.FlowP95 = reader.GetFloat(14);
                    result.FlowMedian = reader.GetFloat(15);
                    result.LeakMax = reader.GetFloat(16);
                    result.LeakP95 = reader.GetFloat(17);
                    result.LeakMedian = reader.GetFloat(18);
                    result.TidalVolumeMax = reader.GetFloat(19);
                    result.TidalVolumeP95 = reader.GetFloat(20);
                    result.TidalVolumeMedian = reader.GetFloat(21);
                    result.MinuteVentilationMax = reader.GetInt32(22);
                    result.MinuteVentilationP95 = reader.GetInt32(23);
                    result.MinuteVentilationMedian = reader.GetInt32(24);
                    result.SpO2Max = reader.GetInt32(25);
                    result.SpO2P95 = reader.GetInt32(26);
                    result.SpO2Median = reader.GetInt32(27);
                    result.PulseRateMax = reader.GetInt32(28);
                    result.PulseRateP95 = reader.GetInt32(29);
                    result.PulseRateMedian = reader.GetInt32(30);
                    result.RespiratoryRateMax = reader.GetInt32(31);
                    result.RespiratoryRateP95 = reader.GetInt32(32);
                    result.RespiratoryRateMedian = reader.GetInt32(33);
                    result.IERatioMax = reader.GetFloat(34);
                    result.IERatioP95 = reader.GetFloat(35);
                    result.IERatioMedian = reader.GetFloat(36);
                    result.IPAPMax = reader.GetFloat(37);
                    result.IPAPP95 = reader.GetFloat(38);
                    result.IPAPMedian = reader.GetFloat(39);
                    result.EPAPMax = reader.GetFloat(40);
                    result.EPAPP95 = reader.GetFloat(41);
                    result.EPAPMedian = reader.GetFloat(42);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="productId">productId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual IEnumerable<ProductWorkingStatisticsData> SelectByProductIdTherapyModeDataTime(Guid productId, int therapyMode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = Convert.ToInt32(SQLiteHelper.ExecuteScalar(sQLiteConnection, CommandType.Text, selectByProductIdTherapyModeDataTimeCount,
                  new SQLiteParameter("@ProductId", productId),
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime)
                 ));
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingStatisticsData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingStatisticsData>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByProductIdTherapyModeDataTime,
                new SQLiteParameter("@ProductId", productId),
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime),
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
                ))
            {
                while (reader.Read())
                {
                    ProductWorkingStatisticsData result = new ProductWorkingStatisticsData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.TherapyMode = reader.GetInt32(2);
                    result.DataTime = reader.GetDateTime(3);
                    result.TotalUsage = reader.GetInt64(4);
                    result.CountAHI = reader.GetInt32(5);
                    result.CountAI = reader.GetInt32(6);
                    result.CountHI = reader.GetInt32(7);
                    result.CountSnore = reader.GetInt32(8);
                    result.CountPassive = reader.GetInt32(9);
                    result.PressureMax = reader.GetFloat(10);
                    result.PressureP95 = reader.GetFloat(11);
                    result.PressureMedian = reader.GetFloat(12);
                    result.FlowMax = reader.GetFloat(13);
                    result.FlowP95 = reader.GetFloat(14);
                    result.FlowMedian = reader.GetFloat(15);
                    result.LeakMax = reader.GetFloat(16);
                    result.LeakP95 = reader.GetFloat(17);
                    result.LeakMedian = reader.GetFloat(18);
                    result.TidalVolumeMax = reader.GetFloat(19);
                    result.TidalVolumeP95 = reader.GetFloat(20);
                    result.TidalVolumeMedian = reader.GetFloat(21);
                    result.MinuteVentilationMax = reader.GetInt32(22);
                    result.MinuteVentilationP95 = reader.GetInt32(23);
                    result.MinuteVentilationMedian = reader.GetInt32(24);
                    result.SpO2Max = reader.GetInt32(25);
                    result.SpO2P95 = reader.GetInt32(26);
                    result.SpO2Median = reader.GetInt32(27);
                    result.PulseRateMax = reader.GetInt32(28);
                    result.PulseRateP95 = reader.GetInt32(29);
                    result.PulseRateMedian = reader.GetInt32(30);
                    result.RespiratoryRateMax = reader.GetInt32(31);
                    result.RespiratoryRateP95 = reader.GetInt32(32);
                    result.RespiratoryRateMedian = reader.GetInt32(33);
                    result.IERatioMax = reader.GetFloat(34);
                    result.IERatioP95 = reader.GetFloat(35);
                    result.IERatioMedian = reader.GetFloat(36);
                    result.IPAPMax = reader.GetFloat(37);
                    result.IPAPP95 = reader.GetFloat(38);
                    result.IPAPMedian = reader.GetFloat(39);
                    result.EPAPMax = reader.GetFloat(40);
                    result.EPAPP95 = reader.GetFloat(41);
                    result.EPAPMedian = reader.GetFloat(42);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id desc排序
        /// </summary>
        /// <param name="productId">productId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <returns></returns>
        public virtual IEnumerable<ProductWorkingStatisticsData> SelectByProductIdTherapyModeDataTime(Guid productId, int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ProductWorkingStatisticsData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingStatisticsData>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByProductIdTherapyModeDataTime2,
                new SQLiteParameter("@ProductId", productId),
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime)
                ))
            {
                while (reader.Read())
                {
                    ProductWorkingStatisticsData result = new ProductWorkingStatisticsData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.TherapyMode = reader.GetInt32(2);
                    result.DataTime = reader.GetDateTime(3);
                    result.TotalUsage = reader.GetInt64(4);
                    result.CountAHI = reader.GetInt32(5);
                    result.CountAI = reader.GetInt32(6);
                    result.CountHI = reader.GetInt32(7);
                    result.CountSnore = reader.GetInt32(8);
                    result.CountPassive = reader.GetInt32(9);
                    result.PressureMax = reader.GetFloat(10);
                    result.PressureP95 = reader.GetFloat(11);
                    result.PressureMedian = reader.GetFloat(12);
                    result.FlowMax = reader.GetFloat(13);
                    result.FlowP95 = reader.GetFloat(14);
                    result.FlowMedian = reader.GetFloat(15);
                    result.LeakMax = reader.GetFloat(16);
                    result.LeakP95 = reader.GetFloat(17);
                    result.LeakMedian = reader.GetFloat(18);
                    result.TidalVolumeMax = reader.GetFloat(19);
                    result.TidalVolumeP95 = reader.GetFloat(20);
                    result.TidalVolumeMedian = reader.GetFloat(21);
                    result.MinuteVentilationMax = reader.GetInt32(22);
                    result.MinuteVentilationP95 = reader.GetInt32(23);
                    result.MinuteVentilationMedian = reader.GetInt32(24);
                    result.SpO2Max = reader.GetInt32(25);
                    result.SpO2P95 = reader.GetInt32(26);
                    result.SpO2Median = reader.GetInt32(27);
                    result.PulseRateMax = reader.GetInt32(28);
                    result.PulseRateP95 = reader.GetInt32(29);
                    result.PulseRateMedian = reader.GetInt32(30);
                    result.RespiratoryRateMax = reader.GetInt32(31);
                    result.RespiratoryRateP95 = reader.GetInt32(32);
                    result.RespiratoryRateMedian = reader.GetInt32(33);
                    result.IERatioMax = reader.GetFloat(34);
                    result.IERatioP95 = reader.GetFloat(35);
                    result.IERatioMedian = reader.GetFloat(36);
                    result.IPAPMax = reader.GetFloat(37);
                    result.IPAPP95 = reader.GetFloat(38);
                    result.IPAPMedian = reader.GetFloat(39);
                    result.EPAPMax = reader.GetFloat(40);
                    result.EPAPP95 = reader.GetFloat(41);
                    result.EPAPMedian = reader.GetFloat(42);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        #endregion

        #region Dispose 

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (!Equals(sQLiteConnection, null))
            {
                sQLiteConnection.Close();
                sQLiteConnection.Dispose();
                sQLiteConnection = null;
            }
        }

        #endregion
    }
}