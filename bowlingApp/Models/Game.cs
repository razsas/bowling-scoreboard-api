namespace bowlingApp.Models
{
    public abstract class Game<TFrame> where TFrame : Frame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<TFrame> Frames { get; set; } = [];
        public int Score { get; set; }
    }
}
