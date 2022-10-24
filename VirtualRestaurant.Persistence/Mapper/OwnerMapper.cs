namespace VirtualRestaurant.Persistence.Mapper
{
    public static class OwnerMapper
    {
        public static Domain.Models.Owner FromEntity(this Persistence.Entities.Owner owner) 
        {
            if (owner == null)
            {
                return null;
            }

            var domainOwner = new Domain.Models.Owner()
            {
                Id = owner.Id,
                Email = owner.Email,    
                LastName = owner.LastName,
                FirstName = owner.FirstName
            };

            return domainOwner;
        }

        public static Entities.Owner ToEntity(this Domain.Models.Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            var entityOwner = new Persistence.Entities.Owner()
            {
                Id = owner.Id,
                Email = owner.Email,
                LastName = owner.LastName,
                FirstName = owner.FirstName
            };

            return entityOwner;
        }
    }
}
