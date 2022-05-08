
using System.Net;

using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.IO;
using System;

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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
        {
            _logger.LogInformation(@"independent enkay");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            Guid obj = Guid.NewGuid();
            

            response.WriteString(obj.ToString());

            return response;



        }
    }
}
