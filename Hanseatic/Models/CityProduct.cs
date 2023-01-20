namespace Hanseatic.Models
{
    public class CityProduct
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Product { get; set; }
        public int DesiredAmount { get; set; }
        public int ActualAmount { get; set; }
        public int BasePrice { get; set; }
        public double MinAmountFluctation { get; set; }
        public double MaxAmountFluctation { get; set; }
        public int SaveId { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }
    }
}
