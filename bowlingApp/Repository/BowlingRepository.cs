using bowlingApp.Data;
using bowlingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace bowlingApp.Repository
{
    public class BowlingRepository(ApplicationDbContext context) : IBowlingRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Game> CreateNewGameAsync(string name)
        {
            var newGame = new Game(name);
            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();
            return newGame;
        }

        public async Task<Game?> GetGameAsync(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Frames.OrderBy(f => f.FrameIndex))
                .FirstOrDefaultAsync(g => g.Id == gameId);

            return game;
        }

        public async Task<Game?> AddFrameAsync(Frame frame, List<Frame> updatedScores)
        {
            await UpdateScore(updatedScores);
            _context.Frames.Add(frame);
            await _context.SaveChangesAsync();
            return await GetGameAsync(frame.GameId);
        }

        public async Task<List<HighScore>> GetTopHighScoresAsync(int limit = 5)
        {
            return await _context.HighScores
                .OrderByDescending(h => h.Score)
                .Take(limit)
                .ToListAsync();
        }

        public async Task AddAndLimitHighScoresAsync(HighScore newScore, int limit = 5)
        {
            _context.HighScores.Add(newScore);
            await _context.SaveChangesAsync();

            var allScores = await _context.HighScores
                .OrderByDescending(h => h.Score)
                .ThenBy(h => h.DateAchieved)
                .ToListAsync();

            if (allScores.Count > limit)
            {
                var scoresToRemove = allScores.Skip(limit).ToList();
                _context.HighScores.RemoveRange(scoresToRemove);
                await _context.SaveChangesAsync();
            }
        }

        private async Task UpdateScore(List<Frame> newFrames)
        {
            foreach (var frame in newFrames)
            {
                _context.Frames.Attach(frame);
                _context.Entry(frame).Property(f => f.Score).IsModified = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> UpdateGameScoreAsync(Game game)
        {
            game.Score = game.Frames.Sum(f => f.Score);
            _context.Games.Attach(game);
            _context.Entry(game).Property(g => g.Score).IsModified = true;
            await _context.SaveChangesAsync();
            HighScore newHighScore = new()
            {
                Score = game.Score,
                Name = game.Name
            };
            await AddAndLimitHighScoresAsync(newHighScore);
            return await GetGameAsync(game.Id);
        }
    }
}
