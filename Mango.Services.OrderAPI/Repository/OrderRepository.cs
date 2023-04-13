using AutoMapper;
using Mango.Services.OrderAPI.Contexts;
using Mango.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mango.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ApplicationDBContext> context;
        //private IMapper mapper;

        public OrderRepository(DbContextOptions<ApplicationDBContext> context)
        {
            this.context = context;
            //this.mapper = mapper;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            await using var db = new ApplicationDBContext(context);
            db.OrderHeaders.Add(orderHeader);

            await db.SaveChangesAsync();

            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            await using var db = new ApplicationDBContext(context);

            OrderHeader orderHeaderFromDb = await db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);

            if(orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await db.SaveChangesAsync();
            }
        }
    }
}
