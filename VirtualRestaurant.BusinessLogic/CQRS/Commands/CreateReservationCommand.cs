using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class CreateReservation
    {
        public class Command : IRequest<Result>
        {
            public Reservation Reservation;

            public Command(Reservation reservation)
            {
                Reservation = reservation;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly ReservationRepository _reservationRepository;

            private readonly RestaurantRepository _restaurantRepository;

            private readonly TableRepository _tableRepository;

            public Handler(ReservationRepository reservationRepository, TableRepository tableRepository, RestaurantRepository restaurantRepository)
            {
                _reservationRepository = reservationRepository;
                _tableRepository = tableRepository;
                _restaurantRepository = restaurantRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {            
                var table = await _tableRepository.GetByRestaurantId(command.Reservation.RestaurantId, command.Reservation.VisitorsCount);
                if (table == null)
                {
                    return Result.Fail("This restaurant is already full");
                }
                await _tableRepository.UpdateTableBookStatus(table.Id);
                await _restaurantRepository.UpdateTablesCount(command.Reservation.RestaurantId);

                command.Reservation.Table = table;
                await _reservationRepository.Add(command.Reservation);
                return Result.Ok();
            }
        }
    }
}
