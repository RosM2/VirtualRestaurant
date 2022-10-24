namespace VirtualRestaurant.Persistence.Mapper
{
    public static class ReservationMapper
    {
        public static Entities.Reservation ToEntity(this Domain.Models.Reservation reservation)
        {
            if (reservation == null)
            {
                return null;
            }

            var entityReservation = new Entities.Reservation()
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
