using MediatR;
using VirtualRestaurant.Domain.Models;
using VirtualRestaurant.Persistence.Repository;

namespace VirtualRestaurant.BusinessLogic.CQRS.Commands
{
    public class SetupRestaurant
    {
        public class Command : IRequest<Result>
        {
            public IList<Table> Tables;
            public int Id { get; set; }

            public Command(IList<Table> tables, int id)
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
                var isSuccessfull = await _tableRepository.UpdateAllTables(command.Tables, command.Id);
                if (!isSuccessfull)
                {
                    return Result.Fail("Tables count doesn't match existing tables");
                }
                return Result.Ok();
            }
        }
    }
}
