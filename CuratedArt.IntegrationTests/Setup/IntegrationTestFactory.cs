using CuratedArt.IntegrationTests.Creators;
using CuratedArt.IntegrationTests.Setup;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _msSqlContainer;

    public IntegrationTestFactory()
    {
        _msSqlContainer = new MsSqlBuilder()
            
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("localdevpassword#123")
            .WithCleanUp(true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<TDbContext>();
            services.AddDbContext<TDbContext>(options => { options.UseSqlServer(_msSqlContainer.GetConnectionString()); });
            services.AddTransient<ArtworkCreator>();
        });
    }

    public async Task InitializeAsync() => await _msSqlContainer.StartAsync();

    public new async Task DisposeAsync() => await _msSqlContainer.DisposeAsync();
}