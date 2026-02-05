using LinnWorksNUnitTestProject.IntegrationDBTests;
using Microsoft.Data.SqlClient;

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