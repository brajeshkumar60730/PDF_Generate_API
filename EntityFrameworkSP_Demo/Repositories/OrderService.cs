using EntityFrameworkSP_Demo.Data;
using EntityFrameworkSP_Demo.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkSP_Demo.Repositories
{
    public class OrderService : IOrder
    {
        private readonly DbContextClass _dbContext;

        public OrderService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrderListAsync()
        {
            return await _dbContext.Order
                .FromSqlRaw<Order>("GetOrderList")
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByIdAsync(Guid Id)
        {
            var param = new SqlParameter("@OrderId", Id);

            var orderDetails = await Task.Run(() => _dbContext.Order
                            .FromSqlRaw(@"exec GetOrderByID @OrderId", param).ToListAsync());

            return orderDetails;
        }

        public async Task<int> AddOrderAsync(Order order)
        {
            // Generate a unique identifier (GUID) for the orders
            order.OrderId = Guid.NewGuid();

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@OrderId", order.OrderId));
            parameter.Add(new SqlParameter("@ProductId", order.ProductId));
            parameter.Add(new SqlParameter("@UnitPrice", order.UnitPrice));
            parameter.Add(new SqlParameter("@Size", order.Size));
            parameter.Add(new SqlParameter("@Quantity", order.Quantity));
            var result = await _dbContext.Database.ExecuteSqlRawAsync(@"exec AddNewOrder @OrderId, @ProductId, @UnitPrice, @Size, @Quantity", parameter.ToArray());

            return result;
        }
        public async Task<int> UpdateOrderAsync(Guid Id, Order order)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@OrderId", order.OrderId));
            parameter.Add(new SqlParameter("@ProductId", order.ProductId));
            parameter.Add(new SqlParameter("@UnitPrice", order.UnitPrice));
            parameter.Add(new SqlParameter("@Size", order.Size));
            parameter.Add(new SqlParameter("@Quantity", order.Quantity));
            var result = await _dbContext.Database.ExecuteSqlRawAsync(@"exec UpdateOrder @OrderId, @ProductId, @UnitPrice, @Size, @Quantity", parameter.ToArray());

            return result;
        }


        public async Task<int> DeleteOrderAsync(Guid Id)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteOrderByID {Id}"));
        }

    }
}
