using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;


namespace Sperry.MxA.DataProvider.Functions
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                .AddJsonFile("hosting.json", optional: true)
                                .AddEnvironmentVariables()
                                .Build();
        public static void Main()
        {
            ConfigurationManager.datamodel = Configuration.GetSection("Chart").Get<Datamodel>();
            var host = new HostBuilder()
                        .ConfigureFunctionsWorkerDefaults()
                        .Build();
            host.Run();
        }

        public class Datamodel
        {
            public string key1 { get; set; }
            public string key2 { get; set; }
            public string key3 { get; set; }
        }

        public static class ConfigurationManager
        {
            public static Datamodel datamodel { get; set; }

        }


    }


}