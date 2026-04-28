using System;

public interface IEntity
{
    Guid Id { get; set; }
}

public class Product : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Статичний метод для генерації випадкових даних (Пункт 2)
    public static Product CreateNew()
    {
        var random = new Random();
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = $"Product_{Guid.NewGuid().ToString().Substring(0, 5)}",
            Price = (decimal)(random.NextDouble() * 1000) + 1, // Ціна від 1 до 1001
            Quantity = random.Next(1, 100)
        };
    }
}

public interface ICrudServiceAsync<T> : IEnumerable<T> where T : class, IEntity
{
    string FilePath { get; }
    Task CreateAsync(T item);
    Task<T?> ReadAsync(Guid id);
    Task<IEnumerable<T>> ReadAllAsync(int pageNumber, int pageSize);
    Task UpdateAsync(T item);
    Task DeleteAsync(Guid id);
    Task SaveToFileAsync();
    Task LoadFromFileAsync();
}