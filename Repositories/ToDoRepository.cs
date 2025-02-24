using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.API.Models;

namespace ToDoApp.API.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly string _connectionString;

        public ToDoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = "SELECT * FROM ToDoItems ORDER BY SortOrder";
            return conn.Query<ToDoItem>(sql);
        }

        public ToDoItem? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = "SELECT * FROM ToDoItems WHERE Id = @Id";
            return conn.QueryFirstOrDefault<ToDoItem>(sql, new { Id = id });
        }

        public ToDoItem Add(ToDoItem item)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"
                INSERT INTO ToDoItems (Title, IsDone, SortOrder)
                VALUES (@Title, @IsDone, @SortOrder);
                SELECT CAST(SCOPE_IDENTITY() as int)";
            item.Id = conn.ExecuteScalar<int>(sql, item);
            return item;
        }

        public bool Update(ToDoItem item)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"
                UPDATE ToDoItems
                SET Title = @Title,
                    IsDone = @IsDone,
                    SortOrder = @SortOrder
                WHERE Id = @Id";
            var rowsAffected = conn.Execute(sql, item);
            return rowsAffected > 0;
        }

        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = "DELETE FROM ToDoItems WHERE Id = @Id";
            var rowsAffected = conn.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
