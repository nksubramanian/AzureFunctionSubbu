
using System.Net;

using Microsoft.Azure.Functions.Worker;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

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
            var headers = req.Headers;
            var apiKey = headers.GetValues("User-Agent").First();
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            _logger.LogInformation(@"User-Agent " + apiKey);
            try
            {

                var traceparent = headers.GetValues("traceparent").First();
                _logger.LogInformation(@"traceparent " + traceparent);

               
              
                Guid obj = Guid.NewGuid();


                response.WriteString(obj.ToString());

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(@"Method failed "+ apiKey);
                Guid obj = Guid.NewGuid();
                response.WriteString(obj.ToString());
                return response;

            }

        }
    }
}
