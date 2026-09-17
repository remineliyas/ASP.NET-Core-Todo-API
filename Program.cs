using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/todos", (TodoService svc) => svc.GetAll());

app.MapGet("/todos/{id}", (int id, TodoService svc) =>
    svc.GetById(id) is { } todo ? Results.Ok(todo) : Results.NotFound());

app.MapPost("/todos", (TodoItem input, TodoService svc) =>
{
    var todo = svc.Add(input.Title);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", (int id, TodoItem input, TodoService svc) =>
    svc.Update(id, input) ? Results.NoContent() : Results.NotFound());

app.MapDelete("/todos/{id}", (int id, TodoService svc) =>
    svc.Delete(id) ? Results.NoContent() : Results.NotFound());

app.Run();