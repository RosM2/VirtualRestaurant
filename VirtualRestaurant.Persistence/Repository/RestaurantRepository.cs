using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Mapper;

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
            var owner = await _context.Owners.FirstOrDefaultAsync(x => x.Id == restaurant.Owner.Id);

            var restaurantEntity = RestaurantMapper.ToEntity(restaurant);
            restaurantEntity.Owner = owner;

            await _context.Restaurants.AddAsync(restaurantEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Restaurant>> GetAll()
        {
            return RestaurantMapper.ListFromEntity(await _context.Restaurants.ToListAsync());
        }

        public async Task<Restaurant> GetById(int id)
        {
            return RestaurantMapper.FromEntity(await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<bool> CheckIfExists(string name)
        {
            return await _context.Restaurants.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> CheckOwner(string email, int id)
        {
            return await _context.Restaurants.AnyAsync(x => x.Id == id && x.Owner.Email == email);
        }

        public async Task UpdateTablesCount(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);
            restaurant.FreeTablesCount = restaurant.FreeTablesCount - 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetIdByName(string name)
        {
            return (await _context.Restaurants.FirstOrDefaultAsync(x => x.Name == name)).Id;
        }
    }
}
