namespace Wpf_Bd_cars
{
    public class Context : DbContext
    {
        public DbSet<Car>? Cars { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        //public Context()
        //{
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = cars.db");
        }
    }
}