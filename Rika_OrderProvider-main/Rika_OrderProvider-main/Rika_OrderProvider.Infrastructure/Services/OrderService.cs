using Rika_OrderProvider.Infrastructure.Data.Entities;
using Rika_OrderProvider.Infrastructure.Data.Repositories;
using Rika_OrderProvider.Infrastructure.Factories;
using Rika_OrderProvider.Infrastructure.Helpers;
using Rika_OrderProvider.Infrastructure.Models;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;
using System.Diagnostics;

namespace Rika_OrderProvider.Infrastructure.Services;



public class OrderService : IOrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly OrderAddressRepository _orderAddressRepository;
    private readonly OrderCustomerRepository _orderCustomerRepository;
    private readonly OrderProductRepository _orderProductRepository;
    public OrderService(OrderRepository orderRepository, OrderAddressRepository orderAddressRepository, OrderCustomerRepository orderCustomerRepository, OrderProductRepository orderProductRepository)
    {
        _orderRepository = orderRepository;
        _orderAddressRepository = orderAddressRepository;
        _orderCustomerRepository = orderCustomerRepository;
        _orderProductRepository = orderProductRepository;
    }

    public async Task<ResponseResult> CreateOrderAsync(OrderModel orderModel)
    {
        try
        {
            var orderAddressId = await GetOrCreateOrderAddressAsync(orderModel.OrderAddress.Address, orderModel.OrderAddress.City, orderModel.OrderAddress.PostalCode, orderModel.OrderAddress.Country);
            if (orderAddressId == null)
            {
                return ResponseFactory.Error("Error creating order address");
            }

            var orderCustomerId = await GetOrCreateOrderCustomerAsync(orderModel.OrderCustomer.CustomerName, orderModel.OrderCustomer.CustomerEmail, orderModel.OrderCustomer.CustomerPhone);
            if (orderCustomerId == null)
            {
                return ResponseFactory.Error("Error creating order customer");
            }

            var createdOrder = await _orderRepository.CreateAsync(OrderFactory.CreateOrder(orderModel, orderAddressId, orderCustomerId));
            return createdOrder != null ? ResponseFactory.Ok(createdOrder) : ResponseFactory.Error("Error creating order");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseFactory.Error("Something went wrong, please try again");
        }
    }

    private async Task<string> GetOrCreateOrderAddressAsync(string address, string city, string postalCode, string country) 
    {
        try
        {
            var orderAddress = await _orderAddressRepository.GetOneAsync(x => x.Address == address && x.PostalCode == postalCode);
            if (orderAddress != null)
            {
                return orderAddress.OrderAddressId;
            }
            else
            {
                var createdOrderAddress = await _orderAddressRepository.CreateAsync(new OrderAddressEntity { Address = address, City = city, PostalCode = postalCode, Country = country});
                if (createdOrderAddress != null)
                {
                    return createdOrderAddress.OrderAddressId;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    private async Task<string> GetOrCreateOrderCustomerAsync(string name, string email, string phone)
    {
        try
        {
            var orderCustomer = await _orderCustomerRepository.GetOneAsync(x => x.CustomerName == name && x.CustomerEmail == email && x.CustomerPhone == phone);
            if (orderCustomer != null)
            {
                return orderCustomer.OrderCustomerId;
            }
            else
            {
                var createdOrderCustomer = await _orderCustomerRepository.CreateAsync(new OrderCustomerEntity { CustomerName = name, CustomerEmail = email, CustomerPhone = phone });
                if (createdOrderCustomer != null)
                {
                    return createdOrderCustomer.OrderCustomerId;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public async Task<ResponseResult> DeleteOrderAsync(int orderId)
    {
        var deletedOrder = await _orderRepository.DeleteAsync(x => x.OrderId == orderId);
        return deletedOrder ? ResponseFactory.Ok() : ResponseFactory.NotFound(); 
    }

    public async Task<ResponseResult> GetAllOrdersAsync()
    {
        var result = await _orderRepository.GetAllAsync();
        return result.Count > 0 ? ResponseFactory.Ok(result.Select(OrderFactory.GetOrder).ToList()) : ResponseFactory.NotFound();
    }

    public async Task<ResponseResult> GetOneOrderAsync(int orderId)
    {
        var existingOrder = await _orderRepository.GetOneAsync(x => x.OrderId == orderId);
        return existingOrder != null ? ResponseFactory.Ok(OrderFactory.GetOrder(existingOrder)) : ResponseFactory.NotFound();
    }

    public async Task<ResponseResult> UpdateOrderAsync(UpdateOrderModel orderModel)
    {
        var existingOrder = await _orderRepository.GetOneAsync(x => x.OrderId == orderModel.OrderId);
        if (existingOrder == null)
        {
            return ResponseFactory.NotFound();
        }
        var orderAddressId = GetOrCreateOrderAddressAsync(orderModel.OrderAddress.Address, orderModel.OrderAddress.City, orderModel.OrderAddress.PostalCode, orderModel.OrderAddress.Country);
        if (orderAddressId == null)
        {
            return ResponseFactory.Error("Error creating order address");
        }
        var orderCustomerId = GetOrCreateOrderCustomerAsync(orderModel.OrderCustomer.CustomerName, orderModel.OrderCustomer.CustomerEmail, orderModel.OrderCustomer.CustomerPhone);
        if (orderCustomerId == null)
        {
            return ResponseFactory.Error("Error creating order customer");
        }
        var updatedOrder = OrderFactory.UpdateOrder(existingOrder, orderModel, await orderAddressId, await orderCustomerId);
        var result =  await _orderRepository.UpdateAsync(x => x.OrderId == existingOrder.OrderId, updatedOrder);
        return result != null ? ResponseFactory.Ok(result) : ResponseFactory.Error("Error updating order");
    }

}