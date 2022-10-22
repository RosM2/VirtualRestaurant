using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Persistence.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public string VisitorEmail { get; set; }

        public int VisitorsCount { get; set; }

        public Table Table { get; set; }
    }
}
