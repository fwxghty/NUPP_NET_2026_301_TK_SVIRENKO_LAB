public class EngineModel
{
    public int Id { get; set; }
    public string Specification { get; set; }

    // Зовнішній ключ для 1:1
    public int CarId { get; set; }
    public CarModel Car { get; set; }
}