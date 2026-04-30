using Microsoft.AspNetCore.Mvc;
using Motorsport.REST.Models; // Моделі API
using ClassLibrary1;
using ClassLibrary1.Repositories;


namespace Motorsport.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Шлях буде: /api/teams
    public class TeamsController : ControllerBase
    {
        // Впровадження залежності (DI) вашого дженерік CRUD сервісу
        private readonly ICrudService<TeamModel> _teamService;

        public TeamsController(ICrudService<TeamModel> teamService)
        {
            _teamService = teamService;
        }

        // GET: api/teams
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _teamService.GetAllEntitiesAsync();

            // Мапимо моделі БД на моделі API (щоб не віддавати зайвого)
            var response = teams.Select(t => new TeamResponseModel
            {
                Id = t.Id,
                Name = t.Name
            });

            return Ok(response); // HTTP 200
        }

        // GET: api/teams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Примітка: у вашому ICrudService має бути метод GetByIdAsync
            var team = await _teamService.GetByIdAsync(id);

            if (team == null)
            {
                return NotFound(); // HTTP 404 (Філдинг: правильні коди відповіді)
            }

            var response = new TeamResponseModel { Id = team.Id, Name = team.Name };
            return Ok(response); // HTTP 200
        }

        // POST: api/teams
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamCreateModel model)
        {
            // Перетворюємо API модель на модель БД
            var newTeam = new TeamModel
            {
                Name = model.Name
            };

            await _teamService.CreateEntityAsync(newTeam);

            // HTTP 201 Created (Філдинг)
            // Повертаємо посилання на новий ресурс (можна доопрацювати маршрут)
            return CreatedAtAction(nameof(GetById), new { id = newTeam.Id }, newTeam);
        }

        // DELETE: api/teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
            {
                return NotFound(); // HTTP 404
            }

            await _teamService.DeleteEntityAsync(id); // За умови наявності методу в сервісі
            return NoContent(); // HTTP 204 No Content (успішно видалено)
        }
    }
}