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
        private readonly  DBManager  _dboperations;
        private Categories _categoriesPage;

        private const string CategoryName = "UI Test Category";

        public CategoryTestPage()
        {
            _categoriesPage = new Categories();
            _dboperations = new DBManager();
        }

        [SetUp]
        public void Setup()
        {
            // ARRANGE
   
            BaseDriver.Current.Driver.Navigate().GoToUrl(StartupConstants.ServiceUrl + "/fetch-category");
        }

     /*   [Test]
        public void CategoryShouldBeDisplayedOnCategoriesPage()
        {
            // ACT
            var addCategoryPage = _categoriesPage.GoToAddCategoryPage();
      

            // ASSERT
            _categoriesPage.IsCategoryRowDisplayed(CategoryName).Should()
                           .BeTrue();
        }

    */

        [TearDown]
        public void Teardown()
        {

        }


    }
}
