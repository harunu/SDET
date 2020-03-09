using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinnWorksUITests.Pages
{
    public class Categories : Base
    {
        private const string editCategoryLinkXPath = ".//a[text() = 'Edit']";
        private const string deleteCategoryLinkXPath = ".//a[text() = 'Delete']";

        private IWebElement CategoriesLbl => Driver.FindElement(By.XPath("//app-fetch-category/h1[text() = 'Categories']"));
        private IWebElement CreateNewCategoryLink => Driver.FindElement(By.XPath("//a[text() = 'Create New']"));
        private IWebElement CategoriesTable => Driver.FindElement(By.TagName("table"));

        public AddCategory GoToAddCategoryPage()
        {
            CreateNewCategoryLink.Click();
            return new AddCategory();
        }

        public bool IsCategoriesLblDisplayed()
        {
            return CategoriesLbl.Displayed;
        }


        public bool IsCategoryRowDisplayed(string category)
        {
            var row = GetCategoryRow(category);

            return row.Displayed
                   && row.FindElement(By.XPath("./td[text() = '0']")).Displayed
                   && row.FindElement(By.XPath(editCategoryLinkXPath)).Displayed
                   && row.FindElement(By.XPath(deleteCategoryLinkXPath)).Displayed;
        }

       private IWebElement GetCategoryRow(string category)
        {
            if (TryGetCategoryRow(category, out var row))
            {
                return row;
            }
            throw new ArgumentException($"Can't get row for '{category}' category!");
        }

        public bool TryGetCategoryRow(string category, out IWebElement row)
        {
            var elements = CategoriesTable.FindElements(By.XPath($".//tr[./td[text() = '{category}']]"));
            row = null;
            if (!elements.Any())
                return false;
            row = elements.FirstOrDefault();
            return true;
        }
    }
}
