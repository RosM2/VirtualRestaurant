using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Queries
{
    public class GetRestaurants
    {
        public class Query : IRequest<Result<List<Restaurant>>>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, Result<List<Restaurant>>>
        {
            private readonly RestaurantRepository _restarauntRepository;
            public Handler(RestaurantRepository restarauntRepository)
            {
                _restarauntRepository = restarauntRepository;
            }
            public async Task<Result<List<Restaurant>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = await _restarauntRepository.GetAll();
                return Result<List<Restaurant>>.Ok(result.Select(x => new Restaurant() 
                {
                    FreeTablesCount = x.FreeTablesCount,
                    TotalTablesCount = x.TotalTablesCount,
                    Name = x.Name,
                }).ToList());
            }
        }
    }
}
