namespace Alcedosoft.Blazor.Sqlite;

public interface IBlazorDbContextFactory<TContext>
        where TContext : BlazorDbContext
{
    Task InitializeAsync(
        FileSystemFileHandle fileHandle,
        CancellationToken cancellationToken = default);

    Task InitializeAsync(
        FileSystemDirectoryHandle directoryHandle,
        CancellationToken cancellationToken = default);

    Task<TContext> CreateDbContextAsync(
        CancellationToken cancellationToken = default);
}
