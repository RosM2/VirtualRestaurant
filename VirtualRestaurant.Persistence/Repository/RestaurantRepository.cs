using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Domain.Models;

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

        public async Task<List<Restaurant>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CheckOwner(string email, int id)
        {
            return await _context.Restaurants.AnyAsync(x => x.Id == id && x.Owner.Email == email);
        }
    }
}
