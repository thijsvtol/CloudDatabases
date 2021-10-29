using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service;

namespace CloudDatabases
{
    public class FunctionUsers
    {
        private readonly IUserService _UserService;
        public FunctionUsers(IUserService userService)
        {
            _UserService = userService;
        }

        [Function("AddUser")]
        public async Task<HttpResponseData> AddBuyer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req, FunctionContext executionContext,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(requestBody);
            await _UserService.AddUser(user);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            return response;
        }
    }
}
