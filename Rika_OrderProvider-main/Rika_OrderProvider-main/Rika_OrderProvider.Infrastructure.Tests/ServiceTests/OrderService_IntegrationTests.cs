using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Rika_OrderProvider.Infrastructure.Data.Contexts;
using Rika_OrderProvider.Infrastructure.Data.Entities;
using Rika_OrderProvider.Infrastructure.Data.Repositories;
using Rika_OrderProvider.Infrastructure.Factories;
using Rika_OrderProvider.Infrastructure.Models;
using Rika_OrderProvider.Infrastructure.Services;
using Rika_OrderProvider.Infrastructure.Services.Interfaces;

namespace Rika_OrderProvider.Infrastructure.Tests.ServiceTests;

public class OrderService_IntegrationTests
{
    private readonly OrderService _orderService;
    private readonly OrderDbContext _dbContext;
    public OrderService_IntegrationTests()
    {
        var options = new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase(databaseName: "TestingDb").Options;
        _dbContext = new OrderDbContext(options);

        var orderRepository = new OrderRepository(_dbContext);
        var orderAddressRepository = new OrderAddressRepository(_dbContext);
        var orderCustomerRepository = new OrderCustomerRepository(_dbContext);
        var orderProductRepos = new OrderProductRepository(_dbContext);

        _orderService = new OrderService(orderRepository, orderAddressRepository, orderCustomerRepository, orderProductRepos );

        SeedDatabase();
    }

