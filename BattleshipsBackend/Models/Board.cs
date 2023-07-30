using System.Text;

namespace BattleshipsBackend.Models
{
    public class Board
    {
        public Square[][] Squares { get; set; }
        public int Size { get; private set; }

        public Board(int size)
        {
            Size = size;
            Squares = new Square[size][];
            for (int i = 0; i < size; i++)
            {
                Squares[i] = new Square[size];
                for (int j = 0; j < size; j++)
                {
                    Squares[i][j] = new Square();
                }
            }
        }

        public bool PlaceShip(Ship ship, Location location)
        {
            //check out of bounds 
            if (location.Row < 0 || location.Column < 0 || location.Row + ship.Size >= Size || location.Column + ship.Size >= Size)
            {
                Console.WriteLine("ship is out of bounds.");
                return false;
            }
            //check to make sure all squares are empty vertically
            if (ship.IsVertical)
            {
                for (int i = location.Row; i < location.Row + ship.Size; i++)
                {
                    if (Squares[i][location.Column].Ship != null)
                    {
                        Console.WriteLine($"placement failed, ship already on location vertical({i}, {location.Column}).");
                        return false;
                    }
                }
            }
            //check horizontally
            else
            {
                for (int j = location.Column; j < location.Column + ship.Size; j++)
                {
                    if (Squares[location.Row][j].Ship != null)
                    {
                        Console.WriteLine($"placement failed, ship already on location horizontal ({location.Row}, {j}).");
                        return false;
                    }
                }
            }

            //if checks pass, place the ship
            for (int i = 0; i < ship.Size; ++i)
            {
                if (ship.IsVertical)
                {
                    Squares[location.Row + i][location.Column].Ship = ship;
                }
                else
                {
                    Squares[location.Row][location.Column + i].Ship = ship;
                }
            }

            ship.Location = location;
            Console.WriteLine("ship placed");
            return true;
        }




        public bool TakeShot(Location location)
        {
            //check shot is within bounds
            if (location.Row < 0 || location.Row >= Size || location.Column < 0 || location.Column >= Size)
            {
                return false;
            }

            //take a valid shot on the square
            bool isShipHit = Squares[location.Row][location.Column].MarkShotAndHitShip();

            // if a ship is hit, add the hit to the ship
            if (isShipHit)
            {
                var ship = Squares[location.Row][location.Column].Ship;
                if (ship != null)
                {
                    ship.AddHit();
                }
            }

            return isShipHit;
        }

        //testing helper 
        public string PrintGrid()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    var square = Squares[row][col];
                    if (square.HasShotBeenTaken)
                    {
                        sb.Append(square.Ship != null ? 'H' : 'M');
                        sb.Append("  ");
                    }
                    else
                    {
                        sb.Append(square.Ship != null ? square.Ship.Name[0] : '#');
                        sb.Append("  ");

                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
