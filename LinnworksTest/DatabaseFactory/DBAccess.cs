using Microsoft.Data.SqlClient;

namespace DatabaseFactory
{
    public static class DBAccess
    {
        public static SqlConnection OpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            connection.Dispose();
        }
    }
}
