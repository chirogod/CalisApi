using Azure.Core;
using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoUploadService _videoUploadService;

        public VideoController(IVideoRepository videoRepository, IVideoUploadService videoUploadService)
        {
            _videoRepository = videoRepository;
            _videoUploadService = videoUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideos(int? categoryId, string? searchTerm)
        {
            var videos = await _videoRepository.GetAllVideosAsync(categoryId, searchTerm);
            return Ok(videos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVideoById(int id)
        {
            var video = await _videoRepository.GetVideoByIdAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] VideoRequest videoRequest)
        {
            if (videoRequest.File == null || videoRequest.File.Length == 0)
            {
                return BadRequest(new { Message = "Se requiere un archivo de video." });
            }

            if (string.IsNullOrWhiteSpace(videoRequest.Title) || videoRequest.CategoryId <= 0)
            {
                return BadRequest(new { Message = "Faltan campos obligatorios." });
            }
            try
            {
                
                var vid = await _videoUploadService.UploadVideoAsync(videoRequest.File, videoRequest);
                var createdVideo = await _videoRepository.CreateVideoAsync(vid);
                return CreatedAtAction(nameof(GetVideoById), new { id = createdVideo.Id }, createdVideo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var video = await _videoRepository.GetVideoByIdAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            try
            {
                await _videoUploadService.DeleteVideoAsync(id);
                await _videoRepository.DeleteVideoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");

            }
        }
    }
}
