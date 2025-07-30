using AccountProvider.Context;
using AccountProvider.Interfaces;
using AccountProvider.Repositories;
using AccountProvider.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<DataContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE")));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
    })
    .Build();
using (var scope = host.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetService<DataContext>();
        var migration = context?.Database.GetPendingMigrations();
        if (migration != null && migration.Any())
        {
            context?.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error : AccountProvider :: {ex.Message}");
    }
}

host.Run();