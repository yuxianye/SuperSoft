using SuperSoft.Model;
using SuperSoft.Utility.Windows;
using SuperSoft.View.ViewModel;
using System.Windows;

namespace SuperSoft.View.UserControls

{
    public class PatientInfoViewModel : MyViewModelBase
    {
        private string expanderHeader;

        private bool isExpanded = true;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { Set(ref isExpanded, value); }
        }

        public string ExpanderHeader
        {
            get { return expanderHeader; }
            set { Set(ref expanderHeader, value); }
        }

        #region 当前选中的患者 SelectedPatient

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                Set(ref selectedPatient, value);
                if (Equals(selectedPatient, value))
                {
                    initPatientInfo();
                }
            }
        }

        #endregion

        #region PatientInfoVisibility

        private void initPatientInfo()
        {
            if (Equals(SelectedPatient, null))
            {
                PatientInfoVisibility = Visibility.Collapsed;
            }
            else
            {
                ExpanderHeader = ResourceHelper.LoadString("PatientInfo") + " -> " + SelectedPatient.FirstName + " " + SelectedPatient.LastName;
                PatientInfoVisibility = Visibility.Visible;
            }
        }

        private Visibility patientInfoVisibility = Visibility.Collapsed;

        public Visibility PatientInfoVisibility
        {
            get { return patientInfoVisibility; }
            set { Set(ref patientInfoVisibility, value); }

        }

        #endregion
    }
}