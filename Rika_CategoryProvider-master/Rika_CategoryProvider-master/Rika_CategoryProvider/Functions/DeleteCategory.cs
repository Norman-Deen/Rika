using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Rika_CategoryProvider.Functions;

public class DeleteCategory
{
    private readonly ILogger<DeleteCategory> _logger;
    private readonly CategoryService _categoryService;

    public DeleteCategory(ILogger<DeleteCategory> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("DeleteCategory")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "category/{id}")] HttpRequest req, int id)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to delete category with ID: {Id}", id);

        if (id <= 0)
        {
            return new BadRequestObjectResult("Invalid category ID.");
        }

        bool isDeleted = await _categoryService.DeleteCategoryAsync(id);

        if (isDeleted)
        {
            return new OkObjectResult($"Category with ID: {id} has been deleted.");
        }
        else
        {
            return new NotFoundObjectResult($"Category with ID: {id} not found.");
        }
    }
}
