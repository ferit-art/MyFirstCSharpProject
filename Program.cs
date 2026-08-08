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
                Console.WriteLine("3. Exit");
                Console.WriteLine("Choose between alternatives (1-3): ");

                string choice = Console.ReadLine();

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

        }

        public static void AddTask(string title) {

        }
    }
}
