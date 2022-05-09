using IndependentFunctions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                       
                        .ConfigureFunctionsWorkerDefaults(configure =>
                        {
                            // other middleware also configure following way.
                            // It will execute in same order it is configured over here.
                            configure.UseMiddleware<SimpleMiddleware>();
                        })
                        .ConfigureServices(services =>
                        {
                            services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
                        })
                        .Build();
            host.Run();
        }
    }


    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly ILogger _logger;
        public CloudRoleNameTelemetryInitializer(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("CloudRoleNameTelemetryInitializer");
        }

        public void Initialize(ITelemetry telemetry)
        {
            string c = telemetry.Context.Operation.Id;
            _logger.LogInformation("the operation Id is " + c);

            // set custom role name here
            if (c != null)
            {
                telemetry.Context.Operation.Id = "12345-" + c.Substring(3);
            }

        }
    }


}

