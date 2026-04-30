using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClassLibrary1.Models;

namespace ClassLibrary1
{
    // Змінюємо DbContext на IdentityDbContext<ApplicationUser>
    public class MotorsportContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<EngineModel> Engines { get; set; }
        public DbSet<SponsorModel> Sponsors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=motorsport.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ЦЕЙ РЯДОК ОБОВ'ЯЗКОВИЙ ДЛЯ IDENTITY! Він створює таблиці користувачів.
            base.OnModelCreating(modelBuilder);

            // ... ваші попередні налаштування (TPT, зв'язки) залишаються тут ...
        }
    }
}