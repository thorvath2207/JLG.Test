using JLG.Test.Server.Database;
using JLG.Test.Server.Services;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keepAliveConnection = new SqliteConnection("Data Source=TaskSample;Mode=Memory;Cache=Shared");
keepAliveConnection.Open(); // keep the in-memory database alive
builder.Services.AddDbContext<TaskDataContext>(options =>
{
    options.UseSqlite(keepAliveConnection);
});

builder.Services.AddControllers();
builder.Services.AddTransient<ITaskService, TaskService>();

// configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers()
    .WithOpenApi();

app.MapFallbackToFile("/swagger/index.html");

using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<TaskDataContext>())
{
    // create database and seed sample data
    context.Database.EnsureCreated();

    context.Tasks.Add(new TaskEntity
    {
        Title = "Sample Task 1",
        Description = "This is a sample task",
        CreatedAt = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(1),
        IsCompleted = false
    });
    context.Tasks.Add(new TaskEntity
    {
        Title = "Sample Task 2",
        Description = "This is another sample task",
        CreatedAt = DateTime.UtcNow,
        DueDate = DateTime.UtcNow.AddDays(2),
        IsCompleted = false
    });
    context.SaveChanges();
}

app.Run();

public partial class Program { }