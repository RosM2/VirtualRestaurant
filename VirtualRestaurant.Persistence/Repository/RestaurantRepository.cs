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

            if (owner == null)
            {
                throw new ArgumentException("Owner of a restaurant is not found");
            }

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
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant is not found");
            }

            return RestaurantMapper.FromEntity(restaurant);
        }

        public Task<bool> CheckIfExists(string name)
        {
            return _context.Restaurants.AnyAsync(x => x.Name == name);
        }

        public Task<bool> CheckOwner(string email, int id)
        {
            return _context.Restaurants.AnyAsync(x => x.Id == id && x.Owner.Email == email);
        }

        public async Task UpdateTablesCount(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant is not found");
            }

            restaurant.FreeTablesCount = restaurant.FreeTablesCount - 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetIdByName(string name)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Name == name);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant is not found");
            }

            return restaurant.Id;
        }
    }
}
