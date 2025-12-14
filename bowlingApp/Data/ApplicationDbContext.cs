using bowlingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<BowlingGame> BowlingGames { get; set; }
        public DbSet<BowlingFrame> BowlingFrames { get; set; }
        public DbSet<HighScore> HighScores { get; set; }
        public DbSet<Log> AppLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BowlingFrame>()
                .HasOne<BowlingGame>()
                .WithMany(g => g.Frames)
                .HasForeignKey(f => f.GameId);

            modelBuilder.Entity<Log>()
                .HasOne<BowlingGame>()
                .WithMany()
                .HasForeignKey(l => l.GameId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Log>()
                .HasOne<BowlingFrame>()
                .WithMany()
                .HasForeignKey(l => l.FrameId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Log>()
                .HasKey(l => l.Id);
        }
    }
}
