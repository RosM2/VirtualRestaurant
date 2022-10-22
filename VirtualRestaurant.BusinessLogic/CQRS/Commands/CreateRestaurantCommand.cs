using MediatR;
using VirtualRestaurant.Persistence.Entities;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class CreateRestaurant
    {
        public class Command : IRequest<Result>
        {
            public Domain.Models.Restaurant Restaurant;

            public Command(Domain.Models.Restaurant restaurant)
            {
                Restaurant = restaurant;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly RestaurantRepository _restarauntRepository;
            public Handler(RestaurantRepository restarauntRepository)
            {
                _restarauntRepository = restarauntRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {

                await _restarauntRepository.Add(new Restaurant() 
                {
                    Name = command.Restaurant.Name,
                    TotalTablesCount = command.Restaurant.TotalTablesCount,
                    FreeTablesCount = command.Restaurant.FreeTablesCount,
                    Owner = new Owner() 
                    {
                        FirstName = command.Restaurant.Owner.FirstName,
                        LastName = command.Restaurant.Owner.LastName,
                        Email = command.Restaurant.Owner.Email
                    }
                });
                return Result.Ok();
            }
        }
    }
}
