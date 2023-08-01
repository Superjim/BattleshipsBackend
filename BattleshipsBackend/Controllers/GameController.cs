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
        public IActionResult PlayTurn(Guid id, [FromBody] PlayTurnRequest request)
        {
            // search game by gameID
            Game game = games.SingleOrDefault(g => g.Id == id);

            // if game doesnt exist, 404 NotFound
            if (game == null)
            {
                return NotFound("Game not found.");
            }

            // if playerID doesnt match currentPlayerId, error 400 BadRequest
            if (game.CurrentPlayer.Id != request.PlayerId)
            {
                return BadRequest("It's not your turn");
            }

            // store the opposing player of the one making the turn
            Player targetPlayer = game.CurrentPlayer == game.Player1 ? game.Player2 : game.Player1;

            // store if shot is hit or miss
            bool shotResult = game.TakeShot(targetPlayer, request.Target);

            // check if any player has won
            Player? winner = game.CheckWinCondition();

            // if winner, return winner
            if (winner != null)
            {
                return Ok($"{winner.Name} has won!");
            }

            // swap currentPlayer
            game.CurrentPlayer = targetPlayer;

            // if hit, return hit
            if (shotResult)
            {
                return Ok("Hit");
            }

            // else return miss
            return Ok("Miss");
        }




    }
}
