namespace CalisApi.Models
{
    public class UserClass
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }

        public Session Class { get; set; }
        public User User { get; set; }

    }
}
