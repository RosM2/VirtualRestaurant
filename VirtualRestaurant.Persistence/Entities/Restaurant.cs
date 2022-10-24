using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Persistence.Entities
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Owner Owner { get; set; }

        public ICollection<Table> Tables { get; set; }

        public int TotalTablesCount { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
