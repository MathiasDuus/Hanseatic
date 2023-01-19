namespace Hanseatic.Data
{
    public class CityProduct
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Product { get; set; }
        public int ActualAmount { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
    }
}
