namespace SvirenkoLab01
{
    public class Motherboard : Component
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public bool HasWifi { get; set; }

        // Конструктор
        public Motherboard() : base()
        {
        }
    }
}