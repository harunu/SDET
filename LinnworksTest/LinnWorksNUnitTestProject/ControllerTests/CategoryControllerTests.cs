using LinnWorksNUnitTestProject.IntegrationDBTests;
using LinnworksTest.DataAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinnWorksNUnitTestProject.ControllerTests
{
    [TestFixture]
    public class CategoryControllerTests : IntegrationBase
    {
        [Test]
        public async Task GetAll_Categories_Returns_True()
        {
            object AllCategories;
            // .Arrange
            // .Act
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                var sut = new GenericRepository<Category>(context);
                AllCategories = await sut.GetAllAsync();
            }
            // .Assert
            Assert.NotNull(AllCategories);
        }

        [Test]
        public async Task Create_DuplicatedCategory_ShouldThrowException()
        {
            // .Arrange
            Category category = new Category() { CategoryName = "DUPLICATED" };
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                await context.AddAsync(category);
                await context.SaveChangesAsync();
            }
            var duplicatedCatgory = new Category() { CategoryName = category.CategoryName };

            // .Assert
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                var sut = new GenericRepository<Category>(context);
                Assert.That(async () =>
                    // .Act
                    await sut.CreateAsync(duplicatedCatgory), Throws.Exception);
            }
        }


        [Test]
        public void Update_ExistingCategory_ShouldNotThrowException()
        {
            // .Assert
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                var sut = new GenericRepository<Category>(context);
                Assert.That(async () =>
                    // .Act
                    await sut.UpdateAsync(Guid.NewGuid(), new Category() { CategoryName = Guid.NewGuid().ToString() }), Throws.Nothing);
            }
        }

        [Test]
        public async Task Delete_CategoryWithProducts_ShouldDeleteCategory()
        {
            // .Arrange
            Category category = new Category() { CategoryName = Guid.NewGuid().ToString() };
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                await context.AddAsync(category);
                await context.SaveChangesAsync();
                await context.AddAsync(new Product() { CategoryId = category.Id, Title = Guid.NewGuid().ToString() });
                await context.SaveChangesAsync();
            }

            // .Act
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                var sut = new GenericRepository<Category>(context);
                await sut.DeleteAsync(category.Id);
            }
            // .Assert
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                Assert.IsNull(context.Find<Category>(category.Id));
            }
        }

        [Test]
        public Task Delete_NotExistingCategory_ShouldThrowException()
        {
            // .Assert
            using (var context = CategoriesContext.GetLinnworksIntegrationContext())
            {
                var sut = new GenericRepository<Category>(context);
                Assert.That(async () =>
                    // .Act
                    await sut.DeleteAsync(Guid.NewGuid()), Throws.Exception);
            }

            return Task.CompletedTask;
        }
    }
}
