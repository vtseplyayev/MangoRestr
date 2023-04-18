namespace Mango.Services.OrderAPI.Messaging
{
    public class UpdatePaymentResultMessage
    {
        public int OrderID { get; set; }
        public bool Status { get; set; }
    }
}
