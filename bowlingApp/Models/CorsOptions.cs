namespace bowlingApp.Models
{
    public class CorsSettings
    {
        public List<string> AllowedOrigins { get; set; } = [];
        public List<string> AllowedMethods { get; set; } = [];
        public List<string> AllowedHeaders { get; set; } = [];
    }
}
