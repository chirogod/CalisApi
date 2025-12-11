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
    public class UserSessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;

        public UserSessionController(ISessionRepository sessionRepository, IUserRepository userRepository, IUserSessionRepository userSessionRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _userSessionRepository = userSessionRepository;
        }

        [HttpPost("{sessionId}")]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> Enroll(int sessionId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            if (!int.TryParse(userIdClaim.Value, out int userIdFromToken))
            {
                return BadRequest("El formato del ID de usuario en el token es inválido.");
            }

            var sesExist = await _sessionRepository.GetSessionById(sessionId);
            var userExist = await _userRepository.GetById(userIdFromToken);

            var userEnroll = await _userSessionRepository.VerifyEnroll(userIdFromToken, sessionId);

            if (sesExist == null)
            {
                return NotFound("Clase no encontrada");
            }
            if (userExist == null)
            {
                return NotFound("Usuario no encontrado");
            }
            if (userEnroll)
            {
                return BadRequest("Usuario ya esta registrado en esta clase");
            }
            var enrolledUsers = sesExist.Enrolled;

            if (enrolledUsers >= sesExist.LimitedSpots)
            {
                return BadRequest("Cupos de esta clase ya estan llenos");
            }

            var userSession = new UserSession
            {
                UserId = userIdFromToken,
                SessionId = sessionId,
            };
            try
            {
                await _userSessionRepository.Enroll(userSession);
                return Ok(userSession.Session);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al inscribir: {ex.Message}");
            }

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> UnEnroll(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            if (!int.TryParse(userIdClaim.Value, out int userIdFromToken))
            {
                return BadRequest("El formato del ID de usuario en el token es inválido.");
            }
            var isEnrolled = await _userSessionRepository.VerifyEnroll(userIdFromToken, id);

            if (!isEnrolled)
            {
                return BadRequest("No estás inscrito en esta clase.");
            }
            try
            {
                var userSession = new UserSession
                {
                    UserId = userIdFromToken,
                    SessionId = id,
                };
                await _userSessionRepository.UnEnroll(userSession);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno al inscribir: {ex.Message}");
            }
        }
    }
}
