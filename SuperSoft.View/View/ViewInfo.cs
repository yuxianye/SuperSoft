using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.View.View
{
    /// <summary>
    /// 视图名称
    /// </summary>
    public enum ViewName
    {
        Unknown = 0,//枚举默认值为第一个从0开始
        PatientAddView,
        PatientEditView,
        PatientListView,

        PatientHomeView,
        AutoAnalysisSettingsView,
        ChannelSettingsView,
        AddAdviseView,

        //DoctorListView,
        //DoctorAddView,
        //DoctorEditView,
        //DoctorDeleteView,

        //PatientListView,
        //PatientAddView,
        //PatientEditView,
        //PatientDeleteView,


        //PatientStatisticsView,
        //PatientSummaryView,
        //PatientDetailedView,
        //PatientProductConfigView,
        //PatientStatisticsReportView,

        //DownloadView,
        //SettingsView,
        SystemParameterSettingView,
        SwitchLanguageView,
        HelpView
    }

    /// <summary>
    /// 视图类型
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// 普通的页面
        /// </summary>
        View,

        /// <summary>
        /// 模式对话框弹出窗体
        /// </summary>
        Popup,

        /// <summary>
        /// 单独的窗体
        /// </summary>
        SingleWindow
    }

    /// <summary>
    /// 视图信息
    /// </summary>
    public class ViewInfo
    {
        public ViewInfo(ViewName viewName = ViewName.Unknown, ViewType viewType = ViewType.View, object parameter = null)
        {
            ViewName = viewName;
            ViewType = viewType;
            Parameter = parameter;
        }
        public ViewName ViewName { get; set; }

        public ViewType ViewType { get; set; }

        public object Parameter { get; set; }
    }
}
