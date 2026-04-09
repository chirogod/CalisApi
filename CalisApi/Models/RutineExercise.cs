namespace CalisApi.Models
{
    public class RutineExercise
    {
        public int Id { get; set; }
        public string Exercise { get; set; }
        public string Tipo { get; set; }
        public int Reps { get; set; }
        public int Series { get; set; }
        public string Descanso { get; set; }
        public string Obs { get; set; }
        public int? VideoId { get; set; }
        public Video Video { get; set; }
        public int RutineId { get; set; }
        public Rutine Rutine { get; set; }

    }
}
