using CalisApi.Database.Interfaces;
using CalisApi.Database.Repositories;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementRepository _achievementRepository;
        public AchievementController(IAchievementRepository achievementRepository)
        {
            _achievementRepository = achievementRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var achievements = await _achievementRepository.GetAllAchievements();
                return Ok(achievements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Achievement achievement)
        {
            var createdAchievement = await _achievementRepository.CreateAchievement(achievement);
            return Ok(createdAchievement);
        }

        [HttpPost("{sessionId}/assign-achievements")]
        public async Task<IActionResult> AssignAchievements(int sessionId, [FromBody] List<AssignAchievementDto> assignments)
        {
            var toSave = new List<UserAchievement>();

            foreach (var assign in assignments)
            {
                foreach (var userId in assign.UserIds)
                {
                    toSave.Add(new UserAchievement
                    {
                        UserId = userId,
                        AchievementId = assign.AchievementId,
                        SessionId = sessionId,
                        DateEarned = DateTime.UtcNow
                    });
                }
            }

            await _achievementRepository.AssignAchievementsToUsers(toSave);
            return Ok("Logros asignados correctamente.");
        }
    }
}
