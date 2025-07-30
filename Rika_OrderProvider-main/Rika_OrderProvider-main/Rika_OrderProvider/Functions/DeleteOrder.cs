using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;

namespace Rika_OrderProvider.Functions
{
    public class DeleteOrder
    {
        private readonly ILogger<DeleteOrder> _logger;
        private readonly IOrderService _orderService;

        public DeleteOrder(ILogger<DeleteOrder> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("DeleteOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteOrder/{id}")] HttpRequest req, int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var deleteResult = await _orderService.DeleteOrderAsync(id);
                return deleteResult.StatusCode switch
                {
                    ResultStatus.OK => new OkResult(),
                    ResultStatus.NOT_FOUND => new NotFoundResult(),
                    _ => new StatusCodeResult(500)
                };
            }

            
            catch (Exception ex)
            {
                _logger.LogError("ERROR :: DeleteOrder() : " + ex.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}
