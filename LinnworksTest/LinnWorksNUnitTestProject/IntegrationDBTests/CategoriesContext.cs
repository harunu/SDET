using _dbtest;
using LinnworksTest.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LinnWorksNUnitTestProject
{

    public static class CategoriesContext
    {
        public static CategoriesManagementContext GetLinnworksIntegrationContext()
        {
            OpenConnection test = new OpenConnection();
            var context = new CategoriesManagementContext(new DbContextOptionsBuilder<CategoriesManagementContext>()
                .UseSqlServer(test.Connection.ConnectionString).Options);
            return context;
        }
    }
}


