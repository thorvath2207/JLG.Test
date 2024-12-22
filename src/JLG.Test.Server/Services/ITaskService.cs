using JLG.Test.Server.Models;

namespace JLG.Test.Server.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> GetTasks();

    Task<TaskModel> GetTask(int id);

    Task<TaskModel> AddTask(TaskModel task);

    Task UpdateTask(TaskModel task);

    Task DeleteTask(int id);
}
