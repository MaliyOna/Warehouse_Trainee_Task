using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class GetWorkersByDepartmentCommand : IRequest<IEnumerable<WorkerDTO>>
    {
        public GetWorkersByDepartmentCommand(int departamentId)
        {
            DepartamentId = departamentId;
        }

        public int DepartamentId { get; }
    }

    public class GetWorkersByDepartmentCommandHandler : IRequestHandler<GetWorkersByDepartmentCommand, IEnumerable<WorkerDTO>>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkersByDepartmentCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<IEnumerable<WorkerDTO>> Handle(GetWorkersByDepartmentCommand request, CancellationToken cancellationToken)
        {
            var workers = await workerRepository.GetWorkersByDepartment(request.DepartamentId);

            return workers.Select(x => new WorkerDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToList();
        }
    }
}
