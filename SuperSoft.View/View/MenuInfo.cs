using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.View.View
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public enum MenuName
    {
        Unknown = 0,//枚举默认值为第一个从0开始

        PatientAddMenu,
        PatientListMenu,

        RespiratoryEventAnalysisMenu,
        SnoreAnalysisMenu,

        PreviousEventsMenu,
        NextEventsMenu,

        GraphZoomInMenu,

        GraphZoomOutMenu,

        ChannelSettingsMenu,
        AutoAnalysisSettingsMenu,

        //PatientHomeView,
        //AutoAnalysisSettingsView,
        //ChannelSettingsView,

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
        SwitchLanguageMenu,
        //HelpView
    }

    /// <summary>
    /// 视图信息
    /// </summary>
    public class MenuInfo
    {
        public MenuInfo(MenuName menuName = MenuName.Unknown, bool isEnabled = true, object parameter = null)
        {
            MenuName = menuName;
            IsEnabled = isEnabled;
            Parameter = parameter;
        }
        public MenuName MenuName { get; set; }

        public bool IsEnabled { get; set; }

        public object Parameter { get; set; }

    }
}
