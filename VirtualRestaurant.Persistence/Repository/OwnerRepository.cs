using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Persistence.DataAccess;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Mapper;

namespace VirtualRestaurant.Persistence.Repository
{
    public class OwnerRepository
    {
        private readonly SqlContext _context;
        public OwnerRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<Owner> GetByEmail(string email)
        {
            return OwnerMapper.FromEntity(await _context.Owners.FirstOrDefaultAsync(x => x.Email == email));
        }

        public async Task Add(Owner owner)
        {
            await _context.AddAsync(OwnerMapper.ToEntity(owner));
            await _context.SaveChangesAsync();
        }
    }
}
