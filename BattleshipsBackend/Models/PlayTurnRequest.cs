namespace BattleshipsBackend.Models
{
    public class PlayTurnRequest
    {
        public Guid PlayerId { get; set; }
        public Location Target { get; set; }
    }
}
