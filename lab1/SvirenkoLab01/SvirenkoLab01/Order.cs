using SvirenkoLab01;
using System;

    public class Order : IEntity
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            OrderDate = DateTime.Now;
        }
    }
