using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Rika_CategoryProvider.Functions;

public class GetAllCategories
{
    private readonly ILogger<GetAllCategories> _logger;
    private readonly CategoryService _categoryService;

    public GetAllCategories(ILogger<GetAllCategories> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetAllCategories")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "categories")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to get all categories.");

        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return new OkObjectResult(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all categories.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
