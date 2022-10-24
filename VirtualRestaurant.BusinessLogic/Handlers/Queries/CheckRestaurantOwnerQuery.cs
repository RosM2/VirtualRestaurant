using MediatR;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Queries
{
    public class CheckRestaurantOwner
    {
        public class Query : IRequest<Result<bool>>
        {
            public string Email { get; set; }

            public int Id { get; set; }

            public Query(string email, int id)
            {
                Id = id;
                Email = email;
            }
        }
        public class Handler : IRequestHandler<Query, Result<bool>>
        {
            private readonly RestaurantRepository _restarauntRepository;

            public Handler(RestaurantRepository restarauntRepository)
            {
                _restarauntRepository = restarauntRepository;
            }

            public async Task<Result<bool>> Handle(Query query, CancellationToken cancellationToken)
            {
                var isOwner = await _restarauntRepository.CheckOwner(query.Email, query.Id);
                return Result<bool>.Ok(isOwner);
            }
        }
    }
}
