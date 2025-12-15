using bowlingApp.Constants;

namespace bowlingApp.Models
{
    public class BowlingGame : Game<BowlingFrame>
    {
        public BowlingGame(string name)
        {
            Name = name;
        }
        public override List<BowlingFrame> Frames { get; set; } = [];
        public int CurrentFrameNumber => Frames.Count;
        public bool IsGameOver => CurrentFrameNumber == BowlingConstants.MaxFrames;

        public void UpdatePreviousFrameScores(BowlingFrame newFrame)
        {
            if (newFrame.FrameIndex == 0) return;

            var lastFrame = Frames.FirstOrDefault(f => f.FrameIndex == newFrame.FrameIndex - 1);
            var last2Frame = Frames.FirstOrDefault(f => f.FrameIndex == newFrame.FrameIndex - 2);

            // 1. Check for Double Strike (scoring 30 points + new roll, e.g., Frame 1 and 2 are strikes)
            if (last2Frame != null && last2Frame.IsStrike && lastFrame != null && lastFrame.IsStrike)
            {
                last2Frame.Score = (BowlingConstants.MaxPins * 2) + newFrame.Roll1;
            }

            if (lastFrame != null)
            {
                // 2. Check for Strike in the immediate previous frame (needs next two rolls for bonus)
                if (lastFrame.IsStrike)
                {
                    lastFrame.Score = BowlingConstants.MaxPins + newFrame.Roll1 + (newFrame.Roll2 ?? 0);
                }

                // 3. Check for Spare in the immediate previous frame (needs next one roll for bonus)
                if (lastFrame.IsSpare)
                {
                    lastFrame.Score = BowlingConstants.MaxPins + newFrame.Roll1;
                }
            }
        }
    }
}
