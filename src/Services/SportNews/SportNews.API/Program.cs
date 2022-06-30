using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using SportNews.API;
using SportNews.Infrastructure.MongoDb;
using SportNews.Infrastructure.MongoDb.SeedWork;
using System.Reflection;

public class Program
{
    public static int Main(string[] args)
    {
        var appName = typeof(Startup).Namespace;


        var configuration = GetConfiguration();

        Log.Logger = CreateSerilogLogger(configuration);
        try
        {
            Log.Information("Starting web host ({ApplicationContext})...", appName);

            var host = CreateHostBuilder(configuration, args).Build();

            Log.Information("Apply configuration web host ({ApplicationContext})...", appName);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var logger = services.GetRequiredService<ILogger<SportNewsMongoDbSeeding>>();
                    var settings = services.GetRequiredService<IOptions<SportNewsSettings>>();
                    var mongoClient = services.GetRequiredService<IMongoClient>();
                    new SportNewsMongoDbSeeding()
                        .SeedAsync(mongoClient, settings, logger)
                        .Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();

            Log.Information("Started web host ({ApplicationContext})...", appName);
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", appName);

            return 1;
        }

        // Create a logger object
        Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, shared: true)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetAssembly(typeof(Startup)));

            var config = builder.Build();
            return builder.Build();
        }
    }

    public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.CaptureStartupErrors(false);
        webBuilder.ConfigureAppConfiguration(x => x.AddConfiguration(configuration));

        webBuilder.UseStartup<Startup>();
        webBuilder.UseSerilog();
        webBuilder.UseIISIntegration();
    });
}