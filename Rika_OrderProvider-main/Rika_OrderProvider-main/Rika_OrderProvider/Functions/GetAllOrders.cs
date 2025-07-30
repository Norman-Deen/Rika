using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;

namespace Rika_OrderProvider.Functions
{
    public class GetAllOrders
    {
        private readonly ILogger<GetAllOrders> _logger;
        private readonly IOrderService _orderService;

        public GetAllOrders(ILogger<GetAllOrders> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("GetAllOrders")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var result = await _orderService.GetAllOrdersAsync();

                return result.StatusCode switch
                {
                    ResultStatus.OK => new OkObjectResult(result.ContentResult),
                    ResultStatus.NOT_FOUND => new NotFoundObjectResult(result.Message),
                    _ => new StatusCodeResult(500)
                };

            }
            catch (Exception ex) 
            {
                _logger.LogError("ERROR :: GetAllOrders() : " + ex.Message);
                return new StatusCodeResult(500); 
            }
        }
    }
}
