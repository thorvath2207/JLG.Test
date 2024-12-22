namespace JLG.Test.Server.Models;

public class TaskModel
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}