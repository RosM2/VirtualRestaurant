namespace VirtualRestaurant.Persistence.Mapper
{
    public static class TableMapper
    {
        public static Persistence.Entities.Table ToEntity(this Domain.Models.Table table)
        {
            if (table == null)
            {
                return null;
            }
            var entityTable = new Persistence.Entities.Table()
            {
                Id = table.Id,
                IsBooked = table.IsBooked,
                Location = table.Location,
                NumberOfSits = table.NumberOfSits
            };
            return entityTable;
        }

        public static Domain.Models.Table FromEntity(this Persistence.Entities.Table table)
        {
            if (table == null)
            {
                return null;
            }
            var domainTable = new Domain.Models.Table()
            {
                Id = table.Id,
                IsBooked = table.IsBooked,
                Location = table.Location,
                NumberOfSits = table.NumberOfSits
            };
            return domainTable;
        }
    }
}
