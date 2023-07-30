using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BattleshipsBackend.Models;

namespace BattleshipsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //POST player
        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            Player player = new Player(name, Guid.NewGuid(), 10);
            //add player to the database, need login component

            return Ok(player.Id);
        }
    }
}
