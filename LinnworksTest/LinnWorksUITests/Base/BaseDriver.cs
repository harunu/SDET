using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksUITests.Base
{
    /*  public class BaseDriver : IDisposable
      {
          public IWebDriver _driver { get; private set; }

          // private readonly IWebDriver _driver;
          private const string URI = "http://localhost:59509/";


          public BaseDriver(IWebDriver driver)
          {
              _driver = driver;
          }

        /*  private BaseDriver(IWebDriver driver)
          {
              _driver = driver;
              driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
              driver.Manage().Window.Maximize();
          }

          public static BaseDriver current => _instancedriver ?? (_instancedriver = new BaseDriver());

          public void Navigate() => _driver.Navigate()
                  .GoToUrl(URI);



          public void Dispose()
          {
              _driver.Quit();
          }
          */

 /*   public class BaseDriver : IDisposable
    {
        public IWebDriver _driver { get; private set; }

        // private readonly IWebDriver _driver;
        private const string URI = "http://localhost:59509/";

        private IWebElement LoginElement => _driver.FindElement(By.LinkText("Login"));
        private IWebElement TokenElement => _driver.FindElement(By.Id("token"));
        private IWebElement AccountNumberElement => _driver.FindElement(By.Id("AccountNumber"));
        private IWebElement LogoutElement => _driver.FindElement(By.LinkText("Logout"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;
        public string AccountNumberErrorMessage => _driver.FindElement(By.Id("AccountNumber-error")).Text;

        public BaseDriver (IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
                .GoToUrl(URI);

        //  public void PopulateLogin(string name) => LoginElement.SendKeys(name);
        //  public void PopulateAge(string token) => TokenElement.SendKeys(token);
        //   public void PopulateAccountNumber(string accountNumber) => AccountNumberElement.SendKeys(accountNumber);
        public void LoginClick() => LoginElement.Click();
        public void LogoutClick() => LogoutElement.Click();

        public void TokenClick() => TokenElement.Click();


        public void Manage() => _driver.Manage().Window.Size = new System.Drawing.Size(1550, 838);

        public void Wait() => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

        public void Dispose()
        {
            _driver.Quit();
        }

    }*/
    public class BaseDriver
    {
        public IWebDriver _driver { get; private set; }

        private static BaseDriver _instance;
        public readonly IWebDriver Driver;

        private BaseDriver()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Driver.Manage().Window.Maximize();
        }
    
        public static BaseDriver Current => _instance ?? (_instance = new BaseDriver());

        public void Dispose()
        {
            _instance = null;
        }
    }



}
