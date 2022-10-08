namespace Alcedosoft.Blazor.Sqlite.FileSystemAccess;

internal class BlazorDbContextFactory<TDbContext> : IBlazorDbContextFactory<TDbContext>
    where TDbContext : BlazorDbContext
{
    private FileSystemFileHandle? _fileHandle;
    private readonly IDbContextFactory<TDbContext> _contextFactory;

    public BlazorDbContextFactory(IDbContextFactory<TDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task InitializeAsync(FileSystemFileHandle fileHandle, CancellationToken cancellationToken = default)
    {
        await InitializeCoreAsync(fileHandle, cancellationToken);
    }

    public async Task InitializeAsync(FileSystemDirectoryHandle directoryHandle, CancellationToken cancellationToken = default)
    {
        await InitializeCoreAsync(directoryHandle, cancellationToken);
    }

    public Task<TDbContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
    {
        var context = _contextFactory.CreateDbContext();

        context.FileHandle = _fileHandle ?? throw new InvalidDataException(
            "Please call method BlazorDbContextFactory.InitializeAsync first.");

        return Task.FromResult(context);
    }

    private async Task InitializeCoreAsync(FileSystemHandle handle, CancellationToken cancellationToken = default)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var dataSource = context.Database.GetDataSource();

        if (handle is FileSystemDirectoryHandle directoryHandle)
        {
            var fileOptions = new FileSystemGetFileOptions { Create = true };

            _fileHandle = await directoryHandle.GetFileHandleAsync(dataSource, fileOptions);

        }
        else if (handle is FileSystemFileHandle fileHandle)
        {
            _fileHandle = fileHandle;
        }
        else
        {
            throw new InvalidProgramException();
        }

        var file = await _fileHandle.GetFileAsync();

        var buffer = await file.ArrayBufferAsync();

        await context.Database.CloseConnectionAsync();

        await File.WriteAllBytesAsync(dataSource, buffer, cancellationToken);

        if (await context.Database.EnsureCreatedAsync(cancellationToken))
        {
            context.FileHandle = _fileHandle;

            await context.PersistenceAsync(cancellationToken);
        }
    }
}
