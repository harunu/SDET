using LinnWorksUITests.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

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
