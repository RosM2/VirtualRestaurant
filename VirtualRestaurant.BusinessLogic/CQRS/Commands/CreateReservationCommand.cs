using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            private readonly TableRepository _tableRepository;

            public Handler(ReservationRepository reservationRepository, TableRepository tableRepository)
            {
                _reservationRepository = reservationRepository;
                _tableRepository = tableRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {            
                var table = await _tableRepository.GetByRestaurantId(command.Reservation.RestaurantId, command.Reservation.VisitorsCount);
                await _tableRepository.UpdateTableBookStatus(table.Id);

                command.Reservation.Table = table;
                await _reservationRepository.Add(command.Reservation);
                return Result.Ok();
            }
        }
    }
}
