using Moq;
using Rika_OrderProvider.Infrastructure.Data.Entities;
using Rika_OrderProvider.Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rika_OrderProvider.Infrastructure.Tests.Repositories.Tests;

public class BaseRepositoryTests
{
    private readonly Mock<IBaseRepository<OrderEntity>> _mockRepository;

    public BaseRepositoryTests()
    {
        _mockRepository = new Mock<IBaseRepository<OrderEntity>>();
    }


    [Fact]
    public async Task CreateAsync_ShouldReturnOrderEntity()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };
        _mockRepository.Setup(x => x.CreateAsync(orderEntity)).ReturnsAsync(orderEntity);

        // Act
        var result = await _mockRepository.Object.CreateAsync(orderEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderEntity, result);
        Assert.Equal("John Doe", result.OrderCustomer.CustomerName);
        Assert.Equal("Product 1", result.OrderProducts.FirstOrDefault(x => x.ProductName == "Product 1")!.ProductName);
        Assert.Equal("Product 2", result.OrderProducts.FirstOrDefault(x => x.ProductName == "Product 2")!.ProductName);
    }

    [Fact]
    public async Task CreateAsync_ShouldNotReturnOrderEntity()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };
        _mockRepository.Setup(x => x.CreateAsync(orderEntity)).ReturnsAsync((OrderEntity)null!);

        // Act
        var result = await _mockRepository.Object.CreateAsync(orderEntity);

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public async Task GetOneAsync_ShouldReturnOneOrderByOrderId()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.GetOneAsync(x => x.OrderId == orderEntity.OrderId)).ReturnsAsync(orderEntity);

        // Act
        var result = await _mockRepository.Object.GetOneAsync(x => x.OrderId == orderEntity.OrderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderEntity.OrderId, result.OrderId);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotReturnOneOrderByOrderId()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };



        // Act
        var result = await _mockRepository.Object.GetOneAsync(x => x.OrderId == orderEntity.OrderId);

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfAllOrders()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<OrderEntity> { orderEntity });

        // Act
        var result = await _mockRepository.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<OrderEntity>>(result);


    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListOfAllOrders()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<OrderEntity>());

        // Act
        var result = await _mockRepository.Object.GetAllAsync();

        // Assert
        Assert.Empty(result);

    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedOrder()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        var updatedOrderEntity = new OrderEntity
        {
            TotalAmount = "200",
            PaymentMethod = "Credit Card",
            ShipmentMethod = "FedEx",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.UpdateAsync(x => x.OrderId == orderEntity.OrderId, updatedOrderEntity)).ReturnsAsync(updatedOrderEntity);

        // Act
        var result = await _mockRepository.Object.UpdateAsync(x => x.OrderId == orderEntity.OrderId, updatedOrderEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedOrderEntity, result);
        Assert.Equal("200", result.TotalAmount);

    }

    [Fact]
    public async Task UpdateAsync_ShouldNotReturnUpdatedOrder()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        var updatedOrderEntity = new OrderEntity
        {
            TotalAmount = "",
            PaymentMethod = "Credit Card",
            ShipmentMethod = "FedEx",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.UpdateAsync(x => x.OrderId == orderEntity.OrderId, updatedOrderEntity))
        .ReturnsAsync((Expression<Func<OrderEntity, bool>> filter, OrderEntity entity) =>
        {
            if (string.IsNullOrEmpty(entity.TotalAmount) ||
                string.IsNullOrEmpty(entity.PaymentMethod) ||
                string.IsNullOrEmpty(entity.ShipmentMethod))
            {
                return null!;
            }
            return entity;
        });

        // Act
        var result = await _mockRepository.Object.UpdateAsync(x => x.OrderId == orderEntity.OrderId, updatedOrderEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteOrderByOrderId()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.DeleteAsync(x => x.OrderId == orderEntity.OrderId)).ReturnsAsync(true);

        // Act
        var result = await _mockRepository.Object.DeleteAsync(x => x.OrderId == orderEntity.OrderId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteOrderIfOrderIdNotFound()
    {
        // Arrange
        var customer = new OrderCustomerEntity
        {
            CustomerName = "John Doe",
            CustomerEmail = "john@domain.com",
            CustomerPhone = "1234567890"
        };

        var address = new OrderAddressEntity
        {
            Address = "123 Main",
            City = "New York",
            PostalCode = "10001",
            Country = "USA"
        };

        var products = new List<OrderProductEntity>
        {
            new() {
                ProductName = "Product 1",
                UnitPrice = "50",
                Quantity = "2"
            },
            new() {
                ProductName = "Product 2",
                UnitPrice = "100",
                Quantity = "5"
            }
        };

        var orderEntity = new OrderEntity
        {
            TotalAmount = "100",
            PaymentMethod = "Cash",
            ShipmentMethod = "UPS",
            OrderAddress = address,
            OrderProducts = products,
            OrderCustomer = customer,
        };

        _mockRepository.Setup(x => x.DeleteAsync(x => x.OrderId == orderEntity.OrderId)).ReturnsAsync(true);

        // Act
        var result = await _mockRepository.Object.DeleteAsync(x => x.OrderId == 0);

        // Assert
        Assert.False(result);
    }

}