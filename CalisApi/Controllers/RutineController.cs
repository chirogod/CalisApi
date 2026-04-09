using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutineController : Controller
    {
        private readonly IRutineRepository _rutineRepository;

        public RutineController(IRutineRepository rutineRepository)
        {
            _rutineRepository = rutineRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rutines = await _rutineRepository.GetAllRutines();
            return Ok(rutines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rutine = await _rutineRepository.GetRutineById(id);
            return Ok(rutine);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRutine([FromBody] RutineDto rutine)
        {
            var newRutine = new Rutine
            {
                Title = rutine.Title,
                Description = rutine.Description,
                Duration = rutine.Duration,
                CategoryId = rutine.CategoryId,
                Exercises = rutine.Exercises.Select(e => new RutineExercise
                {
                    Exercise = e.Exercise,
                    Tipo = e.Tipo,
                    Reps = e.Reps,
                    Series = e.Series,
                    Descanso = e.Descanso,
                    Obs = e.Obs,
                    VideoId = e.VideoId
                }).ToList()
            };

            await _rutineRepository.Create(newRutine);

            return Ok(rutine);
        }
    }
}
