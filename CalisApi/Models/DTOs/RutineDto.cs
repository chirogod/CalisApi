namespace CalisApi.Models.DTOs
{
    public class RutineDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }    
        public string Dificulty { get; set; }
        public int CategoryId { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
    }
}
