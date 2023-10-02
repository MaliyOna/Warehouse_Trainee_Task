using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Workers
{
    public class GetWorkersDetailsCommand : IRequest<IEnumerable<WorkerDetailsDTO>>
    {
        public GetWorkersDetailsCommand()
        {
        }
    }

    public class GetWorkersDetailsCommandHandler : IRequestHandler<GetWorkersDetailsCommand, IEnumerable<WorkerDetailsDTO>>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkersDetailsCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<IEnumerable<WorkerDetailsDTO>> Handle(GetWorkersDetailsCommand request, CancellationToken cancellationToken)
        {
            var workers = await workerRepository.GetWorkersDetails();

            return workers.Select(x => new WorkerDetailsDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Departments = x.Departments
                .Select(y => new DepartmentDTO
                {
                    Id = y.Id,
                    Name = y.Name,
                }).ToList(),
            });
        }
    }
}
