using SuperSoft.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SuperSoft.Utility;
using ProtoBuf;

namespace SuperSoft.BLL.DownloadData
{
    /// <summary>
    /// 详细文件的拆包
    /// 先读取详细的头信息，然后统计本文件中详细信息的内容
    /// 可能跨越中午12点 半夜12点,多天的12点
    /// </summary>
    internal class DetailedFileUnpack : Utility.MyClassBase
    {
        public DetailedFileUnpack(IndexFileField indexFileField, string detailedFileFullName)
        {
            this.indexFileField = indexFileField;
            this.detailedFileFullName = detailedFileFullName;
        }

        #region 公开方法

        /// <summary>
        /// 解包详细文件
        /// 0.通过文件名【140619\222158.DAT】检查是否解包过，如果已经解包过那么跳过不处理。
        /// 1.检查文件格式是否正确，不正确跳过不处理。
        /// 2.检查Sn是否一致，不是该设备的跳过不处理。
        /// 3.解析详细文件的文件头
        /// 4.解析详细内容
        /// 5.检查详细文件是否跨越中午12点，半夜12点，如果跨越则将文件拆分成多个详细文件，
        /// 5.将处理后的【ProductRuningSummaryData】和【ProductRuningDetailedData】存入数据库（1-n条）
        /// 单水平（CPAP、APAP）和双水品（BPAP）数据部分不一样，都在此处做内部处理和区分。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NeedUpDateTherapyMode> StartUnpackAndSaveToDataBase()
        {
            // 文件不存在，直接返回
            if (!File.Exists(detailedFileFullName))
            {
                //LogHelper.Info("FileNotFound:" + detailedFileFullName);
                return null;
            }
            IList<SummaryAndDetailed> listSummaryAndDetailed = new List<SummaryAndDetailed>();

            using (var fileStream = new FileStream(detailedFileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                #region 0.通过文件名【140619\222158.DAT】检查是否解包过，如果已经解包过那么跳过不处理。

                var detailedFilePath = detailedFileFullName.Substring(detailedFileFullName.Length - 17, 17);
                var productWorkingSummaryDataBLL = new ProductWorkingSummaryDataBLL();
                Expression<Func<ProductWorkingSummaryData, bool>> condition =
                    t => t.ProductId == indexFileField.ProductId && t.FileName == detailedFilePath;
                var listProductRuningSummaryDatas = productWorkingSummaryDataBLL.GetByCondition(condition);
                productWorkingSummaryDataBLL.Dispose();
                productWorkingSummaryDataBLL = null;
                if (listProductRuningSummaryDatas != null && listProductRuningSummaryDatas.Count() > 0)
                {
                    //解包过
                    listProductRuningSummaryDatas = null;
                    //LogHelper.Info("Allread Unpacked:" + detailedFileFullName);
                    return null;
                }

                #endregion

                #region  1.检查文件格式是否正确，不正确跳过不处理。 检验文件是否合法(文件大小、文件头)

                //检验文件是否合法，（头512字节 第一组数据20字节）位置是第一组数据结尾，如果没有数据那么不处理
                if (fileStream.Length < 532)
                {
                    //LogHelper.Info("File Length too short:" + detailedFileFullName);
                    return null;
                }
                readBytes = null;
                readBytes = new byte[6]; //Sy_RMS同步字
                fileStream.Read(readBytes, 0, 6);
                if (Encoding.ASCII.GetString(readBytes) != "Sy_RMS")
                {
                    //LogHelper.Info("File Header Sy_RMS Missed:" + detailedFileFullName);
                    return null;
                }

                #endregion

                #region 2.检查Sn是否一致，不是该设备的跳过不处理。

                //fileStream.Seek(6, SeekOrigin.Begin);//读取开始位置跳过同步字0-5
                readBytes = null;
                readBytes = new byte[9];
                fileStream.Read(readBytes, 0, 9);
                var Sn = readBytes.ToHexString();
                if (Sn != indexFileField.SerialNumber)
                {
                    //不是此设备的数据，不处理
                    Sn = null;
                    //LogHelper.Info("File Sn Different:" + detailedFileFullName);
                    return null;
                }
                Sn = null;

                #endregion

                #region 3.解析详细文件的文件头

                productWorkingSummaryData = new ProductWorkingSummaryData();
                productWorkingSummaryData.Id = DateTime.Now.ToGuid();
                productWorkingSummaryData.ProductId = indexFileField.ProductId;
                productWorkingSummaryData.FileName = detailedFilePath;
                detailedFilePath = null;

                #region 型号 模式

                //------------------------------------------------------------
                fileStream.Seek(15, SeekOrigin.Begin); //读取开始位置 产品型号
                readBytes = null;
                readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                indexFileField.ProductModel = readBytes[0]; //TODO设置产品型号,还没有更新到数据库
                productWorkingSummaryData.ProductModel = readBytes[0];
                productWorkingSummaryData.ProductVersion = indexFileField.ProductVersion; //产品版本

                //------------------------------------------------------------

                //当前时间	0x20,0x11,0x12,0x05,0x13,0x25,0x45	2011-12-5 13:25:45 BCD码：年月日时分秒
                //保存年月日到timeBytes,在解析详细包的时候只有时分秒
                fileStream.Seek(16, SeekOrigin.Begin); //读取开始位置跳 
                readBytes = null;
                readBytes = new byte[7];
                fileStream.Read(readBytes, 0, 7);
                productWorkingSummaryData.CurrentTime = DateTime.ParseExact(readBytes.ToHexString(), "yyyyMMddHHmmss",
                    CultureInfo.CurrentCulture);

                fileStream.Seek(16, SeekOrigin.Begin); //读取开始位置跳 
                fileStream.Read(timeBytes, 0, 4);

                //------------------------------------------------------------
                fileStream.Seek(23, SeekOrigin.Begin); //读取开始位置跳
                readBytes = null;
                readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);

                productWorkingSummaryData.TherapyMode = readBytes[0];
                //== 0 ? "S/T" : ((DrivicesMode)readBytes[0]).ToString();
                //下面需要根据治疗模式解析详细
                var CurrentMode = readBytes[0];

                #endregion

                #region 报警 通用

                //------------------------------------------------------------
                fileStream.Seek(38, SeekOrigin.Begin); //读取开始位置  报警总开关
                //readBytes = null;
                //readBytes = new byte[1];

                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(38, SeekOrigin.Begin);//读取开始位置  报警--面罩
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert_Tube = readBytes[0];


                //------------------------------------------------------------
                //fs.Seek(38, SeekOrigin.Begin);//读取开始位置  报警--窒息
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert_Apnea = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(38, SeekOrigin.Begin);//读取开始位置  报警--分钟通气量报警
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert_MinuteVentilation = readBytes[0];


                //------------------------------------------------------------
                //fs.Seek(38, SeekOrigin.Begin);//读取开始位置  报警--高呼吸频率报警
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert_HRate = readBytes[0];
                //------------------------------------------------------------
                //fs.Seek(38, SeekOrigin.Begin);//读取开始位置  报警--低呼吸频率报警
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Alert_LRate = readBytes[0];

                #endregion

                #region 配置 通用

                //------------------------------------------------------------
                fileStream.Seek(48, SeekOrigin.Begin); //读取开始位置  配置--湿化器档位
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_HumidifierLevel = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--数据保存
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_DataStore = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--智能启动
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_SmartStart = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--压力单位
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_PressureUnit = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--界面语言
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_Language = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--背光模式
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_Backlight = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--面罩压力
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_MaskPressure = readBytes[0];

                //------------------------------------------------------------
                //fs.Seek(48, SeekOrigin.Begin);//读取开始位置  配置--临床设置
                //readBytes = null;
                //readBytes = new byte[1];
                fileStream.Read(readBytes, 0, 1);
                productWorkingSummaryData.Config_ClinicalSet = readBytes[0];

                #endregion

                #region 模式不同，数据不同 需要单独解析和统计 扩展点

                #region 24-37字节的数据不一样

                // 0x00：S/T
                // 0x01：T
                // 0x02：S
                // 0x03：CPAP
                // 0x04：APAP
                // 0x05：PCV
                // 0x06：AutoS
                switch (CurrentMode)
                {
                    case 0x00: //S/T
                    case 0x05: //PCV

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 IPAP(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.IPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        //------------------------------------------------------------
                        ////读取开始位置 EPAP(2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.EPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 上升时间(1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RiseTime = readBytes[0]; //分钟

                        //------------------------------------------------------------
                        //读取开始位置 呼吸频率(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RespiratoryRate = readBytes[0]; //breaths/min

                        //------------------------------------------------------------
                        //读取开始位置 吸气时间((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.InspireTime = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 吸气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ITrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 呼气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ETrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 延时升压(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0];

                        #endregion

                        break;
                    case 0x01: //T

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 IPAP(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.IPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        //------------------------------------------------------------
                        ////读取开始位置 EPAP(2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.EPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 上升时间(1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RiseTime = readBytes[0]; //分钟

                        //------------------------------------------------------------
                        //读取开始位置 呼吸频率(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RespiratoryRate = readBytes[0]; //breaths/min

                        //------------------------------------------------------------
                        //读取开始位置 吸气时间((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.InspireTime = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 呼气时间((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ExhaleTime = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 延时升压Ramp((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0];

                        #endregion

                        break;
                    case 0x02: //S

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 IPAP(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.IPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        //------------------------------------------------------------
                        ////读取开始位置 EPAP(2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.EPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 上升时间(1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RiseTime = readBytes[0]; //分钟

                        //------------------------------------------------------------
                        //读取开始位置 吸气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ITrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 呼气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ETrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 延时升压Ramp((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0];

                        #endregion

                        break;
                    case 0x03: //CPAP APAP 详细部分相同

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 CPAP(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.CPAP = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        //------------------------------------------------------------
                        //f读取开始位置 压力延迟(1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0]; //分钟

                        //------------------------------------------------------------
                        //读取开始位置  舒适程度(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.CFlex = readBytes[0];

                        #endregion

                        break;
                    case 0x04: //APAP

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 APAPStart(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.CPAPStart = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        ////读取开始位置 APAPMax (2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.CPAPMax = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        ////读取开始位置 APAPMax (2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.CPAPMin = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //fs.Seek(24, SeekOrigin.Begin);//读取开始位置 压力延迟(1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0]; //分钟

                        //------------------------------------------------------------
                        //fs.Seek(24, SeekOrigin.Begin);//读取开始位置  舒适程度(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.CFlex = readBytes[0];

                        #endregion

                        break;
                    case 0x06: //AutoS

                        #region 24-37字节的数据不一样

                        //24-37字节的数据不一样------------------------------------------------------------
                        fileStream.Seek(24, SeekOrigin.Begin); //读取开始位置 IPAPMax(2)
                        readBytes = null;
                        readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.IPAPMax = readBytes.BytesToInt16(0) * 0.1f; //Kpa0.01 cmH2O 0.1

                        //------------------------------------------------------------
                        ////读取开始位置 EPAPMin(2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.EPAPMin = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 PS Max(2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.PSMax = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 PS Min (2)
                        //readBytes = null;
                        //readBytes = new byte[2];
                        fileStream.Read(readBytes, 0, 2);
                        productWorkingSummaryData.PSMin = readBytes.BytesToInt16(0) * 0.1f; //Kpa

                        //------------------------------------------------------------
                        //读取开始位置 RiseTime((1)
                        readBytes = null;
                        readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.RiseTime = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 吸气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ITrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 呼气灵敏度((1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.ETrigger = readBytes[0];

                        //------------------------------------------------------------
                        //读取开始位置 延时升压(1)
                        //readBytes = null;
                        //readBytes = new byte[1];
                        fileStream.Read(readBytes, 0, 1);
                        productWorkingSummaryData.Ramp = readBytes[0];

                        #endregion

                        break;
                }

                #endregion

                #region 处理详细 详细数据 512之后的数据验证和存入数据库

                //处理详细
                //var tmpDetailedBytes = GetDetailedBytesArrayData(fileStream, 0x3B);
                var tmpDetailedBytes = GetDetailedBytesArrayData(fileStream);
                if (tmpDetailedBytes == null || tmpDetailedBytes.Count < 1)
                {
                    return null;
                }

                IList<ICollection<byte[]>> listSplit = new Collection<ICollection<byte[]>>();

                #region 数据分割 分割12点和0点的数据 

                //分一下几种情况
                //1.正常数据不包含 0点和12点的数据 不需要分割
                //2.跨越中午12点的数据-年月日不变-开始位置不是12点，中间有12点
                //3.中午12点开始的数据-年月日不变-开始位置是12点，中间不含0点
                //4.跨午夜0点的数据-年月日变化-开始位置不是0点，中间有0点
                //5.午夜0点开始的数据-年月日不变-开始位置是0点，中间不含12点

                #endregion

                var doWhile = true;
                //分割12点和0点的数据,TODO
                while (doWhile)
                {
                    var hourIndex12 = tmpDetailedBytes.FindIndex(t => t[0] == 12);
                    var hourIndex24 = tmpDetailedBytes.FindIndex(t => t[0] == 0);
                    if (hourIndex12 > 0)
                    {
                        //有分割，得到 分割位置 年月日不变
                        var currentList = tmpDetailedBytes.GetRange(0, hourIndex12);
                        listSplit.Add(currentList);
                        tmpDetailedBytes = tmpDetailedBytes.GetRange(hourIndex12, tmpDetailedBytes.Count - hourIndex12);
                        doWhile = true;
                    }
                    else if (hourIndex24 > 0)
                    {
                        //有分割，得到 分割位置 年月日变化
                        var currentList = tmpDetailedBytes.GetRange(0, hourIndex24);
                        listSplit.Add(currentList);
                        tmpDetailedBytes = tmpDetailedBytes.GetRange(hourIndex24, tmpDetailedBytes.Count - hourIndex24);
                        isChangeDate = true;
                        doWhile = true;
                    }
                    else //正常年月日时间不变
                    {
                        listSplit.Add(tmpDetailedBytes);
                        doWhile = false;
                    }
                }

                //如果大于2段则无法确定后面的时间，所以出错
                if (listSplit.Count > 2)
                {
                    //LogHelper.Info("File Split Over Ranger " + listSplit.Count + ":" + detailedFileFullName);
                    return null;
                }

                //分割之后 将 summary 和各段的对应起来
                for (var i = 0; i < listSplit.Count; i++)
                {
                    var newProductWorkingSummaryData = (ProductWorkingSummaryData)productWorkingSummaryData.Clone();
                    newProductWorkingSummaryData.Id = DateTime.Now.ToGuid();
                    listSummaryAndDetailed.Add(new SummaryAndDetailed(newProductWorkingSummaryData,
                        listSplit[i].ToList()));
                }
                //处理分段之后的内容
                for (var i = 0; i < listSummaryAndDetailed.Count(); i++)
                {
                    var listDetailed = getDetailedData(listSummaryAndDetailed[i].ProductWorkingSummaryData,
                        listSummaryAndDetailed[i].DetailedList);
                    SaveToBD(listSummaryAndDetailed[i].ProductWorkingSummaryData, listDetailed);
                }

                #endregion

                #endregion

                #endregion
            } //endusing

            //得到数据的日期
            var result = from a in listSummaryAndDetailed
                         select new NeedUpDateTherapyMode
                             (a.ProductWorkingSummaryData.StartTime, a.ProductWorkingSummaryData.TherapyMode);
            return result;
        }

        #endregion

        #region 单双水品的详细数据解析

        /// <summary>
        /// 详细内容
        /// 传入字节数组的list，然后返回详细文件mode的list
        /// </summary>
        /// <param name="tmpDetailedBytes"></param>
        /// <returns></returns>
        private IEnumerable<DetailedField> getDetailedData(ProductWorkingSummaryData productWorkingSummaryData,
            IList<byte[]> tmpDetailedBytes)
        {
            DateTime endTime, startTime;
            string time = timeBytes.ToHexString();
            startTime =
                DateTime.ParseExact(
                   time + tmpDetailedBytes.FirstOrDefault().SubBytes(0, 3).ToDecString()
                    , "yyyyMMddHHmmss"
                    , CultureInfo.InvariantCulture);

            endTime =
                DateTime.ParseExact(
                    time + tmpDetailedBytes.LastOrDefault().SubBytes(0, 3).ToDecString()
                    , "yyyyMMddHHmmss"
                    , CultureInfo.InvariantCulture);
            time = null;
            //半夜12点之后的数据日期加一 todo
            if (tmpDetailedBytes.FirstOrDefault()[0] == 0 && isChangeDate)
            {
                startTime = startTime.AddDays(1);
                endTime = endTime.AddDays(1);
            }
            productWorkingSummaryData.StartTime = startTime;
            productWorkingSummaryData.EndTime = endTime;
            productWorkingSummaryData.WorkingTime = (int)(endTime - startTime).TotalMilliseconds;

            double titick = 1;
            if (tmpDetailedBytes.Count() > 1)
            {
                titick = (endTime - startTime).TotalMilliseconds / (tmpDetailedBytes.Count() - 1);
            }
            ICollection<DetailedField> listDetailed = new Collection<DetailedField>();

            #region 同步方法解析 比异步步快 ，是有序的，使用时不需要排序 ，异步需要锁list所以费时，不锁会导致多线程访问list

            //var objLock = new object();
            //var result = Parallel.For(0, tmpDetailedBytes.Count(), i =>
            for (int i = 0; i < tmpDetailedBytes.Count(); i++)// var result = Parallel.For(0, tmpDetailedBytes.Count(), i =>
            {

                var detailedField = new DetailedField();
                detailedField.RecoredTime = startTime.AddMilliseconds(titick * i);

                #region 详细数据 512之后的数据

                // 0x00：S/T
                // 0x01：T
                // 0x02：S
                // 0x03：CPAP
                // 0x04：APAP
                // 0x05：PCV
                // 0x06：AutoS
                switch (productWorkingSummaryData.TherapyMode)
                {
                    //双水品512之后一样
                    case 0x00: //S/T
                    case 0x01: //T
                    case 0x02: //S
                    case 0x05: //PCV 
                    case 0x06: //AutoS 

                        #region 处理详细 双水品

                        //------------------------------------------------------------压力kpa 0.01 cmH2O 0.1
                        detailedField.TargetPressure = tmpDetailedBytes[i].SubBytes(3, 2).BytesToInt16(0) * 0.1f;

                        //------------------------------------------------------------当前流量.有负值
                        var tt = tmpDetailedBytes[i][5] * 256 + tmpDetailedBytes[i][6];
                        if (tt > short.MaxValue)
                        {
                            detailedField.CurrentFlow = short.MaxValue - tt; //升/分钟
                        }
                        else
                        {
                            detailedField.CurrentFlow = tt; //升/分钟
                        }
                        //tmpbytes = null;

                        //------------------------------------------------------------潮气量(0-2500)mL
                        detailedField.TidalVolume = tmpDetailedBytes[i].SubBytes(7, 2).BytesToInt16(0) * 0.1f;

                        //------------------------------------------------------------漏气量(0-99) L/min
                        //detailedField.Leak = tmpDetailedBytes[i].SubBytes(9, 1)[0];
                        detailedField.Leak = tmpDetailedBytes[i][9];

                        //------------------------------------------------------------分钟通气量(0-30)L/min
                        //detailedField.MinuteVentilation = tmpDetailedBytes[i].SubBytes(10, 1)[0];
                        detailedField.MinuteVentilation = tmpDetailedBytes[i][10];

                        //------------------------------------------------------------呼吸频率(0-60)min
                        //detailedField.RespiratoryRate = tmpDetailedBytes[i].SubBytes(11, 1)[0];
                        detailedField.RespiratoryRate = tmpDetailedBytes[i][11];

                        //------------------------------------------------------------吸呼比(1-99),1:0.1-9.9
                        //detailedField.IERatio = tmpDetailedBytes[i].SubBytes(12, 1)[0] * 0.1m;
                        detailedField.IERatio = tmpDetailedBytes[i][12] * 0.1f;

                        //------------------------------------------------------------IPAP(40-250)4-25cmH2O
                        //detailedField.IPAP = tmpDetailedBytes[i].SubBytes(13, 1)[0] * 0.1m;
                        detailedField.IPAP = tmpDetailedBytes[i][13] * 0.1f;

                        //------------------------------------------------------------EPAP(40-200)4-20cmH2O
                        //detailedField.EPAP = tmpDetailedBytes[i].SubBytes(14, 1)[0] * 0.1m;
                        detailedField.EPAP = tmpDetailedBytes[i][14] * 0.1f;

                        //呼吸事件对128求余后： 0，无；1低通气，2，呼吸暂停 4，鼾声 .主动 / 被动 ：主动 >= 128,被动 < 128;
                        //呼吸事件和

                        //var tmp = tmpDetailedBytes[i].SubBytes(15, 1)[0];
                        var tmp = tmpDetailedBytes[i][15];
                        if (tmp >= 128)
                        {
                            detailedField.TriggerMode = 128;//主动
                        }
                        else
                        {
                            detailedField.TriggerMode = 127;//被动
                        }
                        var vv = tmp % 128;
                        switch (vv)
                        {
                            case 1:
                                detailedField.Events = 1;
                                break;
                            case 2:
                                detailedField.Events = 2;
                                break;
                            case 4:
                                detailedField.Events = 4;
                                break;
                            default:
                                detailedField.Events = 0;
                                break;
                        }
                        //if (vv != 0 && vv != 1 && vv != 2 && vv != 4)//其他值无效
                        //{
                        //    detailedField.Events = 0;
                        //}
                        //else
                        //{
                        //    detailedField.Events = vv;
                        //}
                        //var tmpSingleLevel = tmpDetailedBytes[i][9];
                        //if (detailedField.Events != 0)
                        //    System.Diagnostics.Debug.Print("detailedField.Events " + detailedField.Events);

                        //System.Diagnostics.Debug.Print("tmpDetailedBytes " + tmpDetailedBytes[i].ToHexString());

                        //------------------------------------------------------------血氧0-99
                        //detailedField.SpO2 = tmpDetailedBytes[i].SubBytes(16, 1)[0];
                        detailedField.SpO2 = tmpDetailedBytes[i][16];

                        //------------------------------------------------------------脉率0-255
                        //detailedField.PulseRate = tmpDetailedBytes[i].SubBytes(17, 1)[0];
                        detailedField.PulseRate = tmpDetailedBytes[i][17];

                        #endregion
                        break;
                    //单水品512之后一样
                    case 0x03: //CPAP
                    case 0x04: //APAP

                        #region 处理详细 单水平

                        //------------------------------------------------------------目标压力kpa 0.01 cmH2O 0.1
                        detailedField.TargetPressure = tmpDetailedBytes[i].SubBytes(3, 2).BytesToInt16(0) * 0.1f;

                        //------------------------------------------------------------当前压力kap
                        detailedField.CurrentPressure = tmpDetailedBytes[i].SubBytes(5, 2).BytesToInt16(0) * 0.1f;

                        //------------------------------------------------------------当前流量.有负值
                        //var tmpbytesSingleLevel = tmpDetailedBytes[i].SubBytes(7, 2);

                        //var ttSingleLevel = tmpbytesSingleLevel[0] * 256 + tmpbytesSingleLevel[1];

                        var ttSingleLevel = tmpDetailedBytes[i][7] * 256 + tmpDetailedBytes[i][8];
                        if (ttSingleLevel > short.MaxValue)
                        {
                            detailedField.CurrentFlow = short.MaxValue - ttSingleLevel; //升/分钟
                        }
                        else
                        {
                            detailedField.CurrentFlow = ttSingleLevel; //升/分钟
                        }
                        //tmpbytesSingleLevel = null;

                        //单水平呼吸事件0，无；1低通气，2，呼吸暂停 4，鼾声
                        //var tmpSingleLevel = tmpDetailedBytes[i].SubBytes(9, 1)[0];
                        var tmpSingleLevel = tmpDetailedBytes[i][9];
                        switch (tmpSingleLevel)
                        {
                            case 1:
                                detailedField.Events = 1;
                                break;
                            case 2:
                                detailedField.Events = 2;
                                break;
                            case 4:
                                detailedField.Events = 4;
                                break;
                            default:
                                detailedField.Events = 0;
                                break;
                        }
                        //if (tmpSingleLevel != 0 && tmpSingleLevel != 1 && tmpSingleLevel != 2 && tmpSingleLevel != 4)
                        //{
                        //    detailedField.Events = 0;
                        //}
                        //else
                        //{
                        //    detailedField.Events = tmpSingleLevel;
                        //    //System.Diagnostics.Debug.Print(tmpSingleLevel.ToString());
                        //}

                        //------------------------------------------------------------潮气量(0-2500)mL
                        detailedField.TidalVolume = tmpDetailedBytes[i].SubBytes(10, 2).BytesToInt16(0) * 0.1f;

                        //------------------------------------------------------------漏气量(0-99) L/min
                        //detailedField.Leak = tmpDetailedBytes[i].SubBytes(12, 1)[0];
                        detailedField.Leak = tmpDetailedBytes[i][12];

                        //------------------------------------------------------------分钟通气量(0-30)L/min
                        //detailedField.MinuteVentilation = tmpDetailedBytes[i].SubBytes(13, 1)[0];
                        detailedField.MinuteVentilation = tmpDetailedBytes[i][13];

                        //------------------------------------------------------------血氧0-99
                        //detailedField.SpO2 = tmpDetailedBytes[i].SubBytes(14, 1)[0];
                        detailedField.SpO2 = tmpDetailedBytes[i][14];

                        //------------------------------------------------------------脉率0-255
                        //detailedField.PulseRate = tmpDetailedBytes[i].SubBytes(15, 1)[0];
                        detailedField.PulseRate = tmpDetailedBytes[i][15];

                        #endregion

                        break;
                }

                #endregion

                //lock (objLock)
                //{
                listDetailed.Add(detailedField);
                //}
                //});
            }

            //objLock = null;

            #endregion 同步方法解析

            return listDetailed;
        }

        #endregion

        /// <summary>
        /// 所有模式通用
        /// 详细文件包括文件头512个字节+详细内容
        /// 详细内容为512个字节一大组，有的格式在文件头多写了一个字节，后面的512个字节形式不变
        /// 所以每个文件的大小是512的整数倍或者512整数倍+1
        /// 利用文件大小和512判断是否多写了一个字节
        /// 然后找到详细内容的开始位置（512或者513）
        /// 首先从开始位置循环512字节
        /// 然后嵌套循环里面的25组，每组20个字节
        /// 
        /// 其他条件：
        /// 每大组的末尾12个字节是3B或者3C
        /// 每小组的末尾是3B
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private List<byte[]> GetDetailedBytesArrayData(FileStream fileStream)
        {
            var value = new List<byte[]>();

            if (fileStream == null)
            {
                return value;
            }

            //只有文件头，没有详细内容，不解析
            if (fileStream.Length <= 512 * 2)//至少一包详细数据
            {
                return value;
            }
            var offset = fileStream.Length % 512;
            var detailedStartIndex = 512;//详细文件开始位置
            //文件头之后详细内容之前多了一个字节
            if (offset != 0)
            {
                detailedStartIndex = 512 + (int)offset;
            }
            int packageStartIndex = detailedStartIndex;
            long loopCount = (fileStream.Length - detailedStartIndex) / 512;
            //System.Diagnostics.Debug.Print("loopCount:" + loopCount);
            for (int i = 0; i < loopCount; i++)
            {
                packageStartIndex = detailedStartIndex + 512 * i;
                fileStream.Seek(packageStartIndex, SeekOrigin.Begin);
                //System.Diagnostics.Debug.Print(packageStartIndex.ToString());
                for (int j = 0; j < 25; j++)//一大包里面有25小包的数据
                {
                    byte[] bytes = new byte[20];//20个字节一包数据
                    fileStream.Read(bytes, 0, 20);
                    //System.Diagnostics.Debug.Print(fileStream.Position.ToString());
                    if (bytes[19] == 59 && (bytes[0] < 24) && (bytes[1] < 60) && (bytes[2] < 60))//每小包开始的时分秒检验,末尾的3B校验
                    {
                        value.Add(bytes);
                    }
                    bytes = null;
                }
            }
            return value;
        }

        /// <summary>
        /// 通用方法，将概要数据和详细数据保存到数据库
        /// </summary>
        /// <param name="productWorkingSummaryData"></param>
        /// <param name="detailedList"></param>
        private void SaveToBD(ProductWorkingSummaryData productWorkingSummaryData,
            IEnumerable<DetailedField> detailedList)
        {
            //using (var transactionScope = new TransactionScope())
            //{
            using (var productWorkingSummaryDataBLL = new ProductWorkingSummaryDataBLL())
            {
                productWorkingSummaryDataBLL.Insert(productWorkingSummaryData);
                using (var memoryStream = new MemoryStream())
                {
                    Serializer.Serialize(memoryStream, detailedList);
                    using (var productWorkingDetailedDataBLL = new ProductWorkingDetailedDataBLL())
                    {
                        var productWorkingDetailedData = new ProductWorkingDetailedData();
                        productWorkingDetailedData.Id = DateTime.Now.ToGuid();
                        productWorkingDetailedData.ProductWorkingSummaryDataId = productWorkingSummaryData.Id;
                        productWorkingDetailedData.Content = memoryStream.ToArray();
                        productWorkingDetailedDataBLL.Insert(productWorkingDetailedData);
                    }
                    memoryStream.Close();
                }
            }
            //transactionScope.Complete();
            //}
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            indexFileField = null;
            detailedFileFullName = null;
            readBytes = null;
            timeBytes = null;
            productWorkingSummaryData = null;
        }

        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();
        }

        #region 内部变量

        private IndexFileField indexFileField;

        /// <summary>
        /// 详细文件的全名
        /// </summary>
        private string detailedFileFullName;

        /// <summary>
        /// 局部变量，用于从文件读取的字节
        /// </summary>
        private byte[] readBytes;

        /// <summary>
        /// 头部的年月日字节
        /// </summary>
        private byte[] timeBytes = new byte[4];

        /// <summary>
        /// 概要信息
        /// </summary>
        private ProductWorkingSummaryData productWorkingSummaryData;

        /// <summary>
        /// 是否改变日期
        /// </summary>
        private bool isChangeDate;

        #endregion
    }
}