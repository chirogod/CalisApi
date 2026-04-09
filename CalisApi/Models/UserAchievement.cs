namespace CalisApi.Models
{
    public class UserAchievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime DateEarned { get; set; } = DateTime.UtcNow;
        public int? SessionId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Achievement Achievement { get; set; } = null!;
    }
}
