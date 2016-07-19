using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public class StatisticsInfoModel : MyNotifyClassBase
    {
        #region AHI

        private float? countAHI;

        public float? CountAHI
        {
            get { return countAHI; }
            set
            {
                if (Equals(countAHI, value)) return;
                countAHI = value;
                OnPropertyChanged("CountAHI");
            }
        }

        private float? countAI;

        public float? CountAI
        {
            get { return countAI; }
            set
            {
                if (Equals(countAI, value)) return;
                countAI = value;
                OnPropertyChanged("CountAI");
            }
        }

        private float? countHI;

        public float? CountHI
        {
            get { return countHI; }
            set
            {
                if (Equals(countHI, value)) return;
                countHI = value;
                OnPropertyChanged("CountHI");
            }
        }

        #endregion AHI

        #region Pressure

        private float? pressureMax;

        public float? PressureMax
        {
            get { return pressureMax; }
            set
            {
                if (Equals(pressureMax, value)) return;
                pressureMax = value;
                OnPropertyChanged("PressureMax");
            }
        }

        private float? pressureP95;

        public float? PressureP95
        {
            get { return pressureP95; }
            set
            {
                if (Equals(pressureP95, value)) return;
                pressureP95 = value;
                OnPropertyChanged("PressureP95");
            }
        }

        private float? pressureMedian;

        public float? PressureMedian
        {
            get { return pressureMedian; }
            set
            {
                if (Equals(pressureMedian, value)) return;
                pressureMedian = value;
                OnPropertyChanged("PressureMedian");
            }
        }

        #endregion Pressure

        #region Flow

        private float? flowMax;

        public float? FlowMax
        {
            get { return flowMax; }
            set
            {
                if (Equals(flowMax, value)) return;
                flowMax = value;
                OnPropertyChanged("FlowMax");
            }
        }

        private float? flowP95;

        public float? FlowP95
        {
            get { return flowP95; }
            set
            {
                if (Equals(flowP95, value)) return;
                flowP95 = value;
                OnPropertyChanged("FlowP95");
            }
        }

        private float? flowMedian;

        public float? FlowMedian
        {
            get { return flowMedian; }
            set
            {
                if (Equals(flowMedian, value)) return;
                flowMedian = value;
                OnPropertyChanged("FlowMedian");
            }
        }

        #endregion Flow

        #region Leak

        private float? leakMax;

        public float? LeakMax
        {
            get { return leakMax; }
            set
            {
                if (Equals(leakMax, value)) return;
                leakMax = value;
                OnPropertyChanged("LeakMax");
            }
        }

        private float? leakP95;

        public float? LeakP95
        {
            get { return leakP95; }
            set
            {
                if (Equals(leakP95, value)) return;
                leakP95 = value;
                OnPropertyChanged("LeakP95");
            }
        }

        private float? leakMedian;

        public float? LeakMedian
        {
            get { return leakMedian; }
            set
            {
                if (Equals(leakMedian, value)) return;
                leakMedian = value;
                OnPropertyChanged("LeakMedian");
            }
        }

        #endregion Leak

        #region TidalVolume

        private float? tidalVolumeMax;

        public float? TidalVolumeMax
        {
            get { return tidalVolumeMax; }
            set
            {
                if (Equals(tidalVolumeMax, value)) return;
                tidalVolumeMax = value;
                OnPropertyChanged("TidalVolumeMax");
            }
        }

        private float? tidalVolumeP95;

        public float? TidalVolumeP95
        {
            get { return tidalVolumeP95; }
            set
            {
                if (Equals(tidalVolumeP95, value)) return;
                tidalVolumeP95 = value;
                OnPropertyChanged("TidalVolumeP95");
            }
        }

        private float? tidalVolumeMedian;

        public float? TidalVolumeMedian
        {
            get { return tidalVolumeMedian; }
            set
            {
                if (Equals(tidalVolumeMedian, value)) return;
                tidalVolumeMedian = value;
                OnPropertyChanged("TidalVolumeMedian");
            }
        }

        #endregion TidalVolume

        #region RespiratoryRate

        private float? respiratoryRateMax;

        public float? RespiratoryRateMax
        {
            get { return respiratoryRateMax; }
            set
            {
                if (Equals(respiratoryRateMax, value)) return;
                respiratoryRateMax = value;
                OnPropertyChanged("RespiratoryRateMax");
            }
        }

        private float? respiratoryRateP95;

        public float? RespiratoryRateP95
        {
            get { return respiratoryRateP95; }
            set
            {
                if (Equals(respiratoryRateP95, value)) return;
                respiratoryRateP95 = value;
                OnPropertyChanged("RespiratoryRateP95");
            }
        }

        private float? respiratoryRateMedian;

        public float? RespiratoryRateMedian
        {
            get { return respiratoryRateMedian; }
            set
            {
                if (Equals(respiratoryRateMedian, value)) return;
                respiratoryRateMedian = value;
                OnPropertyChanged("RespiratoryRateMedian");
            }
        }

        #endregion RespiratoryRate

        #region MinuteVentilation

        private float? minuteVentilationMax;

        public float? MinuteVentilationMax
        {
            get { return minuteVentilationMax; }
            set
            {
                if (Equals(minuteVentilationMax, value)) return;
                minuteVentilationMax = value;
                OnPropertyChanged("MinuteVentilationMax");
            }
        }

        private float? minuteVentilationP95;

        public float? MinuteVentilationP95
        {
            get { return minuteVentilationP95; }
            set
            {
                if (Equals(minuteVentilationP95, value)) return;
                minuteVentilationP95 = value;
                OnPropertyChanged("MinuteVentilationP95");
            }
        }

        private float? minuteVentilationMedian;

        public float? MinuteVentilationMedian
        {
            get { return minuteVentilationMedian; }
            set
            {
                if (Equals(minuteVentilationMedian, value)) return;
                minuteVentilationMedian = value;
                OnPropertyChanged("MinuteVentilationMedian");
            }
        }

        #endregion MinuteVentilation

        #region SpO2

        private float? spO2Max;

        public float? SpO2Max
        {
            get { return spO2Max; }
            set
            {
                if (Equals(spO2Max, value)) return;
                spO2Max = value;
                OnPropertyChanged("SpO2Max");
            }
        }

        private float? spO2P95;

        public float? SpO2P95
        {
            get { return spO2P95; }
            set
            {
                if (Equals(spO2P95, value)) return;
                spO2P95 = value;
                OnPropertyChanged("SpO2P95");
            }
        }

        private float? spO2Median;

        public float? SpO2Median
        {
            get { return spO2Median; }
            set
            {
                if (Equals(spO2Median, value)) return;
                spO2Median = value;
                OnPropertyChanged("SpO2Median");
            }
        }

        #endregion SpO2

        #region PulseRate

        private float? pulseRateMax;

        public float? PulseRateMax
        {
            get { return pulseRateMax; }
            set
            {
                if (Equals(pulseRateMax, value)) return;
                pulseRateMax = value;
                OnPropertyChanged("PulseRateMax");
            }
        }

        private float? pulseRateP95;

        public float? PulseRateP95
        {
            get { return pulseRateP95; }
            set
            {
                if (Equals(pulseRateP95, value)) return;
                pulseRateP95 = value;
                OnPropertyChanged("PulseRateP95");
            }
        }

        private float? pulseRateMedian;

        public float? PulseRateMedian
        {
            get { return pulseRateMedian; }
            set
            {
                if (Equals(pulseRateMedian, value)) return;
                pulseRateMedian = value;
                OnPropertyChanged("PulseRateMedian");
            }
        }

        #endregion PulseRate

        #region IERatio

        private float? iERatioMax;

        public float? IERatioMax
        {
            get { return iERatioMax; }
            set
            {
                if (Equals(iERatioMax, value)) return;
                iERatioMax = value;
                OnPropertyChanged("IERatioMax");
            }
        }

        private float? iERatioP95;

        public float? IERatioP95
        {
            get { return iERatioP95; }
            set
            {
                if (Equals(iERatioP95, value)) return;
                iERatioP95 = value;
                OnPropertyChanged("IERatioP95");
            }
        }

        private float? iERatioMedian;

        public float? IERatioMedian
        {
            get { return iERatioMedian; }
            set
            {
                if (Equals(iERatioMedian, value)) return;
                iERatioMedian = value;
                OnPropertyChanged("IERatioMedian");
            }
        }

        #endregion IERatio

        #region IPAP

        private float? iPAPMax;

        public float? IPAPMax
        {
            get { return iPAPMax; }
            set
            {
                if (Equals(iPAPMax, value)) return;
                iPAPMax = value;
                OnPropertyChanged("IPAPMax");
            }
        }

        private float? iPAPP95;

        public float? IPAPP95
        {
            get { return iPAPP95; }
            set
            {
                if (Equals(iPAPP95, value)) return;
                iPAPP95 = value;
                OnPropertyChanged("IPAPP95");
            }
        }

        private float? iPAPMedian;

        public float? IPAPMedian
        {
            get { return iPAPMedian; }
            set
            {
                if (Equals(iPAPMedian, value)) return;
                iPAPMedian = value;
                OnPropertyChanged("IPAPMedian");
            }
        }

        #endregion IPAP

        #region EPAP

        private float? ePAPMax;

        public float? EPAPMax
        {
            get { return ePAPMax; }
            set
            {
                if (Equals(ePAPMax, value)) return;
                ePAPMax = value;
                OnPropertyChanged("EPAPMax");
            }
        }

        private float? ePAPP95;

        public float? EPAPP95
        {
            get { return ePAPP95; }
            set
            {
                if (Equals(ePAPP95, value)) return;
                ePAPP95 = value;
                OnPropertyChanged("EPAPP95");
            }
        }

        private float? ePAPMedian;

        public float? EPAPMedian
        {
            get { return ePAPMedian; }
            set
            {
                if (Equals(ePAPMedian, value)) return;
                ePAPMedian = value;
                OnPropertyChanged("EPAPMedian");
            }
        }

        #endregion EPAP
    }
}
