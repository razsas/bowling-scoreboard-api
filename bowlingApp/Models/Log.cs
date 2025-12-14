namespace bowlingApp.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? LogLevel { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public int? GameId { get; set; }
        public int? FrameId { get; set; }
        public string? InputData { get; set; }
        //public string Game { get; set; }
    }
}
