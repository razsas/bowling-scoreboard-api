namespace bowlingApp.Models
{
    public class RollInput
    {
        public int GameId { get; set; }
        public int? Roll1 { get; set; }
        public int? Roll2 { get; set; }
        public int? Roll3 { get; set; }
        public bool IsStrike => Roll1 == 10;
        public bool IsSpare => !IsStrike && Roll2.HasValue && (Roll1 + Roll2.Value == 10);
        public bool IsSpecialRoll => IsStrike || IsSpare;
    }
}
