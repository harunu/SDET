using DatabaseFactory.Models;
using FluentAssertions;
using LinnworksTest.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinnWorksNUnitTestProject.IntegrationDBTests
{
    [TestFixture]
    public class CategoryTest : IntegrationBase
    {
        private IGenericRepository<Category> _categoryRepo;

        private Categories _category;
        private const string CategoryName = "Test Category";
        [SetUp]
        public void Setup()
        {
            _categoryRepo = DBStartup.ConfigureServiceProvider().GetRequiredService<IGenericRepository<Category>>();
        }

        [Test]
        public void CreateAsync_WhenCategoryDoesntExist_CreatedIsSuccess()
        {
            // ARRANGE
            _category = new Categories
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = CategoryName
            };

            // ACT
            _dbtest.AddCategory(_category);
            var categoriesFromDb = _dbtest.GetCategoriesById(_category.CategoryId);

            // ASSERT
            categoriesFromDb.Should()
                            .HaveCount(1)
                            .And
                            .Contain(c => c.Id.Equals(_category.CategoryId) && c.CategoryName.Equals(_category.CategoryName));
        }
    }
}
