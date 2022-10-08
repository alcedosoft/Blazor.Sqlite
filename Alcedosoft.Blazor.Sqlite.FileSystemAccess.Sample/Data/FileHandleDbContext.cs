namespace Alcedosoft.Blazor.Sqlite.FileSystemAccess.Sample;

public class FileHandleDbContext : BlazorDbContext
{
    public FileHandleDbContext()
    {

    }

    public FileHandleDbContext(DbContextOptions<FileHandleDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItem { get; set; }
}
