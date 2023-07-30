using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleshipsBackend.Models;

namespace BattleshipsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<Game> games = new List<Game>();

        //POST create a game
        [HttpPost]
        public IActionResult Post([FromBody] Player player)
        {
            Game game = new Game(player, null);
            games.Add(game);
            return Ok(game.Id);
        }

        //POST join a game
        [HttpPost("{id}/join")]
        public IActionResult Join(Guid id, [FromBody] Player player)
        {
            Game game = games.SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            game.Player2 = player;
            return Ok();
        }

        //POST play a turn
        [HttpPost("{id}/playturn")]
        public IActionResult PlayTurn(Guid id, [FromBody] Location target)
        {
            Game game = games.SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            bool isHit = game.PlayTurn(target);
            return Ok(isHit ? "Hit" : "Miss");
        }
    }
}
