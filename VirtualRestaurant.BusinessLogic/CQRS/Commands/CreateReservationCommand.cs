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
    public class CreateReservationCommand
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
                await _reservationRepository.Add(command.Reservation);
                await _tableRepository.UpdateTableBookStatus(2);
                return Result.Ok();
            }
        }
    }
}
