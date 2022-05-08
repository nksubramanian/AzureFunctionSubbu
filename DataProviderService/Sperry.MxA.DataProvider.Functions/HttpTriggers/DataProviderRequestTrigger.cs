
using System.Net;

using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.IO;
using static Sperry.MxA.DataProvider.Functions.Program;

namespace Sperry.MxA.DataProvider.Functions.HttpTriggers
{
    public class DataProviderRequestTrigger
    {
        private readonly ILogger<DataProviderRequestTrigger> _logger;

        public DataProviderRequestTrigger(ILogger<DataProviderRequestTrigger> logger)
        {
         
            _logger = logger;
        }

        [Function("v1/DataProvider/ProcessRequest")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            var t = ConfigurationManager.datamodel;
            var g = t.key1;

            _logger.LogInformation(@"DataProviderRequest trigger functions is called subramanian");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions subramanian!");

            return response;



        }
    }
}
