using System;

namespace SuperSoft.Model
{
    /// <summary>
    /// 产品运行统计数据实体
    /// </summary>
    public class ProductWorkingSummaryData : EntityBase<Guid>, ICloneable
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public System.Guid ProductId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public System.DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public System.DateTime EndTime { get; set; }

        /// <summary>
        /// 产品版本
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// 铲平型号
        /// </summary>
        public int ProductModel { get; set; }

        /// <summary>
        /// 运行时间（毫秒）
        /// </summary>
        public int WorkingTime { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public System.DateTime CurrentTime { get; set; }

        /// <summary>
        /// 治疗模式，参考数据格式定义
        /// </summary>
        public int TherapyMode { get; set; }

        /// <summary>
        /// IPAP
        /// </summary>
        public float IPAP { get; set; }

        /// <summary>
        /// EPAP
        /// </summary>
        public float EPAP { get; set; }

        /// <summary>
        /// 上升时间
        /// </summary>
        public int RiseTime { get; set; }

        /// <summary>
        /// 呼吸频率
        /// </summary>
        public int RespiratoryRate { get; set; }

        /// <summary>
        /// 吸气时间
        /// </summary>
        public int InspireTime { get; set; }

        /// <summary>
        /// 吸气灵敏度
        /// </summary>
        public int ITrigger { get; set; }

        /// <summary>
        /// 呼气灵敏度
        /// </summary>
        public int ETrigger { get; set; }

        /// <summary>
        /// 延时升压
        /// </summary>
        public int Ramp { get; set; }

        /// <summary>
        /// 呼气时间
        /// </summary>
        public int ExhaleTime { get; set; }

        /// <summary>
        /// IPAPMax
        /// </summary>
        public float IPAPMax { get; set; }

        /// <summary>
        /// EPAPMin
        /// </summary>
        public float EPAPMin { get; set; }

        /// <summary>
        /// PSMax
        /// </summary>
        public float PSMax { get; set; }

        /// <summary>
        /// PSMin
        /// </summary>
        public float PSMin { get; set; }

        /// <summary>
        /// CPAP
        /// </summary>
        public float CPAP { get; set; }

        /// <summary>
        /// 舒适程度
        /// </summary>
        public int CFlex { get; set; }

        /// <summary>
        /// CPAPStart
        /// </summary>
        public float CPAPStart { get; set; }

        /// <summary>
        /// CPAPMax
        /// </summary>
        public float CPAPMax { get; set; }

        /// <summary>
        /// CPAPMin
        /// </summary>
        public float CPAPMin { get; set; }

        /// <summary>
        /// 报警
        /// </summary>
        public int Alert { get; set; }

        /// <summary>
        /// 报警-管路
        /// </summary>
        public int Alert_Tube { get; set; }

        /// <summary>
        /// 报警-窒息
        /// </summary>
        public int Alert_Apnea { get; set; }

        /// <summary>
        /// 报警-分钟通气量
        /// </summary>
        public int Alert_MinuteVentilation { get; set; }

        /// <summary>
        /// 报警-高呼吸频率
        /// </summary>
        public int Alert_HRate { get; set; }

        /// <summary>
        /// 报警-低呼吸频率
        /// </summary>
        public int Alert_LRate { get; set; }

        /// <summary>
        /// 报警-预留1
        /// </summary>
        public int Alert_Reserve1 { get; set; }

        /// <summary>
        /// 报警-预留2
        /// </summary>
        public int Alert_Reserve2 { get; set; }

        /// <summary>
        /// 报警-预留3
        /// </summary>
        public int Alert_Reserve3 { get; set; }

        /// <summary>
        /// 报警-预留4
        /// </summary>
        public int Alert_Reserve4 { get; set; }

        /// <summary>
        /// 配置-湿化档位
        /// </summary>
        public int Config_HumidifierLevel { get; set; }

        /// <summary>
        /// 配置-数据存储
        /// </summary>
        public int Config_DataStore { get; set; }

        /// <summary>
        /// 配置-智能启动
        /// </summary>
        public int Config_SmartStart { get; set; }

        /// <summary>
        /// 配置-压力单位
        /// </summary>
        public int Config_PressureUnit { get; set; }

        /// <summary>
        /// 配置-语言
        /// </summary>
        public int Config_Language { get; set; }

        /// <summary>
        /// 配置-背光
        /// </summary>
        public int Config_Backlight { get; set; }

        /// <summary>
        /// 配置-面罩压力
        /// </summary>
        public int Config_MaskPressure { get; set; }

        /// <summary>
        /// 配置-临床设置
        /// </summary>
        public int Config_ClinicalSet { get; set; }

        /// <summary>
        /// 配置-预留1
        /// </summary>
        public int Config_Reserve1 { get; set; }

        /// <summary>
        /// 配置-预留2
        /// </summary>
        public int Config_Reserve2 { get; set; }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            FileName = null;
            ProductVersion = null;
        }
    }
}
