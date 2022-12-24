namespace Alcedosoft.Blazor.Sqlite;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlazorDbContextFactory<TDbContext>(
        this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        where TDbContext : BlazorDbContext
    {
        _ = services.AddDbContextFactory<TDbContext>(optionsAction);

        _ = services.AddSingleton<IBlazorDbContextFactory<TDbContext>, BlazorDbContextFactory<TDbContext>>();

        return services;
    }
}
