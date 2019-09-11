using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleAppTemplate.Abstractions;
using ConsoleAppTemplate.Code;
using ConsoleAppTemplate.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConsoleAppTemplate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider(args?.FirstOrDefault());
            await serviceProvider.GetService<IRun>().Run();
        }

        private static IServiceProvider BuildServiceProvider(string environment)
        {
            var serviceCollection = new ServiceCollection();

            var configuration = BuildConfiguration(environment);
            serviceCollection.AddScoped(sp => configuration);
            serviceCollection.Configure<AppSettings>(configuration);

            serviceCollection.AddTransient<IRun, App>();

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            serviceCollection.AddLogging(c => c.AddSerilog(loggerConfig));
            serviceCollection.AddOptions();

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration BuildConfiguration(string environment)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment ?? string.Empty}.json", true, true)
                .Build();
        }
    }
}