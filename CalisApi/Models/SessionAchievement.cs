using System.Text.Json.Serialization;

namespace CalisApi.Models
{
    public class SessionAchievement
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int AchievementId { get; set; }

        [JsonIgnore]
        public virtual Session Session { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}
