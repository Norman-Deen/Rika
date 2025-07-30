using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvider.Models.RequestModels;
using Rika_ProductProvier.Infrastructure.Data.Entities;
using Rika_ProductProvier.Infrastructure.Models.RequestModels;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions;

public class CreateColor
{
    private readonly ILogger<CreateColor> _logger;
    private readonly ProductColorRepository _colorRepository;

    public CreateColor(ILogger<CreateColor> logger, ProductRepository productRepository, ProductColorRepository colorRepository, ProductSizeRepository sizeRepository)
    {
        _logger = logger;
        _colorRepository = colorRepository;
    }

    [Function("CreateColor")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var newColor = JsonConvert.DeserializeObject<CreateColorRequest>(requestBody);

            if (newColor == null)
            {
                return new BadRequestObjectResult("Invalid color data.");
            }

            var createdColor = await _colorRepository.CreateOneAsync(newColor);

            return new OkObjectResult(createdColor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}