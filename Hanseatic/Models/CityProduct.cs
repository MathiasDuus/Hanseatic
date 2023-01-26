using CommunityToolkit.Mvvm.ComponentModel;

namespace Hanseatic.Models
{
    public class CityProduct : ObservableObject
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Product { get; set; }
        public int ProductID { get; set; }
        public int DesiredAmount { get; set; }
        private int actualAmount;
        public int ActualAmount
        {
            get => actualAmount;
            set => SetProperty(ref actualAmount, value);
        }
        public int BasePrice { get; set; }
        public double MinAmountFluctation { get; set; }
        public double MaxAmountFluctation { get; set; }
        public int SaveId { get; set; }
        private int buyPrice;
        public int BuyPrice
        {
            get => buyPrice;
            set => SetProperty(ref buyPrice, value);
        }
        private int sellPrice;
        public int SellPrice
        {
            get => sellPrice;
            set => SetProperty(ref sellPrice, value);
        }
        private int shipProductAmount;
        public int ShipProductAmount
        {
            get => shipProductAmount;
            set => SetProperty(ref shipProductAmount, value);
        }
        public int ShipProductId { get; set; }

    }
}
