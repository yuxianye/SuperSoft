using Microsoft.Practices.ServiceLocation;
using SuperSoft.Utility;
using SuperSoft.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SuperSoft.App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            registerService();
            registerMessenger();
            //初始化资源
            InitResourceAndSetCultureInfo();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            unRegisterMessenger();
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Unregister<SuperSoft.View.ViewModel.ViewModelLocator>();
        }

        private void registerService()
        {
            ServiceLocator.SetLocatorProvider(() => GalaSoft.MvvmLight.Ioc.SimpleIoc.Default);
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<SuperSoft.View.ViewModel.ViewModelLocator>();
        }

        /// <summary>
        /// 注册MVVMLight消息
        /// </summary>
        private void registerMessenger()
        {
            //注册消息，改变界面语言
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<object>(this, Model.MessengerToken.SwitchLanguage, switchLanguage);

        }

        /// <summary>
        /// 取消注册MVVMlight消息
        /// </summary>
        private void unRegisterMessenger()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<object>(this, Model.MessengerToken.SwitchLanguage, switchLanguage);
        }

        /// <summary>
        /// 根据配置文件设置区域语言环境、加载区域语言环境资源.
        /// 程序集名称和语言设置等采用硬编码
        /// </summary>
        private void InitResourceAndSetCultureInfo()
        {
            ResourceDictionary langRd = null;
            var languageString = ConfigHelper.GetAppSetting(@"Language");
            CultureInfo culture;
            try
            {
                //配置文件没有设置界面语言，那么使用环境语言或者英文，默认无语言配置。
                if (string.IsNullOrWhiteSpace(languageString))
                {
                    var currentCulture = Thread.CurrentThread.CurrentUICulture;
                    //如果由当前环境的语言那么使用当前环境的语言
                    if (System.IO.File.Exists(Utility.Const.AppPath + @"SuperSoft.Resource." + currentCulture.Name + @".dll"))
                    {
                        languageString = currentCulture.Name;
                    }
                    else//没有则使用英文
                    {
                        languageString = @"en-US";
                    }
                    ConfigHelper.AddAppSetting(@"Language", languageString);
                }
                //当前设置的语言，然后设置当前线程的区域
                culture = new CultureInfo(languageString);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                //加载对应环境的资源
                langRd =
                    LoadComponent(
                        new Uri(
                            @"/SuperSoft.Resource." + languageString + @";component/DefaultResources.xaml",
                            UriKind.Relative)) as ResourceDictionary;
                if (Resources.MergedDictionaries.Count > 0)
                {
                    Resources.MergedDictionaries.Clear();
                }
                //加载默认的资源
                ResourceDictionary defaultRd = LoadComponent(
                new Uri(
                    @"/SuperSoft.Resource.Default;component/DefaultResources.xaml",
                    UriKind.Relative)) as ResourceDictionary;

                Resources.MergedDictionaries.Add(defaultRd);
                Resources.MergedDictionaries.Add(langRd);

                //MVVMLight 的 ViewModelLocator
                ResourceDictionary mvvmRes = new ResourceDictionary();
                //mvvmRes.Add(@"Locator", new SuperSoft.View.ViewModel.ViewModelLocator());
                mvvmRes.Add(@"Locator", ServiceLocator.Current.GetInstance<SuperSoft.View.ViewModel.ViewModelLocator>());
                Resources.MergedDictionaries.Add(mvvmRes);

                //启动对象
                StartupUri = new Uri(@"/SuperSoft.App;component/MainWindow.xaml",
                    UriKind.Relative);
                //LogHelper.Info("app started, Language:" + languageString);

                //设置标题,第一次启动时不执行，在界面改变语言之后执行。
                if (!Equals(Application.Current.MainWindow, null))
                {
                    Application.Current.MainWindow.Title = Utility.Windows.ResourceHelper.LoadString(@"AppName")
                        + @" " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                    //Application.Current.MainWindow.Title = Utility.Windows.ResourceHelper.LoadString(@"AppName")
                    //    + " "
                    //    + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major
                    //    + "."
                    //    + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;

                    //Application.Current.MainWindow.Title = Utility.Windows.ResourceHelper.LoadString("AppName")
                    //+ " "
                    //+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major
                    //+ "."
                    //+ System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
                }
            }
            //初始化错误提示
            catch (Exception e)
            {
                LogHelper.Error(e);
                MessageBox.Show(e.Message, @"Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            finally
            {
                langRd = null;
                languageString = null;
                culture = null;
            }
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="obj"></param>
        private void switchLanguage(object obj)
        {
            InitResourceAndSetCultureInfo();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
