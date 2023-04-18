namespace Mango.Services.OrderAPI.Messaging
{
    public class PaymentRequestMessage : BaseMessage
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public double OrderTotal { get; set; }
    }
}
