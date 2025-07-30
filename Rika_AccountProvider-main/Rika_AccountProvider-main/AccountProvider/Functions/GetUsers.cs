using AccountProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AccountProvider.Functions
{
    public class GetUsers
    {
        private readonly ILogger<GetUsers> _logger;
        private readonly IUserService _userService;

        public GetUsers(ILogger<GetUsers> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Function("GetUsers")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                if (users != null && users.Any())
                {
                    return new OkObjectResult(users);
                }
                else
                {
                    return new NotFoundObjectResult("No users found");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error getting list of users.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
