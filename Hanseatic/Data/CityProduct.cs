namespace Hanseatic.Data
{
    public class CityProduct
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int City { get; set; }
        public int DesiredAmount { get; set; }
        public int ActualAmount { get; set; }
        public int BasePrice { get; set; }
        public double MinAmountFluctation { get; set; }
        public double MaxAmountFluctation { get; set; }
        public int Save { get; set; }
    }
}
