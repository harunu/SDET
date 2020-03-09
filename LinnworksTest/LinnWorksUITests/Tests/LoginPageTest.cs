using FluentAssertions;
using LinnWorksUITests.Base;
using LinnWorksUITests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksUITests.Tests
{
    public class LoginPageTest : BasePageTest
    {
        private readonly Login _loginPage;
        private readonly Categories _categoriesPage;
        public LoginPageTest()
        {
            _categoriesPage = new Categories();
            _loginPage = new Login();
        }

        [SetUp]
        public void Setup()
        {
            // ARRANGE
            BaseDriver.Current.Driver.Navigate().GoToUrl(StartupConstants.ServiceUrl + "/login");
        }

        [Test]
        public void successfulLogin_ShouldRedirectTo_CategoryPage()
        {
            // ACT
            _loginPage
                .SetToken(StartupConstants.Token)
                .PressLogin();

            // ASSERT
            _categoriesPage.IsCategoriesLblDisplayed().Should()
                .BeTrue();
        }

        [Test]
        public void failedLogin_ShouldDisplay_ErrorMsg()
        {
            // ACT
            _loginPage
                .SetToken("sadsadarereffa")
                .PressLogin();

            // ASSERT
            _loginPage.GetTextFromErrorBlock().Should()
                .BeEquivalentTo("Invalid token.");
        }
    }
}
