using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Domain.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Owner Owner { get; set; }

        public ICollection<Table> Tables { get; set; }

        public int TotalTablesCount { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
