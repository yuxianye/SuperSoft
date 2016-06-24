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
    public partial class ViewProductWorkingSummaryDetailedData : EntityBase<Guid>
    {
        public Nullable<System.Guid> PatientId { get; set; }
        //public System.Guid Id { get; set; }
        public System.Guid ProductId { get; set; }
        public string FileName { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string ProductVersion { get; set; }
        public int ProductModel { get; set; }
        public int WorkingTime { get; set; }
        public System.DateTime CurrentTime { get; set; }
        public int TherapyMode { get; set; }
        public Nullable<float> IPAP { get; set; }
        public Nullable<float> EPAP { get; set; }
        public Nullable<int> RiseTime { get; set; }
        public Nullable<int> RespiratoryRate { get; set; }
        public Nullable<int> InspireTime { get; set; }
        public Nullable<int> ITrigger { get; set; }
        public Nullable<int> ETrigger { get; set; }
        public Nullable<int> Ramp { get; set; }
        public Nullable<int> ExhaleTime { get; set; }
        public Nullable<float> IPAPMax { get; set; }
        public Nullable<float> EPAPMin { get; set; }
        public Nullable<float> PSMax { get; set; }
        public Nullable<float> PSMin { get; set; }
        public Nullable<float> CPAP { get; set; }
        public Nullable<int> CFlex { get; set; }
        public Nullable<float> CPAPStart { get; set; }
        public Nullable<float> CPAPMax { get; set; }
        public Nullable<float> CPAPMin { get; set; }
        public Nullable<int> Alert { get; set; }
        public Nullable<int> Alert_Tube { get; set; }
        public Nullable<int> Alert_Apnea { get; set; }
        public Nullable<int> Alert_MinuteVentilation { get; set; }
        public Nullable<int> Alert_HRate { get; set; }
        public Nullable<int> Alert_LRate { get; set; }
        public Nullable<int> Alert_Reserve1 { get; set; }
        public Nullable<int> Alert_Reserve2 { get; set; }
        public Nullable<int> Alert_Reserve3 { get; set; }
        public Nullable<int> Alert_Reserve4 { get; set; }
        public Nullable<int> Config_HumidifierLevel { get; set; }
        public Nullable<int> Config_DataStore { get; set; }
        public Nullable<int> Config_SmartStart { get; set; }
        public Nullable<int> Config_PressureUnit { get; set; }
        public Nullable<int> Config_Language { get; set; }
        public Nullable<int> Config_Backlight { get; set; }
        public Nullable<int> Config_MaskPressure { get; set; }
        public Nullable<int> Config_ClinicalSet { get; set; }
        public Nullable<int> Config_Reserve1 { get; set; }
        public Nullable<int> Config_Reserve2 { get; set; }
        public byte[] Content { get; set; }
    }
}
