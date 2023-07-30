using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleshipsBackend.Models;
using System.Linq;
using System;

namespace BattleshipsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<Game> games = new List<Game>();

        //GET list all games
        [HttpGet]
        public IActionResult Get()
        {
            var gameData = games.Select(game => new
            {
                game.Id,
                Player1 = game.Player1?.Name,
                Player2 = game.Player2?.Name
            });

            return Ok(gameData);
        }

        //GET a specific game
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Game game = games.SingleOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound("Game not found.");
            }

            return Ok(game);
        }

        //POST create a game
        [HttpPost]
        public IActionResult Post([FromBody] Guid playerId)
        {
            if (!PlayerController.Players.TryGetValue(playerId, out Player player1))
            {
                return NotFound("Player not found.");
            }

            Game game = new Game(player1);
            games.Add(game);
            return Ok(game.Id);
        }


        //POST join a game
        [HttpPost("{id}/join")]
        public IActionResult Join(Guid id, [FromBody] Guid playerId)
        {
            Game game = games.SingleOrDefault(g => g.Id == id);

            if (!PlayerController.Players.TryGetValue(playerId, out Player player2))
            {
                return NotFound("Player not found");
            }

            if (game == null)
            {
                return NotFound("Game not found");
            }

            if (game.Player1.Id == playerId)
            {
                return BadRequest("Game creator cannot join as player 2");
            }

            game.Player2 = player2;
            return Ok();
        }

        //POST play a turn
        [HttpPost("{id}/playturn")]
        public IActionResult PlayTurn(Guid id, [FromBody] PlayTurnRequest playTurnRequest)
        {
            Game game = games.SingleOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound("Game not found.");
            }

            if (game.CurrentPlayer.Id != playTurnRequest.PlayerId)
            {
                return BadRequest("Error: Not your turn");
            }

            bool isHit = game.PlayTurn(playTurnRequest.Target);
            return Ok(isHit ? "Hit" : "Miss");
        }
    }
}
