using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using CalisApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public UserController(IAuthService authService, IUserRepository userRepositorry)
        {
            _authService = authService;
            _userRepository = userRepositorry;
        }

        [HttpGet("{id:int?}")]
        public async Task<IActionResult> GetUsuario(int? id)
        {
            if (id.HasValue)
            {
                User u = await _userRepository.GetUsuarioById(id.Value);
                if (u == null)
                {
                    return NotFound("No existe este usuario");
                }
                return Ok(u);
            }
            else
            {
                var all = await _userRepository.GetAllUsuarios();
                return Ok(all);
            }
            
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            try
            {
                var token = await _authService.Login(user);
                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex) { 
                return StatusCode(500, new {message = "Error interno",  details = ex.Message});
            }
        }


        [HttpPost("register")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            try
            {
                var token = await _authService.Register(user);
                return Ok(token);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(new { message = e.Message });
            }
            catch (Exception e) {
                return StatusCode(500, new { message = "Error interno", details = e.Message });
            }
            
        }


    }
}
