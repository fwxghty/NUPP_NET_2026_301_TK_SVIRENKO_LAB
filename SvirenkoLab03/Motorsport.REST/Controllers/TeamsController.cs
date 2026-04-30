using ClassLibrary1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Motorsport.REST.Models;
using ClassLibrary1.Repositories;

namespace Motorsport.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeamsController : ControllerBase
    {
        // 1. ОСЬ ЦІ ДВА РЯДКИ ВИРІШУЮТЬ ПОМИЛКУ: Оголошуємо змінну...
        private readonly ICrudService<TeamModel> _teamService;

        // 2. ...і передаємо її через конструктор (це називається Dependency Injection)
        public TeamsController(ICrudService<TeamModel> teamService)
        {
            _teamService = teamService;
        }
        // GET: api/teams/5 - Доступно ВСІМ
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            // ... ваш код
            return Ok();
        }

        // POST: api/teams - Доступно ТІЛЬКИ ролям Admin та Manager
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] TeamCreateModel model)
        {
            // 1. СТВОРЮЄМО змінну newTeam (саме на цьому кроці була помилка)
            var newTeam = new TeamModel
            {
                Name = model.Name
            };

            // 2. Зберігаємо її в базу даних через сервіс
            await _teamService.CreateEntityAsync(newTeam);

            // 3. Повертаємо правильну відповідь із новоствореною командою
            return CreatedAtAction(nameof(GetById), new { id = newTeam.Id }, newTeam);
        }

        // DELETE: api/teams/5 - Доступно ТІЛЬКИ ролі Admin
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            // ... ваш код
            return NoContent();
        }
    }
}