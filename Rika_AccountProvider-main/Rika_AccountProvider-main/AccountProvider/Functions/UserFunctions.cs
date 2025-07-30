using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using AccountProvider.Interfaces;
using AccountProvider.Models;  

namespace AccountProvider.Functions
{
    public class UserFunction(IUserService userService)
    {
        private readonly IUserService _userService = userService;

        [Function("CreateUser")]
        public async Task<HttpResponseData> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            _ = executionContext.GetLogger("CreateUser");
            
            var model = await req.ReadFromJsonAsync<CreateUserDto>();

            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteAsJsonAsync(new { message = "Invalid input data" });
                return badRequestResponse;
            }

            var result = await _userService.CreateUserAsync(model);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(new { message = result });

            return response;
        }
    }
}
