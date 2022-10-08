namespace Alcedosoft.Blazor.Sqlite.FileSystemAccess.Sample;

public class OriginPrivateDbContext : BlazorDbContext
{
    public OriginPrivateDbContext()
    {

    }

    public OriginPrivateDbContext(DbContextOptions<OriginPrivateDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItem { get; set; }
}
