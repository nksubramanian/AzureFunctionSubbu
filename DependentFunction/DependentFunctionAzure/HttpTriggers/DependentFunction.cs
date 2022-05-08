
using System.Net;

using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.IO;

namespace Sperry.MxA.DataProvider.Functions.HttpTriggers
{
    public class DependentFunction
    {
        private readonly ILogger<DependentFunction> _logger;

        public DependentFunction(ILogger<DependentFunction> logger)
        {
         
            _logger = logger;
        }

        [Function("v1/DependentFunctionAzure")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation(@"DataProviderRequest trigger functions is called enkay");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions subramanian!");

            return response;



        }
    }
}
