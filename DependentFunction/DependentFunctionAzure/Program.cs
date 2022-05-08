using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;


namespace DependentFunctionAzure
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"appsettings.Development.json", optional: true)
                                .AddJsonFile("hosting.json", optional: true)
                                .AddEnvironmentVariables()
                                .Build();
        public static void Main()
        {

            var host = new HostBuilder()
                        .ConfigureFunctionsWorkerDefaults()
                        .Build();
            host.Run();
        }
    }


}