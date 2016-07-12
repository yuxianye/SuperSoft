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
    /// ViewProductWorkingSummaryDetailedData数据访问层
    /// 只能查询数据
    /// </summary>
    public class ViewProductWorkingSummaryDetailedDataDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ViewProductWorkingSummaryDetailedDataDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //50个字段
        private const string selectByTherapyModeDataTime = @"SELECT Id,PatientId,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2,Content 
FROM ViewProductWorkingSummaryDetailedDatas 
WHERE TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime";

        private const string selectByPatientIdTherapyModeDataTime = @"SELECT Id,PatientId,ProductId,FileName,StartTime,EndTime,ProductVersion,ProductModel,WorkingTime,CurrentTime,
TherapyMode,IPAP,EPAP,RiseTime,RespiratoryRate,InspireTime,ITrigger,ETrigger,Ramp,ExhaleTime,IPAPMax,EPAPMin,PSMax,PSMin,CPAP,CFlex,CPAPStart,CPAPMax,CPAPMin,Alert,
Alert_Tube,Alert_Apnea,Alert_MinuteVentilation,Alert_HRate,Alert_LRate,Alert_Reserve1,Alert_Reserve2,Alert_Reserve3,Alert_Reserve4,Config_HumidifierLevel,Config_DataStore,
Config_SmartStart,Config_PressureUnit,Config_Language,Config_Backlight,Config_MaskPressure,Config_ClinicalSet,Config_Reserve1,Config_Reserve2,Content 
FROM ViewProductWorkingSummaryDetailedDatas 
WHERE PatientId=@PatientId AND TherapyMode=@TherapyMode AND StartTime>=@StartTime AND EndTime<@EndTime";

        #endregion

        #region GetByCondition
        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <returns></returns>
        public virtual ICollection<ViewProductWorkingSummaryDetailedData> SelectByTherapyModeDataTime(int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<ViewProductWorkingSummaryDetailedData> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByTherapyModeDataTime,
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ViewProductWorkingSummaryDetailedData>();
                }
                while (reader.Read())
                {
                    ViewProductWorkingSummaryDetailedData result = new ViewProductWorkingSummaryDetailedData();
                    result.Id = reader.GetGuid(0);
                    result.PatientId = reader.GetGuid(1);
                    result.ProductId = reader.GetGuid(2);
                    result.FileName = reader.GetString(3);
                    result.StartTime = reader.GetDateTime(4);
                    result.EndTime = reader.GetDateTime(5);
                    result.ProductVersion = reader.GetString(6);
                    result.ProductModel = reader.GetInt32(7);
                    result.WorkingTime = reader.GetInt32(8);
                    result.CurrentTime = reader.GetDateTime(9);
                    result.TherapyMode = reader.GetInt32(10);
                    result.IPAP = reader.GetFloat(11);
                    result.EPAP = reader.GetFloat(12);
                    result.RiseTime = reader.GetInt32(13);
                    result.RespiratoryRate = reader.GetInt32(14);
                    result.InspireTime = reader.GetInt32(15);
                    result.ITrigger = reader.GetInt32(16);
                    result.ETrigger = reader.GetInt32(17);
                    result.Ramp = reader.GetInt32(18);
                    result.ExhaleTime = reader.GetInt32(19);
                    result.IPAPMax = reader.GetFloat(20);
                    result.EPAPMin = reader.GetFloat(21);
                    result.PSMax = reader.GetFloat(22);
                    result.PSMin = reader.GetValue(23).GetInt();
                    result.CPAP = reader.GetValue(24).GetInt();
                    result.CFlex = reader.GetValue(25).GetInt();
                    result.CPAPStart = reader.GetValue(26).GetInt();
                    result.CPAPMax = reader.GetValue(27).GetInt();
                    result.CPAPMin = reader.GetValue(28).GetInt();
                    result.Alert = reader.GetValue(29).GetInt();
                    result.Alert_Tube = reader.GetValue(30).GetInt();
                    result.Alert_Apnea = reader.GetValue(31).GetInt();
                    result.Alert_MinuteVentilation = reader.GetValue(32).GetInt();
                    result.Alert_HRate = reader.GetValue(33).GetInt();
                    result.Alert_LRate = reader.GetValue(34).GetInt();
                    result.Alert_Reserve1 = reader.GetInt32(35);
                    result.Alert_Reserve2 = reader.GetInt32(36);
                    result.Alert_Reserve3 = reader.GetInt32(37);
                    result.Alert_Reserve4 = reader.GetInt32(38);
                    result.Config_HumidifierLevel = reader.GetInt32(39);
                    result.Config_DataStore = reader.GetInt32(40);
                    result.Config_SmartStart = reader.GetInt32(41);
                    result.Config_PressureUnit = reader.GetInt32(42);
                    result.Config_Language = reader.GetInt32(43);
                    result.Config_Backlight = reader.GetInt32(44);
                    result.Config_MaskPressure = reader.GetInt32(45);
                    result.Config_ClinicalSet = reader.GetInt32(46);
                    result.Config_Reserve1 = reader.GetInt32(47);
                    result.Config_Reserve2 = reader.GetInt32(48);
                    if (!reader.IsDBNull(49))
                    {
                        long length = reader.GetBytes(49, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(49, 0, blob, 0, blob.Length);
                            result.Content = blob;
                        }
                    }
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="patientId">productId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <returns></returns>
        public virtual ICollection<ViewProductWorkingSummaryDetailedData> SelectByPatientIdTherapyModeDataTime(Guid patientId, int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<ViewProductWorkingSummaryDetailedData> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByPatientIdTherapyModeDataTime,
                new SQLiteParameter("@PatientId", patientId),
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ViewProductWorkingSummaryDetailedData>();
                }
                while (reader.Read())
                {
                    ViewProductWorkingSummaryDetailedData result = new ViewProductWorkingSummaryDetailedData();
                    result.Id = reader.GetGuid(0);
                    result.PatientId = reader.GetGuid(1);
                    result.ProductId = reader.GetGuid(2);
                    result.FileName = reader.GetString(3);
                    result.StartTime = reader.GetDateTime(4);
                    result.EndTime = reader.GetDateTime(5);
                    result.ProductVersion = reader.GetString(6);
                    result.ProductModel = reader.GetInt32(7);
                    result.WorkingTime = reader.GetInt32(8);
                    result.CurrentTime = reader.GetDateTime(9);
                    result.TherapyMode = reader.GetInt32(10);
                    result.IPAP = reader.GetFloat(11);
                    result.EPAP = reader.GetFloat(12);
                    result.RiseTime = reader.GetInt32(13);
                    result.RespiratoryRate = reader.GetInt32(14);
                    result.InspireTime = reader.GetInt32(15);
                    result.ITrigger = reader.GetInt32(16);
                    result.ETrigger = reader.GetInt32(17);
                    result.Ramp = reader.GetInt32(18);
                    result.ExhaleTime = reader.GetInt32(19);
                    result.IPAPMax = reader.GetFloat(20);
                    result.EPAPMin = reader.GetFloat(21);
                    result.PSMax = reader.GetFloat(22);
                    result.PSMin = reader.GetInt32(23);
                    result.CPAP = reader.GetInt32(24);
                    result.CFlex = reader.GetInt32(25);
                    result.CPAPStart = reader.GetInt32(26);
                    result.CPAPMax = reader.GetInt32(27);
                    result.CPAPMin = reader.GetInt32(28);
                    result.Alert = reader.GetInt32(29);
                    result.Alert_Tube = reader.GetInt32(30);
                    result.Alert_Apnea = reader.GetInt32(31);
                    result.Alert_MinuteVentilation = reader.GetInt32(32);
                    result.Alert_HRate = reader.GetInt32(33);
                    result.Alert_LRate = reader.GetInt32(34);
                    result.Alert_Reserve1 = reader.GetInt32(35);
                    result.Alert_Reserve2 = reader.GetInt32(36);
                    result.Alert_Reserve3 = reader.GetInt32(37);
                    result.Alert_Reserve4 = reader.GetInt32(38);
                    result.Config_HumidifierLevel = reader.GetInt32(39);
                    result.Config_DataStore = reader.GetInt32(40);
                    result.Config_SmartStart = reader.GetInt32(41);
                    result.Config_PressureUnit = reader.GetInt32(42);
                    result.Config_Language = reader.GetInt32(43);
                    result.Config_Backlight = reader.GetInt32(44);
                    result.Config_MaskPressure = reader.GetInt32(45);
                    result.Config_ClinicalSet = reader.GetInt32(46);
                    result.Config_Reserve1 = reader.GetInt32(47);
                    result.Config_Reserve2 = reader.GetInt32(48);
                    if (!reader.IsDBNull(49))
                    {
                        long length = reader.GetBytes(49, 0, null, 0, int.MaxValue);
                        if (length > 0)
                        {
                            var blob = new Byte[length];
                            reader.GetBytes(49, 0, blob, 0, blob.Length);
                            result.Content = blob;
                        }
                    }
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