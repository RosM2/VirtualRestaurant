using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Persistence.Entities
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        public Restaurant Restaurant { get; set; }

        public int? NumberOfSits { get; set; }

        public string? Location { get; set; }

        public bool IsBooked { get; set; }
    }
}
