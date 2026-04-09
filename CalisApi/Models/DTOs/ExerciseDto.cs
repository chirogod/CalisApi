namespace CalisApi.Models.DTOs
{
    public class ExerciseDto
    {
        public string Exercise { get; set; }
        public string Tipo { get; set; }
        public int Reps { get; set; }
        public int Series { get; set; }
        public string Descanso { get; set; }
        public string Obs { get; set; }
        public int? VideoId { get; set; }
    }
}
