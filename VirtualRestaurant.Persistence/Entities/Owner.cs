using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Persistence.Entities
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
