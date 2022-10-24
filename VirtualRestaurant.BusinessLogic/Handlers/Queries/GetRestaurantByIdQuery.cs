using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Queries
{
    public class GetRestaurantById
    {
        public class Query : IRequest<Result<Restaurant>>
        {
            public int Id { get; set; }
            public Query(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<Restaurant>>
        {
            private readonly RestaurantRepository _restarauntRepository;
            public Handler(RestaurantRepository restarauntRepository)
            {
                _restarauntRepository = restarauntRepository;
            }
            public async Task<Result<Restaurant>> Handle(Query query, CancellationToken cancellationToken)
            {
                var restaurant = await _restarauntRepository.GetById(query.Id);
                return Result<Restaurant>.Ok(restaurant);
            }
        }
    }
}
