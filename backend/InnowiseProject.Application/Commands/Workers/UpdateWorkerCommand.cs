using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.Application.DTO;
using MediatR;

namespace InnowiseProject.Application.Commands.Workers
{
    public class UpdateWorkerCommand : IRequest
    {
        public UpdateWorkerCommand(WorkerDTO workerDTO)
        {
            WorkerDTO = workerDTO;
        }

        public WorkerDTO WorkerDTO { get; }
    }

    public class UpdateWorkerCommandHandler : IRequestHandler<UpdateWorkerCommand>
    {
        private readonly IWorkerRepository workerRepository;

        public UpdateWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerDTO = request.WorkerDTO;

            var worker = await workerRepository.GetWorkerDetailsById(workerDTO.Id);

            worker.FirstName = workerDTO.FirstName;
            worker.LastName = workerDTO.LastName;
            worker.UserName = workerDTO.FirstName + workerDTO.LastName;

            await workerRepository.UpdateWorker(worker);

            return Unit.Value;
        }
    }
}
