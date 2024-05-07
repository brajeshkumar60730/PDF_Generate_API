using EntityFrameworkSP_Demo.Entities;

namespace EntityFrameworkSP_Demo.Repositories
{
  public interface IOrder
    {
        public Task<List<Order>> GetOrderListAsync();
        public Task<IEnumerable<Order>> GetOrderByIdAsync(Guid Id);
        public Task<int> AddOrderAsync(Order order);
        public Task<int> UpdateOrderAsync(Guid Id, Order order);
        public Task<int> DeleteOrderAsync(Guid Id);
    }
}
