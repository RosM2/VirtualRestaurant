using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.Handlers.Commands
{
    public class CreateOwner
    {
        public class Command : IRequest<Result>
        {
            public Owner Owner;

            public Command(Owner owner)
            {
                Owner = owner;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly OwnerRepository _ownerRepository;

            public Handler(OwnerRepository ownerRepository)
            {
                _ownerRepository = ownerRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                await _ownerRepository.Add(command.Owner);
                return Result.Ok();
            }
        }
    }
}
