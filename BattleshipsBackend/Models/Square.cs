using BattleshipsBackend.Models;

public class Square
{
	public Ship? Ship { get; set; }
	public bool HasShotBeenTaken { get; private set; }

	public Square()
	{
		Ship = null;
		HasShotBeenTaken = false;
	}

	public bool MarkShotAndHitShip()
	{
		// check if valid, you can't shoot same square twice
		if (HasShotBeenTaken)
		{
			return false;
		}

		// mark square as shot
		HasShotBeenTaken = true;

		// if a ship is present, add a hit to the ship
		if (Ship != null)
		{
			Ship.AddHit();
			return true;
		}

		return false;
	}

	public bool IsShipHitResult()
	{
		// check if the square has a ship and has been hit
		return HasShotBeenTaken && Ship != null;
	}
}