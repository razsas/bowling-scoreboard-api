using bowlingApp.Models;
using bowlingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace bowlingApp.Controllers
{
    [Route("api/bowling")]
    public class BowlingController(
    IGameService<BowlingGame, BowlingFrame, BowlingHighScore> gameService,
        LoggerService loggerService)
        : GameController<BowlingGame, BowlingFrame, BowlingHighScore>(gameService, loggerService);

    [Route("api/darts")]
    public class DartsController(IGameService<DartsGame, DartsFrame, DartsHighScore> gameService,
        LoggerService loggerService)
        : GameController<DartsGame, DartsFrame, DartsHighScore>(gameService, loggerService);
}
