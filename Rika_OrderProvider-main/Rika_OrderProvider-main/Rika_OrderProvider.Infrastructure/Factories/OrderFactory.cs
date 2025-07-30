using Rika_OrderProvider.Infrastructure.Data.Entities;
using Rika_OrderProvider.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rika_OrderProvider.Infrastructure.Factories;

public static class OrderFactory
{
    public static OrderEntity CreateOrder(OrderModel orderModel, string orderAddressId, string orderCustomerId)
    {
        return new OrderEntity
        {
            OrderDate = DateTime.Now,
            TotalAmount = orderModel.TotalAmount,
            PaymentMethod = orderModel.PaymentMethod,
            ShipmentMethod = orderModel.ShipmentMethod,
            OrderAddressId = orderAddressId,
            OrderCustomerId = orderCustomerId,
            OrderProducts = orderModel.OrderProducts.Select(x => new OrderProductEntity
            {
                ArticleNumber = x.ArticleNumber,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Color = x.Color,
                Size = x.Size
            }).ToList()
        };
    }

    public static OrderEntity UpdateOrder(OrderEntity existingOrder, UpdateOrderModel orderModel, string orderAddressId, string orderCustomerId)
    {
        existingOrder.TotalAmount = orderModel.TotalAmount;
        existingOrder.PaymentMethod = orderModel.PaymentMethod;
        existingOrder.ShipmentMethod = orderModel.ShipmentMethod;
        existingOrder.OrderStatus = orderModel.OrderStatus;
        existingOrder.OrderAddressId = orderAddressId;
        existingOrder.OrderCustomerId = orderCustomerId;

        var productsToRemove = existingOrder.OrderProducts.Where(existingProduct => !orderModel.OrderProducts.Any(p => p.ArticleNumber == existingProduct.ArticleNumber)).ToList();
        foreach (var productToRemove in productsToRemove)
        {
            existingOrder.OrderProducts.Remove(productToRemove);
        }

        foreach (var productModel in orderModel.OrderProducts)
        {
            var existingProduct = existingOrder.OrderProducts.FirstOrDefault(p => p.ArticleNumber == productModel.ArticleNumber);
            if (existingProduct != null)
            {
                existingProduct.ProductName = productModel.ProductName;
                existingProduct.UnitPrice = productModel.UnitPrice;
                existingProduct.Quantity = productModel.Quantity;
                existingProduct.Color = productModel.Color;
                existingProduct.Size = productModel.Size;
            }
            else
            {
                existingOrder.OrderProducts.Add(new OrderProductEntity
                {
                    ArticleNumber = productModel.ArticleNumber,
                    ProductName = productModel.ProductName,
                    UnitPrice = productModel.UnitPrice,
                    Quantity = productModel.Quantity,
                    Color = productModel.Color,
                    Size = productModel.Size,
                    OrderId = existingOrder.OrderId
                });
            }
        }
        return existingOrder;
    }

    public static GetOrderModel GetOrder(OrderEntity orderEntity)
    {
        return new GetOrderModel
        {
            OrderId = orderEntity.OrderId,
            OrderDate = orderEntity.OrderDate,
            TotalAmount = orderEntity.TotalAmount,
            PaymentMethod = orderEntity.PaymentMethod,
            ShipmentMethod = orderEntity.ShipmentMethod,
            OrderStatus = orderEntity.OrderStatus,
            OrderCustomer = new OrderCustomerModel
            {
                CustomerName = orderEntity.OrderCustomer.CustomerName,
                CustomerEmail = orderEntity.OrderCustomer.CustomerEmail,
                CustomerPhone = orderEntity.OrderCustomer.CustomerPhone
            },
            OrderAddress = new OrderAddressModel
            {
                Address = orderEntity.OrderAddress.Address,
                City = orderEntity.OrderAddress.City,
                PostalCode = orderEntity.OrderAddress.PostalCode,
                Country = orderEntity.OrderAddress.Country
            },
            OrderProducts = orderEntity.OrderProducts.Select(op => new OrderProductModel
            {
                ArticleNumber = op.ArticleNumber,
                ProductName = op.ProductName,
                UnitPrice = op.UnitPrice,
                Quantity = op.Quantity,
                Color = op.Color ?? "",
                Size = op.Size ?? "",
            }).ToList()
        };
    }

  
}
