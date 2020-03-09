using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksUITests.Pages
{
    public class Login : Base
    {
        private IWebElement TokenField => Driver.FindElement(By.Id("token"));
        private IWebElement LoginBtn => Driver.FindElement(By.XPath("//button[text() = 'Login']"));
        private IWebElement ErrorBlock => Driver.FindElement(By.XPath("//div[@role='alert']"));

        public Login SetToken(string value)
        {
            var driverTitle = Driver.Title;
            var driverPageSource = Driver.PageSource;
            var driverUrl = Driver.Url;
            TokenField.SendKeys(value);
            return this;
        }

        public void PressLogin()
        {
            LoginBtn.Click();
        }

        public string GetTextFromErrorBlock()
        {
            return ErrorBlock.FindElement(By.XPath(".//li")).Text;
        }
    }
}
