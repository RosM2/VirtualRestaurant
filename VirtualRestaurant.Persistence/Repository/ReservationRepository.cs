using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.DataAccess;

namespace VirtualRestaurant.Persistence.Repository
{
    public class ReservationRepository
    {
        private readonly SqlContext _context;
        public ReservationRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task Add(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
