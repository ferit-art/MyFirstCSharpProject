using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task AddAsync(string title);
        Task CompleteAsync(string title);
        Task UncompleteAsync(string title);
        Task DeleteAsync(string title);
    }
}
