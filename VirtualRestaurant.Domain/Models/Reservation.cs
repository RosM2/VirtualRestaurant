using System.ComponentModel.DataAnnotations;

namespace VirtualRestaurant.Domain.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public string VisitorEmail { get; set; }

        public int VisitorsCount { get; set; }

        public Table Table { get; set; }

        public int RestaurantId { get; set; }
    }
}
