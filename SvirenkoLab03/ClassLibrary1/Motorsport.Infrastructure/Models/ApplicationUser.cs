using Microsoft.AspNetCore.Identity;

namespace ClassLibrary1.Models
{
    // Наслідуємося від базового класу IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // Тут можна додати власні поля, якщо потрібно
        // Наприклад: public string? FavoriteTeam { get; set; }
    }
}