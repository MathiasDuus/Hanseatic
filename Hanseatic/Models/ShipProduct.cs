namespace Hanseatic.Models
{
    public class ShipProduct
    {
        public int? Id { get; set; }
        public int ShipId { get; set; }
        public int ProductTypeId { get; set; }
        public int Amount { get; set; }
    }
}
