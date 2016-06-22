using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;

namespace SuperSoft.View.ViewModel
{
    public class HelpViewModel : MyViewModelBase
    {

        public HelpViewModel()
        {
            LoadDoc();
        }

        private FixedDocumentSequence helpDocument;

        public FixedDocumentSequence HelpDocument
        {
            get { return helpDocument; }
            set { Set(ref helpDocument, value); }
        }

        private void LoadDoc()
        {
            try
            {
                var languageString = ConfigHelper.GetAppSetting("Language");
                //文件名格式：help.zh-CN.xps
                var xpsPath = Const.AppPath + "help." + languageString + ".xps";
                HelpDocument = new XpsDocument(xpsPath, FileAccess.Read).GetFixedDocumentSequence();
                languageString = xpsPath = null;
            }
            catch
            {
                //LogHelper.Error(ToString(), e);
            }
        }
    }
}
