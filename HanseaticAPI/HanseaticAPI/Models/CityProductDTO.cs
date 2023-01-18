namespace HanseaticAPI.Models
{
    public class CityProductDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int ProductId { get; set; }
        public int DesiredAmount { get; set; }
        public int ActualAmount { get; set; }
        public int BasePrice { get; set; }
        public double MinAmountFluctation { get; set; }
        public double MaxAmountFluctation { get; set; }
        public int Save { get; set; }

    }
}
