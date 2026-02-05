using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace LinnWorksUITests.Base
{
    public class BaseDriver : IDisposable
    {
        private static BaseDriver _instance;
        public readonly IWebDriver Driver;

        private BaseDriver()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        public static BaseDriver Current => _instance ?? (_instance = new BaseDriver());

        public void Dispose()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
            _instance = null;
        }
    }
}
