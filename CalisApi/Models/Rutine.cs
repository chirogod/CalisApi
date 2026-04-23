using CalisApi.Models.DTOs;

namespace CalisApi.Models
{
    public class Rutine
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Dificulty { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<RutineExercise> Exercises { get; set; }
    }
}
