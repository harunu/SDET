using LinnWorksUITests.Base;
using OpenQA.Selenium;

namespace LinnWorksUITests.Pages
{
    public class Base
    {
        protected readonly IWebDriver Driver;
        protected Base()
        {
            Driver = BaseDriver.Current.Driver;
        }
    }
}
