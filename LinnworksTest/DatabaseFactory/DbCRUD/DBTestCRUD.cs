using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DatabaseFactory.Models;

namespace DatabaseFactory.DbCRUD
{
    public class DBTestCRUD {
        public SqlConnection  openConnection()
        {
            var connection = new DBManager().Connection;
            return connection;
        }

        public void AddToken(Guid token)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Query($"INSERT INTO Tokens (ID, Value) Values ('{Guid.NewGuid().ToString()}', '{token.ToString()}')");
            }
        }

        public void RemoveTokenByValue(Guid token)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Query($"DELETE FROM Tokens WHERE Value = '{token.ToString()}'");
            }
        }

        public IEnumerable<Models.Categories> GetCategoriesById(Guid id)
        {
            using (var connection = new DBManager().Connection)
            {
                return connection.Query<Categories>($"SELECT * FROM Categories WHERE ID = '{id.ToString()}'");
            }
        }

        public IEnumerable<Categories> GetCategories()
        {
            using (var connection = new DBManager().Connection)
            {
                return connection.Query<Categories>($"SELECT * FROM Categories");
            }
        }

        public void AddCategory(Categories category)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Query($"INSERT INTO Categories (ID, CategoryName) Values ('{category.CategoryId.ToString()}', '{category.CategoryName}')");
            }
        }

        public void CleanTable(string tableName)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Query($"DELETE FROM {tableName}");
            }
        }
    }
}
