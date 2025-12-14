namespace bowlingApp.Models
{
    public abstract class Frame
    {
        public Frame() { }
        public int Id { get; set; }
        public int GameId { get; set; }
        public int FrameIndex { get; set; }
        public int Roll1 { get; set; }
        public int? Roll2 { get; set; }
        public int? Roll3 { get; set; }
        public int Score { get; set; }
        public abstract string? ValidateRollInput();
    }
}