    private async void SeedDatabase()
    {
        var orderCustomer = new OrderCustomerEntity() 
        {
            CustomerEmail = "hej@hej.se",
            CustomerName = "John Doe",
            CustomerPhone = "1234567"
        };

        var orderAddress = new OrderAddressEntity()
        {
            Address = "vägvägen 1",
            City = "Malmö",
            PostalCode = "123 45",
            Country = "Sweden"
        };

        var orderProducts = new List<OrderProductEntity>
        {
            new() 
            {
                ArticleNumber = "1",
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2",
                Color = "Red",
                Size = "M"
            },
            new()
            {
                ArticleNumber = "2",
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2",
                Color = "Red",
                Size = "M"
            }
        };

        _dbContext.OrderCustomers.Add(orderCustomer);
        _dbContext.OrderAddresses.Add(orderAddress);
        await _dbContext.SaveChangesAsync();

        var order = new OrderEntity()
        {
            TotalAmount = "200",
            PaymentMethod = "Card",
            ShipmentMethod = "DHL",
            OrderCustomerId = orderCustomer.OrderCustomerId,
            OrderAddressId = orderAddress.OrderAddressId,
            OrderProducts = orderProducts
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
    }


    [Fact]
    public async Task CreateOrder_ShouldCreateOrder_AndReturnOK()
    {
        // Arrange
        var orderModel = new OrderModel
        {
            TotalAmount = "600",
            PaymentMethod = "Card",
            ShipmentMethod = "DHL",
            OrderCustomer = new Models.OrderCustomerModel
            {
                CustomerName = "Emil",
                CustomerEmail = "emil@domain.com",
                CustomerPhone = "0701234567"
            },
            OrderAddress = new Models.OrderAddressModel
            {
                Address = "street 1",
                City = "city",
                PostalCode = "123 45",
                Country = "sweden"
            },
            OrderProducts =
            [
               new() {
                   ArticleNumber = "112233",
                   ProductName = "jacket",
                   UnitPrice = "200",
                   Quantity = "1",
                   Color = "black",
                   Size = "m"
            },
               new() {
                   ArticleNumber = "225566",
                   ProductName = "shoes",
                   UnitPrice = "400",
                   Quantity = "1",
                   Color = "white",
                   Size = "44"
               }
            ]
        };

        // Act
        var result = await _orderService.CreateOrderAsync(orderModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("OK", result.StatusCode.ToString());
    }

    [Fact]
    public async Task CreateOrder_ShouldNotCreateOrder_AndReturnError()
    {
        // Arrange
        var orderModel = new OrderModel
        {
            TotalAmount = "600",
            PaymentMethod = "Card",
            ShipmentMethod = "DHL",
            OrderCustomer = new Models.OrderCustomerModel
            {
                CustomerName = "Emil",
                CustomerEmail = "emil@domain.com",
                CustomerPhone = "0701234567"
            },
            OrderProducts =
            [
               new() {
                   ArticleNumber = "112233",
                   ProductName = "jacket",
                   UnitPrice = "200",
                   Quantity = "1",
                   Color = "black",
                   Size = "m"
            },
               new() {
                   ArticleNumber = "225566",
                   ProductName = "shoes",
                   UnitPrice = "400",
                   Quantity = "1",
                   Color = "white",
                   Size = "44"
               }
            ]
        };

        // Act
        var result = await _orderService.CreateOrderAsync(orderModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ERROR", result.StatusCode.ToString());
    }

    [Fact]
    public async Task GetOneOrderById_ShouldReturnOrder()
    {
        // Arrange

        // Act  
        var result = await _orderService.GetOneOrderAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("OK", result.StatusCode.ToString());
    }

    [Fact]
    public async Task GetOneOrderByWrongId_ShouldNotReturnOrder()
    {
        // Arrange
        //Set an id that does not exist

        // Act  
        var result = await _orderService.GetOneOrderAsync(100);

        // Assert
        Assert.Equal("Not found", result.Message);

    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnOrders_InList()
    {
        //arrange

        //Act
        var result = await _orderService.GetAllOrdersAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal("OK", result.StatusCode.ToString());
    }

    [Fact]
    public async Task UpdateOrder_ShouldUpdateOrderByID_ReturnStatusOk()
    {
        // Arrange
        var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync();

        var updateOrderModel = new UpdateOrderModel
        {
            OrderId = 1,
            OrderStatus = "Shipped",
            OrderCustomer = new UpdateOrderCustomerModel { CustomerName = "Emil", CustomerEmail = "emil@example.com", CustomerPhone = "1234567890" },
            OrderAddress = new UpdateOrderAddressModel { Address = "Updated Address", City = "Updated City", PostalCode = "12345", Country = "Updated Country" },
            OrderProducts = new List<UpdateOrderProductModel>
            {
                new UpdateOrderProductModel { ArticleNumber = "P1", ProductName = "Updated Product 1", UnitPrice = "15", Quantity = "1" },
                new UpdateOrderProductModel { ArticleNumber = "P3", ProductName = "New Product 3", UnitPrice = "30", Quantity = "3" }
            }
        };

        // Act
        var result = await _orderService.UpdateOrderAsync(updateOrderModel);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("OK", result.StatusCode.ToString());

        var updatedOrder = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == existingOrder!.OrderId);

        Assert.Equal("Shipped", updatedOrder!.OrderStatus);
        Assert.Equal("Emil", updatedOrder.OrderCustomer.CustomerName);
        Assert.True(updatedOrder.OrderProducts.Count == 2);
        Assert.Contains(updatedOrder.OrderProducts, p => p.ArticleNumber == "P1" && p.ProductName == "Updated Product 1");


    }

    [Fact]
    public async Task UpdateOrder_ShouldNotUpdateOrderByID_IfOrderIdNotFound_ReturnStatusNotFound()
    {
        // Arrange
        var updateOrderRequest = new UpdateOrderModel
        {
            TotalAmount = "600",
            PaymentMethod = "Card",
            ShipmentMethod = "DHL",
            OrderStatus = "Shipped",
            OrderCustomer = new Models.UpdateOrderCustomerModel
            {
                CustomerName = "Emil",
                CustomerEmail = "emil@domain.com",
                CustomerPhone = "0701234567"
            },
            OrderAddress = new Models.UpdateOrderAddressModel
            {
                Address = "street 1",
                City = "city",
                PostalCode = "123 45",
                Country = "sweden"
            },
            OrderProducts =
             [
                new() {
                   ArticleNumber = "112233",
                   ProductName = "jacket",
                   UnitPrice = "200",
                   Quantity = "1",
                   Color = "black",
                   Size = "m"
            }, new() {
                   ArticleNumber = "225566",
                   ProductName = "shoes",
                   UnitPrice = "400",
                   Quantity = "1",
                   Color = "white",
                   Size = "44"
               }
             ]
        };

        // Act
        var result = await _orderService.UpdateOrderAsync( updateOrderRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NOT_FOUND", result.StatusCode.ToString());
    }

    [Fact]
    public async Task DeleteOneOrderById_ShouldReturnOK()
    {
        // Arrange
        var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync();

        // Act  
        var result = await _orderService.DeleteOrderAsync(existingOrder!.OrderId);

        // Assert
        Assert.Equal("OK", result.StatusCode.ToString());
    }

    [Fact]
    public async Task DeleteOneOrderByWrongId_ShouldReturnNot_Found()
    {
        // Arrange
        //Set non existent ID
        // Act  
        var result = await _orderService.DeleteOrderAsync(100);

        // Assert
        Assert.Equal("NOT_FOUND", result.StatusCode.ToString());
    }
}

