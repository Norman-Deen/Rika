using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Rika_CategoryProvider.Functions;

public class GetOneCategory
{
    private readonly ILogger<GetOneCategory> _logger;
    private readonly CategoryService _categoryService;

    public GetOneCategory(ILogger<GetOneCategory> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetOneCategory")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "categories/{id}")] HttpRequest req,
        int id)
    {
        _logger.LogInformation($"C# HTTP trigger function processed a request to get category with ID: {id}");

        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                _logger.LogWarning($"Category with ID: {id} not found.");
                return new NotFoundResult();
            }

            return new OkObjectResult(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting the category with ID: {id}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
