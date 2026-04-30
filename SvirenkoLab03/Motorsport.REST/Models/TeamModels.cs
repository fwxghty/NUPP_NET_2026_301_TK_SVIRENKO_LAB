namespace Motorsport.REST.Models
{
    // Модель для повернення клієнту (GET)
    public class TeamResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // Модель для створення (POST) - не має Id та інших зайвих навігаційних властивостей
    public class TeamCreateModel
    {
        public string Name { get; set; }
    }
}