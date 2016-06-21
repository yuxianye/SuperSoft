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

        #region combobox数据源

        private static Dictionary<int, string> dictionaryList = new Dictionary<int, string>();

        private static object lockObj = new object();
        /// <summary>
        /// 取得时间列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetTimeList()
        {
            if (dictionaryList.Count < 1)
            {
                lock (lockObj)
                {
                    dictionaryList.Add(Const.MilliSecFor24Hour, ResourceHelper.LoadString(@"MilliSecFor24Hour"));
                    dictionaryList.Add(Const.MilliSecFor12Hour, ResourceHelper.LoadString(@"MilliSecFor12Hour"));
                    dictionaryList.Add(Const.MilliSecFor10Hour, ResourceHelper.LoadString(@"MilliSecFor10Hour"));
                    dictionaryList.Add(Const.MilliSecFor8Hour, ResourceHelper.LoadString(@"MilliSecFor8Hour"));
                    dictionaryList.Add(Const.MilliSecFor5Hour, ResourceHelper.LoadString(@"MilliSecFor5Hour"));
                    dictionaryList.Add(Const.MilliSecFor2Hour, ResourceHelper.LoadString(@"MilliSecFor2Hour"));
                    dictionaryList.Add(Const.MilliSecForOneHour, ResourceHelper.LoadString(@"MilliSecForOneHour"));
                    dictionaryList.Add(Const.MilliSecForHalfHour, ResourceHelper.LoadString(@"MilliSecForHalfHour"));
                    dictionaryList.Add(Const.MilliSecFor10Minutes, ResourceHelper.LoadString(@"MilliSecFor10Minutes"));
                    dictionaryList.Add(Const.MilliSecFor5Minutes, ResourceHelper.LoadString(@"MilliSecFor5Minutes"));
                    dictionaryList.Add(Const.MilliSecForOneMinutes, ResourceHelper.LoadString(@"MilliSecForOneMinutes"));
                    dictionaryList.Add(Const.MilliSecForHalfMinutes, ResourceHelper.LoadString(@"MilliSecForHalfMinutes"));
                    dictionaryList.Add(Const.MilliSecFor10Sec, ResourceHelper.LoadString(@"MilliSecFor10Sec"));
                }
            }

            return dictionaryList;
        }

        #endregion


        //public static Model.DataStruct DataStruct;
    }
}
