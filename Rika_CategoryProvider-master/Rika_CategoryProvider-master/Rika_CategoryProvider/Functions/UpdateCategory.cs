using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Rika_CategoryProvider.Infrastructure.Entities;

namespace Rika_CategoryProvider.Functions;

public class UpdateCategory
{
    private readonly ILogger<UpdateCategory> _logger;
    private readonly CategoryService _categoryService;

    public UpdateCategory(ILogger<UpdateCategory> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("UpdateCategory")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put", Route = "category/{id}")] HttpRequest req, int id)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to update category with ID: {Id}", id);

        if (id <= 0)
        {
            return new BadRequestObjectResult("Invalid category ID.");
        }

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var updatedCategory = JsonSerializer.Deserialize<CategoryEntity>(requestBody);

        if (updatedCategory == null || string.IsNullOrEmpty(updatedCategory.CategoryName))
        {
            return new BadRequestObjectResult("Invalid category data.");
        }

        bool isUpdated = await _categoryService.UpdateCategoryAsync(id, updatedCategory);

        if (isUpdated)
        {
            return new OkObjectResult($"Category with ID: {id} has been updated.");
        }
        else
        {
            return new NotFoundObjectResult($"Category with ID: {id} not found.");
        }
    }
}
