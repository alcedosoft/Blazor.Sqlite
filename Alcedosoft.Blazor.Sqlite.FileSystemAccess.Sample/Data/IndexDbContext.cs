namespace Alcedosoft.Blazor.Sqlite.FileSystemAccess.Sample;

public class IndexDbContext : BlazorDbContext
{
    public IndexDbContext()
    {

    }

    public IndexDbContext(DbContextOptions<IndexDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItem { get; set; }
}
