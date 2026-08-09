using MySqlConnector;

namespace TodoApp
{
    class Program
    {
        private static string connectionString = "Server=localhost;Database=todo_db;User ID=user;Password=12345;";

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n" + "=== TODO-LIST ===");
                Console.WriteLine("1. Show all tasks");
                Console.WriteLine("2. Add new task");
                Console.WriteLine("3. Complete task");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Choose between alternatives (1-3): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowTasks();
                        break;

                    case "2":
                        Console.Write("\n" + "The task's title: ");
                        string title = Console.ReadLine();
                        AddTask(title);
                        break;


                    case "3":
                        Console.WriteLine("\n" + "The task's title: ");
                        string completed_title = Console.ReadLine();
                        CompleteTask(completed_title);
                        break;

                    case "4":
                        Console.WriteLine("\n" + "Bye!");
                        return;

                    default:
                        Console.WriteLine("Invalid answer.");
                        break;
                }
            }
        }

        public static void ShowTasks()
        {
            using var connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                string sql = "SELECT * FROM todos";
                using var command = new MySqlCommand(sql, connection);
                using var reader = command.ExecuteReader();

                Console.WriteLine("\n" + "=== YOUR TODO LIST ===");
                Console.WriteLine("---------------------------------------------");

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string title = reader.GetString("title");
                    bool isCompleted = reader.GetBoolean("is_completed");

                    string status = isCompleted ? "[DONE]" : "[]";
                    Console.WriteLine($"{id}. {status} {title}");
                }
                Console.WriteLine("---------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + $"Database Error: {e.Message}");
            }
        }

        public static void AddTask(string title)
        {
            using var connection = new MySqlConnection(connectionString);

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

            ShowTasks();
        }

        public static void CompleteTask(string title)
        {
            using var connection = new MySqlConnection(connectionString);

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
            ShowTasks();
        }
    }
}
