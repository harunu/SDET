using System;

namespace LinnWorksUITests.Base
{
    public static class StartupConstants
    {
        public static string ServiceUrl =>
            Environment.GetEnvironmentVariable("LINNWORKS_SERVICE_URL") ?? "http://localhost:59509/";

        public static string Token =>
            Environment.GetEnvironmentVariable("LINNWORKS_TEST_TOKEN") ?? "bccf905c-6592-40f2-8db1-c976791fa40a";
    }
}
