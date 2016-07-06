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
    /// ProductWorkingSummaryData数据访问层，可使用显示事物
    /// </summary>
    public class ProductWorkingSummaryDataDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ProductWorkingSummaryDataDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //48个字段
        private const string selectCount = "SELECT COUNT(*) FROM ProductWorkingSummaryDatas";
        private const string insert = @"INSERT INTO ProductWorkingSummaryDatas(
Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,
ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,
Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,Config_SmartStart,Config_PressureUnit,
Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2) 
VALUES(@Id,@ProductId,@FileName,@StartTime,@EndTime,@ProductVersion,@ProductModel,@WorkingTime,@CurrentTime,@TherapyMode,@IPAP,@EPAP,@RiseTime,@RespiratoryRate,@InspireTime,
@ITrigger,@ETrigger,@Ramp,@ExhaleTime,@IPAPMax,@EPAPMin,@PSMax,@PSMin,@CPAP,@CFlex,@CPAPStart,@CPAPMax,@CPAPMin,@Alert,@Alert_Tube,@Alert_Apnea,@Alert_MinuteVentilation,
@Alert_HRate,@Alert_LRate,@Alert_Reserve1,@Alert_Reserve2,@Alert_Reserve3,@Alert_Reserve4,@Config_HumidifierLevel,@Config_DataStore,@Config_SmartStart,@Config_PressureUnit,
@Config_Language,@Config_Backlight,@Config_MaskPressure,@Config_ClinicalSet,@Config_Reserve1,@Config_Reserve2)";
        private const string deleteById = "DELETE FROM ProductWorkingSummaryDatas WHERE Id=@Id";
        private const string deleteByIds = "DELETE FROM ProductWorkingSummaryDatas WHERE Id IN (@Ids)";

        private const string updateById = @"UPDATE ProductWorkingSummaryDatas SET Id=@Id,ProductId=@ProductId,FileName=@FileName,StartTime=@StartTime,EndTime=@EndTime,
ProductVersion=@ProductVersion,ProductModel=@ProductModel,WorkingTime=@WorkingTime,CurrentTime=@CurrentTime,TherapyMode=@TherapyMode,IPAP=@IPAP,EPAP=@EPAP,
RiseTime=@RiseTime,RespiratoryRate=@RespiratoryRate,InspireTime=@InspireTime,ITrigger=@ITrigger,ETrigger=@ETrigger,Ramp=@Ramp,ExhaleTime=@ExhaleTime,IPAPMax=@IPAPMax,
EPAPMin=@EPAPMin,PSMax=@PSMax,PSMin=@PSMin,CPAP=@CPAP,CFlex=@CFlex,CPAPStart=@CPAPStart,CPAPMax=@CPAPMax,CPAPMin=@CPAPMin,Alert=@Alert,Alert_Tube=@Alert_Tube,
Alert_Apnea=@Alert_Apnea,Alert_MinuteVentilation=@Alert_MinuteVentilation,Alert_HRate=@Alert_HRate,Alert_LRate=@Alert_LRate,Alert_Reserve1=@Alert_Reserve1,
Alert_Reserve2=@Alert_Reserve2,Alert_Reserve3=@Alert_Reserve3,Alert_Reserve4=@Alert_Reserve4,Config_HumidifierLevel=@Config_HumidifierLevel,Config_DataStore=@Config_DataStore,
Config_SmartStart=@Config_SmartStart,Config_PressureUnit=@Config_PressureUnit,Config_Language=@Config_Language,Config_Backlight=@Config_Backlight,
Config_MaskPressure=@Config_MaskPressure,Config_ClinicalSet=@Config_ClinicalSet,Config_Reserve1=@Config_Reserve1,Config_Reserve2=@Config_Reserve2  
WHERE Id =@Id";

        private const string selectById = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,TherapyMode,IPAP,EPAP,
RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,Alert_Tube,Alert_Apnea,
Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,Config_SmartStart,
Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas WHERE Id =@Id";

        private const string selectPaging = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,TherapyMode,IPAP,EPAP,
RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,Alert_Tube,Alert_Apnea,
Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,Config_SmartStart,
Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas 
ORDER BY Id DESC 
LIMIT @PageSize OFFSET @OffictCount";
        private const string selectByProductIdTherapyModeDataTime = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime 
ORDER BY Id DESC 
LIMIT @PageSize OFFSET @OffictCount";

        private const string selectByProductIdTherapyModeDataTimeCount = @"SELECT COUNT(*) FROM ProductWorkingSummaryDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime";

        private const string selectByProductIdFileName = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas 
WHERE ProductId=@ProductId AND FileName=@FileName ORDER BY Id DESC";
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
        public virtual void Insert(ProductWorkingSummaryData entity)
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
                    new SQLiteParameter("@FileName", entity.FileName),
                    new SQLiteParameter("@StartTime", entity.StartTime),
                    new SQLiteParameter("@EndTime", entity.EndTime),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@WorkingTime", entity.WorkingTime),
                    new SQLiteParameter("@CurrentTime", entity.CurrentTime),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@IPAP", entity.IPAP),
                    new SQLiteParameter("@EPAP", entity.EPAP),
                    new SQLiteParameter("@RiseTime", entity.RiseTime),
                    new SQLiteParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SQLiteParameter("@InspireTime", entity.InspireTime),
                    new SQLiteParameter("@ITrigger", entity.ITrigger),
                    new SQLiteParameter("@ETrigger", entity.ETrigger),
                    new SQLiteParameter("@Ramp", entity.Ramp),
                    new SQLiteParameter("@ExhaleTime", entity.ExhaleTime),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@EPAPMin", entity.EPAPMin),
                    new SQLiteParameter("@PSMax", entity.PSMax),
                    new SQLiteParameter("@PSMin", entity.PSMin),
                    new SQLiteParameter("@CPAP", entity.CPAP),
                    new SQLiteParameter("@CFlex", entity.CFlex),
                    new SQLiteParameter("@CPAPStart", entity.CPAPStart),
                    new SQLiteParameter("@CPAPMax", entity.CPAPMax),
                    new SQLiteParameter("@CPAPMin", entity.CPAPMin),
                    new SQLiteParameter("@Alert", entity.Alert),
                    new SQLiteParameter("@Alert_Tube", entity.Alert_Tube),
                    new SQLiteParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SQLiteParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SQLiteParameter("@Alert_HRate", entity.Alert_HRate),
                    new SQLiteParameter("@Alert_LRate", entity.Alert_LRate),
                    new SQLiteParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SQLiteParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SQLiteParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SQLiteParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SQLiteParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SQLiteParameter("@Config_DataStore", entity.Config_DataStore),
                    new SQLiteParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SQLiteParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SQLiteParameter("@Config_Language", entity.Config_Language),
                    new SQLiteParameter("@Config_Backlight", entity.Config_Backlight),
                    new SQLiteParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SQLiteParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SQLiteParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SQLiteParameter("@Config_Reserve2", entity.Config_Reserve2)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SQLiteTransaction transaction, ProductWorkingSummaryData entity)
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
                    new SQLiteParameter("@FileName", entity.FileName),
                    new SQLiteParameter("@StartTime", entity.StartTime),
                    new SQLiteParameter("@EndTime", entity.EndTime),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@WorkingTime", entity.WorkingTime),
                    new SQLiteParameter("@CurrentTime", entity.CurrentTime),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@IPAP", entity.IPAP),
                    new SQLiteParameter("@EPAP", entity.EPAP),
                    new SQLiteParameter("@RiseTime", entity.RiseTime),
                    new SQLiteParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SQLiteParameter("@InspireTime", entity.InspireTime),
                    new SQLiteParameter("@ITrigger", entity.ITrigger),
                    new SQLiteParameter("@ETrigger", entity.ETrigger),
                    new SQLiteParameter("@Ramp", entity.Ramp),
                    new SQLiteParameter("@ExhaleTime", entity.ExhaleTime),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@EPAPMin", entity.EPAPMin),
                    new SQLiteParameter("@PSMax", entity.PSMax),
                    new SQLiteParameter("@PSMin", entity.PSMin),
                    new SQLiteParameter("@CPAP", entity.CPAP),
                    new SQLiteParameter("@CFlex", entity.CFlex),
                    new SQLiteParameter("@CPAPStart", entity.CPAPStart),
                    new SQLiteParameter("@CPAPMax", entity.CPAPMax),
                    new SQLiteParameter("@CPAPMin", entity.CPAPMin),
                    new SQLiteParameter("@Alert", entity.Alert),
                    new SQLiteParameter("@Alert_Tube", entity.Alert_Tube),
                    new SQLiteParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SQLiteParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SQLiteParameter("@Alert_HRate", entity.Alert_HRate),
                    new SQLiteParameter("@Alert_LRate", entity.Alert_LRate),
                    new SQLiteParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SQLiteParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SQLiteParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SQLiteParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SQLiteParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SQLiteParameter("@Config_DataStore", entity.Config_DataStore),
                    new SQLiteParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SQLiteParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SQLiteParameter("@Config_Language", entity.Config_Language),
                    new SQLiteParameter("@Config_Backlight", entity.Config_Backlight),
                    new SQLiteParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SQLiteParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SQLiteParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SQLiteParameter("@Config_Reserve2", entity.Config_Reserve2)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">一个实体对象</param>
        public virtual void Insert(IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual void Insert(SQLiteTransaction transaction, IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual void Delete(ProductWorkingSummaryData entity)
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
        public virtual void Delete(SQLiteTransaction transaction, ProductWorkingSummaryData entity)
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
        public virtual void Delete(IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual void Delete(SQLiteTransaction transaction, IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual void Update(ProductWorkingSummaryData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(sQLiteConnection, System.Data.CommandType.Text, updateById,
                    new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@FileName", entity.FileName),
                    new SQLiteParameter("@StartTime", entity.StartTime),
                    new SQLiteParameter("@EndTime", entity.EndTime),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@WorkingTime", entity.WorkingTime),
                    new SQLiteParameter("@CurrentTime", entity.CurrentTime),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@IPAP", entity.IPAP),
                    new SQLiteParameter("@EPAP", entity.EPAP),
                    new SQLiteParameter("@RiseTime", entity.RiseTime),
                    new SQLiteParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SQLiteParameter("@InspireTime", entity.InspireTime),
                    new SQLiteParameter("@ITrigger", entity.ITrigger),
                    new SQLiteParameter("@ETrigger", entity.ETrigger),
                    new SQLiteParameter("@Ramp", entity.Ramp),
                    new SQLiteParameter("@ExhaleTime", entity.ExhaleTime),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@EPAPMin", entity.EPAPMin),
                    new SQLiteParameter("@PSMax", entity.PSMax),
                    new SQLiteParameter("@PSMin", entity.PSMin),
                    new SQLiteParameter("@CPAP", entity.CPAP),
                    new SQLiteParameter("@CFlex", entity.CFlex),
                    new SQLiteParameter("@CPAPStart", entity.CPAPStart),
                    new SQLiteParameter("@CPAPMax", entity.CPAPMax),
                    new SQLiteParameter("@CPAPMin", entity.CPAPMin),
                    new SQLiteParameter("@Alert", entity.Alert),
                    new SQLiteParameter("@Alert_Tube", entity.Alert_Tube),
                    new SQLiteParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SQLiteParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SQLiteParameter("@Alert_HRate", entity.Alert_HRate),
                    new SQLiteParameter("@Alert_LRate", entity.Alert_LRate),
                    new SQLiteParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SQLiteParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SQLiteParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SQLiteParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SQLiteParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SQLiteParameter("@Config_DataStore", entity.Config_DataStore),
                    new SQLiteParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SQLiteParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SQLiteParameter("@Config_Language", entity.Config_Language),
                    new SQLiteParameter("@Config_Backlight", entity.Config_Backlight),
                    new SQLiteParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SQLiteParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SQLiteParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SQLiteParameter("@Config_Reserve2", entity.Config_Reserve2),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SQLiteTransaction transaction, ProductWorkingSummaryData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SQLiteHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                       new SQLiteParameter("@ProductId", entity.ProductId),
                    new SQLiteParameter("@FileName", entity.FileName),
                    new SQLiteParameter("@StartTime", entity.StartTime),
                    new SQLiteParameter("@EndTime", entity.EndTime),
                    new SQLiteParameter("@ProductVersion", entity.ProductVersion),
                    new SQLiteParameter("@ProductModel", entity.ProductModel),
                    new SQLiteParameter("@WorkingTime", entity.WorkingTime),
                    new SQLiteParameter("@CurrentTime", entity.CurrentTime),
                    new SQLiteParameter("@TherapyMode", entity.TherapyMode),
                    new SQLiteParameter("@IPAP", entity.IPAP),
                    new SQLiteParameter("@EPAP", entity.EPAP),
                    new SQLiteParameter("@RiseTime", entity.RiseTime),
                    new SQLiteParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SQLiteParameter("@InspireTime", entity.InspireTime),
                    new SQLiteParameter("@ITrigger", entity.ITrigger),
                    new SQLiteParameter("@ETrigger", entity.ETrigger),
                    new SQLiteParameter("@Ramp", entity.Ramp),
                    new SQLiteParameter("@ExhaleTime", entity.ExhaleTime),
                    new SQLiteParameter("@IPAPMax", entity.IPAPMax),
                    new SQLiteParameter("@EPAPMin", entity.EPAPMin),
                    new SQLiteParameter("@PSMax", entity.PSMax),
                    new SQLiteParameter("@PSMin", entity.PSMin),
                    new SQLiteParameter("@CPAP", entity.CPAP),
                    new SQLiteParameter("@CFlex", entity.CFlex),
                    new SQLiteParameter("@CPAPStart", entity.CPAPStart),
                    new SQLiteParameter("@CPAPMax", entity.CPAPMax),
                    new SQLiteParameter("@CPAPMin", entity.CPAPMin),
                    new SQLiteParameter("@Alert", entity.Alert),
                    new SQLiteParameter("@Alert_Tube", entity.Alert_Tube),
                    new SQLiteParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SQLiteParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SQLiteParameter("@Alert_HRate", entity.Alert_HRate),
                    new SQLiteParameter("@Alert_LRate", entity.Alert_LRate),
                    new SQLiteParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SQLiteParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SQLiteParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SQLiteParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SQLiteParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SQLiteParameter("@Config_DataStore", entity.Config_DataStore),
                    new SQLiteParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SQLiteParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SQLiteParameter("@Config_Language", entity.Config_Language),
                    new SQLiteParameter("@Config_Backlight", entity.Config_Backlight),
                    new SQLiteParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SQLiteParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SQLiteParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SQLiteParameter("@Config_Reserve2", entity.Config_Reserve2),
                    new SQLiteParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual void Update(SQLiteTransaction transaction, IEnumerable<ProductWorkingSummaryData> entitys)
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
        public virtual ProductWorkingSummaryData GetById(Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                ProductWorkingSummaryData result = new ProductWorkingSummaryData();
                using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectById,
                      new SQLiteParameter("@Id", id)
                      ))
                {
                    while (reader.Read())
                    {
                        result.Id = reader.GetGuid(0);
                        result.ProductId = reader.GetGuid(1);
                        result.FileName = reader.GetString(2);
                        result.StartTime = reader.GetDateTime(3);
                        result.EndTime = reader.GetDateTime(4);
                        result.ProductVersion = reader.GetString(5);
                        result.ProductModel = reader.GetInt32(6);
                        result.WorkingTime = reader.GetInt32(7);
                        result.CurrentTime = reader.GetDateTime(8);
                        result.TherapyMode = reader.GetInt32(9);
                        result.IPAP = reader.GetFloat(10);
                        result.EPAP = reader.GetFloat(11);
                        result.RiseTime = reader.GetInt32(12);
                        result.RespiratoryRate = reader.GetInt32(13);
                        result.InspireTime = reader.GetInt32(14);
                        result.ITrigger = reader.GetInt32(15);
                        result.ETrigger = reader.GetInt32(16);
                        result.Ramp = reader.GetInt32(17);
                        result.ExhaleTime = reader.GetInt32(18);
                        result.IPAPMax = reader.GetFloat(19);
                        result.EPAPMin = reader.GetFloat(20);
                        result.PSMax = reader.GetFloat(21);
                        result.PSMin = reader.GetInt32(22);
                        result.CPAP = reader.GetInt32(23);
                        result.CFlex = reader.GetInt32(24);
                        result.CPAPStart = reader.GetInt32(25);
                        result.CPAPMax = reader.GetInt32(26);
                        result.CPAPMin = reader.GetInt32(27);
                        result.Alert = reader.GetInt32(28);
                        result.Alert_Tube = reader.GetInt32(29);
                        result.Alert_Apnea = reader.GetInt32(30);
                        result.Alert_MinuteVentilation = reader.GetInt32(31);
                        result.Alert_HRate = reader.GetInt32(32);
                        result.Alert_LRate = reader.GetInt32(33);
                        result.Alert_Reserve1 = reader.GetInt32(34);
                        result.Alert_Reserve2 = reader.GetInt32(35);
                        result.Alert_Reserve3 = reader.GetInt32(36);
                        result.Alert_Reserve4 = reader.GetInt32(37);
                        result.Config_HumidifierLevel = reader.GetInt32(38);
                        result.Config_DataStore = reader.GetInt32(39);
                        result.Config_SmartStart = reader.GetInt32(40);
                        result.Config_PressureUnit = reader.GetInt32(41);
                        result.Config_Language = reader.GetInt32(42);
                        result.Config_Backlight = reader.GetInt32(43);
                        result.Config_MaskPressure = reader.GetInt32(43);
                        result.Config_ClinicalSet = reader.GetInt32(45);
                        result.Config_Reserve1 = reader.GetInt32(46);
                        result.Config_Reserve2 = reader.GetInt32(47);
                    }
                    reader.Close();
                }
                return result;
            }
            return default(ProductWorkingSummaryData);
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual IEnumerable<ProductWorkingSummaryData> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingSummaryData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectPaging,
                new SQLiteParameter("@PageSize", pageSize),
                new SQLiteParameter("@OffsetCount", offsetCount)
                ))
            {
                while (reader.Read())
                {
                    ProductWorkingSummaryData result = new ProductWorkingSummaryData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.FileName = reader.GetString(2);
                    result.StartTime = reader.GetDateTime(3);
                    result.EndTime = reader.GetDateTime(4);
                    result.ProductVersion = reader.GetString(5);
                    result.ProductModel = reader.GetInt32(6);
                    result.WorkingTime = reader.GetInt32(7);
                    result.CurrentTime = reader.GetDateTime(8);
                    result.TherapyMode = reader.GetInt32(9);
                    result.IPAP = reader.GetFloat(10);
                    result.EPAP = reader.GetFloat(11);
                    result.RiseTime = reader.GetInt32(12);
                    result.RespiratoryRate = reader.GetInt32(13);
                    result.InspireTime = reader.GetInt32(14);
                    result.ITrigger = reader.GetInt32(15);
                    result.ETrigger = reader.GetInt32(16);
                    result.Ramp = reader.GetInt32(17);
                    result.ExhaleTime = reader.GetInt32(18);
                    result.IPAPMax = reader.GetFloat(19);
                    result.EPAPMin = reader.GetFloat(20);
                    result.PSMax = reader.GetFloat(21);
                    result.PSMin = reader.GetInt32(22);
                    result.CPAP = reader.GetInt32(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetInt32(25);
                    result.CPAPMax = reader.GetInt32(26);
                    result.CPAPMin = reader.GetInt32(27);
                    result.Alert = reader.GetInt32(28);
                    result.Alert_Tube = reader.GetInt32(29);
                    result.Alert_Apnea = reader.GetInt32(30);
                    result.Alert_MinuteVentilation = reader.GetInt32(31);
                    result.Alert_HRate = reader.GetInt32(32);
                    result.Alert_LRate = reader.GetInt32(33);
                    result.Alert_Reserve1 = reader.GetInt32(34);
                    result.Alert_Reserve2 = reader.GetInt32(35);
                    result.Alert_Reserve3 = reader.GetInt32(36);
                    result.Alert_Reserve4 = reader.GetInt32(37);
                    result.Config_HumidifierLevel = reader.GetInt32(38);
                    result.Config_DataStore = reader.GetInt32(39);
                    result.Config_SmartStart = reader.GetInt32(40);
                    result.Config_PressureUnit = reader.GetInt32(41);
                    result.Config_Language = reader.GetInt32(42);
                    result.Config_Backlight = reader.GetInt32(43);
                    result.Config_MaskPressure = reader.GetInt32(43);
                    result.Config_ClinicalSet = reader.GetInt32(45);
                    result.Config_Reserve1 = reader.GetInt32(46);
                    result.Config_Reserve2 = reader.GetInt32(47);
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
        public virtual IEnumerable<ProductWorkingSummaryData> SelectByProductIdTherapyModeDataTime(Guid productId, int therapyMode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, out int recordCount)
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
            ICollection<ProductWorkingSummaryData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
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
                    ProductWorkingSummaryData result = new ProductWorkingSummaryData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.FileName = reader.GetString(2);
                    result.StartTime = reader.GetDateTime(3);
                    result.EndTime = reader.GetDateTime(4);
                    result.ProductVersion = reader.GetString(5);
                    result.ProductModel = reader.GetInt32(6);
                    result.WorkingTime = reader.GetInt32(7);
                    result.CurrentTime = reader.GetDateTime(8);
                    result.TherapyMode = reader.GetInt32(9);
                    result.IPAP = reader.GetFloat(10);
                    result.EPAP = reader.GetFloat(11);
                    result.RiseTime = reader.GetInt32(12);
                    result.RespiratoryRate = reader.GetInt32(13);
                    result.InspireTime = reader.GetInt32(14);
                    result.ITrigger = reader.GetInt32(15);
                    result.ETrigger = reader.GetInt32(16);
                    result.Ramp = reader.GetInt32(17);
                    result.ExhaleTime = reader.GetInt32(18);
                    result.IPAPMax = reader.GetFloat(19);
                    result.EPAPMin = reader.GetFloat(20);
                    result.PSMax = reader.GetFloat(21);
                    result.PSMin = reader.GetInt32(22);
                    result.CPAP = reader.GetInt32(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetInt32(25);
                    result.CPAPMax = reader.GetInt32(26);
                    result.CPAPMin = reader.GetInt32(27);
                    result.Alert = reader.GetInt32(28);
                    result.Alert_Tube = reader.GetInt32(29);
                    result.Alert_Apnea = reader.GetInt32(30);
                    result.Alert_MinuteVentilation = reader.GetInt32(31);
                    result.Alert_HRate = reader.GetInt32(32);
                    result.Alert_LRate = reader.GetInt32(33);
                    result.Alert_Reserve1 = reader.GetInt32(34);
                    result.Alert_Reserve2 = reader.GetInt32(35);
                    result.Alert_Reserve3 = reader.GetInt32(36);
                    result.Alert_Reserve4 = reader.GetInt32(37);
                    result.Config_HumidifierLevel = reader.GetInt32(38);
                    result.Config_DataStore = reader.GetInt32(39);
                    result.Config_SmartStart = reader.GetInt32(40);
                    result.Config_PressureUnit = reader.GetInt32(41);
                    result.Config_Language = reader.GetInt32(42);
                    result.Config_Backlight = reader.GetInt32(43);
                    result.Config_MaskPressure = reader.GetInt32(43);
                    result.Config_ClinicalSet = reader.GetInt32(45);
                    result.Config_Reserve1 = reader.GetInt32(46);
                    result.Config_Reserve2 = reader.GetInt32(47);
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
        /// <param name="fileName">fileName</param>
        /// <returns></returns>
        public virtual IEnumerable<ProductWorkingSummaryData> SelectByProductIdFileName(Guid productId, string fileName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ProductWorkingSummaryData> resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByProductIdFileName,
                new SQLiteParameter("@ProductId", productId),
                new SQLiteParameter("@FileName", fileName)
                ))
            {
                while (reader.Read())
                {
                    ProductWorkingSummaryData result = new ProductWorkingSummaryData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.FileName = reader.GetString(2);
                    result.StartTime = reader.GetDateTime(3);
                    result.EndTime = reader.GetDateTime(4);
                    result.ProductVersion = reader.GetString(5);
                    result.ProductModel = reader.GetInt32(6);
                    result.WorkingTime = reader.GetInt32(7);
                    result.CurrentTime = reader.GetDateTime(8);
                    result.TherapyMode = reader.GetInt32(9);
                    result.IPAP = reader.GetFloat(10);
                    result.EPAP = reader.GetFloat(11);
                    result.RiseTime = reader.GetInt32(12);
                    result.RespiratoryRate = reader.GetInt32(13);
                    result.InspireTime = reader.GetInt32(14);
                    result.ITrigger = reader.GetInt32(15);
                    result.ETrigger = reader.GetInt32(16);
                    result.Ramp = reader.GetInt32(17);
                    result.ExhaleTime = reader.GetInt32(18);
                    result.IPAPMax = reader.GetFloat(19);
                    result.EPAPMin = reader.GetFloat(20);
                    result.PSMax = reader.GetFloat(21);
                    result.PSMin = reader.GetInt32(22);
                    result.CPAP = reader.GetInt32(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetInt32(25);
                    result.CPAPMax = reader.GetInt32(26);
                    result.CPAPMin = reader.GetInt32(27);
                    result.Alert = reader.GetInt32(28);
                    result.Alert_Tube = reader.GetInt32(29);
                    result.Alert_Apnea = reader.GetInt32(30);
                    result.Alert_MinuteVentilation = reader.GetInt32(31);
                    result.Alert_HRate = reader.GetInt32(32);
                    result.Alert_LRate = reader.GetInt32(33);
                    result.Alert_Reserve1 = reader.GetInt32(34);
                    result.Alert_Reserve2 = reader.GetInt32(35);
                    result.Alert_Reserve3 = reader.GetInt32(36);
                    result.Alert_Reserve4 = reader.GetInt32(37);
                    result.Config_HumidifierLevel = reader.GetInt32(38);
                    result.Config_DataStore = reader.GetInt32(39);
                    result.Config_SmartStart = reader.GetInt32(40);
                    result.Config_PressureUnit = reader.GetInt32(41);
                    result.Config_Language = reader.GetInt32(42);
                    result.Config_Backlight = reader.GetInt32(43);
                    result.Config_MaskPressure = reader.GetInt32(43);
                    result.Config_ClinicalSet = reader.GetInt32(45);
                    result.Config_Reserve1 = reader.GetInt32(46);
                    result.Config_Reserve2 = reader.GetInt32(47);
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