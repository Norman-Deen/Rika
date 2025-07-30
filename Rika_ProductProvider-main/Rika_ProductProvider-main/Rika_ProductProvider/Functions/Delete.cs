using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rika_ProductProvier.Infrastructure.Data.Entities;
using Rika_ProductProvier.Infrastructure.Repositories;

namespace Rika_ProductProvider.Functions
{
    public class Delete
    {
        private readonly ProductRepository _repository;
        private readonly ILogger<Delete> _logger;

        public Delete(ILogger<Delete> logger, ProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Function("DeleteProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var productToDelete = JsonConvert.DeserializeObject<ProductEntity>(requestBody);

                if (productToDelete != null)
                {
                    var result = await _repository.DeleteOneAsync(x => x.Id == productToDelete.Id);

                    if (result)
                        return new OkObjectResult("The product was deleted successfully.");

                    return new NotFoundObjectResult("The product was not found in the database. No product has been deleted.");
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
