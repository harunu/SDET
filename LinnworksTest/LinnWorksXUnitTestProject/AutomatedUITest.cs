using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using Xunit;

namespace LinnWorksXUnitTestProject
{
    public class AutomatedUITest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly BaseHelper _page;
        public AutomatedUITest()
        {    
            //ARRANGE
            _driver = new FirefoxDriver();
            _page = new BaseHelper(_driver);
            _page.Navigate();
        }

        [Fact]
        public void WhenExecuted_ReturnsPageTitleCorrect()
        {
            // ACT
            string test = _page.Title;
            _page.Wait();
            _page.Manage();

            //ASSERT
            Assert.Equal("LinnworksTest", _page.Title);
        }

        [Fact]
        public void WhenNewCategoryAdded_FetchCategories_Returns_Successfully()
        {    
            //ACT
            _page.Wait();
            _page.Manage();
            _page.LoginClick();
            _page.TokenClick();
            _page.PopulateLogin("bccf905c-6592-40f2-8db1-c976791fa40a");

            _driver.FindElement(By.CssSelector(".btn")).Click();
            _driver.FindElement(By.LinkText("Create New")).Click();
            _driver.FindElement(By.CssSelector(".form-control")).Click();
            _driver.FindElement(By.CssSelector(".form-control")).SendKeys("testharun");
            _driver.FindElement(By.CssSelector(".btn-default")).Click();
            _page.WaitPageLoad();
            //ASSERT
            Assert.Equal(_driver.Url, _page.CATEGORYURI);
            _page.LogoutClick();

        }

        [Fact]
        public void WhenAPIDescriptionClicked_APIPage_Returns_Successfully()
        {
            //ACT
            _page.Wait();
            _page.Manage();
            _page.LoginClick();
            _page.TokenClick();
            _page.PopulateLogin("bccf905c-6592-40f2-8db1-c976791fa40a");
            _driver.FindElement(By.CssSelector(".btn")).Click();
             _driver.FindElement(By.LinkText("Home")).Click();
             _driver.FindElement(By.LinkText("API Description")).Click();        
            _page.WaitPageLoad();
            //ASSERT
            Assert.Equal(_driver.Url, _page.APIURI);
            _page.LogoutClick();
        }


        /*     [Fact]
             public void Login_andTestSwaggerGet200()
             {

                 _page.Wait();
                 _page.Manage();
                 _page.LoginClick();
                 _page.TokenClick();
                 _page.PopulateLogin("bccf905c-6592-40f2-8db1-c976791fa40a");

                 _driver.FindElement(By.CssSelector(".btn")).Click();
                 _page.Wait();
                 _page.WaitPageLoad();
                 _page.NavigateAPI();
                 _page.Wait();
                 _page.WaitPageLoad();
                 _driver.FindElement(By.CssSelector("#operations-Auth-Login > .opblock-summary")).Click();
                 _driver.FindElement(By.CssSelector(".btn")).Click();
                 _driver.FindElement(By.CssSelector(".body-param__text")).Click();
                 _driver.FindElement(By.CssSelector(".body-param__text")).Click();
                 {
                     var element = _driver.FindElement(By.CssSelector(".body-param__text"));
                     Actions builder = new Actions(_driver);
                     builder.DoubleClick(element).Perform();
                 }
                 _driver.FindElement(By.CssSelector(".body-param__text")).SendKeys("{\\n  \"token\": \"bccf905c-6592-40f2-8db1-c976791fa40a\"\\n}");
                 _driver.FindElement(By.CssSelector(".execute")).Click();

                 _page.Wait();
                 _driver.FindElement(By.CssSelector(".response-col_description__inner > .markdown")).Click();
                 _driver.FindElement(By.CssSelector(".response-col_description__inner > .markdown")).Click();
                 {
                     var element = _driver.FindElement(By.CssSelector(".response-col_description__inner > .markdown"));
                     Actions builder = new Actions(_driver);
                     builder.DoubleClick(element).Perform();
                 }
                 _driver.SwitchTo().DefaultContent();
                 _driver.FindElement(By.LinkText("Logout")).Click();

                 Assert.Equal("LinnworksTest", _page.Title);
                 // Assert.Contains("Please provide a new employee data", _page.Source);
             }*/

        /*[Fact]
        public void Login_andTestSwaggerGet400()
        {
            string test = _page.Title;
            _page.Wait();
            _page.Manage();
        }*/

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
