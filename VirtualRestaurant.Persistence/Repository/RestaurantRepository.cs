using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Persistence.Entities;

namespace VirtualRestaurant.Persistence.Repository
{
    public class RestaurantRepository
    {
        private readonly SqlContext _context;
        public RestaurantRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task Add(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
