using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.OrderAPI
{
    public class Config
    {
        public static string CheckOutMessageTopic { get; set; }
        public static string OrderPaymentProcessTopic { get; set; }
        public static string ServiceBusURL { get; set; }
        public static string Subscription { get; set; }
        public static string PaymentSubscription { get; set; }
    }
}
