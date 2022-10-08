namespace Alcedosoft.Blazor.Sqlite.FileSystemAccess;

internal static class BlazorDbContextExtensions
{
    public static string GetDataSource(this DatabaseFacade database)
    {
        var connection = database.GetDbConnection();

        return connection.DataSource;
    }
}
