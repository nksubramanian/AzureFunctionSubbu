
using System.Net;

using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.IO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sperry.MxA.DataProvider.Functions.HttpTriggers
{
    public class IndpendentFunction
    {
        private readonly ILogger<IndpendentFunction> _logger;

        public IndpendentFunction(ILogger<IndpendentFunction> logger)
        {
         
            _logger = logger;
        }

        [Function("v1/IndependentFunctionAzure")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation(@"independent enkay");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            using var client = new HttpClient();
            Guid obj = Guid.NewGuid();
            string f = obj.ToString();
            string traceparent = "00-0af7651916cd43dd8448eb211c80319c-b9c7c989f97918e1-01";
            _logger.LogInformation("independent-------  " + f);
            client.DefaultRequestHeaders.Add("User-Agent", f);
            client.DefaultRequestHeaders.Add("traceparent", traceparent);
            var content = await client.GetStringAsync("https://functions20220509115733.azurewebsites.net/api/v1/DependentFunctionAzure");
            
            response.WriteString(content+" subramanianenkay");

            return response;



        }
    }
}
