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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository) 
        { 
            _postRepository = postRepository;
        }


        [HttpGet("{id:int?}")]
        public async Task<IActionResult> GetUsuario(int? id)
        {
            if (id.HasValue)
            {
                var p = await _postRepository.GetByIdAsync(id.Value);
                if (p == null)
                {
                    return NotFound("No existe este post");
                }
                return Ok(p);
            }
            else
            {
                var all = await _postRepository.GetAllAsync();
                return Ok(all);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostRequest request)
        {
            try
            {
                var p = await _postRepository.Create(request);
                return Ok(p);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error interno", details = e.Message });

            }
        }
    }
}
