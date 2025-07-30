using AccountProvider.Interfaces;
using AccountProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AccountProvider.Functions
{
    public class UpdateUser
    {
        private readonly ILogger<UpdateUser> _logger;
        private readonly IUserService _userService;

        public UpdateUser(ILogger<UpdateUser> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Function("UpdateUser")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updatedUser = JsonConvert.DeserializeObject<UpdateUserDto>(requestBody);

                if (updatedUser != null)
                {
                    var result = await _userService.UpdateUserAsync(updatedUser);

                    switch (result)
                    {
                        case OkObjectResult okResult:
                            return new OkObjectResult(okResult.Value);

                        case NotFoundResult:
                            return new NotFoundResult();

                        case BadRequestResult:
                            return new BadRequestResult();

                        default:
                            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    }
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user information.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
