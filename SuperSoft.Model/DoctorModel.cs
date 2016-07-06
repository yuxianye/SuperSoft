using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Model
{
    public class DoctorModel : Utility.MyNotifyClassBase
    {
        #region Id

        private Guid id;

        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                    OnPropertyChanged("Id");
            }
        }

        #endregion

        #region FirstName

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                    OnPropertyChanged("FirstName");
            }
        }

        #endregion

        #region LastName

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                    OnPropertyChanged("LastName");
            }
        }

        #endregion


    }
}
