using bowlingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<HighScore> HighScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Frame>()
                .HasOne<Game>()
                .WithMany(g => g.Frames)
                .HasForeignKey(f => f.GameId);
        }
    }
}
