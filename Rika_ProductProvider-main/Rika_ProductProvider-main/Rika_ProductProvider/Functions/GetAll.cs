using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions;

public class GetAll
{
    private readonly ILogger<GetAll> _logger;
    private readonly ProductRepository _productRepository;

    public GetAll(ILogger<GetAll> logger, ProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [Function("GetAll")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        try
        {
            if (req.Method == "POST")
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();

                var filterCriteria = JsonConvert.DeserializeObject<FilterCriteria>(body);

                if (filterCriteria != null)
                {
                    var sortedProducts = await _productRepository.GetAllAsync(x =>
                        (string.IsNullOrEmpty(filterCriteria.ProductName) || x.ProductName.Contains(filterCriteria.ProductName)) &&
                        (filterCriteria.ProductCategory == 0 || x.ProductCategoryId == filterCriteria.ProductCategory) &&
                        (filterCriteria.ProductColor == 0 || x.ProductColorId == filterCriteria.ProductColor) &&
                        (filterCriteria.ProductSize == 0 || x.ProductSizeId == filterCriteria.ProductSize));

                    if (sortedProducts.Count() == 0)
                        return new NotFoundObjectResult("No products that matches the filter criterias exists.");

                    return new OkObjectResult(sortedProducts);
                }
            }

            var products = await _productRepository.GetAllAsync();
            return new OkObjectResult(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new BadRequestObjectResult(ex.Message);
        }
    }
}

public class FilterCriteria
{
    public string? ProductName { get; set; }
    public int ProductColor { get; set; }
    public int ProductSize { get; set; }
    public int ProductCategory { get; set; }
}