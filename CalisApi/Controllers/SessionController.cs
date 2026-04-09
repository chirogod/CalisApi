using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        public SessionController(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        //obtener todas las clases api/session y filtrar por fecha api/session?datetime=2025-12-25 por ej
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? datetime)
        {
            IEnumerable<Session> sessions;

            if (datetime.HasValue)
            {
                sessions = await _sessionRepository.GetAllSessionsByDate(datetime.Value.Date);
            }
            else
            {
                sessions = await _sessionRepository.GetAll();
            }

            if (sessions == null || !sessions.Any()) return NoContent();

            // Mapeamos a DTO para que el JSON sea idéntico al del POST
            var response = sessions.Select(s => new SessionResponseDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Date = s.Date,
                LimitedSpots = s.LimitedSpots,
                Enrolled = s.Enrolled,
                AchievementIds = s.SessionAchievements.Select(sa => sa.AchievementId).ToList()
            });

            return Ok(response);
        }

        //obtener una clase por id api/session/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var session = await _sessionRepository.GetSessionById(id);

            if (session == null) return NotFound("La sesión no existe.");

            // Mapeamos manualmente al DTO para romper el ciclo
            var response = new SessionResponseDto
            {
                Id = session.Id,
                Title = session.Title,
                Description = session.Description,
                Date = session.Date,
                LimitedSpots = session.LimitedSpots,
                Enrolled = session.Enrolled,
                AchievementIds = session.SessionAchievements.Select(sa => sa.AchievementId).ToList()
            };

            return Ok(response);
        }

        //obtener la clase y los usuarios alistados
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetFullSessionDetails(int id)
        {
            var session =  await _sessionRepository.GetSessionById(id);
            if (session == null)
            {
                return NotFound("La sesión no existe.");
            }
            var enrolledUsers = await _sessionRepository.GetEnrolledUsers(id);
            var result = new
            {
                Session = session,
                EnrolledUsers = enrolledUsers
            };
            return Ok(result);

        }

        //obtener usuarios alistados a una clase
        [HttpGet("{id:int?}/Users")]
        public async Task<IActionResult> GetSessionUsers(int id) {
            var exist = await _sessionRepository.GetSessionById(id);
            if(exist == null)
            {
                return NotFound("La sesion no existe");
            }

            try
            {
                var usuarios = await _sessionRepository.GetEnrolledUsers(id);
                return Ok(usuarios);
            }catch(Exception e)
            {
                return StatusCode(500, "Error interno: " + e.Message);
            }

        }

        //obtener usuarios alistados a una clase
        [HttpGet("{id:int?}/Achievements")]
        public async Task<IActionResult> GetSessionAchievements(int id)
        {
            var exist = await _sessionRepository.GetSessionById(id);
            if (exist == null)
            {
                return NotFound("La sesion no existe");
            }

            try
            {
                var achievements = await _sessionRepository.GetSessionAchievements(id);
                return Ok(achievements);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error interno: " + e.Message);
            }

        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto sessionDto)
        {
            var exist = await _sessionRepository.GetSessionByDate(sessionDto.Date);
            if (exist != null)
            {
                return Conflict("Ya existe una sesión programada para esta fecha.");
            }

            Session s = new Session
            {
                Title = sessionDto.Title,
                Description = sessionDto.Description,
                Date = sessionDto.Date,
                LimitedSpots = sessionDto.LimitedSpots,
                Enrolled = 0,
                SessionAchievements = sessionDto.AchievementIds.Select(id => new SessionAchievement
                {
                    AchievementId = id
                }).ToList()
            };

            await _sessionRepository.Create(s);

            return Ok(new SessionResponseDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Date = s.Date,
                LimitedSpots = s.LimitedSpots,
                Enrolled = s.Enrolled,
                AchievementIds = sessionDto.AchievementIds
            });
        }


    }
}
