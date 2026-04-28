using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class CrudServiceAsyncTests
{
    private readonly string _testFilePath = "test_data.json";

    // Допоміжний метод для створення сервісу з очищенням файлу
    private CrudServiceAsync<Product> CreateService()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);

        return new CrudServiceAsync<Product>(_testFilePath);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddItem()
    {
        // Arrange
        var service = CreateService();
        var product = Product.CreateNew();

        // Act
        await service.CreateAsync(product);

        // Assert
        Assert.Single(service);
        var addedProduct = await service.ReadAsync(product.Id);
        Assert.NotNull(addedProduct);
        Assert.Equal(product.Name, addedProduct.Name);
    }

    [Fact]
    public async Task ReadAllAsync_Pagination_ShouldReturnCorrectPage()
    {
        // Arrange
        var service = CreateService();
        for (int i = 0; i < 15; i++)
        {
            await service.CreateAsync(Product.CreateNew());
        }

        // Act
        var page1 = await service.ReadAllAsync(pageNumber: 1, pageSize: 10);
        var page2 = await service.ReadAllAsync(pageNumber: 2, pageSize: 10);

        // Assert
        Assert.Equal(10, page1.Count());
        Assert.Equal(5, page2.Count());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveItem()
    {
        // Arrange
        var service = CreateService();
        var product = Product.CreateNew();
        await service.CreateAsync(product);

        // Act
        await service.DeleteAsync(product.Id);

        // Assert
        Assert.Empty(service);
        var deleted = await service.ReadAsync(product.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task SaveAndLoadFromFile_ShouldPersistData()
    {
        // Arrange
        var service1 = CreateService();
        var product = Product.CreateNew();
        await service1.CreateAsync(product);

        // Act
        await service1.SaveToFileAsync();

        var service2 = new CrudServiceAsync<Product>(_testFilePath);
        await service2.LoadFromFileAsync();

        // Assert
        Assert.Single(service2);
        Assert.Equal(product.Id, service2.First().Id);

        // Clean up
        if (File.Exists(_testFilePath)) File.Delete(_testFilePath);
    }
}