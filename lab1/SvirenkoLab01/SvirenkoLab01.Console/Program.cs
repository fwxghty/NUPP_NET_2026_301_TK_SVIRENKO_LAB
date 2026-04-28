using System;
using System.Linq;
using SvirenkoLab01;

namespace ComputerHardware.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Демонстрація статичного поля та методу
            Component.ShowTotalCreated();

            // Ініціалізація CRUD сервісів
            ICrudService<Motherboard> mbService = new CrudService<Motherboard>();
            ICrudService<Peripheral> peripheralService = new CrudService<Peripheral>();

            // Створення об'єктів
            var motherboard1 = new Motherboard
            {
                Name = "ASRock B850 LiveMixer WiFi",
                Price = 280.50m,
                Socket = "AM5",
                Chipset = "B850",
                HasWifi = true
            };

            // Підписка на подію зміни ціни
            motherboard1.OnPriceChanged += (oldPrice, newPrice) =>
            {
                Console.WriteLine($"\n[Подія] Увага! Ціна на {motherboard1.Name} змінилась з {oldPrice:C} на {newPrice:C}");
            };

            var mouse1 = new Peripheral
            {
                Name = "Logitech G304X",
                Price = 45.00m,
                ConnectionType = "Wireless",
                Color = "Black",
                IsGaming = true
            };

            // 1. CREATE
            mbService.Create(motherboard1);
            peripheralService.Create(mouse1);
            
            Console.WriteLine("\n--- Дані після Create ---");
            PrintMotherboards(mbService);
            PrintPeripherals(peripheralService);
            // Демонстрація методу розширення (викличе подію OnPriceChanged)
            motherboard1.ApplyDiscount(10); 

            // 2. UPDATE
            mbService.Update(motherboard1);

            // 3. READ (демонстрація)
            var readMb = mbService.Read(motherboard1.Id);
            Console.WriteLine($"\n--- Read (один елемент) ---\nОтримано: {readMb.Name}, Поточна ціна: {readMb.Price:C}");

            // 4. ДОДАТКОВЕ ЗАВДАННЯ: Save & Load
            string filePath = "hardware_data.json";
            mbService.Save(filePath);
            Console.WriteLine($"\n--- Дані збережено у файл {filePath} ---");

            // Створюємо новий порожній сервіс та завантажуємо дані з файлу
            ICrudService<Motherboard> newMbService = new CrudService<Motherboard>();
            newMbService.Load(filePath);
            
            Console.WriteLine("\n--- Дані після Load з файлу ---");
            PrintMotherboards(newMbService);

            // 5. REMOVE
            var itemToRemove = newMbService.ReadAll().First();
            newMbService.Remove(itemToRemove);
            
            Console.WriteLine("\n--- Дані після Remove ---");
            if (!newMbService.ReadAll().Any())
            {
                Console.WriteLine("Список материнських плат порожній.");
            }

            // Статичний метод в кінці
            Component.ShowTotalCreated();
            
            Console.ReadLine();
        }

        static void PrintMotherboards(ICrudService<Motherboard> service)
        {
            foreach (var item in service.ReadAll())
            {
                Console.WriteLine($"- {item.Name} | Чіпсет: {item.Chipset} | Ціна: {item.Price:C}");
            }
        }
        static void PrintPeripherals(ICrudService<Peripheral> service)
        {
            foreach (var item in service.ReadAll())
            {
                Console.WriteLine($"- {item.Name} | Підключення: {item.ConnectionType} | Ціна: {item.Price:C}");
            }
        }
    }
}