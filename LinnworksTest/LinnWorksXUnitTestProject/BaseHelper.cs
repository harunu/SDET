using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksXUnitTestProject
{
    public class BaseHelper : IDisposable
    {
        public IWebDriver _driver { get; private set; }

        // private readonly IWebDriver _driver;
        private const string URI = "http://localhost:59509/";
        public string APIURI = "http://localhost:59509/api";
        public string CATEGORYURI = "http://localhost:59509/fetch-category";


        private IWebElement LoginElement => _driver.FindElement(By.LinkText("Login"));
        private IWebElement TokenElement => _driver.FindElement(By.Id("token"));
        private IWebElement LogoutElement => _driver.FindElement(By.LinkText("Logout"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;

        public BaseHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
                .GoToUrl(URI);

        public void NavigateAPI() => _driver.Navigate()
               .GoToUrl(APIURI);

        public void PopulateLogin(string token) => TokenElement.SendKeys(token);

        public void LoginClick() => LoginElement.Click();
        public void LogoutClick() => LogoutElement.Click();
        public void TokenClick() => TokenElement.Click();

        public void Manage() => _driver.Manage().Window.Size = new System.Drawing.Size(1550, 838);

        public void Wait() => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);

        public void WaitPageLoad() => _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}

