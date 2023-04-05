using System.Collections.Generic;

namespace Mango.Services.CouponAPI.Models.DTO
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string Message { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
