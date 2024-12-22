namespace JLG.Test.Server.Database;

public class TaskEntity
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required DateTime CreatedAt { get; set; }

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}
