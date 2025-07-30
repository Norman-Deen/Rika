using Rika_ProductProvier.Infrastructure.Data.Contexts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rika_ProductProvier.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Rika_ProductProvier.Infrastructure.Interfaces;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<ProductRepository>();
        services.AddScoped<ProductColorRepository>();
        services.AddScoped<ProductSizeRepository>();
    })
    .Build();

host.Run();