using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task AddAsync(string title);
        Task CompleteAsync(int id);
        Task UncompleteAsync(int id);
        Task DeleteAsync(int id);
    }
}
