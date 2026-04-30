using ClassLibrary1;
using ClassLibrary1.Repositories;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Ініціалізація Motorsport БД...");

        using var context = new MotorsportContext();
        // Створюємо БД, якщо її ще немає
        await context.Database.EnsureCreatedAsync();

        IRepository<TeamModel> teamRepo = new EfRepository<TeamModel>(context);
        ICrudService<TeamModel> teamService = new CrudService<TeamModel>(teamRepo);

        // Перевіряємо, чи є дані
        var teams = await teamService.GetAllEntitiesAsync();
        if (!teams.Any())
        {
            Console.WriteLine("Створення нової команди...");
            var newTeam = new TeamModel { Name = "Red Bull Racing" };
            await teamService.CreateEntityAsync(newTeam);
        }

        teams = await teamService.GetAllEntitiesAsync();
        Console.WriteLine("\n--- Список команд ---");
        foreach (var team in teams)
        {
            Console.WriteLine($"ID: {team.Id}, Name: {team.Name}");
        }
    }
}