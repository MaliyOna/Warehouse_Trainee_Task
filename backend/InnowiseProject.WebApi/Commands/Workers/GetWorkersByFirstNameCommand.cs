using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Workers
{
    public class GetWorkersByFirstNameCommand : IRequest <IEnumerable<WorkerDTO>>
    {
        public GetWorkersByFirstNameCommand(string firstName)
        {
            FirstName = firstName;
        }

        public string FirstName { get; }
    }

    public class GetWorkersByFirstNameCommandHandler : IRequestHandler<GetWorkersByFirstNameCommand, IEnumerable<WorkerDTO>>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkersByFirstNameCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<IEnumerable<WorkerDTO>> Handle(GetWorkersByFirstNameCommand request, CancellationToken cancellationToken)
        {
            var workers = await workerRepository.GetWorkersByFirstName(request.FirstName);

            return workers.Select(x => new WorkerDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
            })
            .ToList();
        }
    }
}
