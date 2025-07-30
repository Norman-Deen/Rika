using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Services;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;

namespace Rika_OrderProvider.Functions
{
    public class GetOneOrder
    {
        private readonly ILogger<GetOneOrder> _logger;
        private readonly IOrderService _orderService;

        public GetOneOrder(ILogger<GetOneOrder> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("GetOneOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route ="GetOneOrder/{id}")] HttpRequest req, int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var result = await _orderService.GetOneOrderAsync(id);
                return result.StatusCode switch
                {
                    ResultStatus.OK => new OkObjectResult(result.ContentResult),
                    ResultStatus.NOT_FOUND => new NotFoundResult(),
                    _ => new StatusCodeResult(500)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR :: GetOneOrder() : " + ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
