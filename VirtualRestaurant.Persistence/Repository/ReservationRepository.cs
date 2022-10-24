using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Persistence.Mapper;

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
            var table = await _context.Tables.FirstOrDefaultAsync(x => x.Id == reservation.Table.Id);

            if (table == null)
            {
                throw new ArgumentException("Table is not found");
            }

            var reservationEntity = ReservationMapper.ToEntity(reservation);
            reservationEntity.Table = table;

            await _context.Reservations.AddAsync(reservationEntity);
            await _context.SaveChangesAsync();
        }
    }
}
