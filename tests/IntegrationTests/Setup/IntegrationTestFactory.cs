using System.Data.Common;
using DotNet.Testcontainers.Builders;
using HotelManagement.Api.Data;
using IntegrationTests.Setup.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Setup;

[CollectionDefinition(nameof(HotelManagementTestCollection))]
public class HotelManagementTestCollection : ICollectionFixture<IntegrationTestFactory> { }

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithDatabase("hotel")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilCommandIsCompleted("pg_isready"))
        .WithCleanUp(true)
        .Build();

    public HttpClient Client = null!;
    private AppDbContext _db = null!;
    private DbConnection _connection = null!;
    private Respawner _respawner = null!;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        Client = CreateClient();
        _db = Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        _connection = _db.Database.GetDbConnection();
        await _connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            WithReseed = true
        });
    }

    public async Task ResetDatabase() => await _respawner.ResetAsync(_connection);

    public new async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<AppDbContext>();
            services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(_container.GetConnectionString()); });
            services.EnsureDbCreated<AppDbContext>();
        });
    }
}