namespace Hanseatic.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Coin { get; set; }
        public Save? Save { get; set; } = null!;
        public int SaveId { get; set; }
    }
}
