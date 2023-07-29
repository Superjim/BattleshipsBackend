namespace BattleshipsBackend.Models
{
	public class Ship
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Size { get; set; }
		public int Hits { get; private set; }
		public Location? Location { get; set; }
		public bool IsVertical { get; set; }

		public Ship(string name, int size, bool isVertical)
		{
			Id = Guid.NewGuid();
			Name = name;
			Size = size;
			IsVertical = isVertical;
			Hits = 0;
		}

		public void AddHit()
		{
			Hits++;
		}

		public bool IsShipSunk()
		{
			return Hits >= Size;
		}

	}
}
