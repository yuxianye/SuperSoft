using System;

namespace SuperSoft.Utility
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        private static readonly string logFileName = @"log\log.log";

        /// <summary>
        /// log类型、记录时间、信息、
        /// </summary>
        private static readonly string logFormat = "{0}\t{1}\t{2}\r\n";

        /// <summary>
        /// log类型、记录时间、信息、异常
        /// </summary>
        private static readonly string logFormat2 = "{0}\t{1}\t{2}\r\n{3}\r\n";

        /// <summary>
        /// 
        /// </summary>
        public static void Debug(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.Print(message);
            AppendAllText(string.Format(logFormat, @"Debug", DateTime.Now.ToString(), message));
#else

            var debug = Utility.ConfigHelper.GetAppSetting(@"DebugLog");
            if (debug.GetBool(false))
            {
                AppendAllText(string.Format(logFormat, @"Debug", DateTime.Now.ToString(), message));
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Info(string message)
        {
            AppendAllText(string.Format(logFormat, @"Info", DateTime.Now.ToString(), message));
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Error(string message, Exception ex)
        {
            AppendAllText(string.Format(logFormat2, @"Error", DateTime.Now.ToString(), message, ex == null ? null : ex.Message + ex.StackTrace));
        }

        private static object lockObj = new object();
        private static void AppendAllText(string message)
        {
            //TODO:会带来性能问题
            lock (lockObj)
            {
                System.IO.File.AppendAllText(logFileName, message);
            }
        }
    }
}
