using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class TodoServiceTests
{
    [Fact]
    public void Add_CreatesTodoWithIncrementingId()
    {
        var svc = new TodoService();

        var first = svc.Add("Buy milk");
        var second = svc.Add("Walk dog");

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
        Assert.False(first.IsComplete);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenNotFound()
    {
        var svc = new TodoService();

        var result = svc.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public void GetById_ReturnsTodo_WhenExists()
    {
        var svc = new TodoService();
        var created = svc.Add("Test task");

        var result = svc.GetById(created.Id);

        Assert.NotNull(result);
        Assert.Equal("Test task", result!.Title);
    }

    [Fact]
    public void Update_ModifiesExistingTodo_ReturnsTrue()
    {
        var svc = new TodoService();
        var created = svc.Add("Original");

        var success = svc.Update(created.Id, new() { Title = "Updated", IsComplete = true });
        var updated = svc.GetById(created.Id);

        Assert.True(success);
        Assert.Equal("Updated", updated!.Title);
        Assert.True(updated.IsComplete);
    }

    [Fact]
    public void Update_ReturnsFalse_WhenTodoDoesNotExist()
    {
        var svc = new TodoService();

        var success = svc.Update(999, new() { Title = "X" });

        Assert.False(success);
    }

    [Fact]
    public void Delete_RemovesTodo_ReturnsTrue()
    {
        var svc = new TodoService();
        var created = svc.Add("To delete");

        var success = svc.Delete(created.Id);

        Assert.True(success);
        Assert.Null(svc.GetById(created.Id));
    }

    [Fact]
    public void Delete_ReturnsFalse_WhenTodoDoesNotExist()
    {
        var svc = new TodoService();

        var success = svc.Delete(999);

        Assert.False(success);
    }
}