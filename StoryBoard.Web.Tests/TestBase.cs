using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryBoard.Web.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Tests
{
    public class TestBase
    {
        [TestInitialize]
        public void Init()
        {
            //Driver Initialization
            Driver.InitializeAsRemoteWebDriver();
            if (!(Driver.ForSafari || Driver.ForiPad))
            {
                Driver.SetPageLoadTimeout(Constants.SecondsFindElementWait);
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
        }
    }
}
