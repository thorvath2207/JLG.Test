using JLG.Test.Server.Database;
using JLG.Test.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace JLG.Test.Server.Services;

public class TaskService(TaskDataContext context) : ITaskService
{
    private readonly TaskDataContext _context = context;

    public async Task<TaskModel> AddTask(TaskModel task)
    {
        var taskToAdd = new TaskEntity
        {
            Title = task.Title,
            Description = task.Description,
            CreatedAt = DateTime.UtcNow,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted
        };

        _context.Tasks.Add(taskToAdd);
        await _context.SaveChangesAsync();

        task.Id = taskToAdd.Id;
        task.CreatedAt = taskToAdd.CreatedAt;

        return task;
    }

    public async Task DeleteTask(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("Task not found");
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskModel> GetTask(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new Exception("Task not found");
        return new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted
        };
    }

    public async Task<IEnumerable<TaskModel>> GetTasks()
    {
        var tasks = await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
        return tasks.Select(t => new TaskModel
        {
            CreatedAt = t.CreatedAt,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            IsCompleted = t.IsCompleted,
            Id = t.Id
        });
    }

    public async Task UpdateTask(TaskModel task)
    {
        var taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id) ?? throw new Exception("Task not found");

        taskToUpdate.Title = task.Title;
        taskToUpdate.Description = task.Description;
        taskToUpdate.DueDate = task.DueDate;
        taskToUpdate.IsCompleted = task.IsCompleted;
        await _context.SaveChangesAsync();
    }
}
