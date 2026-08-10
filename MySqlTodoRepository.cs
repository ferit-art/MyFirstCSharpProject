using System;
using System.Collections.Generic;
using MySqlConnector;

namespace TodoApp
{
    public class MySqlTodoRepository : ITodoRepository
    {
        private readonly string _connectionString = "Server=localhost;Database=todo_db;User ID=user;Password=12345;";

        public List<TodoItem> GetAll()
        {

            var tasks = new List<TodoItem>();
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                string sql = "SELECT * FROM todos";
                using var command = new MySqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
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

        public void Add(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                string sql = "INSERT INTO todos (title) Values (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public void Complete(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                string sql = "UPDATE `todos` SET `is_completed`='1' WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public void Uncomplete(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                string sql = "UPDATE `todos` SET `is_completed`='0' WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public void Delete(string title)
        {
            using var connection = new MySqlConnection(_connectionString);

            try
            {
                connection.Open();
                string sql = "DELETE FROM `todos` WHERE title LIKE (@title)";
                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", title);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }
    }
}
