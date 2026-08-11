using MySqlConnector;
using System.Threading.Tasks;

namespace TodoApp
{
    class Program
    {
        private static readonly ITodoRepository _repository = new MySqlTodoRepository();

        public static async Task Main(string[] args)
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
                        await ShowTasksAsync();
                        break;

                    case "2":
                        Console.Write("\n" + "The task's title: ");
                        string title = Console.ReadLine();
                        await _repository.AddAsync(title);
                        await ShowTasksAsync();
                        break;

                    case "3":
                        Console.WriteLine("\n" + "The task's title: ");
                        string completedTitle = Console.ReadLine();
                        await _repository.CompleteAsync(completedTitle);
                        await ShowTasksAsync();
                        break;

                    case "4":
                        Console.WriteLine("\n" + "The task's title: ");
                        string uncompleteTitle = Console.ReadLine();
                        await _repository.UncompleteAsync(uncompleteTitle);
                        await ShowTasksAsync();
                        break;

                    case "5":
                        Console.WriteLine("\n" + "The task's title: ");
                        string deleteTitle = Console.ReadLine();
                        await _repository.DeleteAsync(deleteTitle);
                        await ShowTasksAsync();
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

        public static async Task ShowTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();

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
                    Console.WriteLine($"#{item.Id} {status} {item.Title}");
                }
            }

            Console.WriteLine("---------------------------------------------");
        }
    }
}
