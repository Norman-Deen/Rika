using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Models;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;
using System.Diagnostics;

namespace Rika_OrderProvider.Functions
{
    public class UpdateOrder
    {
        private readonly ILogger<UpdateOrder> _logger;
        private readonly IOrderService _orderService;

        public UpdateOrder(ILogger<UpdateOrder> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Function("UpdateOrder")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                using var reader = new StreamReader(req.Body);
                var requestBody = await reader.ReadToEndAsync();
                var orderModel = JsonConvert.DeserializeObject<UpdateOrderModel>(requestBody);

                if (orderModel == null)
                {
                    return new BadRequestObjectResult("Invalid request");
                }

                var result = await _orderService.UpdateOrderAsync(orderModel);

                return result.StatusCode switch
                {
                    ResultStatus.OK => new OkResult(),
                    ResultStatus.NOT_FOUND => new NotFoundObjectResult(result.Message),
                    _ => new StatusCodeResult(500)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR :: UpdateOrder() : " + ex.Message);
                return new StatusCodeResult(500);
            }

        }
    }
}