using DatabaseFactory;
using FluentAssertions;
using LinnWorksUITests.Base;
using LinnWorksUITests.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinnWorksUITests.Tests
{
    public class CategoryTestPage : BasePageTest
    {
        private readonly DBManager _dboperations;

        public CategoryTestPage(Categories categoriesPage)
        {
            _categoriesPage = categoriesPage;
        }

        private readonly Categories _categoriesPage;

        private const string CategoryName = "UI Test Category";

        public CategoryTestPage()
        {
            _categoriesPage = new Categories();
            _dboperations = new DBManager();
        }

        public CategoryTestPage(DBManager dboperations)
        {
            _dboperations = dboperations;
        }

        [SetUp]
        public void Setup()
        {
            // ARRANGE
   
            BaseDriver.Current.Driver.Navigate().GoToUrl(StartupConstants.ServiceUrl + "/fetch-category");
        }

        [TearDown]
        public void Teardown()
        {

        }
    }
}
