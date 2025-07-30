using Rika_ProductProvider.Models.RequestModels;
using Rika_ProductProvier.Infrastructure.Models.RequestModels;
using System.Text.Json.Serialization;

namespace Rika_ProductProvier.Infrastructure.Data.Entities;

public class ProductColorEntity
{
    public int Id { get; set; }

    public string ColorName { get; set; } = null!;

    [JsonIgnore]
    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public static implicit operator ProductColorEntity(CreateColorRequest createRequest)
    {
        return new ProductColorEntity
        {
            ColorName = createRequest.ColorName
        };
    }
}
