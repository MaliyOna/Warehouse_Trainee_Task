using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Workers
{
    public class DeleteWorkerCommand : IRequest
    {
        public DeleteWorkerCommand(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }

    public class DeleteWorkerCommandHandler : IRequestHandler<DeleteWorkerCommand>
    {
        private readonly IWorkerRepository workerRepository;

        public DeleteWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }
        public async Task<Unit> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            await workerRepository.DeleteWorker(request.Id);

            return Unit.Value;
        }
    }
}
