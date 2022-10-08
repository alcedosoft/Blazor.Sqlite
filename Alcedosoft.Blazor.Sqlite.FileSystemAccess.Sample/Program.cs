var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFileSystemAccessService();

builder.Services.AddBlazorDbContextFactory<IndexDbContext>(
    options => options.UseSqlite("Data Source=Index.sqlite3"));
builder.Services.AddBlazorDbContextFactory<DirectoryHandleDbContext>(
    options => options.UseSqlite("Data Source=DirectoryHandle.sqlite3"));
builder.Services.AddBlazorDbContextFactory<OriginPrivateDbContext>(
    options => options.UseSqlite("Data Source=OriginPrivate.sqlite3"));
builder.Services.AddBlazorDbContextFactory<FileHandleDbContext>(
    options => options.UseSqlite("Data Source=FileHandle.sqlite3"));

builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "Alcedosoft.Blazor.Sqlite.FileSystemAccess.Sample";
    dbStore.Version = 1;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "DirectoryHandle",
    });

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "FileHandle",
    });
});

await builder.Build().RunAsync();
