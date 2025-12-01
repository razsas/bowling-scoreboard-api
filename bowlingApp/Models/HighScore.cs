namespace bowlingApp.Models
{
    public class HighScore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime DateAchieved { get; set; } = DateTime.UtcNow;
    }
}
