using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions;

public class GetOne
{
    private readonly ILogger<GetOne> _logger;
    private readonly ProductRepository _repo;

    public GetOne(ILogger<GetOne> logger, ProductRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    [Function("GetOne")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        try
        {
            string productId = req.Query["ProductId"]!;

            var product = await _repo.GetOneAsync(p => p.Id == productId);

            return new OkObjectResult(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}