using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Workers
{
    public class GetWorkerDetailsByIdCommand : IRequest<WorkerDetailsDTO>
    {
        public GetWorkerDetailsByIdCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    public class GetWorkerDetailsByIdCommandHandler : IRequestHandler<GetWorkerDetailsByIdCommand, WorkerDetailsDTO>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkerDetailsByIdCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<WorkerDetailsDTO> Handle(GetWorkerDetailsByIdCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetWorkerDetailsById(request.Id);

            return worker == null
                ? null
                : new WorkerDetailsDTO
                {
                    Id = worker.Id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                    Departments = worker.Departments == null
                        ? null
                        : worker.Departments
                        .Select(x => new DepartmentDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                        })
                        .ToList()
                };
        }
    }
}
