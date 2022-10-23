using Microsoft.EntityFrameworkCore;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.DataAccess;

namespace VirtualRestaurant.Persistence.Repository
{
    public class TableRepository
    {
        private readonly SqlContext _context;
        public TableRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task Add(List<Table> tableList)
        {
            foreach (var item in tableList)
            {
                await _context.Tables.AddAsync(item);
            }   
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAllTables(IList<Table> tableList, int id)
        {
            var oldTables = _context.Tables.Where(x => x.Restaurant.Id == id).ToList();
            if (oldTables.Count != tableList.Count)
            {
                return false;
            }
            for (int i = 0; i < oldTables.Count; i++)
            {
                oldTables[i].IsBooked = tableList[i].IsBooked;
                oldTables[i].NumberOfSits = tableList[i].NumberOfSits;
                oldTables[i].Location = tableList[i].Location;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateTableBookStatus(int id)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(x => x.Id == id);
            table.IsBooked = true;
            await _context.SaveChangesAsync();
        }

        public async Task<Table> GetByRestaurantId(int id, int visitorsCount)
        {
            return await _context.Tables.OrderBy(x => x.NumberOfSits).FirstOrDefaultAsync(x => x.Restaurant.Id == id && x.IsBooked == false && x.NumberOfSits >= visitorsCount);
        }
    }
}
