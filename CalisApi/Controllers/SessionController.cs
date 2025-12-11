using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id:int?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id.HasValue)
            {
                var detail = await _sessionRepository.GetSessionById(id.Value);
                if(detail == null)
                {
                    return NoContent();
                }
                return Ok(detail);

            }
            else
            {
                var all = await _sessionRepository.GetAll();
                return Ok(all);
            }
            
        }

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


        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSession([FromBody] SessionDto session)
        {
            var exist = await _sessionRepository.GetByDate(session.Date);
            if (exist != null)
            {
                return Conflict("Ya existe una sesión programada para esta fecha.");
            }
            Session s = new Session
            {
                Title = session.Title,
                Description = session.Description,
                Date = session.Date,
                LimitedSpots = session.LimitedSpots,
                Enrolled = 0
            };
            await _sessionRepository.Create(s);
            return Ok(s);

        }

        
    }
}
