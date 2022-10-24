namespace VirtualRestaurant.Persistence.Mapper
{
    public static class TableMapper
    {
        public static Entities.Table ToEntity(this Domain.Models.Table table)
        {
            if (table == null)
            {
                return null;
            }

            var entityTable = new Entities.Table()
            {
                Id = table.Id,
                IsBooked = table.IsBooked,
                Location = table.Location,
                NumberOfSits = table.NumberOfSits
            };

            return entityTable;
        }

        public static Domain.Models.Table FromEntity(this Entities.Table table)
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
