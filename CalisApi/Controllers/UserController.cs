using CalisApi.Models.DTOs;
using CalisApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
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
