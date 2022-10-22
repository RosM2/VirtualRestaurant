using MediatR;
using VirtualRestaurant.Domain.Models;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class CreateRestaurant
    {
        public class Command : IRequest<int>
        {
            public Owner Owner;

            public Command(Owner owner)
            {
                Owner = owner;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
           // public int REPOSITORY { get; set; }
            public Handler(/*int REPOSITORY*/)
            {
                //REPOSITORY = REPOSITORY;
            }
            public async Task<int> Handle(Command command, CancellationToken cancellationToken)
            {
                var a = command.Owner;
                throw new NotImplementedException();
            }
        }
    }
}
