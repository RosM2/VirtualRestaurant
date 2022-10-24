namespace VirtualRestaurant.Persistence.Mapper
{
    public static class RestaurantMapper
    {
        public static Persistence.Entities.Restaurant ToEntity(this Domain.Models.Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return null;
            }
            var entityRestaurant = new Persistence.Entities.Restaurant()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                FreeTablesCount = restaurant.FreeTablesCount,
                TotalTablesCount = restaurant.TotalTablesCount
            };
            return entityRestaurant;
        }
        public static Domain.Models.Restaurant FromEntity(this Persistence.Entities.Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return null;
            }
            var domainRestaurant = new Domain.Models.Restaurant()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                TotalTablesCount = restaurant.TotalTablesCount,
                FreeTablesCount = restaurant.FreeTablesCount
            };
            return domainRestaurant;
        }

        public static List<Domain.Models.Restaurant> ListFromEntity(this List<Persistence.Entities.Restaurant> restaurants)
        {
            if (restaurants == null)
            {
                return null;
            }
            var domainRestaurantList = new List<Domain.Models.Restaurant>();

            foreach (var restaurant in restaurants)
            {
                domainRestaurantList.Add(new Domain.Models.Restaurant()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    TotalTablesCount = restaurant.TotalTablesCount,
                    FreeTablesCount = restaurant.FreeTablesCount
                });
            }                    
            return domainRestaurantList;
        }
    }
}
