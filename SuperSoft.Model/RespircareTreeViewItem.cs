using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public struct RespircareTreeViewItem
    {
        public RespircareTreeViewItem(string displayName, DateTime startTime, DateTime endTime, string icon, double useTotalMillisecond)
        {
            DisplayName = displayName;
            StartTime = startTime;
            EndTime = endTime;
            Icon = icon;
            UseTotalMillisecond = useTotalMillisecond;
            Children = new Collection<RespircareTreeViewItem>();
        }

        public string DisplayName;

        public DateTime StartTime;

        public DateTime EndTime;

        public string Icon;

        /// <summary>
        /// 当天使用的毫秒数
        /// </summary>
        public double UseTotalMillisecond;

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RespircareTreeViewItem> Children;

    }
}
