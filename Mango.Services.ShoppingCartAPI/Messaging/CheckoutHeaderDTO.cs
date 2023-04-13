﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Messaging
{
    public class CheckoutHeaderDTO : BaseMessage
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CartTotalItems { get; set; }
        public IEnumerable<CartDetailsDTO> CartDetails { get; set; }
    }
}
