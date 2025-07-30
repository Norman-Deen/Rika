using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions;

public class GetAllSizes
{
    private readonly ILogger<GetAllSizes> _logger;
    private readonly ProductSizeRepository _sizeRepository;

    public GetAllSizes(ILogger<GetAllSizes> logger, ProductSizeRepository sizeRepository)
    {
        _logger = logger;
        _sizeRepository = sizeRepository;
    }

    [Function("GetAllSizes")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            var colors = await _sizeRepository.GetAllAsync();
            return new OkObjectResult(colors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}