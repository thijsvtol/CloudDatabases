using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Service;

namespace CloudDatabases
{
    public class Functions
    {
        private readonly IHouseService _HouseService;
        public Functions(IHouseService houseService)
        {
            _HouseService = houseService;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> GetHousesByPriceAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext, int size, string paginationToken, float priceFrom, float priceTo)
        {
            var houses = _HouseService.GetHousesByPriceEF(priceFrom, priceTo, size, paginationToken);



            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
