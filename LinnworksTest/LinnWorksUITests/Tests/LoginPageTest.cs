using FluentAssertions;
using LinnWorksUITests.Base;
using LinnWorksUITests.Pages;
using NUnit.Framework;

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
        public void SuccessfulLogin_ShouldRedirectTo_CategoryPage()
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
        public void FailedLogin_ShouldDisplay_ErrorMessage()
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
