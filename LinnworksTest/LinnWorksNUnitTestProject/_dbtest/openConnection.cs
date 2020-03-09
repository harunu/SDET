using LinnWorksNUnitTestProject.IntegrationDBTests;
using System;
using System.Data.SqlClient;

namespace _dbtest
{
    public class openConnection : IntegrationBase
    {
        public SqlConnection Connection;
        public openConnection()
        {
            Connection = _dbtest.openConnection();
        }
    }
}