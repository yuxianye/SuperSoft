using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperSoft.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoft.Utility.Tests
{
    [TestClass()]
    public class ConfigHelperTests
    {
        [TestMethod()]
        public void AddAppSettingTest()
        {
            string expected = "admin";
            string actual = "21232f297a57a5a743894a0e4a801fc3";
            Utility.ConfigHelper.AddAppSetting(expected, actual);
            Assert.AreEqual(ConfigHelper.GetAppSetting(expected), actual);

            expected = "";
            actual = "21232f297a57a5a743894a0e4a801fc3";
            Utility.ConfigHelper.AddAppSetting(expected, actual);
            Assert.AreEqual(ConfigHelper.GetAppSetting(expected), actual);

            //expected = null;
            //actual = "21232f297a57a5a743894a0e4a801fc3";
            //Utility.ConfigHelper.AddAppSetting(expected, actual);
            //Assert((ConfigHelper.GetAppSetting(expected), actual);
        }

        [TestMethod()]
        public void GetAppSettingTest()
        {
            const string expected = "DebugLog";
            const string actual = "false";
            Utility.ConfigHelper.AddAppSetting(expected, actual);
            Assert.AreEqual(ConfigHelper.GetAppSetting(expected), actual);
            Assert.AreEqual(ConfigHelper.GetAppSetting(""), "21232f297a57a5a743894a0e4a801fc3");
            Assert.AreEqual(ConfigHelper.GetAppSetting(null), "");

        }
    }
}