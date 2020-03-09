using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


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
