/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SuperSoft.View"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace SuperSoft.View.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view Model in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator : Utility.MyClassBase
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and Model
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and Model
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PatientListViewModel>();
            SimpleIoc.Default.Register<PatientAddViewModel>();
            SimpleIoc.Default.Register<PatientDeleteViewModel>();
            SimpleIoc.Default.Register<PatientEditViewModel>();

            SimpleIoc.Default.Register<DoctorListViewModel>();
            SimpleIoc.Default.Register<DoctorAddViewModel>();
            SimpleIoc.Default.Register<DoctorDeleteViewModel>();
            SimpleIoc.Default.Register<DoctorEditViewModel>();



            //SimpleIoc.Default.Register<PatientListViewModel>();
            //SimpleIoc.Default.Register<PatientHomeViewModel>();

            //SimpleIoc.Default.Register<AutoAnalysisSettingsViewModel>();
            //SimpleIoc.Default.Register<ChannelSettingsViewModel>();


            SimpleIoc.Default.Register<SwitchLanguageViewModel>();
            SimpleIoc.Default.Register<HelpViewModel>();

            SimpleIoc.Default.Register<AddAdviseViewModel>();

        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        #region Patient

        public PatientListViewModel PatientListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientListViewModel>();
            }
        }

        public PatientAddViewModel PatientAddViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientAddViewModel>();
            }
        }
        public PatientDeleteViewModel PatientDeleteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientDeleteViewModel>();
            }
        }

        public PatientEditViewModel PatientEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PatientEditViewModel>();
            }
        }

        #endregion

        #region Doctor

        public DoctorListViewModel DoctorListViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DoctorListViewModel>();
            }
        }

        public DoctorAddViewModel DoctorAddViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DoctorAddViewModel>();
            }
        }

        public DoctorDeleteViewModel DoctorDeleteViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DoctorDeleteViewModel>();
            }
        }

        public DoctorEditViewModel DoctorEditViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DoctorEditViewModel>();
            }
        }

        #endregion





        //public PatientListViewModel PatientListViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<PatientListViewModel>();
        //    }
        //}


        //public PatientHomeViewModel PatientHomeViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<PatientHomeViewModel>();
        //    }
        //}




        public SwitchLanguageViewModel SwitchLanguageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SwitchLanguageViewModel>();
            }
        }

        public HelpViewModel HelpViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HelpViewModel>();
            }
        }

        //public AutoAnalysisSettingsViewModel AutoAnalysisSettingsViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<AutoAnalysisSettingsViewModel>();
        //    }
        //}

        //public ChannelSettingsViewModel ChannelSettingsViewModel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ChannelSettingsViewModel>();
        //    }
        //}

        public AddAdviseViewModel AddAdviseViewModel

        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddAdviseViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModel

            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Register<PatientListViewModel>();
            SimpleIoc.Default.Unregister<PatientAddViewModel>();
            SimpleIoc.Default.Unregister<PatientDeleteViewModel>();
            SimpleIoc.Default.Unregister<PatientEditViewModel>();


            //SimpleIoc.Default.Unregister<PatientHomeViewModel>();



            SimpleIoc.Default.Unregister<SwitchLanguageViewModel>();
            SimpleIoc.Default.Unregister<HelpViewModel>();
            SimpleIoc.Default.Unregister<AddAdviseViewModel>();

        }
        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            Cleanup();

        }
    }
}