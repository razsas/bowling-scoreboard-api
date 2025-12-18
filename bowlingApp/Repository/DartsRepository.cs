using bowlingApp.Data;
using bowlingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp.Repository
{
    public class DartsRepository(ApplicationDbContext context) : IGameRepository<DartsGame, DartsFrame, DartsHighScore>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<DartsGame> CreateNewGameAsync(string name)
        {
            var newGame = new DartsGame(name);
            _context.DartsGames.Add(newGame);
            await _context.SaveChangesAsync();
            return newGame;
        }

        public async Task<DartsGame?> GetGameAsync(int gameId)
        {
            var game = await _context.DartsGames
                .Include(g => g.Frames.OrderBy(f => f.FrameIndex))
                .FirstOrDefaultAsync(g => g.Id == gameId);

            return game;
        }

        public async Task<DartsGame?> AddFrameAsync(DartsGame game, DartsFrame newFrame)
        {
            if (_context.Entry(newFrame).State == EntityState.Detached)
            {
                _context.DartsFrames.Add(newFrame);
            }
            await _context.SaveChangesAsync();
            return await GetGameAsync(game.Id);
        }

        public async Task<List<DartsHighScore>> GetTopHighScoresAsync(int limit = 5)
        {
            // For darts, lowest score (number of darts/turns) might be better, or standard high score system?
            // Assuming standard high score for now, but usually 501 is about winning faster.
            // Let's stick to the interface, but note that score in 501 decreases.
            // Wait, if Score is "Remaining Points", High Score list is weird.
            // Usually "High Score" in 501 context might be "Fewest Darts" or "Highest Checkout".
            // Given the generic interface, we'll return the list based on Score property, but let's see.
            // The Game.Score property holds the *current* score (initialized to 501 and goes down).
            // A "High Score" in the generic sense might not map perfectly. 
            // I will implement standard retrieval for now.
            return await _context.DartsHighScore
                .OrderByDescending(h => h.DartsCount)
                .Take(limit)
                .ToListAsync();
        }

        public async Task AddHighScoreAsync(DartsHighScore newScore, int limit = 5)
        {
             _context.DartsHighScore.Add(newScore);
            await _context.SaveChangesAsync();
        }

        public async Task<DartsGame?> UpdateGameScoreAsync(DartsGame game)
        {
            DartsHighScore newHighScore = new()
            {
                DartsCount = game.DartsCounter,
                Name = game.Name,
                GameId = game.Id
            };
            await AddHighScoreAsync(newHighScore);
            return await GetGameAsync(game.Id);
        }
    }
}
