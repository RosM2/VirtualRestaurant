using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class SetupRestaurant
    {
        public class Command : IRequest<Result>
        {
            public List<Table> Tables;
            public int Id { get; set; }

            public Command(List<Table> tables, int id)
            {
                Tables = tables;
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly TableRepository _tableRepository;

            public Handler(TableRepository tableRepository)
            {
                _tableRepository = tableRepository;
            }
            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                await _tableRepository.UpdateAllTables(command.Tables, command.Id);

                return Result.Ok();
            }
        }
    }
}
