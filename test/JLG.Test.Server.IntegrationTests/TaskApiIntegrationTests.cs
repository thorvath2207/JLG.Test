using System.Net.Http.Json;

using JLG.Test.Server.Models;

using Microsoft.AspNetCore.Mvc.Testing;

namespace JLG.Test.Server.IntegrationTests;
public class TaskApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TaskApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AddTask_ReturnsOkResult()
    {
        var client = _factory.CreateClient();

        var task = new TaskModel { Id = 1, Title = "Test Task", Description = "Test Description", CreatedAt = DateTime.Now, DueDate = DateTime.Now.AddDays(1), IsCompleted = false };

        var response = await client.PostAsJsonAsync("/api/task/add", task);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTask_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync("/api/task/delete/2");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetTasks_ShouldReturnOk()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/task/get");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<TaskModel>>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetTask_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/task/get/1");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TaskModel>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateTask_ShouldReturnOk()
    {
        var client = _factory.CreateClient();

        var task = new TaskModel
        {
            Id = 1,
            Title = "Updated Task",
            Description = "Updated Description",
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false
        };

        var response = await client.PutAsJsonAsync("/api/task/update", task);

        response.EnsureSuccessStatusCode();
    }
}
