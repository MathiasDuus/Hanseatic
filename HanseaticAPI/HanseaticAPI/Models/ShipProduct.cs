using System.Text.Json.Serialization;

namespace HanseaticAPI.Models
{
    public class ShipProduct
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Ship? Ship { get; set; } = null!;
        public int ShipId { get; set; }

        [JsonIgnore]
        public ProductType? ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public int Amount { get; set; }

    }
}
