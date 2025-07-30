using Moq;
using Rika_ProductProvier.Infrastructure.Interfaces;

namespace Infrastructure.tests.Repositories;
public class SampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class BaseRepository_tests
{
    private readonly Mock<IBaseRepository<SampleEntity>> _repositoryMock;
    private readonly IBaseRepository<SampleEntity> _repository;

    public BaseRepository_tests()
    {
        _repositoryMock = new Mock<IBaseRepository<SampleEntity>>();
        _repository = _repositoryMock.Object;
    }

    [Fact]
    public async Task CreateOneAsync_ShouldReturnEntity()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.CreateOneAsync(entity)).ReturnsAsync(entity);

        // Act
        var result = await _repository.CreateOneAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<SampleEntity>(result);
    }

    [Fact]
    public async Task CreateOneAsync_ShouldReturnNull_IfNameIsNull()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = null };

        _repositoryMock.Setup(x => x.CreateOneAsync(entity)).ReturnsAsync((SampleEntity)null);

        // Act
        var result = await _repository.CreateOneAsync(entity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateOneAsync_ShouldReturnUpdatedEntity()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.UpdateOneAsync(entity)).ReturnsAsync(entity);

        // Act
        var result = await _repository.UpdateOneAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<SampleEntity>(result);
    }

    [Fact]
    public async Task UpdateOneAsync_ShouldReturnNull_IfNameIsNull()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = null };

        _repositoryMock.Setup(x => x.UpdateOneAsync(entity)).ReturnsAsync((SampleEntity)null);

        // Act
        var result = await _repository.UpdateOneAsync(entity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteOneAsync_ShouldRemoveEntity()
    {
        // Arrange

        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.DeleteOneAsync(x => entity.Id == 0)).ReturnsAsync(true);

        // Act
        var result = await _repository.DeleteOneAsync(x => entity.Id == 0);

        // Assert
        Assert.True(result);
    }

    [Fact]

    public async Task DeleteOneAsync_ShouldReturnNull_IfIdDoesNotExist()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.DeleteOneAsync(x => entity.Id == 1)).ReturnsAsync(false);

        // Act
        var result = await _repository.DeleteOneAsync(x => entity.Id == 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldReturnEntity()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.GetOneAsync(x => entity.Id == entity.Id)).ReturnsAsync(entity);

        // Act
        var result = await _repository.GetOneAsync(x => entity.Id == entity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<SampleEntity>(result);
        Assert.Equal(entity.Id, result.Id);
    }

    [Fact]
    public async Task GetOneAsync_ShouldReturnNull_IfEntityNotFound()
    {
        // Arrange
        var entity = new SampleEntity { Id = 0, Name = "Test" };

        _repositoryMock.Setup(x => x.GetOneAsync(x => entity.Id == entity.Id)).ReturnsAsync((SampleEntity)null!);

        // Act
        var result = await _repository.GetOneAsync(x => entity.Id == entity.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEntities()
    {
        // Arrange
        var entities = new List<SampleEntity>
        {
            new SampleEntity { Id = 0, Name = "Test1" },
            new SampleEntity { Id = 1, Name = "Test2" },
            new SampleEntity { Id = 2, Name = "Test3" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(entities);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SampleEntity>>(result);
        Assert.Equal(entities.Count, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_IfNoEntitiesFound()
    {
        // Arrange
        var entities = new List<SampleEntity>();

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(entities);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SampleEntity>>(result);
        Assert.Empty(result);
    }


    [Fact]
    public async Task GetAllAsync_ShouldReturnEntitiesWithSelectedName()
    {
        // Arrange
        var entities = new List<SampleEntity>
        {
            new SampleEntity { Id = 0, Name = "Test1" },
            new SampleEntity { Id = 1, Name = "Test2" },
            new SampleEntity { Id = 2, Name = "Test3" },
            new SampleEntity { Id = 3, Name = "Test3" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync(x => x.Name == "Test3")).ReturnsAsync(entities.Where(x => x.Name == "Test3").ToList());

        // Act
        var result = await _repository.GetAllAsync(x => x.Name == "Test3");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SampleEntity>>(result);
        Assert.NotEqual(entities.Count, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyListIfSelectedNameDoesNotExist()
    {
        // Arrange
        var entities = new List<SampleEntity>
        {
            new SampleEntity { Id = 0, Name = "Test1" },
            new SampleEntity { Id = 1, Name = "Test2" },
            new SampleEntity { Id = 2, Name = "Test3" },
            new SampleEntity { Id = 3, Name = "Test3" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync(x => x.Name == "Test4")).ReturnsAsync(entities.Where(x => x.Name == "Test4").ToList());

        // Act
        var result = await _repository.GetAllAsync(x => x.Name == "Test4");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<SampleEntity>>(result);
        Assert.Empty(result);
    }
}
