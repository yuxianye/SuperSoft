using SuperSoft.Model;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SuperSoft.DAL
{
    /// <summary>
    /// ViewProductWorkingStatisticsDataDAL数据访问层
    /// 只能查询数据
    /// </summary>
    public class ViewProductWorkingStatisticsDataDAL : Utility.MyClassBase
    {
        /// <summary>
        /// 构造函数，使用内部新建的数据库链接sQLiteConnection
        /// </summary>
        public ViewProductWorkingStatisticsDataDAL()
        {
            sQLiteConnection = new System.Data.SQLite.SQLiteConnection(Const.SQLiteConnectionString);
            sQLiteConnection.Open();
        }

        /// <summary>
        /// 链接对象
        /// </summary>
        private System.Data.SQLite.SQLiteConnection sQLiteConnection;

        #region 数据库操作字符串SQL语句
        //44个字段
        private const string selectByPatientIdTherapyModeDataTime = @"SELECT Id,PatientId,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ViewProductWorkingStatisticsDatas WHERE PatientId=@PatientId AND TherapyMode=@TherapyMode AND DataTime>=@StartTime AND DataTime<@EndTime";

        private const string selectByPatientIdTherapyMode = @"SELECT Id,PatientId,ProductId,TherapyMode,DataTime,TotalUsage,CountAHI,CountAI,CountHI,CountSnore,CountPassive,PressureMax,PressureP95,PressureMedian,
FlowMax,FlowP95,FlowMedian,LeakMax,LeakP95,LeakMedian,TidalVolumeMax,TidalVolumeP95,TidalVolumeMedian,MinuteVentilationMax,MinuteVentilationP95,MinuteVentilationMedian,
SpO2Max,SpO2P95,SpO2Median,PulseRateMax,PulseRateP95,PulseRateMedian,RespiratoryRateMax,RespiratoryRateP95,RespiratoryRateMedian,IERatioMax,IERatioP95,IERatioMedian,
IPAPMax,IPAPP95,IPAPMedian,EPAPMax,EPAPP95,EPAPMedian 
FROM ViewProductWorkingStatisticsDatas WHERE PatientId=@PatientId AND TherapyMode=@TherapyMode";

        private const string selectTherapyModeByPatientId = @"SELECT TherapyMode FROM ViewProductWorkingStatisticsDatas WHERE PatientId=@PatientId GROUP BY (TherapyMode) ORDER BY TherapyMode";

        #endregion

        #region GetByCondition


        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <param name="startTime">startTime</param>
        /// <param name="endTime">endTime</param>
        /// <returns></returns>
        public virtual ICollection<ViewProductWorkingStatisticsData> SelectByPatientIdTherapyModeDataTime(Guid patientId, int therapyMode, DateTime startTime, DateTime endTime)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<ViewProductWorkingStatisticsData> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByPatientIdTherapyModeDataTime,
                new SQLiteParameter("@PatientId", patientId),
                new SQLiteParameter("@TherapyMode", therapyMode),
                new SQLiteParameter("@StartTime", startTime),
                new SQLiteParameter("@EndTime", endTime)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ViewProductWorkingStatisticsData>();
                }
                while (reader.Read())
                {
                    ViewProductWorkingStatisticsData result = new ViewProductWorkingStatisticsData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.ProductId = reader.GetGuid(2);
                    result.TherapyMode = reader.GetInt32(3);
                    result.DataTime = reader.GetDateTime(4);
                    result.TotalUsage = reader.GetInt64(5);
                    result.CountAHI = reader.GetInt32(6);
                    result.CountAI = reader.GetInt32(7);
                    result.CountHI = reader.GetInt32(8);
                    result.CountSnore = reader.GetInt32(9);
                    result.CountPassive = reader.GetInt32(10);
                    result.PressureMax = reader.GetFloat(11);
                    result.PressureP95 = reader.GetFloat(12);
                    result.PressureMedian = reader.GetFloat(13);
                    result.FlowMax = reader.GetFloat(14);
                    result.FlowP95 = reader.GetFloat(15);
                    result.FlowMedian = reader.GetFloat(16);
                    result.LeakMax = reader.GetFloat(17);
                    result.LeakP95 = reader.GetFloat(18);
                    result.LeakMedian = reader.GetFloat(19);
                    result.TidalVolumeMax = reader.GetFloat(20);
                    result.TidalVolumeP95 = reader.GetFloat(21);
                    result.TidalVolumeMedian = reader.GetFloat(22);
                    result.MinuteVentilationMax = reader.GetInt32(23);
                    result.MinuteVentilationP95 = reader.GetInt32(24);
                    result.MinuteVentilationMedian = reader.GetInt32(25);
                    result.SpO2Max = reader.GetInt32(26);
                    result.SpO2P95 = reader.GetInt32(27);
                    result.SpO2Median = reader.GetInt32(28);
                    result.PulseRateMax = reader.GetInt32(29);
                    result.PulseRateP95 = reader.GetInt32(30);
                    result.PulseRateMedian = reader.GetInt32(31);
                    result.RespiratoryRateMax = reader.GetInt32(32);
                    result.RespiratoryRateP95 = reader.GetInt32(33);
                    result.RespiratoryRateMedian = reader.GetInt32(34);
                    result.IERatioMax = reader.GetFloat(35);
                    result.IERatioP95 = reader.GetFloat(36);
                    result.IERatioMedian = reader.GetFloat(37);
                    result.IPAPMax = reader.GetFloat(38);
                    result.IPAPP95 = reader.GetFloat(39);
                    result.IPAPMedian = reader.GetFloat(40);
                    result.EPAPMax = reader.GetFloat(41);
                    result.EPAPP95 = reader.GetFloat(42);
                    result.EPAPMedian = reader.GetFloat(43);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询,使用Id DESC排序
        /// </summary>
        /// <param name="patientId">patientId</param>
        /// <param name="therapyMode">therapyMode</param>
        /// <returns></returns>
        public virtual ICollection<ViewProductWorkingStatisticsData> SelectByPatientIdTherapyMode(Guid patientId, int therapyMode)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<ViewProductWorkingStatisticsData> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectByPatientIdTherapyMode,
                new SQLiteParameter("@PatientId", patientId),
                new SQLiteParameter("@TherapyMode", therapyMode)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new System.Collections.ObjectModel.Collection<ViewProductWorkingStatisticsData>();
                }
                while (reader.Read())
                {
                    ViewProductWorkingStatisticsData result = new ViewProductWorkingStatisticsData();
                    result.Id = reader.GetGuid(0);
                    result.ProductId = reader.GetGuid(1);
                    result.ProductId = reader.GetGuid(2);
                    result.TherapyMode = reader.GetInt32(3);
                    result.DataTime = reader.GetDateTime(4);
                    result.TotalUsage = reader.GetInt64(5);
                    result.CountAHI = reader.GetInt32(6);
                    result.CountAI = reader.GetInt32(7);
                    result.CountHI = reader.GetInt32(8);
                    result.CountSnore = reader.GetInt32(9);
                    result.CountPassive = reader.GetInt32(10);
                    result.PressureMax = reader.GetFloat(11);
                    result.PressureP95 = reader.GetFloat(12);
                    result.PressureMedian = reader.GetFloat(13);
                    result.FlowMax = reader.GetFloat(14);
                    result.FlowP95 = reader.GetFloat(15);
                    result.FlowMedian = reader.GetFloat(16);
                    result.LeakMax = reader.GetFloat(17);
                    result.LeakP95 = reader.GetFloat(18);
                    result.LeakMedian = reader.GetFloat(19);
                    result.TidalVolumeMax = reader.GetFloat(20);
                    result.TidalVolumeP95 = reader.GetFloat(21);
                    result.TidalVolumeMedian = reader.GetFloat(22);
                    result.MinuteVentilationMax = reader.GetInt32(23);
                    result.MinuteVentilationP95 = reader.GetInt32(24);
                    result.MinuteVentilationMedian = reader.GetInt32(25);
                    result.SpO2Max = reader.GetInt32(26);
                    result.SpO2P95 = reader.GetInt32(27);
                    result.SpO2Median = reader.GetInt32(28);
                    result.PulseRateMax = reader.GetInt32(29);
                    result.PulseRateP95 = reader.GetInt32(30);
                    result.PulseRateMedian = reader.GetInt32(31);
                    result.RespiratoryRateMax = reader.GetInt32(32);
                    result.RespiratoryRateP95 = reader.GetInt32(33);
                    result.RespiratoryRateMedian = reader.GetInt32(34);
                    result.IERatioMax = reader.GetFloat(35);
                    result.IERatioP95 = reader.GetFloat(36);
                    result.IERatioMedian = reader.GetFloat(37);
                    result.IPAPMax = reader.GetFloat(38);
                    result.IPAPP95 = reader.GetFloat(39);
                    result.IPAPMedian = reader.GetFloat(40);
                    result.EPAPMax = reader.GetFloat(41);
                    result.EPAPP95 = reader.GetFloat(42);
                    result.EPAPMedian = reader.GetFloat(43);
                    resultList.Add(result);
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// 查询治疗模式,使用TherapyMode ASC
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public virtual ICollection<KeyValuePair<TherapyMode, string>> SelectTherapyModeByPatientId(Guid patientId)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString());
            }
            ICollection<KeyValuePair<TherapyMode, string>> resultList = null;
            using (var reader = SQLiteHelper.ExecuteReader(sQLiteConnection, System.Data.CommandType.Text, selectTherapyModeByPatientId,
                new SQLiteParameter("@PatientId", patientId)
                ))
            {
                if (reader.HasRows)
                {
                    resultList = new Collection<KeyValuePair<TherapyMode, string>>();
                }
                while (reader.Read())
                {
                    ViewProductWorkingStatisticsData result = new ViewProductWorkingStatisticsData();
                    var tm = (TherapyMode)reader.GetInt32(0);
                    resultList.Add(new KeyValuePair<TherapyMode, string>(tm, tm.ToDescription()));
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