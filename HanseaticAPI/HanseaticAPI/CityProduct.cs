namespace HanseaticAPI
{
    public class CityProduct
    {
        public int Id { get; set; }
        public int city_id { get; set; }
        public int product_type { get; set; }
        public int desired_amount { get; set; }
        public int actual_amount { get; set; }
        public int sell_price { get; set; }
        public int buy_price { get; set; }
        public double min_fluctation { get; set; }
        public double max_fluctation { get; set; }

    }
}
