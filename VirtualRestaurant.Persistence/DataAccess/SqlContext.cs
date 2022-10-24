using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Persistence.Entities;

namespace VirtualRestaurant.Persistence.DataAccess
{
    public class SqlContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {

        }
    }
}
