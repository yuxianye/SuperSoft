using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public enum MessengerToken
    {
        /// <summary>
        /// 视图跳转
        /// </summary>
        Navigate,

        /// <summary>
        /// 视图跳转切换效果
        /// </summary>
        NavigateSplash,

        /// <summary>
        /// 设置菜单状态
        /// </summary>
        SetMenuStatus,

        /// <summary>
        /// 关闭Popup
        /// </summary>
        ClosePopup,

        /// <summary>
        /// 切换语言
        /// </summary>
        SwitchLanguage,

        ///// <summary>
        ///// 放大
        ///// </summary>
        //ZoomIn,

        ///// <summary>
        ///// 缩小
        ///// </summary>
        //ZoomOut,

        /// <summary>
        /// 事件分析
        /// </summary>
        EventAnalysis,

        /// <summary>
        /// 更新事件分析的图表
        /// </summary>
        UpEventAnalysisChart,

        /// <summary>
        /// 鼾声分析
        /// </summary>
        SnoreAnalysis,

        /// <summary>
        /// 更新鼾声分析的图表
        /// </summary>
        UpSnoreAnalysissChart,

        /// <summary>
        /// 更新报表的医嘱建议
        /// </summary>
        UpAdviseMsg,

        /// <summary>
        /// 上一个事件
        /// </summary>
        PreviousEvent,

        /// <summary>
        /// 下一个事件
        /// </summary>
        NextEvent,

    }
}
