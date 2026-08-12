using System;
using System.Collections.Generic;
using MySqlConnector;
using System.Threading.Tasks;
using Dapper;

namespace TodoApp
{
    public class MySqlTodoRepository : ITodoRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=todo_db;User ID=user;Password=12345;";

        public async Task<List<TodoItem>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            try
            {
                string sqlCommand = "SELECT id, title, is_completed AS IsCompleted FROM todos";
                var tasks = await connection.QueryAsync<TodoItem>(sqlCommand);

                return tasks.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"{e.Message}");
                return new List<TodoItem>();
            }
        }

        public async Task AddAsync(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                string sql = "INSERT INTO todos (title) Values (@Title)";
                await connection.ExecuteAsync(sql, new { Title = title });
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task CompleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                string sql = "UPDATE `todos` SET `is_completed`='1' WHERE id LIKE (@Id)";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task UncompleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                string sql = "UPDATE `todos` SET `is_completed`='0' WHERE id LIKE (@Id)";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                string sql = "DELETE FROM `todos` WHERE id LIKE (@Id)";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }
    }
}
