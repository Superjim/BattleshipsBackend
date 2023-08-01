using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleshipsBackend.Models;
using System;
using System.Linq;

namespace BattleshipsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // store players in dictionary
        public static Dictionary<Guid, Player> Players { get; private set; } = new Dictionary<Guid, Player>();

        // POST: api/player
        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            // check player name sent in body
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Please enter a name");
            }

            Player player = new Player(name, Guid.NewGuid(), 10);

            // add ships to the player
            player.AddShip(new Ship("Aircraft Carrier", 5, GetRandomBoolean()));
            player.AddShip(new Ship("Battleship", 4, GetRandomBoolean()));
            player.AddShip(new Ship("Cruiser", 3, GetRandomBoolean()));
            player.AddShip(new Ship("Submarine", 3, GetRandomBoolean()));
            player.AddShip(new Ship("Patrol Boat", 2, GetRandomBoolean()));

            // place ships RANDOMLY
            player.PlaceShips();

            Players[player.Id] = player;

            return Ok(player.Id);
        }
        private bool GetRandomBoolean()
        {
            return new Random().Next(0, 2) == 0;
        }

        // GET: api/player
        [HttpGet]
        public IActionResult Get()
        {
            // send only id and name
            return Ok(Players.Values.Select(p => new { p.Id, p.Name }).ToList());
        }
    }
}
