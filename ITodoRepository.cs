using System.Collections.Generic;

namespace TodoApp
{
    public interface ITodoRepository
    {
        List<TodoItem> GetAll();
        void Add(string title);
        void Complete(string title);
        void Uncomplete(string title);
        void Delete(string title);
    }
}
