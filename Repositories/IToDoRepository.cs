using ToDoApp.API.Models;

namespace ToDoApp.API.Repositories
{
    public interface IToDoRepository
    {
        IEnumerable<ToDoItem> GetAll();
        ToDoItem Add(ToDoItem item);
        bool Update(ToDoItem item);
        bool Delete(int id);
        ToDoItem? GetById(int id);
    }
}
