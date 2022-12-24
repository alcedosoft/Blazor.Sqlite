namespace Alcedosoft.Blazor.Sqlite.Sample;

public class DirectoryHandleDbContext : BlazorDbContext
{
    public DirectoryHandleDbContext()
    {

    }

    public DirectoryHandleDbContext(DbContextOptions<DirectoryHandleDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItem { get; set; }
}
