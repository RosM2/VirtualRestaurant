namespace VirtualRestaurant.Persistence.Mapper
{
    public static class ReservationMapper
    {
        public static Persistence.Entities.Reservation ToEntity(this Domain.Models.Reservation reservation)
        {
            if (reservation == null)
            {
                return null;
            }
            var entityReservation = new Persistence.Entities.Reservation()
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                RestaurantId = reservation.RestaurantId,
                VisitorEmail = reservation.VisitorEmail,
                VisitorsCount = reservation.VisitorsCount
            };
            return entityReservation;
        }
    }
}
