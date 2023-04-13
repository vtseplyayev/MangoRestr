using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Messaging
{
    public class CartDetailsDTO
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int ProductId { get; set; }
        public virtual CartProductDTO CartProduct { get; set; }
        public int Count { get; set; }
    }
}
