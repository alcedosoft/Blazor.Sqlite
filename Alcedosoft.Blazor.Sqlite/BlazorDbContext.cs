namespace Alcedosoft.Blazor.Sqlite;

public class BlazorDbContext : DbContext
{
    public BlazorDbContext()
    {

    }

    public BlazorDbContext(DbContextOptions options) : base(options)
    {

    }

    internal FileSystemFileHandle FileHandle { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        if (result > 0 && FileHandle is not null)
        {
            await PersistenceAsync(cancellationToken);
        }

        return result;
    }

    internal async Task PersistenceAsync(CancellationToken cancellationToken = default)
    {
        await Database.CloseConnectionAsync();

        var options = new FileSystemCreateWritableOptions{ KeepExistingData = false};

        var sourceDataSource = Database.GetDataSource();
        var backupDataSource = $"{(long)(DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds}.bak";

        var sourceConnection = new SqliteConnection($"Data Source={sourceDataSource}");
        var backupConnection = new SqliteConnection($"Data Source={backupDataSource}");

        await sourceConnection.OpenAsync(cancellationToken);
        await backupConnection.OpenAsync(cancellationToken);

        sourceConnection.BackupDatabase(backupConnection);

        await sourceConnection.CloseAsync();
        await backupConnection.CloseAsync();

        var reader = File.OpenRead(backupDataSource);
        var writer = await FileHandle.CreateWritableAsync(options);

        await reader.CopyToAsync(writer, cancellationToken);

        reader.Close();
        writer.Close();

        File.Delete(backupDataSource);
    }
}
