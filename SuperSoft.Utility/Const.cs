using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Utility
{
    /// <summary>
    /// 常量
    /// </summary>
    public static class Const
    {
        #region

        public const string SQLiteConnectionString = @"Data Source=Database\SuperSoft.db";

        #endregion

        #region 常用路径字符串

        /// <summary>
        /// AppPath(包含末尾的斜线)，应用程序的exe路径
        /// </summary>
        public static readonly string AppPath = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf('\\') + 1);

        /// <summary>
        /// AppDatabasePath(包含末尾的斜线)，应用程序数据库的路径，AppPath + "Database\"
        /// </summary>
        public static readonly string AppDatabasePath = AppPath + @"Database\";

        #endregion 

        #region 数值格式化字符串

        /// <summary>
        /// 数值格式化字符串，精确到0位小数
        /// </summary>
        public const string NumericFormatString0 = @"{0:N0}";

        /// <summary>
        /// 数值格式化字符串，精确到1位小数
        /// </summary>
        public const string NumericFormatString1 = @"{0:N1}";

        /// <summary>
        /// 数值格式化字符串，精确到2位小数
        /// </summary>
        public const string NumericFormatString2 = @"{0:N2}";

        #endregion

        #region 时间常数值

        /// <summary>
        /// 24小时的毫秒数24*60*60*1000
        /// </summary>
        public const int MilliSecFor24Hour = 86400000;

        /// <summary>
        /// 12小时的毫秒数12*60*60*1000
        /// </summary>
        public const int MilliSecFor12Hour = 43200000;

        /// <summary>
        /// 10小时的毫秒数10*60*60*1000
        /// </summary>
        public const int MilliSecFor10Hour = 36000000;

        /// <summary>
        /// 8小时的毫秒数8*60*60*1000
        /// </summary>
        public const int MilliSecFor8Hour = 28800000;

        /// <summary>
        /// 5小时的毫秒数5*60*60*1000
        /// </summary>
        public const int MilliSecFor5Hour = 18000000;

        /// <summary>
        /// 2小时的毫秒数2*60*60*1000
        /// </summary>
        public const int MilliSecFor2Hour = 7200000;

        /// <summary>
        /// 1小时的毫秒数60*60*1000
        /// </summary>
        public const int MilliSecForOneHour = 3600000;

        /// <summary>
        /// 0.5小时的毫秒数30*60*1000
        /// </summary>
        public const int MilliSecForHalfHour = 1800000;

        /// <summary>
        /// 20分钟的毫秒数20*60*1000
        /// </summary>
        public const int MilliSecFor20Minutes = 1200000;

        /// <summary>
        /// 10分钟的毫秒数10*60*1000
        /// </summary>
        public const int MilliSecFor10Minutes = 600000;

        /// <summary>
        /// 5分钟的毫秒数5*60*1000
        /// </summary>
        public const int MilliSecFor5Minutes = 300000;

        /// <summary>
        /// 2分钟的毫秒数2*60*1000
        /// </summary>
        public const int MilliSecFor2Minutes = 120000;

        /// <summary>
        /// 1分钟的毫秒数60*1000
        /// </summary>
        public const int MilliSecForOneMinutes = 60000;

        /// <summary>
        /// 30秒的的毫秒数30*1000
        /// </summary>
        public const int MilliSecForHalfMinutes = 30000;

        /// <summary>
        /// 20秒的毫秒数20*1000
        /// </summary>
        public const int MilliSecFor20Sec = 20000;

        /// <summary>
        /// 10秒的毫秒数10*1000
        /// </summary>
        public const int MilliSecFor10Sec = 10000;

        /// <summary>
        /// 5秒的毫秒数5*1000
        /// </summary>
        public const int MilliSecFor5Sec = 5000;

        /// <summary>
        /// 2秒的毫秒数2*1000
        /// </summary>
        public const int MilliSecFor2Sec = 2000;

        /// <summary>
        /// 1秒的毫秒数0*1000
        /// </summary>
        public const int MilliSecForOneSec = 1000;

        #endregion

        #region 文件扩展名字符串

        /// <summary>
        /// SD卡内开机文件的名称 SY_RMS.TXT
        /// </summary>
        public const string RMSFileName = "SY_RMS.TXT";

        /// <summary>
        /// 数据文件过滤 ??????.DAT
        /// </summary>
        public const string DatFileFilter = "??????.DAT";

        ///// <summary>
        ///// *.pat
        ///// </summary>
        //public const string RMSFileExtensionPatientSearch = @"*.pat";

        ///// <summary>
        ///// 患者文件扩展名
        ///// </summary>
        //public const string RMSFileExtensionPatient = @".pat";

        /// <summary>
        /// 数据文件扩展名 .DAT
        /// </summary>
        public const string RMSFileExtensionData = @".DAT";

        ///// <summary>
        ///// 数据文件序列化之后的文件扩展名
        ///// </summary>
        //public const string RMSFileExtensionDataSeializer = @".dat.ser";


        //public const string RMSFileExtensionDataSeializer2 = @".ser";

        #endregion

        #region 日期时间格式化字符串

        /// <summary>
        /// HH:mm:ss
        /// </summary>
        public const string DeteHHmmss = @"HH:mm:ss";

        /// <summary>
        /// HH:mm
        /// </summary>
        public const string DeteHHmm = @"HH:mm";

        #endregion


    }
}
