using LinnWorksUITests.Base;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksUITests.Tests
{
    public class BasePageTest
    {
        protected BasePageTest()
        {
            BaseDriver.Current.Driver.Navigate().GoToUrl("about:blank");
            BaseDriver.Current.Driver.Navigate().GoToUrl(StartupConstants.ServiceUrl);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            BaseDriver.Current.Driver.Quit();
            BaseDriver.Current.Dispose();
        }

        [TearDown]
        public void TearDown()
        {
            BaseDriver.Current.Driver.Manage().Cookies.DeleteAllCookies();
            BaseDriver.Current.Driver.Navigate().GoToUrl(StartupConstants.ServiceUrl);
        }
    }
}
