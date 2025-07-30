using Microsoft.EntityFrameworkCore;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Models;

namespace Rika_OrderProvider.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseResult> CreateOrderAsync (OrderModel orderModel);
    Task<ResponseResult> GetOneOrderAsync(int orderId);
    Task<ResponseResult> GetAllOrdersAsync();
    Task<ResponseResult> UpdateOrderAsync(UpdateOrderModel orderModel);
    Task<ResponseResult> DeleteOrderAsync(int orderId);


}
