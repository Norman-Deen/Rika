using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Rika_CategoryProvider.Infrastructure.Entities;
using System.Text.Json;

namespace Rika_CategoryProvider.Functions;

public class CreateCategory
{
    private readonly ILogger<CreateCategory> _logger;
    private readonly CategoryService _categoryService;

    public CreateCategory(ILogger<CreateCategory> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("CreateCategory")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "category")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to create a new category.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var newCategory = JsonSerializer.Deserialize<CategoryEntity>(requestBody);

        if (newCategory == null || string.IsNullOrEmpty(newCategory.CategoryName))
        {
            return new BadRequestObjectResult("Invalid category data.");
        }

        try
        {
            var createdCategory = await _categoryService.AddCategoryAsync(newCategory.CategoryName);
            return new OkObjectResult(createdCategory);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Category already exists.");
            return new ConflictObjectResult("Category already exists.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the category.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
