namespace HanseaticAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<CityProduct> CityProducts { get; set; }
    }
}
