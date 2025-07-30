using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions;

public class GetAllColors
{
    private readonly ILogger<GetAllColors> _logger;
    private readonly ProductColorRepository _colorRepository;

    public GetAllColors(ILogger<GetAllColors> logger, ProductColorRepository colorRepository)
    {
        _logger = logger;
        _colorRepository = colorRepository;
    }

    [Function("GetAllColors")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            var colors = await _colorRepository.GetAllAsync();
            return new OkObjectResult(colors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}
