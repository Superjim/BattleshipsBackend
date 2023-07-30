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
        public static Dictionary<Guid, Player> Players { get; private set; } = new Dictionary<Guid, Player>();

        //POST player
        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            //check player has name entered
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Please enter a name");
            }
            Player player = new Player(name, Guid.NewGuid(), 10);
            Players[player.Id] = player;

            return Ok(player.Id);
        }

        //GET all players
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Players.Values.Select(p => new { p.Id, p.Name }).ToList());
        }
    }
}
