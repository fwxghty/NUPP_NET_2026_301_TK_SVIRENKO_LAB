using SvirenkoLab01;
using System;

    // Делегат
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

    public class Component : IEntity
    {
        // Статичне поле
        public static int TotalComponentsCreated;

        // Статичний конструктор
        static Component()
        {
            TotalComponentsCreated = 0;
        }

        // Властивості (мінімум 3 для кожного класу за завданням)
        public Guid Id { get; set; }
        public string Name { get; set; }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    // Виклик події
                    OnPriceChanged?.Invoke(_price, value);
                    _price = value;
                }
            }
        }

        // Подія
        public event PriceChangedHandler OnPriceChanged;

        // Конструктор
        public Component()
        {
            Id = Guid.NewGuid();
            TotalComponentsCreated++;
        }

        // Метод
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[{Id}] {Name} - {Price}$");
        }

        // Статичний метод
        public static void ShowTotalCreated()
        {
            Console.WriteLine($"Загальна кількість створених компонентів: {TotalComponentsCreated}");
        }
    }