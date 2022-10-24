using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Queries
{
    public class GetOwner
    {
        public class Query : IRequest<Result<Owner>>
        {
            public string Email { get; set; }

            public Query(string email)
            {
                Email = email;
            }
        }

        public class Handler : IRequestHandler<Query, Result<Owner>>
        {
            private readonly OwnerRepository _ownerRepository;

            public Handler(OwnerRepository ownerRepository)
            {
                _ownerRepository = ownerRepository;
            }

            public async Task<Result<Owner>> Handle(Query query, CancellationToken cancellationToken)
            {
                var owner = await _ownerRepository.GetByEmail(query.Email);
                return Result<Owner>.Ok(owner);            
            }
        }
    }
}
