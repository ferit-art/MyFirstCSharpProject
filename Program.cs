using MySqlConnector;

namespace TodoApp
{
    class Program
    {
        private static readonly ITodoRepository _repository = new MySqlTodoRepository();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n" + "=== TODO-LIST ===");
                Console.WriteLine("1. Show all tasks");
                Console.WriteLine("2. Add new task");
                Console.WriteLine("3. Complete task");
                Console.WriteLine("4. Uncomplete task");
                Console.WriteLine("5. Delete task");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Choose between alternatives (1-6): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowTasks();
                        break;

                    case "2":
                        Console.Write("\n" + "The task's title: ");
                        string title = Console.ReadLine();
                        _repository.Add(title);
                        ShowTasks();
                        break;

                    case "3":
                        Console.WriteLine("\n" + "The task's title: ");
                        string completedTitle = Console.ReadLine();
                        _repository.Complete(completedTitle);
                        ShowTasks();
                        break;

                    case "4":
                        Console.WriteLine("\n" + "The task's title: ");
                        string uncompleteTitle = Console.ReadLine();
                        _repository.Uncomplete(uncompleteTitle);
                        ShowTasks();
                        break;

                    case "5":
                        Console.WriteLine("\n" + "The task's title: ");
                        string deleteTitle = Console.ReadLine();
                        _repository.Delete(deleteTitle);
                        ShowTasks();
                        break;

                    case "6":
                        Console.WriteLine("\n" + "Bye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        public static void ShowTasks()
        {
            var tasks = _repository.GetAll();

            Console.WriteLine("\n" + "=== YOUR TODO LIST ===");
            Console.WriteLine("---------------------------------------------");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var item in tasks)
                {
                    string status = item.IsCompleted ? "[DONE]" : "[ ]";
                    Console.WriteLine($"{item.Id}. {status} {item.Title}");
                }
            }

            Console.WriteLine("---------------------------------------------");
        }
    }
}
