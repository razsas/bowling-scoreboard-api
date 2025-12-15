using bowlingApp.Constants;

namespace bowlingApp.Models
{
    public class DartsGame : Game<DartsFrame>
    {
        public DartsGame() { }
        public DartsGame(string name)
        {
            Name = name;
            Score = DartsConstants.StartingScore;
        }
        public int DartsCounter { get; set; }
    }
}
