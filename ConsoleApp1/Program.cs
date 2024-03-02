using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

//DI, Logging, Settings

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our configuration
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            //set up logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            //setup our HOST
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //Greeting Service
                    services.AddTransient<IDoTheWorkService, DoTheWorkService>();
                })
                .UseSerilog()
                .Build();

            //run our greeting service
            var svc = ActivatorUtilities.CreateInstance<DoTheWorkService>(host.Services);
            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional:true)
                .AddEnvironmentVariables();
        }
    }
}

