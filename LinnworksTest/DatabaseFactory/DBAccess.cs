using System;
using System.Configuration;
using System.Data.SqlClient;

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
            var sqlConnection = (SqlConnection)connection;
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
