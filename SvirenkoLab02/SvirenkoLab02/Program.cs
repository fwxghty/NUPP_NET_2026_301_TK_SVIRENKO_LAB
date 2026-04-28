using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string path = "products.json";
        var service = new CrudServiceAsync<Product>(path);

        Console.WriteLine("Починаємо паралельну генерацію 10 000 об'єктів...");

        // Пункт 3: Паралельне створення об'єктів
        Parallel.For(0, 10000, i =>
        {
            var newProduct = Product.CreateNew();
            // Task.Run використовується, бо сигнатура методу Parallel.For синхронна, 
            // але наші CRUD операції (номінально) асинхронні. 
            // В реальному проекті варто використовувати Parallel.ForEachAsync для .NET 6+
            service.CreateAsync(newProduct).Wait();
        });

        // Альтернатива для .NET 6+ (більш правильна асинхронно):
        // await Parallel.ForEachAsync(Enumerable.Range(0, 10000), async (i, token) =>
        // {
        //     await service.CreateAsync(Product.CreateNew());
        // });

        Console.WriteLine($"Створено об'єктів: {service.Count()}");

        // Обчислення статистики (для Price та Quantity)
        var minPrice = service.Min(p => p.Price);
        var maxPrice = service.Max(p => p.Price);
        var avgPrice = service.Average(p => p.Price);

        var avgQuantity = service.Average(p => p.Quantity);

        Console.WriteLine("\n--- Статистика ---");
        Console.WriteLine($"Мінімальна ціна: {minPrice:F2}");
        Console.WriteLine($"Максимальна ціна: {maxPrice:F2}");
        Console.WriteLine($"Середня ціна: {avgPrice:F2}");
        Console.WriteLine($"Середня кількість: {avgQuantity:F2}");

        // Збереження у файл
        Console.WriteLine("\nЗбереження у файл...");
        await service.SaveToFileAsync();
        Console.WriteLine($"Дані успішно збережено у {service.FilePath}");
    }
}