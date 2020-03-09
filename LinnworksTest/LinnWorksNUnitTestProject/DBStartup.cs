using System;
using System.Collections.Generic;
using System.Text;
using LinnworksTest.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinnWorksNUnitTestProject
{
    public static class DBStartup
    {
        public static IServiceProvider ConfigureServiceProvider()
        {
            var testConnection = new DatabaseFactory.DBManager().Connection;

            var services = new ServiceCollection();
            services.AddDbContext<CategoriesManagementContext>
                (options => options.UseSqlServer(testConnection.ConnectionString.ToString()));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITokenRepository, TokenRepository>();
            return services.BuildServiceProvider();
        }
    }
}
