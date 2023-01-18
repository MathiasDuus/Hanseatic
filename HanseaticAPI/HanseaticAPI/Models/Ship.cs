using System.Text.Json.Serialization;

namespace HanseaticAPI.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Coin { get; set; }

        [JsonIgnore]
        public Save? Save { get; set; } = null!;
        public int SaveId { get; set; }
    }
}
