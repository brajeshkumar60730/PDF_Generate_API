using EntityFrameworkSP_Demo.Entities;
using EntityFrameworkSP_Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkSP_Demo.Controllers
{
    [Authorize] // Secures all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder orderService;
        public OrdersController(IOrder orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("getorderlist")]
        public async Task<List<Order>> GetOrderListAsync()
        {
            try
            {
                return await orderService.GetOrderListAsync();
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("getorderbyid")]
        public async Task<IEnumerable<Order>> GetOrderByIdAsync(Guid Id)
        {
            try
            {
                var response = await orderService.GetOrderByIdAsync(Id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("addorder")]
        public async Task<IActionResult> AddOrderAsync(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await orderService.AddOrderAsync(order);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut("updateorder")]
        public async Task<IActionResult> UpdateOrderAsync(Guid Id, Order order)
        {


            try
            {
                var result = await orderService.UpdateOrderAsync(Id, order);
                return Ok(result);

            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("deleteorder")]
        public async Task<int> DeleteOrderAsync(Guid Id)
        {
            try
            {
                var response = await orderService.DeleteOrderAsync(Id);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
