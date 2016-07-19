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
using SuperSoft.Utility.Windows;
//using SuperSoft.BLL.DownloadData;
using System.Linq.Expressions;
using SuperSoft.BLL;
using System.Linq;
using System.IO;
using SuperSoft.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SuperSoft.BLL.DownloadData;

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
            initPatientSearchConditionList();
            initDoctorSearchConditionList();

        }

        #region Command����Ϣ��ʼ��

        /// <summary>
        /// ��ʼ��Command
        /// </summary>
        private void initCommand()
        {
            PatientListCommand = new RelayCommand(OnExecutePatientListCommand);
            PatientAddCommand = new RelayCommand(OnExecutePatientAddCommand);
            PatientEditCommand = new RelayCommand(OnExecutePatientEditCommand, OnCanExecutePatientEditCommand);
            PatientDeleteCommand = new RelayCommand(OnExecutePatientDeleteCommand, OnCanExecutePatientDeleteCommand);
            PatientSearchCommand = new RelayCommand(OnExecutePatientSearchCommand, OnCanExecutePatientSearchCommand);

            DoctorListCommand = new RelayCommand(OnExecuteDoctorListCommand);
            DoctorAddCommand = new RelayCommand(OnExecuteDoctorAddCommand);
            DoctorEditCommand = new RelayCommand(OnExecuteDoctorEditCommand, OnCanExecuteDoctorEditCommand);
            DoctorDeleteCommand = new RelayCommand(OnExecuteDoctorDeleteCommand, OnCanExecuteDoctorDeleteCommand);
            DoctorSearchCommand = new RelayCommand(OnExecuteDoctorSearchCommand, OnCanExecuteDoctorSearchCommand);



            DownloadFormFileCommand = new RelayCommand(OnExecuteDownloadFormFileCommand, OnCanExecuteDownloadFormFileCommand);
            DownloadFormSdCommand = new RelayCommand(OnExecuteDownloadFormSdCommand, OnCanExecuteDownloadFormSdCommand);

            SettingsCommand = new RelayCommand(OnExecuteSettingsCommand);
            SwitchLanguageCommand = new RelayCommand(OnExecuteSwitchLanguageCommand);

            HelpCommand = new RelayCommand(OnExecuteHelpCommand);
        }

        /// <summary>
        /// ע��MVVMLight��Ϣ
        /// </summary>
        private void registerMessenger()
        {
            Messenger.Default.Register<ViewInfo>(this, Model.MessengerToken.Navigate, Navigate);

            Messenger.Default.Register<object>(this, Model.MessengerToken.ClosePopup, ClosePopup);

            Messenger.Default.Register<MenuInfo>(this, Model.MessengerToken.SetMenuStatus, SetMenuStatus);
        }

        /// <summary>
        /// ȡ��ע��MVVMlight��Ϣ
        /// </summary>
        private void unRegisterMessenger()
        {
            Messenger.Default.Unregister<ViewInfo>(this, Model.MessengerToken.Navigate, Navigate);

            Messenger.Default.Unregister<object>(this, Model.MessengerToken.ClosePopup, ClosePopup);

            Messenger.Default.Unregister<MenuInfo>(this, Model.MessengerToken.SetMenuStatus, SetMenuStatus);
        }
        #endregion

        #region ��Ϣ������������Ҫ����ҳ�浼�������˵�״̬�����


        private void Navigate(ViewInfo viewInfo)
        {
            UserControlBase view;
            view =
                   System.Reflection.Assembly.Load(@"SuperSoft.View")
                       .CreateInstance(@"SuperSoft.View.View." + viewInfo.ViewName.ToString()) as UserControlBase;

            //if (Equals(viewInfo.Parameter, null))
            //{
            //    view =
            //        System.Reflection.Assembly.Load(@"SuperSoft.View")
            //            .CreateInstance(@"SuperSoft.View.View." + viewInfo.ViewName.ToString()) as UserControlBase;
            //}
            //else
            //{
            //    view = System.Reflection.Assembly.Load(@"SuperSoft.View").
            //        CreateInstance(@"SuperSoft.View.View." + viewInfo.ViewName.ToString(), true, System.Reflection.BindingFlags.Default,
            //            null, new[] { viewInfo.Parameter }, null, null) as UserControlBase;
            //}
            if (view == null)
            {//δ�ҵ���ͼ���׳��쳣
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
                case ViewType.Popup://ģʽ�Ի���
                    MahApps.Metro.Controls.MetroWindow popupWindows = new MahApps.Metro.Controls.MetroWindow();
                    popupWindows.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    popupWindows.Style = Utility.Windows.ResourceHelper.FindResource(@"CleanWindowStyleKey") as Style;
                    popupWindows.GlowBrush = Utility.Windows.ResourceHelper.FindResource(@"AccentColorBrush") as System.Windows.Media.Brush;
                    //�������ʱ����̬���¶Ի���Title,Titleȡ���ڿؼ���Tag
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.TitleProperty, new Binding(@"Tag") { Source = view });
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.WidthProperty, new Binding(@"Width") { Source = view });
                    popupWindows.SetBinding(MahApps.Metro.Controls.MetroWindow.HeightProperty, new Binding(@"Height") { Source = view });
                    popupWindows.Owner = Application.Current.MainWindow;
                    popupWindows.ResizeMode = ResizeMode.NoResize;
                    popupWindows.IsCloseButtonEnabled = false;
                    popupWindows.ShowCloseButton = false;
                    //popupWindows.Icon = new BitmapImage(new Uri("pack://application:,,,/SuperSoft.Resource.Default;component/Images/Logo_White.png", UriKind.Absolute));
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

                case ViewType.View://��ͨ��ͼ
                                   //ҳ���л�Ч��
                                   //GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<object>(null, Model.MessengerToken.NavigateSplash);
                    if (!Equals(MainContent, null))
                    {
                        MainContent.Dispose();
                        MainContent = null;
                        GC.Collect();
                    }
                    MainContent = view;
                    break;

                case ViewType.SingleWindow://������ͼ����ҪΪ����ʾ��������
                    MahApps.Metro.Controls.MetroWindow singleWindows = new MahApps.Metro.Controls.MetroWindow();
                    singleWindows.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    //popupWindows.Style = Utility.ResourceHelper.FindResource("CleanWindowStyleKey") as Style;
                    singleWindows.GlowBrush = Utility.Windows.ResourceHelper.FindResource(@"AccentColorBrush") as System.Windows.Media.Brush;
                    //�������ʱ����̬���¶Ի���Title,Titleȡ���ڿؼ���Tag
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
        /// �ر�Popup
        /// </summary>
        /// <param name="obj"></param>
        private void ClosePopup(object obj)
        {
            //���Popupʱ��Ҫ��ջ
            if (popupWindowsStack.Count > 0)
            {
                MetroWindow popupWindow = popupWindowsStack.Pop() as MetroWindow;
                popupWindow.Close();
                popupWindow = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// ���ò˵���״̬
        /// </summary>
        private void SetMenuStatus(MenuInfo menuInfo)
        {
            switch (menuInfo.MenuName)
            {
                //case MenuName.RespiratoryEventAnalysisMenu:
                //    RespiratoryEventAnalysisCommandIsEnabled = menuInfo.IsEnabled;
                //    break;
                //case MenuName.SnoreAnalysisMenu:
                //    // SnoreAnalysisCommandIsEnabled = menuInfo.IsEnabled;
                //    SnoreAnalysisCommandIsEnabled = false;//����ʱ����Ҫ������ص����ݺ͹���
                //    break;
                //case MenuName.PreviousEventsMenu:
                //    PreviousEventsCommandIsEnabled = menuInfo.IsEnabled;
                //    break;
                //case MenuName.NextEventsMenu:
                //    NextEventsCommandIsEnabled = menuInfo.IsEnabled;
                //    break;
                //case MenuName.GraphZoomInMenu:
                //    GraphZoomInCommandIsEnabled = menuInfo.IsEnabled;
                //    break;
                //case MenuName.GraphZoomOutMenu:
                //    GraphZoomOutCommandIsEnabled = menuInfo.IsEnabled;
                //    break;
            }
        }

        #endregion

        #region ���������ݣ�Ĭ��ΪPatientListView�������ϲ��˵�֮���������Ҫ���ݣ�


        private UserControlBase mainContent;

        /// <summary>
        /// ����������
        /// </summary>
        public UserControlBase MainContent
        {
            get { return mainContent; }
            set { Set(ref mainContent, value); }
        }

        #endregion

        #region ���߹���

        #region PatientListCommand

        public ICommand PatientListCommand { get; private set; }

        private void OnExecutePatientListCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View), Model.MessengerToken.Navigate);

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

        #region PatientAddCommand

        public ICommand PatientAddCommand { get; private set; }

        private void OnExecutePatientAddCommand()
        {
            //var openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = Utility.Windows.ResourceHelper.LoadString(@"OpenFileDialogTitle");
            ////openFileDialog.FileName = Const.RMSFileName;
            //openFileDialog.Filter = Utility.Windows.ResourceHelper.LoadString(@"RMSFileFilter");
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    //StartDownload(openFileDialog.FileName);
            //    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, openFileDialog.FileName), Model.MessengerToken.Navigate);
            //}
            //openFileDialog = null;

            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup), Model.MessengerToken.Navigate);


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

        #region PatientDeleteCommand

        public ICommand PatientDeleteCommand { get; private set; }

        private void OnExecutePatientDeleteCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientDeleteView, ViewType.Popup), Model.MessengerToken.Navigate);
        }

        private bool OnCanExecutePatientDeleteCommand()
        {
            if (StaticDatas.CurrentSelectedPatient != null)
            {
                return true;
            }
            return false;
        }

        #endregion PatientDeleteCommand

        #region PatientEditCommand

        public ICommand PatientEditCommand { get; private set; }

        /// <summary>
        /// �༭��������ִ��
        /// </summary>
        private void OnExecutePatientEditCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientEditView, ViewType.Popup), Model.MessengerToken.Navigate);
        }

        private bool OnCanExecutePatientEditCommand()
        {
            if (StaticDatas.CurrentSelectedPatient != null)
            {
                return true;
            }
            return false;
        }

        #endregion PatientEditCommand

        #region PatientSearchCommand

        public ICommand PatientSearchCommand { get; private set; }

        /// <summary>
        /// ������������ִ��
        /// </summary>
        private void OnExecutePatientSearchCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientListView, ViewType.View, new KeyValuePair<string, string>(SelectedSearchConditionPatient.Key, ConditionContainPatient)),
               Model.MessengerToken.Navigate);
        }

        private bool OnCanExecutePatientSearchCommand()
        {
            return true;
        }

        /// <summary>
        /// �������������б�
        /// </summary>
        public ICollection<KeyValuePair<string, string>> PatientSearchConditionList { get; private set; }

        private KeyValuePair<string, string> selectedSearchConditionPatient;

        /// <summary>
        /// ѡ��Ļ�����������
        /// </summary>
        public KeyValuePair<string, string> SelectedSearchConditionPatient
        {
            get { return selectedSearchConditionPatient; }
            set { Set(ref selectedSearchConditionPatient, value); }
        }

        /// <summary>
        /// ��ʼ���������������б�
        /// </summary>
        private void initPatientSearchConditionList()
        {
            PatientSearchConditionList = new Collection<KeyValuePair<string, string>>();
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("FirstName",
                ResourceHelper.LoadString("FirstName")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("LastName",
                ResourceHelper.LoadString("LastName")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("DateOfBirth",
                ResourceHelper.LoadString("DateOfBirth")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("Weight",
                ResourceHelper.LoadString("Weight")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("Height",
                ResourceHelper.LoadString("Height")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("Gender",
                ResourceHelper.LoadString("Gender")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("EMail",
                ResourceHelper.LoadString("EMail")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("TelephoneNumbers",
                ResourceHelper.LoadString("TelephoneNumbers")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("PostalCode",
                ResourceHelper.LoadString("PostalCode")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("Address",
                ResourceHelper.LoadString("Address")));
            PatientSearchConditionList.Add(new KeyValuePair<string, string>("Diagnosis",
                ResourceHelper.LoadString("Diagnosis")));
            SelectedSearchConditionPatient = PatientSearchConditionList.FirstOrDefault();
        }

        private string conditionContainPatient;

        /// <summary>
        /// ������������������
        /// </summary>
        public string ConditionContainPatient
        {
            get { return conditionContainPatient; }
            set { Set(ref conditionContainPatient, value); }
        }

        #endregion PatientSearchCommand

        #endregion

        #region ҽ������

        #region DoctorListCommand

        public ICommand DoctorListCommand { get; private set; }

        private void OnExecuteDoctorListCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorListView, ViewType.View), Model.MessengerToken.Navigate);
        }

        #endregion

        #region DoctorAddCommand

        public ICommand DoctorAddCommand { get; private set; }

        /// <summary>
        /// ����ҽ������ִ��
        /// </summary>
        private void OnExecuteDoctorAddCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorAddView, ViewType.Popup), Model.MessengerToken.Navigate);
        }

        #endregion DoctorAddCommand

        #region DoctorDeleteCommand

        public ICommand DoctorDeleteCommand { get; private set; }

        private void OnExecuteDoctorDeleteCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorDeleteView, ViewType.Popup, null), Model.MessengerToken.Navigate);
        }

        private bool OnCanExecuteDoctorDeleteCommand()
        {
            if (StaticDatas.CurrentSelectedDoctor != null)
            {
                return true;
            }
            return false;
        }

        #endregion DoctorDeleteCommand

        #region DoctorEditCommand

        public ICommand DoctorEditCommand { get; private set; }

        /// <summary>
        /// �༭ҽ������ִ��
        /// </summary>
        private void OnExecuteDoctorEditCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorEditView, ViewType.Popup), Model.MessengerToken.Navigate);
        }

        private bool OnCanExecuteDoctorEditCommand()
        {
            if (StaticDatas.CurrentSelectedDoctor != null)
            {
                return true;
            }
            return false;
        }

        #endregion DoctorEditCommand

        #region DoctorSearchCommand

        public ICommand DoctorSearchCommand { get; private set; }

        /// <summary>
        /// ����ҽ������ִ��
        /// </summary>
        private void OnExecuteDoctorSearchCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DoctorListView, ViewType.View, new KeyValuePair<string, string>(SelectedSearchConditionDoctor.Key, ConditionContainDoctor)),
                Model.MessengerToken.Navigate);
        }

        private bool OnCanExecuteDoctorSearchCommand()
        {
            return true;
        }

        #endregion DoctorSearchCommand


        /// <summary>
        /// ҽ�����������б�
        /// </summary>
        public ICollection<KeyValuePair<string, string>> DoctorSearchConditionList { get; private set; }

        private KeyValuePair<string, string> selectedSearchConditionDoctor;

        /// <summary>
        /// ѡ��Ļ�����������
        /// </summary>
        public KeyValuePair<string, string> SelectedSearchConditionDoctor
        {
            get { return selectedSearchConditionDoctor; }
            set { Set(ref selectedSearchConditionDoctor, value); }
        }

        /// <summary>
        /// ��ʼ��ҽ�����������б�
        /// </summary>
        private void initDoctorSearchConditionList()
        {
            DoctorSearchConditionList = new Collection<KeyValuePair<string, string>>();
            DoctorSearchConditionList.Add(new KeyValuePair<string, string>("FirstName",
                ResourceHelper.LoadString("FirstName")));
            DoctorSearchConditionList.Add(new KeyValuePair<string, string>("LastName",
                ResourceHelper.LoadString("LastName")));
            SelectedSearchConditionDoctor = DoctorSearchConditionList.FirstOrDefault();
        }

        private string conditionContainDoctor;

        /// <summary>
        /// ҽ����������������
        /// </summary>
        public string ConditionContainDoctor
        {
            get { return conditionContainDoctor; }
            set { Set(ref conditionContainDoctor, value); }
        }

        #endregion

        #region ��������

        #region DownloadCommand

        public ICommand DownloadCommand { get; private set; }

        /// <summary>
        /// ��������ִ��
        /// </summary>
        private void OnExecuteDownloadCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.DownloadView, ViewType.View), Model.MessengerToken.Navigate);
        }

        #endregion DownloadCommand

        #region DownloadFormFileCommand

        public ICommand DownloadFormFileCommand { get; private set; }

        /// <summary>
        /// ��������ִ��
        /// </summary>
        private void OnExecuteDownloadFormFileCommand()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = ResourceHelper.LoadString("OpenFileDialogTitle");
            openFileDialog.FileName = Const.RMSFileName;
            openFileDialog.Filter = ResourceHelper.LoadString("RMSFileFilter");
            if (openFileDialog.ShowDialog() == true)
            {
                StartDownload(openFileDialog.FileName);
            }
            openFileDialog = null;
        }

        private bool OnCanExecuteDownloadFormFileCommand()
        {
            //�ж��ļ����ذ�ť�Ƿ����
            if (bllDownloadData != null)
            {
                return !bllDownloadData.IsBusy();
            }
            return true;
        }

        #endregion DownloadFormFileCommand

        #region DownloadFormSdCommand

        public ICommand DownloadFormSdCommand { get; private set; }

        /// <summary>
        /// ��������ִ��
        /// </summary>
        private void OnExecuteDownloadFormSdCommand()
        {
            var deviceList = from a in DriveInfo.GetDrives()
                             where a.DriveType == DriveType.Removable
                             select a;
            if (deviceList != null && deviceList.Count() > 0)
            {
                var deviceListArrary = deviceList.ToArray();
                for (var i = 0; i < deviceList.Count(); i++)
                {
                    var indexFileName = deviceListArrary[i].Name + Const.RMSFileName;
                    if (File.Exists(indexFileName))
                    {
                        StartDownload(indexFileName);
                        break;
                    }
                    if (i == deviceList.Count() - 1)
                    {
                        MessageBox.Show(Application.Current.MainWindow,
                            string.Format(ResourceHelper.LoadString("DataFileNotFound") + Environment.NewLine,
                                indexFileName)
                            , ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var result =
                        MessageBox.Show(Application.Current.MainWindow,
                            string.Format(ResourceHelper.LoadString("IndexFileNotFound") + Environment.NewLine,
                                indexFileName)
                            , ResourceHelper.LoadString("Error"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// �ҵ������ļ�֮�󣬿�ʼ����
        /// </summary>
        /// <param name="indexFileName"></param>
        private void StartDownload(string indexFileName)
        {
            //LogHelper.Info("start download:" + indexFileName);
            if (File.Exists(indexFileName))
            {
                var indexFileField = DownloadData.GetIndexFileField(indexFileName);
                if (indexFileField == null || indexFileField.SerialNumber.Length != 18)
                {
                    MessageBox.Show(Application.Current.MainWindow,
                        string.Format(ResourceHelper.LoadString("RMSFileFormatError") + Environment.NewLine,
                            indexFileName, ResourceHelper.LoadString("RMSFileName"))
                        , ResourceHelper.LoadString("Error"), MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var viewPatientProductBLL = new ViewPatientsProductBLL();
                var viewPatientProductsList = viewPatientProductBLL.SelectBySerialNumber(indexFileField.SerialNumber);
                if (viewPatientProductsList != null && viewPatientProductsList.Count() > 0)
                {
                    //�в�Ʒ���� �ͻ�����Ϣ ֱ������
                    downloadData(viewPatientProductsList.FirstOrDefault().PatientId, indexFileName);
                }
                else
                {
                    //�޲�Ʒ���ݣ����ӻ��� ,Ȼ�����Ƿ����ӳɹ���Ϊ���ӳɹ� ֱ�ӷ��أ����ӳɹ� ֱ������
                    //Navigate(ViewNames.PatientAddView, indexFileField.SerialNumber);
                    Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.PatientAddView, ViewType.Popup, indexFileField.SerialNumber), Model.MessengerToken.Navigate);

                    viewPatientProductsList = viewPatientProductBLL.SelectBySerialNumber(indexFileField.SerialNumber);

                    if (viewPatientProductsList != null && viewPatientProductsList.Count() > 0)
                    {
                        //�в�Ʒ���� �ͻ�����Ϣ ֱ������
                        downloadData(viewPatientProductsList.FirstOrDefault().PatientId, indexFileName);
                    }
                }
            }
        }

        private bool OnCanExecuteDownloadFormSdCommand()
        {
            //�ж�SD�����ذ�ť�Ƿ����
            //�ҵ��ƶ��洢 ����ʾ���á�
            var v = from a in DriveInfo.GetDrives()
                    where a.DriveType == DriveType.Removable
                    select a;
            if (v != null && v.Count() > 0)
            {
                if (bllDownloadData != null)
                {
                    return !bllDownloadData.IsBusy();
                }
                return true;
            }
            return false;
        }

        #endregion DownloadFormSdCommand

        #region DownloadDataProgressVisibility

        private Visibility downloadDataProgressVisibility = Visibility.Collapsed;

        public Visibility DownloadDataProgressVisibility
        {
            get { return downloadDataProgressVisibility; }
            set { Set(ref downloadDataProgressVisibility, value); }
        }

        #endregion DownloadDataProgress

        #region DownloadDataProgress

        private int downloadDataProgress;

        public int DownloadDataProgress
        {
            get { return downloadDataProgress; }
            set { Set(ref downloadDataProgress, value); }
        }

        #endregion DownloadDataProgress

        #region   BLL.DownloadData.DownloadData ���� �¼�

        private DownloadData bllDownloadData;
#if DEBUG
        System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();

#endif


        private void downloadData(Guid patientId, string indexFileName)
        {
            if (bllDownloadData == null)
            {
#if DEBUG
                st.Start();
#endif

                bllDownloadData = new DownloadData();

                bllDownloadData.ProgressChanged -= DownloadData_ProgressChanged;
                bllDownloadData.RunWorkerCompleted -= DownloadData_RunWorkerCompleted;

                bllDownloadData.ProgressChanged += DownloadData_ProgressChanged;
                bllDownloadData.RunWorkerCompleted += DownloadData_RunWorkerCompleted;
            }
            bllDownloadData.Start(patientId, indexFileName);

            DownloadDataProgressVisibility = Visibility.Visible;
        }

        private void DownloadData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadDataProgress = e.ProgressPercentage;
        }

        private void DownloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                DownloadDataProgressVisibility = Visibility.Collapsed;
                DownloadDataProgress = 0;
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    LogHelper.Error(ToString(), e.Error);
                    MessageBox.Show(Application.Current.MainWindow,
                        ResourceHelper.LoadString("Error") + e.Error.Message +
                        (e.Error.InnerException == null ? string.Empty : e.Error.InnerException.Message)
                        , ResourceHelper.LoadString("DownloadData")
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                }
                else if (e.Cancelled)
                {
                    // Next, handle the case where the user canceled 
                    // the operation.
                    // Note that due to a race condition in 
                    // the DoWork event handler, the Cancelled
                    // flag may not have been set, even though
                    // CancelAsync was called.
                    //LogHelper.Info(ResourceHelper.LoadString("DownloadCanceled"));
                    MessageBox.Show(Application.Current.MainWindow,
                        ResourceHelper.LoadString("DownloadCanceled") + (e.Result == null ? string.Empty : e.Result)
                        , ResourceHelper.LoadString("DownloadData")
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                }
                else
                {
#if DEBUG
                    st.Stop();
                    Console.WriteLine("����������ʱ��{0}", st.ElapsedMilliseconds.ToString());
#endif
                    // Finally, handle the case where the operation 
                    // succeeded.
                    MessageBox.Show(Application.Current.MainWindow,
                        ResourceHelper.LoadString("DownloadCompleted") + (e.Result == null ? string.Empty : e.Result)
                        , ResourceHelper.LoadString("DownloadData")
                        , MessageBoxButton.OK
                        , MessageBoxImage.Information);
                }
            }
            finally
            {
                if (!Equals(bllDownloadData, null))
                {
                    bllDownloadData.Dispose();
                    bllDownloadData = null;
                }
            }
        }

        #endregion DownloadData �¼�

        #endregion

        #region ϵͳ���� 

        #region SettingsCommand

        public ICommand SettingsCommand { get; private set; }

        /// <summary>
        /// ����ִ��
        /// </summary>
        private void OnExecuteSettingsCommand()
        {
            Messenger.Default.Send<ViewInfo>(new ViewInfo(ViewName.SystemParameterSettingView, ViewType.View), Model.MessengerToken.Navigate);
        }

        #endregion

        #region SwitchLanguageCommand

        public ICommand SwitchLanguageCommand { get; private set; }

        /// <summary>
        /// �����л�����ִ��
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

        #endregion 

        #region HelpCommand

        public ICommand HelpCommand { get; private set; }

        /// <summary>
        /// ��������ִ��
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