using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility;
using SuperSoft.Utility.Windows;
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
                //CommandManager.InvalidateRequerySuggested();
                //if (!Equals(value, null))
                //{
                //    //AllPatientList = getAllPatient();
                //}
            }
        }

        #endregion

        #region PatientCount

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
