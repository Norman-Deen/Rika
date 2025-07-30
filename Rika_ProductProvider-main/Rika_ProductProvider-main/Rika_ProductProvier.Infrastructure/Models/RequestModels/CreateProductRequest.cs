namespace Rika_ProductProvider.Models.RequestModels
{
    public class CreateProductRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; } = null!;
        public decimal ProductPrice { get; set; } = 0;
        public decimal ProductSalePrice { get; set; } = 0;
        public string ProductDescription { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public int ProductSizeId { get; set; }
        public int? ProductColorId { get; set; }
    }
}