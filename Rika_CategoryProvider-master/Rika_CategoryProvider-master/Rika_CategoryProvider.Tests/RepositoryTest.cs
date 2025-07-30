using Moq;
using Rika_CategoryProvider.Infrastructure.Entities;
using Rika_CategoryProvider.Infrastructure.Repos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Rika_CategoryProvider.Tests
{
    public class BaseRepositoryTests
    {
        private readonly Mock<IBaseRepository<Category>> _repositoryMock;

        public BaseRepositoryTests()
        {
            _repositoryMock = new Mock<IBaseRepository<Category>>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEntity()
        {
            // Arrange
            var expectedCategory = new Category { Id = 1, Name = "Test Category" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(expectedCategory);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(1);

            // Assert
            Assert.Equal(expectedCategory, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var categories = new List<Category>
            {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _repositoryMock.Object.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedEntity()
        {
            // Arrange
            var newCategory = new Category { Id = 3, Name = "New Category" };
            _repositoryMock.Setup(repo => repo.AddAsync(newCategory)).ReturnsAsync(newCategory);

            // Act
            var result = await _repositoryMock.Object.AddAsync(newCategory);

            // Assert
            Assert.Equal(newCategory, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldThrowException()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new System.Exception("Test exception"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(async () => await _repositoryMock.Object.GetAllAsync());

            // Verify the exception message
            Assert.Equal("Test exception", exception.Message);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            // Arrange
            var newCategory = new Category { Id = 3, Name = "New Category" };
            _repositoryMock.Setup(repo => repo.AddAsync(newCategory)).ThrowsAsync(new System.Exception("Test exception"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<System.Exception>(async () => await _repositoryMock.Object.AddAsync(newCategory));

            // Verify the exception message
            Assert.Equal("Test exception", exception.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            int nonExistentCategoryId = 999; // Ett ID som vi vet inte finns
            _repositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentCategoryId)).ReturnsAsync((Category)null);

            // Act
            var result = await _repositoryMock.Object.GetByIdAsync(nonExistentCategoryId);

            // Assert
            Assert.Null(result); // Testet misslyckas om `result` inte är null
        }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
       
    }
}
