namespace Motorsport.REST.Models
{
    public class CarResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopSpeed { get; set; }
        public int TeamId { get; set; }
    }

    public class CarCreateModel
    {
        public string Name { get; set; }
        public int TopSpeed { get; set; }
        public int TeamId { get; set; }
    }
}