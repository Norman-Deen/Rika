using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvier.Infrastructure.Data.Entities;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions
{
    public class Update
    {
        private readonly ProductRepository _repository;
        private readonly ILogger<Update> _logger;

        public Update(ILogger<Update> logger, ProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Function("UpdateProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var productToUpdate = JsonConvert.DeserializeObject<ProductEntity>(requestBody);

                if (productToUpdate != null)
                {
                    var updatedProduct = await _repository.UpdateOneAsync(productToUpdate);

                    return new OkObjectResult(updatedProduct);
                }
                return new BadRequestObjectResult("Your request is invalid.");

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
