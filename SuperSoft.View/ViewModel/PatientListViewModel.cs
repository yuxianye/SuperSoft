using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
using SuperSoft.View.UserControls;
using SuperSoft.View.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SuperSoft.View.ViewModel
{
    public class PatientListViewModel : MyViewModelBase
    {
        public PatientListViewModel()
        {
            IsParameterRepeatChanged = true;
        }
        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();

            if (Parameter == null)
            {
                initPatientList(new KeyValuePair<string, string>());
            }
            else
            {
                initPatientList((KeyValuePair<string, string>)Parameter);
            }
        }

        private void initPatientList(KeyValuePair<string, string> searchCondition)
        {
            if (AllPatientList != null)
            {
                AllPatientList.Clear();
            }
            AllPatientList = getAllPatient(searchCondition);
            if (AllPatientList != null && AllPatientList.Count() > 0)//有医生
            {
                if (StaticDatas.CurrentSelectedPatient == null) //默认选择第一个
                {
                    SelectedPatient = AllPatientList.FirstOrDefault();
                }
                else//选择原有的医生
                {
                    var v = AllPatientList.Where(a => a.Id == StaticDatas.CurrentSelectedPatient.Id);
                    if (v != null && v.Count() > 0)
                    {
                        SelectedPatient = v.FirstOrDefault();
                    }
                    else
                    {
                        SelectedPatient = AllPatientList.FirstOrDefault();
                    }
                }
            }
            else
            {
                SelectedPatient = null;
            }
            if (AllPatientList == null || AllPatientList.Count() < 1)
            {
                AllPatientListVisibility = Visibility.Collapsed;
            }
            else
            {
                AllPatientListVisibility = Visibility.Visible;
                PatientCount = ResourceHelper.LoadString("PatientListView_PatientCount") + AllPatientList.Count();
            }
        }

        ///// <summary>
        ///// 内部采用异步线程取得所有患者,搜索条件增加时在此扩展
        ///// </summary>
        ///// <returns></returns>
        //private ICollection<Patient> getAllPatient(KeyValuePair<string, string> searchCondition)
        //{
        //    using (Task<ICollection<Patient>> task = Task.Run<ICollection<Patient>>(new Func<ICollection<Patient>>(() =>
        //    {
        //        //return result;
        //        int count;
        //        using (PatientBLL PatientBLL = new PatientBLL())
        //        {
        //            ICollection<Patient> result = null;
        //            switch (searchCondition.Key)
        //            {
        //                case "FirstName":
        //                    //result = PatientBLL.SelectByFirstName(searchCondition.Value);
        //                    break;
        //                case "LastName":
        //                    //result = PatientBLL.SelectByFirstName(searchCondition.Value);
        //                    break;
        //                default:
        //                    result = PatientBLL.SelectPaging(1, short.MaxValue, out count);
        //                    break;
        //            }
        //            return result;
        //        }
        //    })))
        //    {
        //        return task.Result;
        //    }
        //}

        private ICollection<Patient> getAllPatient(KeyValuePair<string, string> searchCondition)
        {
            using (Task<ICollection<Patient>> task = Task.Run<ICollection<Patient>>(new Func<ICollection<Patient>>(() =>
            {
                //    //return result;
                int count;
                using (PatientBLL patientBLL = new PatientBLL())
                {
                    ICollection<Patient> result = null;
                    switch (searchCondition.Key)
                    {
                        case "FirstName":
                            result = patientBLL.SelectByFirstName(searchCondition.Value);
                            break;
                        case "LastName":
                            result = patientBLL.SelectByLastName(searchCondition.Value);
                            break;
                        case "DateOfBirth":
                            var tmpDateOfBirth = DateTime.MinValue;
                            DateTime.TryParse(searchCondition.Value, out tmpDateOfBirth);
                            result = patientBLL.SelectByDateOfBirth(tmpDateOfBirth);
                            break;
                        case "Weight":
                            int tmpWeight = -1;
                            if (int.TryParse(searchCondition.Value, out tmpWeight))
                            {
                                result = patientBLL.SelectByWeight(tmpWeight);
                            }
                            else
                            {
                                result = null;
                            }
                            break;
                        case "Height":
                            int tmpHeight = -1;
                            if (int.TryParse(searchCondition.Value, out tmpHeight))
                            {
                                int.TryParse(searchCondition.Value, out tmpHeight);
                                result = patientBLL.SelectByHeight(tmpHeight);
                            }
                            else
                            {
                                result = null;
                            }
                            break;
                        case "Gender":
                            bool? tmpGender = null;

                            if (!string.IsNullOrWhiteSpace(searchCondition.Value))
                            {
                                if (searchCondition.Value.Contains(ResourceHelper.LoadString("Male")))
                                {
                                    tmpGender = true;
                                }
                                if (searchCondition.Value.Contains(ResourceHelper.LoadString("Female")))
                                {
                                    tmpGender = false;
                                }
                            }
                            if (tmpGender == null)
                            {
                                result = null;
                            }
                            else
                            {
                                result = patientBLL.SelectByGender(tmpGender.Value);
                            }
                            break;
                        case "EMail":
                            result = patientBLL.SelectByEMail(searchCondition.Value);
                            break;
                        case "TelephoneNumbers":
                            result = patientBLL.SselectByTelephoneNumbers(searchCondition.Value);
                            break;
                        case "PostalCode":
                            result = patientBLL.SelectByPostalCode(searchCondition.Value);
                            break;
                        case "Address":
                            result = patientBLL.SelectByAddress(searchCondition.Value);
                            break;
                        case "Diagnosis":
                            result = patientBLL.SelectByDiagnosis(searchCondition.Value);
                            break;
                        default:
                            result = patientBLL.SelectPaging(1, short.MaxValue, out count);
                            break;
                    }
                    return result;
                }
            })))
            {
                return task.Result;
            }
        }

        #region AllPatientList

        private ICollection<Patient> allPatientList;

        public ICollection<Patient> AllPatientList
        {
            get { return allPatientList; }
            set { Set(ref allPatientList, value); }
        }

        #endregion

        #region SelectedPatient

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                Set(ref selectedPatient, value);
                StaticDatas.CurrentSelectedPatient = value;
                CommandManager.InvalidateRequerySuggested();
                if (!Equals(selectedPatient, null))
                {
                    initTherapyModeList();
                }
            }
        }

        #endregion

        #region 治疗模式 没有治疗模式则 后面的统计信息等不显示，由治疗模式改变出发加载数据

        /// <summary>
        /// 初始化治疗模式列表，根据产品的运行数据，加载不同的治疗模式，未使用的模式不列出。
        /// 查询视图ViewProductWorkingStatisticsData中的治疗模式种类的数量
        /// </summary>
        private void initTherapyModeList()
        {
            var tmpTherapyModeList = new Collection<KeyValuePair<TherapyMode, string>>();
            var viewProductWorkingStatisticsDataBLL = new ViewProductWorkingStatisticsDataBLL();
            //Expression<Func<ViewProductWorkingStatisticsData, bool>> condition = t => t.PatientId == SelectedPatient.Id;
            //var tmp = viewProductWorkingStatisticsDataBLL.GetByCondition(condition).GroupBy(a => a.TherapyMode);
            var tmp = viewProductWorkingStatisticsDataBLL.SelectTherapyModeByPatientId(SelectedPatient.Id);
            viewProductWorkingStatisticsDataBLL.Dispose();
            viewProductWorkingStatisticsDataBLL = null;
            if (tmp != null && tmp.Count() > 0)
            {
                //foreach (var v in tmp)
                //{
                //    var tm = (TherapyMode)v.Key;
                //    tmpTherapyModeList.Add(new KeyValuePair<TherapyMode, string>(tm, tm.ToDescription()));
                //}
                StaticDatas.IsCurrentSelectedPatientHaveProduct = true;
                TherapyModeList = tmp;
                TherapyMode = TherapyModeList.FirstOrDefault();
                TherapyModelInfoVisibility = Visibility.Visible;
            }
            else
            {
                StaticDatas.IsCurrentSelectedPatientHaveProduct = false;
                //没有治疗模式不显示统计信息
                TherapyMode = default(KeyValuePair<TherapyMode, string>);
                TherapyModelInfoVisibility = Visibility.Collapsed;
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private IEnumerable<KeyValuePair<TherapyMode, string>> therapyModeList;

        public IEnumerable<KeyValuePair<TherapyMode, string>> TherapyModeList
        {
            get { return therapyModeList; }
            set { Set(ref therapyModeList, value); }
        }

        /// <summary>
        /// 选择治疗模式
        /// </summary>
        private KeyValuePair<TherapyMode, string> therapyMode;

        /// <summary>
        /// 选择的治疗模式
        /// </summary>
        public KeyValuePair<TherapyMode, string> TherapyMode
        {
            get { return therapyMode; }
            set
            {
                Set(ref therapyMode, value);
                //TaskAsyncHelper.RunAsync(initData, initDataComplete);
                initData();
            }
        }

        #endregion

        private void initData()
        {
            //改变子ViewModel的数据
            PatientInfoViewModel.SelectedPatient = selectedPatient;
            ProductInfoViewModel.SelectedPatient = selectedPatient;
            StatisticsInfoViewModel.TherapyMode = TherapyMode.Key;
            initViewProductWorkingStatisticsDataList();
        }

        private void initViewProductWorkingStatisticsDataList()
        {
            var viewProductWorkingStatisticsDataBLL = new ViewProductWorkingStatisticsDataBLL();
            Expression<Func<ViewProductWorkingStatisticsData, bool>> condition =
                t => t.PatientId == SelectedPatient.Id && t.TherapyMode == (byte)TherapyMode.Key;
            //var tmp = viewProductWorkingStatisticsDataBLL.GetByCondition(condition);
            var tmp = viewProductWorkingStatisticsDataBLL.SelectByPatientIdTherapyMode(SelectedPatient.Id, (byte)TherapyMode.Key);

            StatisticsInfoViewModel.ViewProductWorkingStatisticsDataList = tmp;
            viewProductWorkingStatisticsDataBLL.Dispose();
            viewProductWorkingStatisticsDataBLL = null;
        }


        #region PatientInfoViewModel

        private PatientInfoViewModel patientInfoViewModel = new PatientInfoViewModel();

        public PatientInfoViewModel PatientInfoViewModel
        {
            get { return patientInfoViewModel; }
            set { Set(ref patientInfoViewModel, value); }
        }

        #endregion

        #region ProductInfoViewModel

        private ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel();

        public ProductInfoViewModel ProductInfoViewModel
        {
            get { return productInfoViewModel; }
            set { Set(ref productInfoViewModel, value); }
        }

        #endregion

        #region StatisticsInfoViewModel

        private StatisticsInfoViewModel statisticsInfoViewModel = new StatisticsInfoViewModel();

        public StatisticsInfoViewModel StatisticsInfoViewModel
        {
            get { return statisticsInfoViewModel; }
            set { Set(ref statisticsInfoViewModel, value); }
        }

        #endregion

        #region TherapyModelInfoVisibility

        private Visibility therapyModelInfoVisibility = Visibility.Collapsed;

        public Visibility TherapyModelInfoVisibility
        {
            get { return therapyModelInfoVisibility; }
            set { Set(ref therapyModelInfoVisibility, value); }
        }

        #endregion

        #region AllPatientListVisibility

        private Visibility allPatientListVisibility;

        public Visibility AllPatientListVisibility
        {
            get { return allPatientListVisibility; }
            private set
            {
                Set(ref allPatientListVisibility, value);

            }
        }

        #endregion

        #region PatientCount

        private string patientCount;

        public string PatientCount
        {
            get { return patientCount; }
            private set
            {
                Set(ref patientCount, value);
            }
        }

        #endregion

    }
}
