using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;
using System;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Markup;
using System.Windows.Data;
using System.Collections;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using SuperSoft.Model;
using SuperSoft.View.View;
using SuperSoft.View;
using System.Windows.Media.Imaging;

namespace SuperSoft.View.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : MyViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            initCommand();
            registerMessenger();
        }

        #region Command和消息初始化

        /// <summary>
        /// 初始化Command
        /// </summary>
        private void initCommand()
        {
            PatientAddCommand = new RelayCommand(OnExecutePatientAddCommand);
            PatientListCommand = new RelayCommand(OnExecutePatientListCommand);

            RespiratoryEventAnalysisCommand = new RelayCommand(OnExecuteRespiratoryEventAnalysisCommand);
            SnoreAnalysisCommand = new RelayCommand(OnExecuteSnoreAnalysisCommand);

            PreviousEventsCommand = new RelayCommand(OnExecutePreviousEventsCommand);
            NextEventsCommand = new RelayCommand(OnExecuteNextEventsCommand);

            GraphZoomInCommand = new RelayCommand(OnExecuteGraphZoomInCommand);
            GraphZoomOutCommand = new RelayCommand(OnExecuteGraphZoomOutCommand);

            ChannelSettingsCommand = new RelayCommand(OnExecuteChannelSettingsCommand);
            AutoAnalysisSettingsCommand = new RelayCommand(OnExecuteAutoAnalysisSettingsCommand);

            //PatientListCommand = new RelayCommand(OnExecutePatientListCommand, OnCanExecutePatientListCommand);
            SwitchLanguageCommand = new RelayCommand(OnExecuteSwitchLanguageCommand);

            HelpCommand = new RelayCommand(OnExecuteHelpCommand);

        }

        /// <summary>
        /// 注册MVVMLight消息
        /// </summary>
        private void registerMessenger()
        {
            Messenger.Default.Register<ViewInfo>(this, Model.MessengerToken.Navigate, Navigate);

            Messenger.Default.Register<object>(this, Model.MessengerToken.ClosePopup, ClosePopup);

            Messenger.Default.Register<MenuInfo>(this, Model.MessengerToken.SetMenuStatus, SetMenuStatus);
        }

        /// <summary>
        /// 取消注册MVVMlight消息
        /// </summary>
        private void unRegisterMessenger()
        {
            Messenger.Default.Unregister<ViewInfo>(this, Model.MessengerToken.Navigate, Navigate);

            Messenger.Default.Unregister<object>(this, Model.MessengerToken.ClosePopup, ClosePopup);

            Messenger.Default.Unregister<MenuInfo>(this, Model.MessengerToken.SetMenuStatus, SetMenuStatus);
        }
        #endregion

        #region 消息关联函数，主要包括页面导航管理、菜单状态管理等


        private void Navigate(ViewInfo viewInfo)
        {
            UserControlBase view;

            if (Equals(viewInfo.Parameter, null))
            {
                view =
                    System.Reflection.Assembly.Load(@"SuperSoft.View")
                        .CreateInstance(@"SuperSoft.View.View." + viewInfo.ViewName.ToString()) as UserControlBase;
            }
            else
            {
                view = System.Reflection.Assembly.Load(@"SuperSoft.View").
                    CreateInstance(@"SuperSoft.View.View." + viewInfo.ViewName.ToString(), true, System.Reflection.BindingFlags.Default,
                        null, new[] { viewInfo.Parameter }, null, null) as UserControlBase;
            }
            if (view == null)
            {//未找到视图，抛出异常
                throw new Exception(viewInfo.ViewName.ToString());
            }
            MyViewModelBase viewModelBase = null;
            viewModelBase = view.DataContext as MyViewModelBase;
            if (!Equals(null, viewModelBase))
            {
                viewModelBase.Parameter = viewInfo.Parameter;
            }

            switch (viewInfo.ViewType)
            {
                case ViewType.Popup://模式对话框
                    MahApps.Metro.Controls.MetroWindow popupWindows = new MahApps.Metro.Controls.MetroWindow();
                    popupWindows.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    popupWindows.Style = Utility.Windows.ResourceHelper.FindResource(@"CleanWindowStyleKey") as Style;
                    popupWindows.GlowBrush = Utility.Windows.ResourceHelper.FindResource(@"AccentColorBrush") as System.Windows.Media.Brush;
                    //变更语言时，动态更新对话框Title,Title取决于控件的Tag
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.TitleProperty, new Binding(@"Tag") { Source = view });
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.WidthProperty, new Binding(@"Width") { Source = view });
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.HeightProperty, new Binding(@"Height") { Source = view });
                    popupWindows.Owner = Application.Current.MainWindow;
                    popupWindows.ResizeMode = ResizeMode.NoResize;
                    popupWindows.IsCloseButtonEnabled = false;
                    popupWindows.ShowCloseButton = false;
                    popupWindows.ShowInTaskbar = false;
                    popupWindows.Focus();
                    view.Margin = new Thickness(2);
                    popupWindows.Content = view;
                    popupWindowsStack.Push(popupWindows);
                    popupWindows.ShowDialog();
                    if (!Equals(view, null))
                    {
                        view.Dispose();
                        view = null;
                        GC.Collect();
                    }
                    break;

                case ViewType.View://普通视图
                                   //页面切换效果
                                   //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<object>(null, Model.MessengerToken.NavigateSplash);
                    if (!Equals(MainContent, null))
                    {
                        MainContent.Dispose();
                        MainContent = null;
                        GC.Collect();
                    }
                    MainContent = view;
                    break;

                case ViewType.SingleWindow://单个视图。主要为了显示帮助窗口
                    MahApps.Metro.Controls.MetroWindow singleWindows = new MahApps.Metro.Controls.MetroWindow();
                    singleWindows.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    //popupWindows.Style = Utility.ResourceHelper.FindResource("CleanWindowStyleKey") as Style;
                    singleWindows.GlowBrush = Utility.Windows.ResourceHelper.FindResource(@"AccentColorBrush") as System.Windows.Media.Brush;
                    //变更语言时，动态更新对话框Title,Title取决于控件的Tag
                    singleWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.TitleProperty, new Binding(@"Tag") { Source = view });
                    singleWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.WidthProperty, new Binding(@"Width") { Source = view });
                    singleWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.HeightProperty, new Binding(@"Height") { Source = view });
                    singleWindows.Content = view;
                    singleWindows.ResizeMode = ResizeMode.NoResize;
                    //singleWindows.ShowIconOnTitleBar = true;
                    singleWindows.Icon = new BitmapImage(new Uri("pack://application:,,,/SuperSoft.Resource.Default;component/Images/Logo_White.png", UriKind.Absolute));
                    //singleWindows.Icon = Utility.Windows.ResourceHelper.FindResource("pack://application:,,,/SuperSoft.Resource.Default;component/Images/Logo_White.png");
                    //singleWindows.IsCloseButtonEnabled = true;
                    //singleWindows.ShowCloseButton = true;
                    //singleWindows.Owner = Application.Current.MainWindow;
                    singleWindows.Topmost = true;
                    singleWindows.Show();
                    singleWindows.Focus();
                    break;
            }
        }

        private Stack popupWindowsStack = new Stack();

        /// <summary>
        /// 关闭Popup
        /// </summary>
        /// <param name="obj"></param>
        private void ClosePopup(object obj)
        {
            //多个Popup时需要入栈
            if (popupWindowsStack.Count > 0)
            {
                MetroWindow popupWindow = popupWindowsStack.Pop() as MetroWindow;
                popupWindow.Close();
                popupWindow = null;
                GC.Collect();

            }
        }

        /// <summary>
        /// 设置菜单的状态
        /// </summary>
        private void SetMenuStatus(MenuInfo menuInfo)
        {
            switch (menuInfo.MenuName)
            {
                case MenuName.RespiratoryEventAnalysisMenu:
                    RespiratoryEventAnalysisCommandIsEnabled = menuInfo.IsEnabled;
                    break;
                case MenuName.SnoreAnalysisMenu:
                    // SnoreAnalysisCommandIsEnabled = menuInfo.IsEnabled;
                    SnoreAnalysisCommandIsEnabled = false;//验收时不需要鼾声相关的内容和功能
                    break;
                case MenuName.PreviousEventsMenu:
                    PreviousEventsCommandIsEnabled = menuInfo.IsEnabled;
                    break;
                case MenuName.NextEventsMenu:
                    NextEventsCommandIsEnabled = menuInfo.IsEnabled;
                    break;
                case MenuName.GraphZoomInMenu:
                    GraphZoomInCommandIsEnabled = menuInfo.IsEnabled;
                    break;
                case MenuName.GraphZoomOutMenu:
                    GraphZoomOutCommandIsEnabled = menuInfo.IsEnabled;
                    break;
            }
        }

        #endregion

        #region 主区域内容，默认为PatientListView（除了上部菜单之外的其他主要内容）

        //默认页面为患者列表
        private UserControlBase mainContent= new PatientListView();

        /// <summary>
        /// 主区域内容
        /// </summary>
        public UserControlBase MainContent
        {
            get { return mainContent; }
            set { Set(ref mainContent, value); }
        }

        #endregion

        #region PatientAddCommand

        public ICommand PatientAddCommand { get; private set; }

        private void OnExecutePatientAddCommand()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = Utility.Windows.ResourceHelper.LoadString(@"OpenFileDialogTitle");
            //openFileDialog.FileName = Const.RMSFileName;
            openFileDialog.Filter = Utility.Windows.ResourceHelper.LoadString(@"RMSFileFilter");
            if (openFileDialog.ShowDialog() == true)
            {
                //StartDownload(openFileDialog.FileName);
                Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, openFileDialog.FileName), Model.MessengerToken.Navigate);
            }
            openFileDialog = null;



            //NewCaseCommand.CanExecute(false);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup), Token.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");

            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            ////Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";
            //GalaSoft.MvvmLight.Views.INavigationService a;
        }

        #endregion

        #region PatientListCommand

        public ICommand PatientListCommand { get; private set; }

        private void OnExecutePatientListCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.Popup), Model.MessengerToken.Navigate);

            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = ResourceHelper.LoadString("OpenFileDialogTitle");
            //openFileDialog.FileName = Const.RMSFileName;
            //openFileDialog.Filter = ResourceHelper.LoadString("RMSFileFilter");
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //StartDownload(openFileDialog.FileName);
            //    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, openFileDialog.FileName), Model.MessengerToken.Navigate);

            //}
            //openFileDialog = null;

            //NewCaseCommand.CanExecute(false);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup), Token.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");

            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            ////Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";
            //GalaSoft.MvvmLight.Views.INavigationService a;
        }

        #endregion

        #region RespiratoryEventAnalysisCommand

        public ICommand RespiratoryEventAnalysisCommand { get; private set; }

        private void OnExecuteRespiratoryEventAnalysisCommand()
        {
            Messenger.Default.Send<object>(null, Model.MessengerToken.EventAnalysis);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region RespiratoryEventAnalysisCommandIsEnabled

        private bool respiratoryEventAnalysisCommandIsEnabled = false;

        public bool RespiratoryEventAnalysisCommandIsEnabled
        {
            get { return respiratoryEventAnalysisCommandIsEnabled; }
            set { Set(ref respiratoryEventAnalysisCommandIsEnabled, value); }
        }

        #endregion

        #region SnoreAnalysisCommand

        public ICommand SnoreAnalysisCommand { get; private set; }

        private void OnExecuteSnoreAnalysisCommand()
        {
            Messenger.Default.Send<object>(null, Model.MessengerToken.SnoreAnalysis);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region SnoreAnalysisCommandIsEnabled

        private bool snoreAnalysisCommandIsEnabled = false;

        public bool SnoreAnalysisCommandIsEnabled
        {
            get { return snoreAnalysisCommandIsEnabled; }
            set { Set(ref snoreAnalysisCommandIsEnabled, value); }
        }

        #endregion

        #region PreviousEventsCommand

        public ICommand PreviousEventsCommand { get; private set; }

        private void OnExecutePreviousEventsCommand()
        {
            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region PreviousEventsCommandIsEnabled

        private bool previousEventsCommandIsEnabled = false;

        public bool PreviousEventsCommandIsEnabled
        {
            get { return previousEventsCommandIsEnabled; }
            set { Set(ref previousEventsCommandIsEnabled, value); }
        }

        #endregion

        #region NextEventsCommand

        public ICommand NextEventsCommand { get; private set; }

        private void OnExecuteNextEventsCommand()
        {
            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region NextEventsCommandIsEnabled

        private bool nextEventsCommandIsEnabled = false;

        public bool NextEventsCommandIsEnabled
        {
            get { return nextEventsCommandIsEnabled; }
            set { Set(ref nextEventsCommandIsEnabled, value); }
        }

        #endregion

        #region GraphZoomInCommand

        public ICommand GraphZoomInCommand { get; private set; }

        private void OnExecuteGraphZoomInCommand()
        {
            //Messenger.Default.Send<object>(null, Model.MessengerToken.ZoomIn);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region GraphZoomInCommandIsEnabled

        private bool graphZoomInCommandIsEnabled = false;

        public bool GraphZoomInCommandIsEnabled
        {
            get { return graphZoomInCommandIsEnabled; }
            set { Set(ref graphZoomInCommandIsEnabled, value); }
        }

        #endregion

        #region GraphZoomOutCommand

        public ICommand GraphZoomOutCommand { get; private set; }

        private void OnExecuteGraphZoomOutCommand()
        {
            //Messenger.Default.Send<object>(null, Model.MessengerToken.ZoomOut);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));



            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");



            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            //Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";

        }

        #endregion

        #region GraphZoomOutCommandIsEnabled

        private bool graphZoomOutCommandIsEnabled = false;

        public bool GraphZoomOutCommandIsEnabled
        {
            get { return graphZoomOutCommandIsEnabled; }
            set { Set(ref graphZoomOutCommandIsEnabled, value); }
        }

        #endregion

        #region ChannelSettingsCommand

        public ICommand ChannelSettingsCommand { get; private set; }

        private void OnExecuteChannelSettingsCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.ChannelSettingsView, ViewType.Popup), Model.MessengerToken.Navigate);

            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = ResourceHelper.LoadString("OpenFileDialogTitle");
            //openFileDialog.FileName = Const.RMSFileName;
            //openFileDialog.Filter = ResourceHelper.LoadString("RMSFileFilter");
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //StartDownload(openFileDialog.FileName);
            //    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, openFileDialog.FileName), Model.MessengerToken.Navigate);

            //}
            //openFileDialog = null;

            //NewCaseCommand.CanExecute(false);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup), Token.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");

            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            ////Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";
            //GalaSoft.MvvmLight.Views.INavigationService a;
        }

        #endregion

        #region AutoAnalysisSettingsCommand

        public ICommand AutoAnalysisSettingsCommand { get; private set; }

        private void OnExecuteAutoAnalysisSettingsCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.AutoAnalysisSettingsView, ViewType.Popup), Model.MessengerToken.Navigate);

            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = ResourceHelper.LoadString("OpenFileDialogTitle");
            //openFileDialog.FileName = Const.RMSFileName;
            //openFileDialog.Filter = ResourceHelper.LoadString("RMSFileFilter");
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //StartDownload(openFileDialog.FileName);
            //    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, openFileDialog.FileName), Model.MessengerToken.Navigate);

            //}
            //openFileDialog = null;

            //NewCaseCommand.CanExecute(false);

            //Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup), Token.Navigate);

            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string, PageTwoViewModel>("123");

            //GalaSoft.MvvmLight.Messaging.NotificationMessage m = new GalaSoft.MvvmLight.Messaging.NotificationMessage(this, "123");
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<GalaSoft.MvvmLight.Messaging.GenericMessage<int>>(new GalaSoft.MvvmLight.Messaging.GenericMessage<int>(999));

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction(this, new PageTwoViewModel(), "123", new Action(aa));
            ////GalaSoft.MvvmLight.Messaging.NotificationMessageAction nma = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction("123", aa);
            //nma.Execute();

            //GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string> nmaa = new GalaSoft.MvvmLight.Messaging.NotificationMessageAction<string>("123",,, new Action<string>(aaa));
            //nmaa.Execute("NotificationMessageAction<string>");


            //GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback nmaacbcb = new GalaSoft.MvvmLight.Messaging.NotificationMessageWithCallback("111", new Action<string>(aaa));
            //nmaacbcb.Execute("NotificationMessageWithCallback");

            //GalaSoft.MvvmLight.Helpers.WeakAction<string> aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //aa.Execute("");

            ///....WeakAction aa = new GalaSoft.MvvmLight.Helpers.WeakAction<string>(aaa);

            //BLL.PatientBLL patientBLL = new BLL.PatientBLL();
            //Model.Patient patient = new Model.Patient();
            //patient.Id = System.Guid.NewGuid();
            //patient.FirstName = "FirstName";
            //patient.LastName = "LastName";
            //patientBLL.Insert(patient);
            //// patientBLL.SaveChanges();
            //patientBLL.ExecuteSqlCommand("delete from Patients");

            ////Title = patientBLL.Count() + "ChangeTitleHello MvvmLight";
            //GalaSoft.MvvmLight.Views.INavigationService a;
        }

        #endregion

        #region SwitchLanguageCommand

        public ICommand SwitchLanguageCommand { get; private set; }

        /// <summary>
        /// 语言切换命令执行
        /// </summary>
        private void OnExecuteSwitchLanguageCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.SwitchLanguageView, ViewType.Popup), Model.MessengerToken.Navigate);
        }

        #endregion

        #region SwitchLanguageCommandIsEnabled

        private bool switchLanguageCommandIsEnabled = true;

        public bool SwitchLanguageCommandIsEnabled
        {
            get { return switchLanguageCommandIsEnabled; }
            set { Set(ref switchLanguageCommandIsEnabled, value); }
        }

        #endregion

        #region HelpCommand

        public ICommand HelpCommand { get; private set; }

        /// <summary>
        /// 帮助命令执行
        /// </summary>
        private void OnExecuteHelpCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.HelpView, ViewType.SingleWindow), Model.MessengerToken.Navigate);
        }

        #endregion

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            unRegisterMessenger();
        }
    }
}