using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JLG.Test.Server.Database;

public class TaskDataContext : DbContext
{
    public TaskDataContext(DbContextOptions<TaskDataContext> options) : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}
