using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperSoft.View.ViewModel
{
    public class SwitchLanguageViewModel : MyViewModelBase
    {
        public SwitchLanguageViewModel()
        {
            LangList = new ObservableCollection<KeyValuePair<string, string>>();
            LangList.Add(new KeyValuePair<string, string>(@"zh-CN", @"中文"));
            //LangList.Add(new KeyValuePair<string, string>("en-US", "English"));

            loadConfigValue();
            ConfirmCommand = new RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            CancelCommand = new RelayCommand(OnExecuteCancelCommand);
        }

        private void loadConfigValue()
        {
            var lang = ConfigHelper.GetAppSetting(@"Language");
            var qry = from c in LangList
                      where c.Key.ToLower().Contains(lang.ToLower())
                      select c;
            SelectItem = qry.FirstOrDefault();
        }

        #region LangList

        private ObservableCollection<KeyValuePair<string, string>> langList;

        public ObservableCollection<KeyValuePair<string, string>> LangList
        {
            get { return langList; }
            set { Set(ref langList, value); }
        }

        #endregion

        #region SelectItem

        private KeyValuePair<string, string> selectItem;

        public KeyValuePair<string, string> SelectItem
        {
            get { return selectItem; }
            set { Set(ref selectItem, value); }
        }

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            try
            {
                ConfigHelper.AddAppSetting(@"Language", SelectItem.Key);
                //设置2次则界面变回正常，一次有些会显示不全。
                Messenger.Default.Send<object>(null, Model.MessengerToken.SwitchLanguage);
                //Messenger.Default.Send<object>(null, Model.MessengerToken.SwitchLanguage);
            }
            catch (Exception e)
            {
                LogHelper.Error(ToString(), e);
            }
        }

        private bool OnCanExecuteConfirmCommand()
        {
            //只有中文
            //if (LangList != null && LangList.Count() > 0)
            //{
            //    return true;
            //}
            return false;
        }

        #endregion

        #region CancelCommand

        public ICommand CancelCommand { get; private set; }

        private void OnExecuteCancelCommand()
        {
            loadConfigValue();
            Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        }

        #endregion

    }
}
