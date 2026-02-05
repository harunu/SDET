using Microsoft.Data.SqlClient;

namespace DatabaseFactory
{
    public class DBManager
    {
        public SqlConnection Connection;
        public DBManager()
        {
            string connectionString = DBStart.GetConnectionString();
            Connection = DBAccess.OpenConnection(connectionString);
        }
    }
}
