using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Domain.Models;

namespace VirtualRestaurant.Persistence.Repository
{
    public class OwnerRepository
    {
        private readonly SqlContext _context;
        public OwnerRepository(SqlContext context)
        {
            _context = context;
        }

        public Task<Owner> GetByEmail(string email)
        {
            return _context.Owners.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
