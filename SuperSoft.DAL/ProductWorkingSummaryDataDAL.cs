using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            sqlConnection = new SqlConnection(Const.DbConnectionString);
            sqlConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private SqlConnection sqlConnection;

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
FROM ProductWorkingSummaryDatas ORDER BY Id DESC LIMIT @PageSize OFFSET @OffictCount";

        private const string selectByProductIdTherapyModeDataTime = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime ORDER BY Id DESC LIMIT @PageSize OFFSET @OffictCount";

        private const string selectByProductIdTherapyModeDataTimeCount = @"SELECT COUNT(*) FROM ProductWorkingSummaryDatas 
WHERE ProductId=@ProductId AND TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime";

        private const string selectByProductIdFileName = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas WHERE ProductId=@ProductId AND FileName=@FileName";

        private const string selectByProductId = @"SELECT Id,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2 
FROM ProductWorkingSummaryDatas WHERE ProductId=@ProductId ";

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
            return SqlHelper.ExecuteScalar(sqlConnection, System.Data.CommandType.Text, selectCount).GetInt();
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, insert,
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ProductId", entity.ProductId),
                    new SqlParameter("@FileName", entity.FileName),
                    new SqlParameter("@StartTime", entity.StartTime),
                    new SqlParameter("@EndTime", entity.EndTime),
                    new SqlParameter("@ProductVersion", entity.ProductVersion),
                    new SqlParameter("@ProductModel", entity.ProductModel),
                    new SqlParameter("@WorkingTime", entity.WorkingTime),
                    new SqlParameter("@CurrentTime", entity.CurrentTime),
                    new SqlParameter("@TherapyMode", entity.TherapyMode),
                    new SqlParameter("@IPAP", entity.IPAP),
                    new SqlParameter("@EPAP", entity.EPAP),
                    new SqlParameter("@RiseTime", entity.RiseTime),
                    new SqlParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SqlParameter("@InspireTime", entity.InspireTime),
                    new SqlParameter("@ITrigger", entity.ITrigger),
                    new SqlParameter("@ETrigger", entity.ETrigger),
                    new SqlParameter("@Ramp", entity.Ramp),
                    new SqlParameter("@ExhaleTime", entity.ExhaleTime),
                    new SqlParameter("@IPAPMax", entity.IPAPMax),
                    new SqlParameter("@EPAPMin", entity.EPAPMin),
                    new SqlParameter("@PSMax", entity.PSMax),
                    new SqlParameter("@PSMin", entity.PSMin),
                    new SqlParameter("@CPAP", entity.CPAP),
                    new SqlParameter("@CFlex", entity.CFlex),
                    new SqlParameter("@CPAPStart", entity.CPAPStart),
                    new SqlParameter("@CPAPMax", entity.CPAPMax),
                    new SqlParameter("@CPAPMin", entity.CPAPMin),
                    new SqlParameter("@Alert", entity.Alert),
                    new SqlParameter("@Alert_Tube", entity.Alert_Tube),
                    new SqlParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SqlParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SqlParameter("@Alert_HRate", entity.Alert_HRate),
                    new SqlParameter("@Alert_LRate", entity.Alert_LRate),
                    new SqlParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SqlParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SqlParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SqlParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SqlParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SqlParameter("@Config_DataStore", entity.Config_DataStore),
                    new SqlParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SqlParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SqlParameter("@Config_Language", entity.Config_Language),
                    new SqlParameter("@Config_Backlight", entity.Config_Backlight),
                    new SqlParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SqlParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SqlParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SqlParameter("@Config_Reserve2", entity.Config_Reserve2)
                    );
            }
        }

        /// <summary>
        /// 创建对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Insert(SqlTransaction transaction, ProductWorkingSummaryData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, insert,
                    new SqlParameter("@Id", entity.Id),
                    new SqlParameter("@ProductId", entity.ProductId),
                    new SqlParameter("@FileName", entity.FileName),
                    new SqlParameter("@StartTime", entity.StartTime),
                    new SqlParameter("@EndTime", entity.EndTime),
                    new SqlParameter("@ProductVersion", entity.ProductVersion),
                    new SqlParameter("@ProductModel", entity.ProductModel),
                    new SqlParameter("@WorkingTime", entity.WorkingTime),
                    new SqlParameter("@CurrentTime", entity.CurrentTime),
                    new SqlParameter("@TherapyMode", entity.TherapyMode),
                    new SqlParameter("@IPAP", entity.IPAP),
                    new SqlParameter("@EPAP", entity.EPAP),
                    new SqlParameter("@RiseTime", entity.RiseTime),
                    new SqlParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SqlParameter("@InspireTime", entity.InspireTime),
                    new SqlParameter("@ITrigger", entity.ITrigger),
                    new SqlParameter("@ETrigger", entity.ETrigger),
                    new SqlParameter("@Ramp", entity.Ramp),
                    new SqlParameter("@ExhaleTime", entity.ExhaleTime),
                    new SqlParameter("@IPAPMax", entity.IPAPMax),
                    new SqlParameter("@EPAPMin", entity.EPAPMin),
                    new SqlParameter("@PSMax", entity.PSMax),
                    new SqlParameter("@PSMin", entity.PSMin),
                    new SqlParameter("@CPAP", entity.CPAP),
                    new SqlParameter("@CFlex", entity.CFlex),
                    new SqlParameter("@CPAPStart", entity.CPAPStart),
                    new SqlParameter("@CPAPMax", entity.CPAPMax),
                    new SqlParameter("@CPAPMin", entity.CPAPMin),
                    new SqlParameter("@Alert", entity.Alert),
                    new SqlParameter("@Alert_Tube", entity.Alert_Tube),
                    new SqlParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SqlParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SqlParameter("@Alert_HRate", entity.Alert_HRate),
                    new SqlParameter("@Alert_LRate", entity.Alert_LRate),
                    new SqlParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SqlParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SqlParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SqlParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SqlParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SqlParameter("@Config_DataStore", entity.Config_DataStore),
                    new SqlParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SqlParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SqlParameter("@Config_Language", entity.Config_Language),
                    new SqlParameter("@Config_Backlight", entity.Config_Backlight),
                    new SqlParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SqlParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SqlParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SqlParameter("@Config_Reserve2", entity.Config_Reserve2)
                    );
            }
        }

        /// <summary>
        /// 创建实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">一个实体对象</param>
        public virtual void Insert(ICollection<ProductWorkingSummaryData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SqlTransaction tran = sqlConnection.BeginTransaction();
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
        public virtual void Insert(SqlTransaction transaction, ICollection<ProductWorkingSummaryData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys != null && entitys.Count() > 0)
            {
                SqlTransaction tran = transaction;
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteById,
                   new SqlParameter("@Id", id)
                   );
            }
        }

        /// <summary>
        /// 删除对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="id">一个实体对象的Id</param>
        public virtual void Delete(SqlTransaction transaction, Guid id)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (id != Guid.Empty)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, deleteById,
                   new SqlParameter("@Id", id)
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
        public virtual void Delete(SqlTransaction transaction, ProductWorkingSummaryData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlTransaction tran = transaction;
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
        public virtual void Delete(ICollection<ProductWorkingSummaryData> entitys)
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, deleteByIds, new SqlParameter("@Ids", sb.ToString()));
                sb.Clear();
                sb = null;
            }
        }

        /// <summary>
        /// 删除实体对象集合，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entitys">实体对象集合</param>
        public virtual void Delete(SqlTransaction transaction, ICollection<ProductWorkingSummaryData> entitys)
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
                SqlHelper.ExecuteNonQuery(sqlConnection, System.Data.CommandType.Text, updateById,
                    new SqlParameter("@ProductId", entity.ProductId),
                    new SqlParameter("@FileName", entity.FileName),
                    new SqlParameter("@StartTime", entity.StartTime),
                    new SqlParameter("@EndTime", entity.EndTime),
                    new SqlParameter("@ProductVersion", entity.ProductVersion),
                    new SqlParameter("@ProductModel", entity.ProductModel),
                    new SqlParameter("@WorkingTime", entity.WorkingTime),
                    new SqlParameter("@CurrentTime", entity.CurrentTime),
                    new SqlParameter("@TherapyMode", entity.TherapyMode),
                    new SqlParameter("@IPAP", entity.IPAP),
                    new SqlParameter("@EPAP", entity.EPAP),
                    new SqlParameter("@RiseTime", entity.RiseTime),
                    new SqlParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SqlParameter("@InspireTime", entity.InspireTime),
                    new SqlParameter("@ITrigger", entity.ITrigger),
                    new SqlParameter("@ETrigger", entity.ETrigger),
                    new SqlParameter("@Ramp", entity.Ramp),
                    new SqlParameter("@ExhaleTime", entity.ExhaleTime),
                    new SqlParameter("@IPAPMax", entity.IPAPMax),
                    new SqlParameter("@EPAPMin", entity.EPAPMin),
                    new SqlParameter("@PSMax", entity.PSMax),
                    new SqlParameter("@PSMin", entity.PSMin),
                    new SqlParameter("@CPAP", entity.CPAP),
                    new SqlParameter("@CFlex", entity.CFlex),
                    new SqlParameter("@CPAPStart", entity.CPAPStart),
                    new SqlParameter("@CPAPMax", entity.CPAPMax),
                    new SqlParameter("@CPAPMin", entity.CPAPMin),
                    new SqlParameter("@Alert", entity.Alert),
                    new SqlParameter("@Alert_Tube", entity.Alert_Tube),
                    new SqlParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SqlParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SqlParameter("@Alert_HRate", entity.Alert_HRate),
                    new SqlParameter("@Alert_LRate", entity.Alert_LRate),
                    new SqlParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SqlParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SqlParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SqlParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SqlParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SqlParameter("@Config_DataStore", entity.Config_DataStore),
                    new SqlParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SqlParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SqlParameter("@Config_Language", entity.Config_Language),
                    new SqlParameter("@Config_Backlight", entity.Config_Backlight),
                    new SqlParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SqlParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SqlParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SqlParameter("@Config_Reserve2", entity.Config_Reserve2),
                    new SqlParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新对象，使用显示事物
        /// </summary>
        /// <param name="transaction">事物对象</param>
        /// <param name="entity">一个实体对象</param>
        public virtual void Update(SqlTransaction transaction, ProductWorkingSummaryData entity)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entity != null)
            {
                SqlHelper.ExecuteNonQuery(transaction, System.Data.CommandType.Text, updateById,
                       new SqlParameter("@ProductId", entity.ProductId),
                    new SqlParameter("@FileName", entity.FileName),
                    new SqlParameter("@StartTime", entity.StartTime),
                    new SqlParameter("@EndTime", entity.EndTime),
                    new SqlParameter("@ProductVersion", entity.ProductVersion),
                    new SqlParameter("@ProductModel", entity.ProductModel),
                    new SqlParameter("@WorkingTime", entity.WorkingTime),
                    new SqlParameter("@CurrentTime", entity.CurrentTime),
                    new SqlParameter("@TherapyMode", entity.TherapyMode),
                    new SqlParameter("@IPAP", entity.IPAP),
                    new SqlParameter("@EPAP", entity.EPAP),
                    new SqlParameter("@RiseTime", entity.RiseTime),
                    new SqlParameter("@RespiratoryRate", entity.RespiratoryRate),
                    new SqlParameter("@InspireTime", entity.InspireTime),
                    new SqlParameter("@ITrigger", entity.ITrigger),
                    new SqlParameter("@ETrigger", entity.ETrigger),
                    new SqlParameter("@Ramp", entity.Ramp),
                    new SqlParameter("@ExhaleTime", entity.ExhaleTime),
                    new SqlParameter("@IPAPMax", entity.IPAPMax),
                    new SqlParameter("@EPAPMin", entity.EPAPMin),
                    new SqlParameter("@PSMax", entity.PSMax),
                    new SqlParameter("@PSMin", entity.PSMin),
                    new SqlParameter("@CPAP", entity.CPAP),
                    new SqlParameter("@CFlex", entity.CFlex),
                    new SqlParameter("@CPAPStart", entity.CPAPStart),
                    new SqlParameter("@CPAPMax", entity.CPAPMax),
                    new SqlParameter("@CPAPMin", entity.CPAPMin),
                    new SqlParameter("@Alert", entity.Alert),
                    new SqlParameter("@Alert_Tube", entity.Alert_Tube),
                    new SqlParameter("@Alert_Apnea", entity.Alert_Apnea),
                    new SqlParameter("@Alert_MinuteVentilation", entity.Alert_MinuteVentilation),
                    new SqlParameter("@Alert_HRate", entity.Alert_HRate),
                    new SqlParameter("@Alert_LRate", entity.Alert_LRate),
                    new SqlParameter("@Alert_Reserve1", entity.Alert_Reserve1),
                    new SqlParameter("@Alert_Reserve2", entity.Alert_Reserve2),
                    new SqlParameter("@Alert_Reserve3", entity.Alert_Reserve3),
                    new SqlParameter("@Alert_Reserve4", entity.Alert_Reserve4),
                    new SqlParameter("@Config_HumidifierLevel", entity.Config_HumidifierLevel),
                    new SqlParameter("@Config_DataStore", entity.Config_DataStore),
                    new SqlParameter("@Config_SmartStart", entity.Config_SmartStart),
                    new SqlParameter("@Config_PressureUnit", entity.Config_PressureUnit),
                    new SqlParameter("@Config_Language", entity.Config_Language),
                    new SqlParameter("@Config_Backlight", entity.Config_Backlight),
                    new SqlParameter("@Config_MaskPressure", entity.Config_MaskPressure),
                    new SqlParameter("@Config_ClinicalSet", entity.Config_ClinicalSet),
                    new SqlParameter("@Config_Reserve1", entity.Config_Reserve1),
                    new SqlParameter("@Config_Reserve2", entity.Config_Reserve2),
                    new SqlParameter("@Id", entity.Id)
                    );
            }
        }

        /// <summary>
        /// 更新实体对象集合，内部采用事物整体提交
        /// </summary>
        /// <param name="entitys">将要编辑的实体对象集合</param>
        public virtual void Update(ICollection<ProductWorkingSummaryData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SqlTransaction tran = sqlConnection.BeginTransaction();
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
        public virtual void Update(SqlTransaction transaction, ICollection<ProductWorkingSummaryData> entitys)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            if (entitys.Any())
            {
                SqlTransaction tran = transaction;
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
            ProductWorkingSummaryData result = null;
            if (id != Guid.Empty)
            {
                using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectById,
                      new SqlParameter("@Id", id)
                      ))
                {
                    if (reader.HasRows)
                    {
                        result = new ProductWorkingSummaryData();
                    }
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
            return null;
        }

        /// <summary>
        /// 分页查询,使用Id desc排序
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public virtual ICollection<ProductWorkingSummaryData> SelectPaging(int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = this.Count();
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingSummaryData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectPaging,
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
                }
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
                    result.PSMin = reader.GetFloat(22);
                    result.CPAP = reader.GetFloat(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetFloat(25);
                    result.CPAPMax = reader.GetFloat(26);
                    result.CPAPMin = reader.GetFloat(27);
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
        public virtual ICollection<ProductWorkingSummaryData> SelectByProductIdTherapyModeDataTime(Guid productId, int therapyMode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, out int recordCount)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            recordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnection, CommandType.Text, selectByProductIdTherapyModeDataTimeCount,
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@TherapyMode", therapyMode),
                new SqlParameter("@StartTime", startTime),
                new SqlParameter("@EndTime", endTime)
                 ));
            int offsetCount = (pageIndex - 1) * pageSize;
            ICollection<ProductWorkingSummaryData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByProductIdTherapyModeDataTime,
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@TherapyMode", therapyMode),
                new SqlParameter("@StartTime", startTime),
                new SqlParameter("@EndTime", endTime),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@OffsetCount", offsetCount)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
                }
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
                    result.PSMin = reader.GetFloat(22);
                    result.CPAP = reader.GetFloat(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetFloat(25);
                    result.CPAPMax = reader.GetFloat(26);
                    result.CPAPMin = reader.GetFloat(27);
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
        public virtual ICollection<ProductWorkingSummaryData> SelectByProductIdFileName(Guid productId, string fileName)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ProductWorkingSummaryData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByProductIdFileName,
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@FileName", fileName)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
                }
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
                    result.PSMin = reader.GetFloat(22);
                    result.CPAP = reader.GetFloat(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetFloat(25);
                    result.CPAPMax = reader.GetFloat(26);
                    result.CPAPMin = reader.GetFloat(27);
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
        /// 查询
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns></returns>
        public virtual ICollection<ProductWorkingSummaryData> SelectByProductId(Guid productId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }

            ICollection<ProductWorkingSummaryData> resultList = null;
            using (var reader = SqlHelper.ExecuteReader(sqlConnection, System.Data.CommandType.Text, selectByProductId,
                new SqlParameter("@ProductId", productId)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ProductWorkingSummaryData>();
                }
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
                    result.PSMin = reader.GetFloat(22);
                    result.CPAP = reader.GetFloat(23);
                    result.CFlex = reader.GetInt32(24);
                    result.CPAPStart = reader.GetFloat(25);
                    result.CPAPMax = reader.GetFloat(26);
                    result.CPAPMin = reader.GetFloat(27);
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
            if (!Equals(sqlConnection, null))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }

        #endregion
    }
}