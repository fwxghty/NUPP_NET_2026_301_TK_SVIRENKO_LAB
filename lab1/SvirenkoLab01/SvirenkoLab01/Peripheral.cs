namespace SvirenkoLab01
{
    public class Peripheral : Component
    {
        public string ConnectionType { get; set; }
        public string Color { get; set; }
        public bool IsGaming { get; set; }

        public Peripheral() : base() { }
    }
}