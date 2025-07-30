namespace Rika_WebApp.ViewModels
{
    public class ProductViewModel
    {
        public string? ArticleNumber { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
