using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class CreateRestaurant
    {
        public class Command : IRequest<Result>
        {
            public Restaurant Restaurant;
            public Owner Owner { get; set; }

            public Command(Restaurant restaurant, Owner owner)
            {
                Restaurant = restaurant;
                Owner = owner;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly RestaurantRepository _restarauntRepository;

            private readonly TableRepository _tableRepository;

            public Handler(RestaurantRepository restarauntRepository, TableRepository tableRepository)
            {
                _restarauntRepository = restarauntRepository;
                _tableRepository = tableRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var restaurant = new Restaurant()
                {
                    Name = command.Restaurant.Name,
                    TotalTablesCount = command.Restaurant.TotalTablesCount,
                    FreeTablesCount = command.Restaurant.FreeTablesCount,
                    Owner = command.Owner
                };

                await _restarauntRepository.Add(restaurant);

                var tablesList = new List<Table>();
                for (int i = 0; i < command.Restaurant.TotalTablesCount; i++)
                {
                    tablesList.Add(new Table() { Restaurant = restaurant });
                }
                await _tableRepository.Add(tablesList);

                return Result.Ok();
            }
        }
    }
}
