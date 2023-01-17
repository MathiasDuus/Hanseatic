using HanseaticAPI.Models;

namespace HanseaticAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CityProduct> CityProducts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ShipProduct> ShipProducts { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
