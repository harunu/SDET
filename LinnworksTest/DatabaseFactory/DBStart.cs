using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace DatabaseFactory
{
    public static class DBStart
    {
        public static string ConnectionString { get; private set; }
        public static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);
            return appRoot;
        }

        public static IConfigurationRoot GetAppSettings()
        {
            _ = ApplicationExeDirectory();
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            return builder.Build();
        }
        public static string GetConnectionString()
        {
            IConfigurationRoot Configuration = DBStart.GetAppSettings();
            var connectionString = Configuration.GetConnectionString("LinnworksDB");
            return connectionString.ToString();
        }
    }
}