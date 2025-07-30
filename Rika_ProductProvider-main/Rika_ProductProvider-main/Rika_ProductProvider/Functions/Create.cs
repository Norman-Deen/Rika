using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvider.Models.RequestModels;
using Rika_ProductProvier.Infrastructure.Data.Entities;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions
{
    public class Create
    {
        private readonly ILogger<Create> _logger;
        private readonly ProductRepository _productRepository;
        private readonly ProductColorRepository _colorRepository;
        private readonly ProductSizeRepository _sizeRepository;

        public Create(ILogger<Create> logger, ProductRepository productRepository, ProductColorRepository colorRepository, ProductSizeRepository sizeRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
        }

        [Function("CreateProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var newProduct = JsonConvert.DeserializeObject<CreateProductRequest>(requestBody);

                if (newProduct == null)
                {
                    return new BadRequestObjectResult("Invalid product data.");
                }

                var createdProduct = await _productRepository.CreateOneAsync(newProduct);

                return new OkObjectResult(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}