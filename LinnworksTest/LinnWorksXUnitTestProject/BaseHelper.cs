using OpenQA.Selenium;
using System;

namespace LinnWorksXUnitTestProject
{
    public class BaseHelper : IDisposable
    {
        public IWebDriver _driver { get; private set; }

        private static readonly string URI =
            Environment.GetEnvironmentVariable("LINNWORKS_SERVICE_URL") ?? "http://localhost:59509/";

        public static readonly string TestToken =
            Environment.GetEnvironmentVariable("LINNWORKS_TEST_TOKEN") ?? "bccf905c-6592-40f2-8db1-c976791fa40a";

        public string APIURI = URI + "api";
        public string CATEGORYURI = URI + "fetch-category";


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

        public void Wait() => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        public void WaitPageLoad() => _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}

