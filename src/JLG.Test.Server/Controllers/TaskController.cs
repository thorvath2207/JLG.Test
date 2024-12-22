using JLG.Test.Server.Models;
using JLG.Test.Server.Services;

using Microsoft.AspNetCore.Mvc;

namespace JLG.Test.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService,
    ILogger<TaskController> logger) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;
    private readonly ILogger<TaskController> _logger = logger;

    [HttpPost("add")]
    public async Task<IActionResult> AddTask([FromBody] TaskModel task)
    {
        _logger.LogInformation("Adding task {@task}", task);
        var result = await _taskService.AddTask(task);

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        _logger.LogInformation("Deleting task with id {id}", id);
        await _taskService.DeleteTask(id);
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTasks()
    {
        _logger.LogInformation("Getting all tasks");
        var result = await _taskService.GetTasks();
        return Ok(result);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        _logger.LogInformation("Getting task with id {id}", id);
        var result = await _taskService.GetTask(id);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskModel task)
    {
        _logger.LogInformation("Updating task {@task}", task);
        await _taskService.UpdateTask(task);
        return Ok();
    }
}
