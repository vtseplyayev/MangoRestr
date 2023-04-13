using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ShoppingCartAPI
{
    public class Config
    {
        public static string CheckOutMessageTopic { get; set; }
        public static string ServiceBusURL { get; set; }
    }
}
