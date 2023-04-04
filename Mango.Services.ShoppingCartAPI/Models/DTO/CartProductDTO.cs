namespace Mango.Services.ShoppingCartAPI.Models.DTO
{
    public class CartProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageSrc { get; set; }
    }
}
