using System;
using System.Collections.Generic;
using MySqlConnector;
using System.Threading.Tasks;

namespace TodoApp
{
    public class MySqlTodoRepository : ITodoRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=todo_db;User ID=user;Password=12345;";

        public async Task<List<TodoItem>> GetAllAsync()
        {

            var tasks = new List<TodoItem>();
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM todos";
                using var command = new MySqlCommand(sql, connection);
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tasks.Add(new TodoItem
                    {
                        Id = reader.GetInt32("id"),
                        Title = reader.GetString("title"),
                        IsCompleted = reader.GetBoolean("is_completed")
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }

            return tasks;
        }

        public async Task AddAsync(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                string sql = "INSERT INTO todos (title) Values (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task CompleteAsync(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                string sql = "UPDATE `todos` SET `is_completed`='1' WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task UncompleteAsync(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                string sql = "UPDATE `todos` SET `is_completed`='0' WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public async Task DeleteAsync(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                await connection.OpenAsync();
                string sql = "DELETE FROM `todos` WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }
    }
}
