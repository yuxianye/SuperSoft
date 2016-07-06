using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.View
{
    public class StaticDatas
    {
        /// <summary>
        /// 当前选中的患者
        /// </summary>
        public static Patient CurrentOpenedPatient;

        public static Patient CurrentSelectedPatient;

        private static object lockObj = new object();

        #region 时间选择数据源

        private static Dictionary<int, string> dictionaryListTime = new Dictionary<int, string>();

        /// <summary>
        /// 取得时间列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetTimeList()
        {
            if (dictionaryListTime.Count < 1)
            {
                lock (lockObj)
                {
                    dictionaryListTime.Add(Const.MilliSecFor24Hour, ResourceHelper.LoadString(@"MilliSecFor24Hour"));
                    dictionaryListTime.Add(Const.MilliSecFor12Hour, ResourceHelper.LoadString(@"MilliSecFor12Hour"));
                    dictionaryListTime.Add(Const.MilliSecFor10Hour, ResourceHelper.LoadString(@"MilliSecFor10Hour"));
                    dictionaryListTime.Add(Const.MilliSecFor8Hour, ResourceHelper.LoadString(@"MilliSecFor8Hour"));
                    dictionaryListTime.Add(Const.MilliSecFor5Hour, ResourceHelper.LoadString(@"MilliSecFor5Hour"));
                    dictionaryListTime.Add(Const.MilliSecFor2Hour, ResourceHelper.LoadString(@"MilliSecFor2Hour"));
                    dictionaryListTime.Add(Const.MilliSecForOneHour, ResourceHelper.LoadString(@"MilliSecForOneHour"));
                    dictionaryListTime.Add(Const.MilliSecForHalfHour, ResourceHelper.LoadString(@"MilliSecForHalfHour"));
                    dictionaryListTime.Add(Const.MilliSecFor10Minutes, ResourceHelper.LoadString(@"MilliSecFor10Minutes"));
                    dictionaryListTime.Add(Const.MilliSecFor5Minutes, ResourceHelper.LoadString(@"MilliSecFor5Minutes"));
                    dictionaryListTime.Add(Const.MilliSecForOneMinutes, ResourceHelper.LoadString(@"MilliSecForOneMinutes"));
                    dictionaryListTime.Add(Const.MilliSecForHalfMinutes, ResourceHelper.LoadString(@"MilliSecForHalfMinutes"));
                    dictionaryListTime.Add(Const.MilliSecFor10Sec, ResourceHelper.LoadString(@"MilliSecFor10Sec"));
                }
            }
            return dictionaryListTime;
        }

        #endregion

        #region 语言列表数据源

        private static Dictionary<string, string> dictionaryListLanguage = new Dictionary<string, string>();

        /// <summary>
        /// 取得时间列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetLanguageList()
        {
            if (dictionaryListLanguage.Count < 1)
            {
                lock (lockObj)
                {
                    dictionaryListLanguage.Add(@"zh-CN", @"中文");
                    dictionaryListLanguage.Add(@"en-US", "English");
                }
            }
            return dictionaryListLanguage;
        }

        #endregion

        //public static Model.DataStruct DataStruct;
    }
}
