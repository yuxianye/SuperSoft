using System;

namespace SuperSoft.Model
{
    public partial class ViewProductWorkingStatisticsData : EntityBase<Guid>
    {
        public System.Guid PatientId { get; set; }
        public System.Guid ProductId { get; set; }
        public int TherapyMode { get; set; }
        public System.DateTime DataTime { get; set; }
        public long TotalUsage { get; set; }
        public int CountAHI { get; set; }
        public int CountAI { get; set; }
        public int CountHI { get; set; }
        public int CountSnore { get; set; }
        public int CountPassive { get; set; }
        public float PressureMax { get; set; }
        public float PressureP95 { get; set; }
        public float PressureMedian { get; set; }
        public float FlowMax { get; set; }
        public float FlowP95 { get; set; }
        public float FlowMedian { get; set; }
        public float LeakMax { get; set; }
        public float LeakP95 { get; set; }
        public float LeakMedian { get; set; }
        public float TidalVolumeMax { get; set; }
        public float TidalVolumeP95 { get; set; }
        public float TidalVolumeMedian { get; set; }
        public int MinuteVentilationMax { get; set; }
        public int MinuteVentilationP95 { get; set; }
        public int MinuteVentilationMedian { get; set; }
        public int SpO2Max { get; set; }
        public int SpO2P95 { get; set; }
        public int SpO2Median { get; set; }
        public int PulseRateMax { get; set; }
        public int PulseRateP95 { get; set; }
        public int PulseRateMedian { get; set; }
        public int RespiratoryRateMax { get; set; }
        public int RespiratoryRateP95 { get; set; }
        public int RespiratoryRateMedian { get; set; }
        public float IERatioMax { get; set; }
        public float IERatioP95 { get; set; }
        public float IERatioMedian { get; set; }
        public float IPAPMax { get; set; }
        public float IPAPP95 { get; set; }
        public float IPAPMedian { get; set; }
        public float EPAPMax { get; set; }
        public float EPAPP95 { get; set; }
        public float EPAPMedian { get; set; }
    }
}
