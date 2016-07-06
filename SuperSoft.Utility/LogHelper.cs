using System;
using System.Diagnostics;

namespace SuperSoft.Utility
{
    /// <summary>
    /// 日志,封装了System.Diagnostics.Trace;
    /// 具体见配置文件
    /// </summary>
    public class LogHelper
    {
        private static TraceSwitch traceSwitch = new TraceSwitch("MySwitch", string.Empty, "off");//开关

        /// <summary>
        /// 信息
        /// </summary>
        public static void Info(string message)
        {
            if (traceSwitch.TraceInfo)
                Trace.TraceInformation(message);
        }

        /// <summary>
        /// 警告
        /// </summary>
        public static void Warn(string message)
        {
            if (traceSwitch.TraceWarning)
                Trace.TraceWarning(message);
        }

        /// <summary>
        /// 错误
        /// </summary>
        public static void Error(Exception ex)
        {
            if (traceSwitch.TraceError)
                Trace.TraceError("{0} {1}", ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// 错误
        /// </summary>
        public static void Error(string message, Exception ex)
        {
            if (traceSwitch.TraceError)
                Trace.TraceError("{0} {1} {2}", message, ex.Message, ex.StackTrace);
        }
    }
}
