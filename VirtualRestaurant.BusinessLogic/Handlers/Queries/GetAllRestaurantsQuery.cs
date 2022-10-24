using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Queries
{
    public class GetRestaurants
    {
        public class Query : IRequest<Result<IList<Restaurant>>>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, Result<IList<Restaurant>>>
        {
            private readonly RestaurantRepository _restarauntRepository;

            public Handler(RestaurantRepository restarauntRepository)
            {
                _restarauntRepository = restarauntRepository;
            }

            public async Task<Result<IList<Restaurant>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = await _restarauntRepository.GetAll();
                return Result<IList<Restaurant>>.Ok(result);
            }
        }
    }
}
