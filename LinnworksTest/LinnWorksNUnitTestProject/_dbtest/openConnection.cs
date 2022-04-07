using LinnWorksNUnitTestProject.IntegrationDBTests;
using System;
using System.Data.SqlClient;

namespace _dbtest
{
    public class OpenConnection : IntegrationBase
    {
        public SqlConnection Connection;
        public OpenConnection()
        {
            Connection = _dbtest.OpenConnection();
        }
    }
}