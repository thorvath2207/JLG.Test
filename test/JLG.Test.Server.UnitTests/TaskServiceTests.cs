using JLG.Test.Server.Database;
using JLG.Test.Server.Models;
using JLG.Test.Server.Services;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace JLG.Test.Server.UnitTests;

public class TaskServiceTests
{
    [Fact]
    public async Task AddTask_ShouldAddTask()
    {
        var context = await CreateContextAsync();
        var sut = new TaskService(context);

        var task = new TaskModel
        {
            Title = "Test Task",
            Description = "Test Description",
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        var result = await sut.AddTask(task);

        Assert.NotNull(result);
        Assert.Equal(task.Title, result.Title);
        Assert.Equal(task.Description, result.Description);
        Assert.Equal(task.DueDate, result.DueDate);
        Assert.Equal(task.IsCompleted, result.IsCompleted);
    }

    [Fact]
    public async Task DeleteTask_ShouldRemoveTask()
    {
        var context = await CreateContextAsync();
        var sut = new TaskService(context);

        var task = new TaskEntity
        {
            Id = 3000,
            Title = "Test Task",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        await sut.DeleteTask(task.Id);

        var deletedTask = await context.Tasks.FindAsync(task.Id);
        Assert.Null(deletedTask);
    }

    [Fact]
    public async Task GetTask_ShouldReturnTask()
    {
        var context = await CreateContextAsync();
        var sut = new TaskService(context);

        var task = new TaskEntity
        {
            Id = 1000,
            Title = "Test Task",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var result = await sut.GetTask(task.Id);

        Assert.NotNull(result);
        Assert.Equal(task.Id, result.Id);
        Assert.Equal(task.Title, result.Title);
        Assert.Equal(task.Description, result.Description);
        Assert.Equal(task.CreatedAt, result.CreatedAt);
        Assert.Equal(task.DueDate, result.DueDate);
        Assert.Equal(task.IsCompleted, result.IsCompleted);
    }

    [Fact]
    public async Task GetTasks_ShouldReturnAllTasks()
    {
        var context = await CreateContextAsync();
        var sut = new TaskService(context);

        var tasks = new List<TaskEntity>
        {
            new() {
                Id = 2000,
                Title = "Test Task 1",
                Description = "Test Description 1",
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(1),
                IsCompleted = false
            },
            new() {
                Id = 2001,
                Title = "Test Task 2",
                Description = "Test Description 2",
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(2),
                IsCompleted = true
            }
        };

        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();

        var result = await sut.GetTasks();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateTask_ShouldUpdateTask()
    {
        var context = await CreateContextAsync();
        var sut = new TaskService(context);

        var task = new TaskEntity
        {
            Id = 10001,
            Title = "Test Task",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var updatedTask = new TaskModel
        {
            Id = task.Id,
            Title = "Updated Task",
            Description = "Updated Description",
            DueDate = DateTime.UtcNow.AddDays(2),
            IsCompleted = true
        };

        await sut.UpdateTask(updatedTask);

        var result = await context.Tasks.FindAsync(task.Id);

        Assert.NotNull(result);
        Assert.Equal(updatedTask.Title, result.Title);
        Assert.Equal(updatedTask.Description, result.Description);
        Assert.Equal(updatedTask.DueDate, result.DueDate);
        Assert.Equal(updatedTask.IsCompleted, result.IsCompleted);
    }

    private static DbContextOptions<TaskDataContext> CreateInMemoryDatabaseOptions()
    {
        var connection = new SqliteConnection("Data Source=TaskUnitTests;Mode=Memory;Cache=Shared");
        connection.Open(); // we need to open the connection explicitly and keep it open. if the connection is closed, the in-memory database will be deleted
        return new DbContextOptionsBuilder<TaskDataContext>()
            .UseSqlite(connection)
            .Options;
    }

    private static async Task<TaskDataContext> CreateContextAsync()
    {
        var options = CreateInMemoryDatabaseOptions();
        var context = new TaskDataContext(options);
        await context.Database.EnsureCreatedAsync();
        return context;
    }
}
