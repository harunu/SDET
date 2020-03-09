using _dbtest;
using LinnWorksNUnitTestProject.IntegrationDBTests;
using LinnworksTest.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LinnWorksNUnitTestProject
{

    public static class CategoriesContext
    {
        public static CategoriesManagementContext GetLinnworksIntegrationContext()
        {
            openConnection test = new openConnection();
            var context = new CategoriesManagementContext(new DbContextOptionsBuilder<CategoriesManagementContext>()
                .UseSqlServer(test.Connection.ConnectionString).Options);
            return context;
        }

    }
}


