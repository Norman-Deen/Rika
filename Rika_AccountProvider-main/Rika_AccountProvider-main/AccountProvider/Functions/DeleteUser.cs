using AccountProvider.Interfaces;
using AccountProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AccountProvider.Functions
{
    public class DeleteUser
    {
        private readonly ILogger<DeleteUser> _logger;
		private readonly IUserService _userService;

		public DeleteUser(ILogger<DeleteUser> logger, IUserService userService)
        {
            _logger = logger;
			_userService = userService;
		}

        [Function("DeleteUser")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req)
        {
			try
			{
				string userId = req.Query["UserId"];

				if (!string.IsNullOrEmpty(userId))
				{
					var result = await _userService.DeleteUserAsync(userId);
					switch (result)
					{
						case OkResult:
							return new OkObjectResult("User was deleted.");

						case NotFoundResult:
							return new NotFoundObjectResult("User was not found");

						case BadRequestResult:
							return new BadRequestObjectResult("Invalid User Id");

						default:
							return new StatusCodeResult(StatusCodes.Status500InternalServerError);
					}
				}
				else
				{
					return new BadRequestObjectResult("UserId is required.");
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while deleting User");
				return new BadRequestObjectResult("Error::" + ex.Message);
			}
		}
    }
}
