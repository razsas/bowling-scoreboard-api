using bowlingApp.Constants;

namespace bowlingApp.Models
{
    public class Game(string name)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public List<Frame> Frames { get; set; } = [];
        public int Score { get; set; }
        public int CurrentFrameNumber => Frames.Count;
        public bool IsGameOver => CurrentFrameNumber == BowlingConstants.MaxFrames;
    }
}
