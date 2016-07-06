using ProtoBuf;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SuperSoft.BLL.DownloadData
{
    /// <summary>
    /// 下载数据
    /// 从sd卡或者文件系统中解析数据并存入数据库中
    /// </summary>
    public class DownloadData : MyClassBase
    {
        public DownloadData()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.Disposed += backgroundWorker_Disposed;
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            if (backgroundWorker != null)
            {
                backgroundWorker.Dispose();
            }

            if (ProgressChanged != null)
            {
                ProgressChanged = null;
            }
            if (RunWorkerCompleted != null)
            {
                RunWorkerCompleted = null;
            }
        }

        #region 公开 方法 事件 

        /// <summary>
        /// 获取一个值，指示 Download 是否正在运行异步操作。
        /// 如果 Download 正在运行异步操作，则为 true；否则为 false。
        /// </summary>
        public bool IsBusy()
        {
            return backgroundWorker.IsBusy;
        }

        /// <summary>
        /// 处理进度事件
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// 当后台操作已完成、被取消或引发异常时发生。
        /// </summary>
        public event EventHandler<RunWorkerCompletedEventArgs> RunWorkerCompleted;

        /// <summary>
        /// 开始异步解析文件
        /// </summary>
        /// <param name="respircareFilePath">【SY_RMS.TXT】文件 或者【000338.DAT】文件的路径</param>
        public void Start(Guid patientId, string respircareFilePath)
        {
            if (Disposed)
            {
                CallCompleted(new RunWorkerCompletedEventArgs(null, new ObjectDisposedException(ToString()), true));
                return;
            }

            if (cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                cancellationTokenSource = new CancellationTokenSource();
            }

            //检查患者Id
            if (Guid.Empty == patientId)
            {
                CallCompleted(new RunWorkerCompletedEventArgs(null,
                    new Exception(ResourceHelper.LoadString("PatientNotFound")), true));
                return;
            }
            //检查数据库中是否有患者Id
            using (var patientBLL = new PatientBLL())
            {
                var tmp = patientBLL.GetById(patientId);
                if (tmp == null)
                {
                    CallCompleted(new RunWorkerCompletedEventArgs(null,
                        new Exception(ResourceHelper.LoadString("PatientNotFound")), true));
                    return;
                }
            }

            if (!File.Exists(respircareFilePath))
            {
                CallCompleted(new RunWorkerCompletedEventArgs(null, new FileNotFoundException(respircareFilePath), true));
                return;
            }

            if (backgroundWorker != null)
            {
                this.patientId = patientId;
                backgroundWorker.RunWorkerAsync(respircareFilePath);
            }
        }

        /// <summary>
        /// 异步取消
        /// </summary>
        public void CancelAsync()
        {
            cancellationTokenSource.Cancel();
            if (backgroundWorker != null)
            {
                backgroundWorker.CancelAsync();
            }
        }

        #endregion

        #region 私有变量定义

        private readonly BackgroundWorker backgroundWorker;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Guid patientId;
        private Guid productId;


        #endregion

        #region 内部方法 处理进度 和 处理完成方法 

        /// <summary>
        /// 内部方法
        /// </summary>
        /// <param Name="e"></param>
        private void CallCompleted(RunWorkerCompletedEventArgs e)
        {
            if (RunWorkerCompleted != null)
            {
                RunWorkerCompleted(this, e);
            }
        }

        /// <summary>
        /// 内部方法
        /// </summary>
        /// <param Name="e"></param>
        private void CallProgressChanged(ProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CallProgressChanged(e);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CallCompleted(e);
        }

        private void backgroundWorker_Disposed(object sender, EventArgs e)
        {
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var respircareFilePath = e.Argument.ToString();
            CallProgressChanged(new ProgressChangedEventArgs(0, null));

            if (respircareFilePath.EndsWith(".TXT")) //索引文件
            {
                processRespircareTxtFile(e, respircareFilePath);
            }
            else //DAT文件
            {
                //TODO 当前版本不支持此功能
                processRespircareDatFile(e, respircareFilePath);
            }
        }

        #endregion

        #region 下载文件

        #region 下载文件 有索引文件

        private void processRespircareTxtFile(DoWorkEventArgs e, string respircareFilePath)
        {
            CallProgressChanged(new ProgressChangedEventArgs(1, null));

            var indexFileField = GetIndexFileField(respircareFilePath);

            if (indexFileField == null || string.IsNullOrWhiteSpace(indexFileField.SerialNumber) ||
                indexFileField.SerialNumber.Length != 18)
            {
                throw new Exception(string.Format(ResourceHelper.LoadString("RMSFileFormatError")
                                                  + Environment.NewLine
                    , respircareFilePath, ResourceHelper.LoadString("RMSFileName")));
            }
            //开机文件检查OK，患者信息产品信息和患者产品信息已经在新增患者时增加完成。 
            //不用再新增这些信息，只需要更新即可
            //取得产品信息
            using (var productBLL = new ProductBLL())
            {
                Expression<Func<Product, bool>> condition = c => c.SerialNumber == indexFileField.SerialNumber;
                var productTmp = productBLL.GetByCondition(condition);
                if (productTmp != null && productTmp.Count() > 0)
                {
                    //更新产品
                    productId = productTmp.FirstOrDefault().Id;
                    productTmp.FirstOrDefault().ProductVersion = indexFileField.ProductVersion;
                    //product.ProductModel = indexFileField.ProductModel;  开机文件没有产品型号信息,需要在详细文件中取得并更新
                    productTmp.FirstOrDefault().TotalWorkingTime = indexFileField.TotalWorkingTime;
                    productBLL.Update(productTmp.FirstOrDefault());
                }
                else
                {
                    //为找到产品
                    throw new Exception(ResourceHelper.LoadString("ProductNotExist"));
                }
                indexFileField.ProductId = productId;
            }

            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            //根目录 c:\ 或者h:\
            var rootName = respircareFilePath.Substring(0, respircareFilePath.LastIndexOf('\\') + 1);
            //处理详细数据文件
            var directoryInfo = new DirectoryInfo(rootName);

            //得到所有详细数据文件的列表,搜索所有 xxxxxx.dat 的文件和文件件
            var searchedFileInfos = directoryInfo.GetFileSystemInfos(Const.DatFileFilter, SearchOption.AllDirectories);
            directoryInfo = null;
            // Use ParallelOptions instance to store the CancellationToken
            var parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = cancellationTokenSource.Token;
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;
            var objLock = new object();

            ICollection<FileSystemInfo> datFileInfos = new Collection<FileSystemInfo>();
            //非法文件目录和文件名过滤。I:\140904\173416.DAT,(根目录-->******(年月日)******.DAT(时分秒) )
            Parallel.ForEach(searchedFileInfos, parallelOptions, datFile =>
            {
                parallelOptions.CancellationToken.ThrowIfCancellationRequested();

                if (datFile.Attributes != FileAttributes.Directory)
                {
                    try
                    {
                        //验证路径的格式 "140904\173416.DAT" 去掉扩展名和路径分隔符。
                        //如果文件名格式不正确则不处理
                        var strPath =
                            datFile.FullName.Replace(rootName, "")
                                .Replace("\\", "")
                                .Replace(Const.RMSFileExtensionData, "");
                        var dateTime = DateTime.ParseExact(strPath,
                            "yyMMddHHmmss",
                            CultureInfo.InvariantCulture);

                        //
                        lock (objLock)
                        {
                            datFileInfos.Add(datFile);
                        }
                    }
                    catch
                    {
                        //LogHelper.Info("File Path Format Error:" + datFile.FullName);
                    }
                }
            });

            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (datFileInfos == null || datFileInfos.Count() < 1)
            {
                throw new FileNotFoundException(ResourceHelper.LoadString("DataFileNotFound"));
            }

            CallProgressChanged(new ProgressChangedEventArgs(2, null));

            //循环处理每个dat文件

            var needUpdata = new List<NeedUpDateTherapyMode>();
            var progress = 0;
            var datFileCount = datFileInfos.Count();
            ////循环需要处理的每个文件，采用并行迭代
            //Parallel.ForEach(datFileInfos, parallelOptions, datFile =>
            //{
            //    parallelOptions.CancellationToken.ThrowIfCancellationRequested();
            //    IEnumerable<NeedUpDateTherapyMode> needUpdataTmp = new Collection<NeedUpDateTherapyMode>();
            //    //如果文件存在则继续执行
            //    if (datFile.Exists)
            //    {
            //        var DetailedFileUnpack = new DetailedFileUnpack(indexFileField, datFile.FullName);
            //        needUpdataTmp = DetailedFileUnpack.StartUnpackAndSaveToDataBase();
            //    }

            //    //更新进度
            //    lock (objLock)
            //    {
            //        if (needUpdataTmp != null && needUpdataTmp.Count() > 0)
            //        {
            //            needUpdata.AddRange(needUpdataTmp);
            //        }

            //        progress++;
            //        // 前面有2% 
            //        var a = (int)(progress / (float)datFileCount * 100 * 0.9) + 2;
            //        CallProgressChanged(new ProgressChangedEventArgs(a, null));
            //    }
            //});
            foreach (var datFile in datFileInfos)
            {
                parallelOptions.CancellationToken.ThrowIfCancellationRequested();
                IEnumerable<NeedUpDateTherapyMode> needUpdataTmp = new Collection<NeedUpDateTherapyMode>();
                //如果文件存在则继续执行
                if (datFile.Exists)
                {
                    var DetailedFileUnpack = new DetailedFileUnpack(indexFileField, datFile.FullName);
                    needUpdataTmp = DetailedFileUnpack.StartUnpackAndSaveToDataBase();
                }

                //更新进度
                //lock (objLock)
                //{
                if (needUpdataTmp != null && needUpdataTmp.Count() > 0)
                {
                    needUpdata.AddRange(needUpdataTmp);
                }

                progress++;
                // 前面有2% 
                var a = (int)(progress / (float)datFileCount * 100 * 0.9) + 2;
                CallProgressChanged(new ProgressChangedEventArgs(a, null));
                //}
            }


            //更新产品型号
            using (var productBLL = new ProductBLL())
            {
                Expression<Func<Product, bool>> condition = c => c.SerialNumber == indexFileField.SerialNumber;
                var productTmp = productBLL.GetByCondition(condition);
                if (productTmp != null && productTmp.Count() > 0)
                {
                    //更新产品
                    productTmp.FirstOrDefault().ProductModel = indexFileField.ProductModel; // 详细文件中取得并更新
                    productBLL.Update(productTmp.FirstOrDefault());
                }
            }
            //根据需要统计的日期更新 统计表中的数据
            e.Result = UpStatistics(needUpdata);
        }

        /// <summary>
        /// 更新 统计表中的数据，如果某天的数据已经统计过那么更新，如果没有统计过那么新增。
        /// 统计时按照中午12点分割。
        /// </summary>
        /// <param name="needUpDate">需要更新的日期时间</param>
        /// <returns>返回更新的天数</returns>
        private object UpStatistics(IEnumerable<NeedUpDateTherapyMode> needUpDate)
        {
            ICollection<NeedUpDateTherapyMode> listNeedUpDay = new Collection<NeedUpDateTherapyMode>();
            var progress = 0;

            foreach (var v in needUpDate)
            {
                if (v.DataTime.Hour < 12)
                {
                    var tt =
                        listNeedUpDay.Where(
                            a => a.DataTime.Date == v.DataTime.Date.AddDays(-1) && a.TherapyMode == v.TherapyMode);

                    if (tt == null || tt.Count() < 1)
                    {
                        listNeedUpDay.Add(new NeedUpDateTherapyMode(v.DataTime.Date.AddDays(-1), v.TherapyMode));
                    }
                }
                else
                {
                    var tt =
                        listNeedUpDay.Where(a => a.DataTime.Date == v.DataTime.Date && a.TherapyMode == v.TherapyMode);

                    if (tt == null || tt.Count() < 1)
                    {
                        listNeedUpDay.Add(new NeedUpDateTherapyMode(v.DataTime.Date, v.TherapyMode));
                    }
                }
            }
            if (listNeedUpDay == null || listNeedUpDay.Count() < 1)
            {
                CallProgressChanged(new ProgressChangedEventArgs(100, null));
                return null;
            }

            var dayCount = listNeedUpDay.Count;
            var objLock = new object();
            //异步循环需要更新的日期
            foreach (var dataTimeItem in listNeedUpDay)
            {
                //查询 summary 和 detailed 的数据
                DateTime startTime, endTime;
                startTime = dataTimeItem.DataTime.AddHours(12);
                endTime = startTime.AddHours(24);
                IEnumerable<ViewProductWorkingSummaryDetailedData> listSummaryDetailed;
                using (var viewProductWorkingSummaryDetailedDataBLL = new ViewProductWorkingSummaryDetailedDataBLL())
                {
                    Expression<Func<ViewProductWorkingSummaryDetailedData, bool>> condition =
                        t =>
                            t.StartTime >= startTime && t.EndTime < endTime && t.TherapyMode == dataTimeItem.TherapyMode;
                    listSummaryDetailed = viewProductWorkingSummaryDetailedDataBLL.GetByCondition(condition);
                }
                if (listSummaryDetailed == null || listSummaryDetailed.Count() < 1)
                {
                    CallProgressChanged(new ProgressChangedEventArgs(100, null));
                    return null;
                }
                var listDetailedRecord = new List<DetailedField>();
                var objLock2 = new object();
                Parallel.ForEach(listSummaryDetailed, bytes =>
                {
                    if (bytes != null)
                    {
                        try
                        {
                            var memoryStream = new MemoryStream(bytes.Content);
                            var all = Serializer.Deserialize<ICollection<DetailedField>>(memoryStream);
                            memoryStream.Close();
                            memoryStream.Dispose();
                            memoryStream = null;
                            lock (objLock2)
                            {
                                listDetailedRecord.AddRange(all);
                            }
                        }
                        catch
                        {
                        }
                    }
                });
                objLock2 = null;

                var productWorkingStatisticsData = new ProductWorkingStatisticsData();
                productWorkingStatisticsData.Id = DateTime.Now.ToGuid();
                productWorkingStatisticsData.ProductId = productId;
                productWorkingStatisticsData.TherapyMode = dataTimeItem.TherapyMode;
                productWorkingStatisticsData.DataTime = dataTimeItem.DataTime;
                productWorkingStatisticsData.TotalUsage = listSummaryDetailed.Sum(a => a.WorkingTime);

                productWorkingStatisticsData.CountAI =
                    Convert.ToInt32(listDetailedRecord.Count(a => a.Events == 2) /
                                    ((double)listSummaryDetailed.Sum(a => a.WorkingTime) / Const.MilliSecForOneHour));
                productWorkingStatisticsData.CountHI =
                    Convert.ToInt32(listDetailedRecord.Count(a => a.Events == 1) /
                                    ((double)listSummaryDetailed.Sum(a => a.WorkingTime) / Const.MilliSecForOneHour));
                productWorkingStatisticsData.CountAHI = productWorkingStatisticsData.CountAI +
                                                        productWorkingStatisticsData.CountHI;

                productWorkingStatisticsData.CountSnore =
                    Convert.ToInt32(listDetailedRecord.Count(a => a.Events == 4) /
                                    (double)listSummaryDetailed.Sum(a => a.WorkingTime) / Const.MilliSecForOneHour);
                productWorkingStatisticsData.CountPassive =
                    Convert.ToInt32(listDetailedRecord.Count(a => a.TriggerMode == 127) /
                                    (double)listSummaryDetailed.Sum(a => a.WorkingTime) / Const.MilliSecForOneHour);

                productWorkingStatisticsData.PressureMax = listDetailedRecord.Max(a => a.TargetPressure);
                productWorkingStatisticsData.PressureP95 =
                    listDetailedRecord.OrderBy(m => m.TargetPressure)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .TargetPressure;
                productWorkingStatisticsData.PressureMedian =
                    listDetailedRecord.OrderBy(m => m.TargetPressure)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .TargetPressure;

                productWorkingStatisticsData.FlowMax = listDetailedRecord.Max(m => m.CurrentFlow);
                productWorkingStatisticsData.FlowP95 =
                    listDetailedRecord.OrderBy(m => m.CurrentFlow)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .CurrentFlow;
                productWorkingStatisticsData.FlowMedian =
                    listDetailedRecord.OrderBy(m => m.CurrentFlow)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .CurrentFlow;

                productWorkingStatisticsData.LeakMax = listDetailedRecord.Max(m => m.Leak);
                productWorkingStatisticsData.LeakP95 =
                    listDetailedRecord.OrderBy(m => m.Leak)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .Leak;
                productWorkingStatisticsData.LeakMedian =
                    listDetailedRecord.OrderBy(m => m.Leak).Skip(listDetailedRecord.Count() / 2).FirstOrDefault().Leak;

                productWorkingStatisticsData.TidalVolumeMax = listDetailedRecord.Max(m => m.TidalVolume);
                productWorkingStatisticsData.TidalVolumeP95 =
                    listDetailedRecord.OrderBy(m => m.TidalVolume)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .TidalVolume;
                productWorkingStatisticsData.TidalVolumeMedian =
                    listDetailedRecord.OrderBy(m => m.TidalVolume)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .TidalVolume;

                productWorkingStatisticsData.MinuteVentilationMax = listDetailedRecord.Max(m => m.MinuteVentilation);
                productWorkingStatisticsData.MinuteVentilationP95 =
                    listDetailedRecord.OrderBy(m => m.MinuteVentilation)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .MinuteVentilation;
                productWorkingStatisticsData.MinuteVentilationMedian =
                    listDetailedRecord.OrderBy(m => m.MinuteVentilation)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .MinuteVentilation;

                productWorkingStatisticsData.SpO2Max = listDetailedRecord.Max(m => m.SpO2);
                productWorkingStatisticsData.SpO2P95 =
                    listDetailedRecord.OrderBy(m => m.SpO2)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .SpO2;
                productWorkingStatisticsData.SpO2Median =
                    listDetailedRecord.OrderBy(m => m.SpO2).Skip(listDetailedRecord.Count() / 2).FirstOrDefault().SpO2;

                productWorkingStatisticsData.PulseRateMax = listDetailedRecord.Max(m => m.PulseRate);
                productWorkingStatisticsData.PulseRateP95 =
                    listDetailedRecord.OrderBy(m => m.PulseRate)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .PulseRate;
                productWorkingStatisticsData.PulseRateMedian =
                    listDetailedRecord.OrderBy(m => m.PulseRate)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .PulseRate;

                productWorkingStatisticsData.RespiratoryRateMax = listDetailedRecord.Max(m => m.RespiratoryRate);
                productWorkingStatisticsData.RespiratoryRateP95 =
                    listDetailedRecord.OrderBy(m => m.RespiratoryRate)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .RespiratoryRate;
                productWorkingStatisticsData.RespiratoryRateMedian =
                    listDetailedRecord.OrderBy(m => m.RespiratoryRate)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .RespiratoryRate;

                productWorkingStatisticsData.IERatioMax = listDetailedRecord.Max(m => m.IERatio);
                productWorkingStatisticsData.IERatioP95 =
                    listDetailedRecord.OrderBy(m => m.IERatio)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .IERatio;
                productWorkingStatisticsData.IERatioMedian =
                    listDetailedRecord.OrderBy(m => m.IERatio)
                        .Skip(listDetailedRecord.Count() / 2)
                        .FirstOrDefault()
                        .IERatio;

                productWorkingStatisticsData.IPAPMax = listDetailedRecord.Max(m => m.IPAP);
                productWorkingStatisticsData.IPAPP95 =
                    listDetailedRecord.OrderBy(m => m.IPAP)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .IPAP;
                productWorkingStatisticsData.IPAPMedian =
                    listDetailedRecord.OrderBy(m => m.IPAP).Skip(listDetailedRecord.Count() / 2).FirstOrDefault().IPAP;

                productWorkingStatisticsData.EPAPMax = listDetailedRecord.Max(m => m.EPAP);
                productWorkingStatisticsData.EPAPP95 =
                    listDetailedRecord.OrderBy(m => m.EPAP)
                        .Skip((int)(listDetailedRecord.Count() * 0.95))
                        .FirstOrDefault()
                        .EPAP;
                productWorkingStatisticsData.EPAPMedian =
                    listDetailedRecord.OrderBy(m => m.EPAP).Skip(listDetailedRecord.Count() / 2).FirstOrDefault().EPAP;


                //根据产品Id,治疗模式、日期查询。如果已经统计过，那么更新，没有统计过则新增
                Expression<Func<ProductWorkingStatisticsData, bool>>
                    conditionStatistics =
                        t =>
                            t.ProductId == productId && t.TherapyMode == dataTimeItem.TherapyMode &&
                            t.DataTime == dataTimeItem.DataTime;
                IEnumerable<ProductWorkingStatisticsData> listProductWorkingStatisticsDatas;
                using (var productWorkingStatisticsDataBLL = new ProductWorkingStatisticsDataBLL())
                {
                    listProductWorkingStatisticsDatas =
                        productWorkingStatisticsDataBLL.GetByCondition(conditionStatistics);

                    if (listProductWorkingStatisticsDatas != null && listProductWorkingStatisticsDatas.Count() > 0)
                    {
                        //更新
                        productWorkingStatisticsData.Id = listProductWorkingStatisticsDatas.FirstOrDefault().Id;
                        productWorkingStatisticsDataBLL.Update(listProductWorkingStatisticsDatas);
                    }
                    else
                    {
                        //新增 
                        productWorkingStatisticsDataBLL.Insert(productWorkingStatisticsData);
                    }
                }
                progress++;
                // 前面有2% 
                var per = (int)(progress / (float)dayCount * 100 * 0.08) + 92;
                CallProgressChanged(new ProgressChangedEventArgs(per, null));
            } //end foreach

            var minDay = listNeedUpDay.Min(a => a.DataTime);
            var maxDay = listNeedUpDay.Max(a => a.DataTime);
            listNeedUpDay.Clear();
            listNeedUpDay = null;
            return minDay.ToShortDateString() + " - " + maxDay.ToShortDateString();
        }

        #endregion

        #region 下载文件 无索引文件

        private void processRespircareDatFile(DoWorkEventArgs e, string respircareFilePath)
        {
        }

        #endregion

        #region 下载文件公用部分

        /// <summary>
        /// 取得SY_RMS.txt文件的第一行的SN和最大运行时间的数据
        /// </summary>
        /// <param Name="indexFileName"></param>
        /// <returns></returns>
        public static IndexFileField GetIndexFileField(string indexFileName)
        {
            if (!File.Exists(indexFileName))
            {
                return null;
            }

            ICollection<IndexFileField> RmsFileList = new Collection<IndexFileField>();
            //读取索引文件，

            foreach (var line in File.ReadLines(indexFileName))
            {
                var item = new IndexFileField();
                //Sy_RMS:302200003311180015,APAP20_V030_131105,20140613171209,000030hrs;
                if (line.StartsWith("Sy_RMS:") && line.EndsWith("hrs;") && line.Contains(","))
                {
                    var tmp = line.TrimEnd('h', 'r', 's', ';');
                    tmp = tmp.Remove(0, 7);
                    var c = tmp.Split(',');
                    item.SerialNumber = c[0];
                    item.ProductVersion = c[1];

                    ProductModels pm;
                    Enum.TryParse(c[1].Substring(0, c[1].IndexOf('_')), true, out pm);
                    item.ProductModel = (byte)pm;

                    item.TotalWorkingTime = c[3].GetInt(0);
                    RmsFileList.Add(item);
                }
            }
            //取得第一个设备Sn
            var v = from a in RmsFileList
                    where a.SerialNumber == RmsFileList.FirstOrDefault().SerialNumber
                    select a;

            //取得第一个Sn的最大总运行时间
            var tmpResult = v.Where(m => m.TotalWorkingTime == v.Max(n => n.TotalWorkingTime)).FirstOrDefault();
            RmsFileList.Clear();
            RmsFileList = null;
            //IndexFileField result = tmpResult.Clone() as IndexFileField;
            return tmpResult;
        }

        #endregion

        #endregion
    }
}