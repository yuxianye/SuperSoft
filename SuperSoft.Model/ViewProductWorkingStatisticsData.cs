using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public partial class ViewProductWorkingStatisticsData : EntityBase<Guid>
    {
        public Nullable<System.Guid> PatientId { get; set; }
        //public System.Guid Id { get; set; }
        public Nullable<System.Guid> ProductId { get; set; }
        public int TherapyMode { get; set; }
        public System.DateTime DataTime { get; set; }
        public Nullable<long> TotalUsage { get; set; }
        public Nullable<int> CountAHI { get; set; }
        public Nullable<int> CountAI { get; set; }
        public Nullable<int> CountHI { get; set; }
        public Nullable<int> CountSnore { get; set; }
        public Nullable<int> CountPassive { get; set; }
        public Nullable<float> PressureMax { get; set; }
        public Nullable<float> PressureP95 { get; set; }
        public Nullable<float> PressureMedian { get; set; }
        public Nullable<float> FlowMax { get; set; }
        public Nullable<float> FlowP95 { get; set; }
        public Nullable<float> FlowMedian { get; set; }
        public Nullable<float> LeakMax { get; set; }
        public Nullable<float> LeakP95 { get; set; }
        public Nullable<float> LeakMedian { get; set; }
        public Nullable<float> TidalVolumeMax { get; set; }
        public Nullable<float> TidalVolumeP95 { get; set; }
        public Nullable<float> TidalVolumeMedian { get; set; }
        public Nullable<int> MinuteVentilationMax { get; set; }
        public Nullable<int> MinuteVentilationP95 { get; set; }
        public Nullable<int> MinuteVentilationMedian { get; set; }
        public Nullable<int> SpO2Max { get; set; }
        public Nullable<int> SpO2P95 { get; set; }
        public Nullable<int> SpO2Median { get; set; }
        public Nullable<int> PulseRateMax { get; set; }
        public Nullable<int> PulseRateP95 { get; set; }
        public Nullable<int> PulseRateMedian { get; set; }
        public Nullable<int> RespiratoryRateMax { get; set; }
        public Nullable<int> RespiratoryRateP95 { get; set; }
        public Nullable<int> RespiratoryRateMedian { get; set; }
        public Nullable<float> IERatioMax { get; set; }
        public Nullable<float> IERatioP95 { get; set; }
        public Nullable<float> IERatioMedian { get; set; }
        public Nullable<float> IPAPMax { get; set; }
        public Nullable<float> IPAPP95 { get; set; }
        public Nullable<float> IPAPMedian { get; set; }
        public Nullable<float> EPAPMax { get; set; }
        public Nullable<float> EPAPP95 { get; set; }
        public Nullable<float> EPAPMedian { get; set; }
    }
}
