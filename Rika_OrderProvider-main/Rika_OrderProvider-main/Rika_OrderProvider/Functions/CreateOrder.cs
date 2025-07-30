using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Models;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;
using static Rika_OrderProvider.Infrastructure.Helpers.CustomValidation;

namespace Rika_OrderProvider.Functions;

public class CreateOrder
{
    private readonly ILogger<CreateOrder> _logger;
    private readonly IOrderService _orderService;

    public CreateOrder(ILogger<CreateOrder> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [Function("CreateOrder")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var modelState = await ValidateRequestAsync(req);
        if (!modelState.IsValid)
            return new BadRequestObjectResult(modelState.ValidationResults);

        var createResult = await _orderService.CreateOrderAsync(modelState.Value!);
        return createResult.StatusCode switch
        {
            ResultStatus.OK => new OkResult(),
            ResultStatus.ERROR => new BadRequestObjectResult(createResult.Message),
            _ => new StatusCodeResult(500)
        };
    }

    private static async Task<ValidationModel<OrderModel?>> ValidateRequestAsync(HttpRequest req)
    {
        using var reader = new StreamReader(req.Body);
        string requestBody = await reader.ReadToEndAsync();
        var model = JsonConvert.DeserializeObject<OrderModel>(requestBody);
        var modelState = CustomValidation.ValidateModel(model);
        return modelState;
    }
}