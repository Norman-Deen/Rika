namespace Rika_Backoffice.Models;

public class CreateProductModel
{
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; } = 0;
    public decimal ProductSalePrice { get; set; } = 0;
    public string ProductDescription { get; set; } = null!;
    public int ProductCategoryId { get; set; }

    public int ProductSizeId { get; set; }
    public ProductSizeEntity ProductSize { get; set; } = null!;

    public int? ProductColorId { get; set; }
    public ProductColorEntity? ProductColor { get; set; }
}

public class ProductModel
{
    public string Id { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; } = 0;
    public decimal ProductSalePrice { get; set; } = 0;
    public string ProductDescription { get; set; } = null!;
    public int ProductCategoryId { get; set; }
    public CategoryEntity? ProductCategory { get; set; }

    public int ProductSizeId { get; set; }
    public ProductSizeEntity ProductSize { get; set; } = null!;

    public int ProductColorId { get; set; }
    public ProductColorEntity ProductColor { get; set; } = null!;
}


public class ProductColorEntity
{
    public int Id { get; set; }
    public string ColorName { get; set; } = null!;
}

public class ProductSizeEntity
{
    public int Id { get; set; }
    public string ProductSizeName { get; set; } = null!;
}

public class CategoryEntity
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

}