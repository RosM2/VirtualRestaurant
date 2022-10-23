namespace VirtualRestaurant.Api.DTO.RequestDto
{
    public class CreateReservationDto
    {
        public DateTime ReservationDate { get; set; }

        public string VisitorEmail { get; set; }

        public int VisitorsCount { get; set; }

        public int RestaurantId { get; set; }
    }
}
