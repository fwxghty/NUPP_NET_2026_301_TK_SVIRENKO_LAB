using Microsoft.EntityFrameworkCore;
using ClassLibrary1;
using System.Reflection.Emit;

public class MotorsportContext : DbContext
{
    public DbSet<VehicleModel> Vehicles { get; set; }
    public DbSet<CarModel> Cars { get; set; }
    public DbSet<TeamModel> Teams { get; set; }
    public DbSet<EngineModel> Engines { get; set; }
    public DbSet<SponsorModel> Sponsors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Використовуємо SQLite для портативності
        optionsBuilder.UseSqlite("Data Source=motorsport.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Налаштування TPT (Table-per-Type)
        modelBuilder.Entity<VehicleModel>().ToTable("Vehicles");
        modelBuilder.Entity<CarModel>().ToTable("Cars");

        // 2. Зв'язок 1:1 (Car <-> Engine)
        modelBuilder.Entity<CarModel>()
            .HasOne(c => c.Engine)
            .WithOne(e => e.Car)
            .HasForeignKey<EngineModel>(e => e.CarId);

        // 3. Зв'язок 1:Багатьох (Team <-> Cars)
        modelBuilder.Entity<TeamModel>()
            .HasMany(t => t.Cars)
            .WithOne(c => c.Team)
            .HasForeignKey(c => c.TeamId);

        // 4. Зв'язок Багато:Багатьох (Car <-> Sponsor)
        modelBuilder.Entity<CarModel>()
            .HasMany(c => c.Sponsors)
            .WithMany(s => s.Cars)
            .UsingEntity(j => j.ToTable("CarSponsors")); // Проміжна таблиця
    }
}