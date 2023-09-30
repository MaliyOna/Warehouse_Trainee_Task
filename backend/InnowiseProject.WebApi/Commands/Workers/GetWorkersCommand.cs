using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Workers
{
    public class GetWorkersCommand : IRequest<IEnumerable<WorkerDTO>>
    {
        public GetWorkersCommand()
        {
        }
    }

    public class GetWorkersCommandHandler : IRequestHandler<GetWorkersCommand, IEnumerable<WorkerDTO>>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkersCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<IEnumerable<WorkerDTO>> Handle(GetWorkersCommand request, CancellationToken cancellationToken)
        {
            var workers = await workerRepository.GetWorkers();

            return workers.Where(x => !x.IsSystem).Select(x => new WorkerDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();
        }
    }
}
