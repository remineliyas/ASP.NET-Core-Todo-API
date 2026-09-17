using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    private readonly List<TodoItem> _todos = new();
    private int _nextId = 1;

    public List<TodoItem> GetAll() => _todos;

    public TodoItem? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);

    public TodoItem Add(string title)
    {
        var todo = new TodoItem { Id = _nextId++, Title = title, IsComplete = false };
        _todos.Add(todo);
        return todo;
    }

    public bool Update(int id, TodoItem updated)
    {
        var existing = GetById(id);
        if (existing is null) return false;
        existing.Title = updated.Title;
        existing.IsComplete = updated.IsComplete;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return false;
        _todos.Remove(existing);
        return true;
    }
}