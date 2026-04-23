using CalisApi.Database.Interfaces;
using CalisApi.Models;
using CalisApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CalisApi.Database.Repositories
{
    public class RutineRepository : IRutineRepository
    {
        private readonly DatabaseContext _context;

        public RutineRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RutinesDto>> GetAllRutines()
        {
            return await _context.Rutines
                .Select(r => new RutinesDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Dificulty = r.Dificulty,
                    Duration = r.Duration,
                    CategoryName = r.Category.Name
                })
                .ToListAsync();
        }

        public async Task<RutineDto> GetRutineById(int id)
        {
            var rutine = await _context.Rutines
                                .Include(r => r.Category)
                                .Include(r => r.Exercises)
                                .FirstOrDefaultAsync(r => r.Id == id);

            if (rutine == null) return null;

            return new RutineDto
            {
                Title = rutine.Title,
                Description = rutine.Description,
                Duration = rutine.Duration,
                Dificulty = rutine.Dificulty,
                CategoryId = rutine.CategoryId,
                Exercises = rutine.Exercises
                    .OrderBy(e => e.Tipo == "Calentamiento" ? 0 : 1)
                    .Select(e => new ExerciseDto
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
        }

        public async Task Create(Rutine rutine)
        {
            await _context.Rutines.AddAsync(rutine);
            await _context.SaveChangesAsync();
        }
    }
}
