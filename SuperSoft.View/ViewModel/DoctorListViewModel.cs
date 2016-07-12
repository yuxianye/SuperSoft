using SuperSoft.BLL;
using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SuperSoft.View.ViewModel
{
    public class DoctorListViewModel : MyViewModelBase
    {
        public DoctorListViewModel()
        {
            IsParameterRepeatChanged = true;
        }

        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();

            if (Parameter == null)
            {
                initDoctorList(new KeyValuePair<string, string>());
            }
            else
            {
                initDoctorList((KeyValuePair<string, string>)Parameter);
            }
        }

        private void initDoctorList(KeyValuePair<string, string> searchCondition)
        {
            if (AllDoctorList != null)
            {
                AllDoctorList.Clear();
            }
            var tt = getAllDoctor(searchCondition);
            if (tt != null && tt.Count > 0)
            {
                AllDoctorList = tt.OrderByDescending(a => a.Id).ToList();
            }
            if (AllDoctorList != null && AllDoctorList.Count() > 0)//有医生
            {
                if (StaticDatas.CurrentSelectedDoctor == null) //默认选择第一个
                {
                    SelectedDoctor = AllDoctorList.FirstOrDefault();
                }
                else//选择原有的医生
                {
                    var v = AllDoctorList.Where(a => a.Id == StaticDatas.CurrentSelectedDoctor.Id);
                    if (v != null && v.Count() > 0)
                    {
                        SelectedDoctor = v.FirstOrDefault();
                    }
                    else
                    {
                        SelectedDoctor = AllDoctorList.FirstOrDefault();
                    }
                }
            }
            else
            {
                SelectedDoctor = null;
            }
            if (AllDoctorList == null || AllDoctorList.Count() < 1)
            {
                AllDoctorListVisibility = Visibility.Collapsed;
            }
            else
            {
                AllDoctorListVisibility = Visibility.Visible;
                DoctorCount = ResourceHelper.LoadString("DoctorListView_DoctorCount") + AllDoctorList.Count();
            }
        }

        /// <summary>
        /// 内部采用异步线程取得所有患者,搜索条件增加时在此扩展
        /// </summary>
        /// <returns></returns>
        private ICollection<Doctor> getAllDoctor(KeyValuePair<string, string> searchCondition)
        {
            using (Task<ICollection<Doctor>> task = Task.Run<ICollection<Doctor>>(new Func<ICollection<Doctor>>(() =>
            {
                //return result;
                int count;
                using (DoctorBLL doctorBLL = new DoctorBLL())
                {
                    ICollection<Doctor> result;
                    switch (searchCondition.Key)
                    {
                        case "FirstName":
                            result = doctorBLL.SelectByFirstName(searchCondition.Value);
                            break;
                        case "LastName":
                            result = doctorBLL.SelectByLastName(searchCondition.Value);
                            break;
                        default:
                            result = doctorBLL.SelectPaging(1, short.MaxValue, out count);
                            break;
                    }
                    return result;
                }
            })))
            {
                return task.Result;
            }
        }

        #region AllDoctorList

        private ICollection<Doctor> allDoctorList;

        public ICollection<Doctor> AllDoctorList
        {
            get { return allDoctorList; }
            set
            {
                //RaisePropertyChanged("AllDoctorList");
                Set(ref allDoctorList, value);
            }
        }

        #endregion

        #region 取得所有患者 getAllPatient 

        /// <summary>
        /// 取得所有患者
        /// </summary>
        /// <returns></returns>
        private ICollection<Patient> getAllPatient()
        {
            using (PatientBLL patientBLL = new PatientBLL())
            {

                var tmpList = patientBLL.SelectByDoctorId(SelectedDoctor.Id);
                if (tmpList != null && tmpList.Count() > 0)
                {
                    AllPatientListVisibility = Visibility.Visible;
                    PatientCount = ResourceHelper.LoadString("DoctorListView_PatientCount") + tmpList.Count();
                }
                else
                {
                    AllPatientListVisibility = Visibility.Collapsed;
                }
                return tmpList;
            }
        }

        #endregion

        #region AllDoctorListVisibility

        private Visibility allDoctorListVisibility = Visibility.Collapsed;

        public Visibility AllDoctorListVisibility
        {
            get { return allDoctorListVisibility; }
            set { Set(ref allDoctorListVisibility, value); }
        }

        #endregion

        #region 当前选中的医生 SelectedDoctor

        private Doctor selectedDoctor;

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                Set(ref selectedDoctor, value);
                StaticDatas.CurrentSelectedDoctor = value;
                CommandManager.InvalidateRequerySuggested();
                if (!Equals(value, null))
                {
                    AllPatientList = getAllPatient();
                }
            }
        }

        #endregion

        #region AllPatientList

        private ICollection<Patient> allPatientList;

        /// <summary>
        /// </summary>
        public ICollection<Patient> AllPatientList
        {
            get { return allPatientList; }
            set { Set(ref allPatientList, value); }

        }

        #endregion

        #region AllPatientListVisibility

        private Visibility allPatientListVisibility = Visibility.Collapsed;

        /// <summary>
        /// AllPatientListVisibility
        /// </summary>
        public Visibility AllPatientListVisibility
        {
            get { return allPatientListVisibility; }
            set { Set(ref allPatientListVisibility, value); }

        }

        #endregion

        #region 当前医生看护患者的数量

        private string patientCount;

        /// <summary>
        /// PatientCount
        /// </summary>
        public string PatientCount
        {
            get { return patientCount; }
            set { Set(ref patientCount, value); }

        }

        #endregion

        #region 当前医生的数量

        private string doctorCount;

        /// <summary>
        /// DoctorCount
        /// </summary>
        public string DoctorCount
        {
            get { return doctorCount; }
            set { Set(ref doctorCount, value); }
        }

        #endregion
    }
}
