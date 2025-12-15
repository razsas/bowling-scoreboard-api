namespace bowlingApp.Models
{
    public class BowlingHighScore : BaseHighScore
    {
        public string GameName { get; set; } = "Bowling";
        public int Score { get; set; }
    }

    public class DartsHighScore : BaseHighScore
    {
        public string GameName { get; set; } = "Darts";
        public int DartsCount { get; set; }
    }

    public abstract class BaseHighScore
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateAchieved { get; set; } = DateTime.UtcNow;
        public int GameId { get; set; }
    }
}
