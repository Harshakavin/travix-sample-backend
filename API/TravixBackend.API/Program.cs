using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace TravixBackend.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                Log.Information("Starting up");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var configuration = config.AddJsonFile(
                        "appsettings.json", optional: false, reloadOnChange: false)
                        .AddJsonFile(
                          $"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables().Build();

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((ctx, options) =>
                    {
                        options.ListenAnyIP(3000);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
