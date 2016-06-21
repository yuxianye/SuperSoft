using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SuperSoft.Utility;
using SuperSoft.View;

namespace SuperSoft.View.ViewModel
{
    public class AddAdviseViewModel : MyViewModelBase
    {
        public AddAdviseViewModel()
        {
            //ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand, OnCanExecuteConfirmCommand);
            ConfirmCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteConfirmCommand);
            CancelCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnExecuteCancelCommand);
        }

        #region AdviseMsg

        private string adviseMsg;
        public string AdviseMsg
        {
            get { return adviseMsg; }
            set { Set(ref adviseMsg, value); }
        }

        #endregion


        private string oldAdviseMsg;

        private bool isClear = true;


        protected override void OnParameterChanged()
        {
            base.OnParameterChanged();
            isClear = false;
            oldAdviseMsg = Parameter.ToString();
            AdviseMsg = oldAdviseMsg;
        }

        #region ConfirmCommand

        public ICommand ConfirmCommand { get; private set; }

        private void OnExecuteConfirmCommand()
        {
            try
            {
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
                Messenger.Default.Send<string>(this.AdviseMsg, Model.MessengerToken.UpAdviseMsg);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ToString(), ex);
                Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
            }
            finally
            {
                this.oldAdviseMsg = null;
                this.AdviseMsg = null;
            }
        }

        //private bool OnCanExecuteConfirmCommand()
        //{
        //    return true;
        //    //if (!string.IsNullOrWhiteSpace(AdviseMsg))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //}

        #endregion

        #region CancelCommand

        public ICommand CancelCommand { get; private set; }
        private void OnExecuteCancelCommand()
        {
            clearData();
            Messenger.Default.Send<object>(null, Model.MessengerToken.ClosePopup);
        }

        #endregion

        /// <summary>
        /// ViewModel有缓存，执行之后清空
        /// </summary>
        private void clearData()
        {
            if (isClear)
            {
                this.AdviseMsg = null;
            }
            else
            {
                this.AdviseMsg = oldAdviseMsg;
            }
        }
    }
}
