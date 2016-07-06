using System;

namespace SuperSoft.Model
{
    public partial class ViewProductWorkingSummaryDetailedData : EntityBase<Guid>
    {
        public System.Guid PatientId { get; set; }
        public System.Guid ProductId { get; set; }
        public string FileName { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string ProductVersion { get; set; }
        public int ProductModel { get; set; }
        public int WorkingTime { get; set; }
        public System.DateTime CurrentTime { get; set; }
        public int TherapyMode { get; set; }
        public float IPAP { get; set; }
        public float EPAP { get; set; }
        public int RiseTime { get; set; }
        public int RespiratoryRate { get; set; }
        public int InspireTime { get; set; }
        public int ITrigger { get; set; }
        public int ETrigger { get; set; }
        public int Ramp { get; set; }
        public int ExhaleTime { get; set; }
        public float IPAPMax { get; set; }
        public float EPAPMin { get; set; }
        public float PSMax { get; set; }
        public float PSMin { get; set; }
        public float CPAP { get; set; }
        public int CFlex { get; set; }
        public float CPAPStart { get; set; }
        public float CPAPMax { get; set; }
        public float CPAPMin { get; set; }
        public int Alert { get; set; }
        public int Alert_Tube { get; set; }
        public int Alert_Apnea { get; set; }
        public int Alert_MinuteVentilation { get; set; }
        public int Alert_HRate { get; set; }
        public int Alert_LRate { get; set; }
        public int Alert_Reserve1 { get; set; }
        public int Alert_Reserve2 { get; set; }
        public int Alert_Reserve3 { get; set; }
        public int Alert_Reserve4 { get; set; }
        public int Config_HumidifierLevel { get; set; }
        public int Config_DataStore { get; set; }
        public int Config_SmartStart { get; set; }
        public int Config_PressureUnit { get; set; }
        public int Config_Language { get; set; }
        public int Config_Backlight { get; set; }
        public int Config_MaskPressure { get; set; }
        public int Config_ClinicalSet { get; set; }
        public int Config_Reserve1 { get; set; }
        public int Config_Reserve2 { get; set; }
        public byte[] Content { get; set; }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            FileName = null;
            ProductVersion = null;
        }
    }
}
