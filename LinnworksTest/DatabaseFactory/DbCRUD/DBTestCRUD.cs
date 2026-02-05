using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using Dapper;
using DatabaseFactory.Models;

namespace DatabaseFactory.DbCRUD
{
    public class DBTestCRUD
    {
        public SqlConnection OpenConnection()
        {
            var connection = new DBManager().Connection;
            return connection;
        }

        public void AddToken(Guid token)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Execute(
                    "INSERT INTO Tokens (ID, Value) VALUES (@Id, @Value)",
                    new { Id = Guid.NewGuid(), Value = token });
            }
        }

        public void RemoveTokenByValue(Guid token)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Execute(
                    "DELETE FROM Tokens WHERE Value = @Value",
                    new { Value = token });
            }
        }

        public IEnumerable<Models.Categories> GetCategoriesById(Guid id)
        {
            using (var connection = new DBManager().Connection)
            {
                return connection.Query<Categories>(
                    "SELECT * FROM Categories WHERE ID = @Id",
                    new { Id = id });
            }
        }

        public IEnumerable<Categories> GetCategories()
        {
            using (var connection = new DBManager().Connection)
            {
                return connection.Query<Categories>("SELECT * FROM Categories");
            }
        }

        public void AddCategory(Categories category)
        {
            using (var connection = new DBManager().Connection)
            {
                connection.Execute(
                    "INSERT INTO Categories (ID, CategoryName) VALUES (@Id, @CategoryName)",
                    new { Id = category.CategoryId, CategoryName = category.CategoryName });
            }
        }

        public void CleanTable(string tableName)
        {
            // Only allow known table names to prevent SQL injection
            var allowedTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Tokens", "Categories", "Products"
            };

            if (!allowedTables.Contains(tableName))
            {
                throw new ArgumentException($"Table '{tableName}' is not in the allowed list.", nameof(tableName));
            }

            using (var connection = new DBManager().Connection)
            {
                connection.Execute($"DELETE FROM [{tableName}]");
            }
        }
    }
}
