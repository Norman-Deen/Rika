namespace Rika_WebApp.Models
{
    public class CartItemModel
    {
        public string ArticleNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    //public class CartModel
    //{
    //    public List<CartItemModel> CartItems { get; set; } = [];
    //    public decimal TotalAmount => CartItems.Sum(item => item.Price * item.Quantity);
    //}
}