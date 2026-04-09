namespace CalisApi.Models.DTOs
{
    public class AssignAchievementDto
    {
        public int AchievementId { get; set; }
        public List<int> UserIds { get; set; }
    }
}
