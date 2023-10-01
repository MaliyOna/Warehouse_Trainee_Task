using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class DeleteDepartamentWorkerCommand : IRequest
    {
        public DeleteDepartamentWorkerCommand(int departmentId, string workerId)
        {
            DepartmentId = departmentId;
            WorkerId = workerId;
        }

        public int DepartmentId { get; }
        public string WorkerId { get; }
    }

    public class DeleteDepartamentWorkerCommandHandler : IRequestHandler<DeleteDepartamentWorkerCommand>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IWorkerRepository workerRepository;

        public DeleteDepartamentWorkerCommandHandler(IDepartmentRepository departmentRepository, IWorkerRepository workerRepository)
        {
            this.departmentRepository = departmentRepository;
            this.workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(DeleteDepartamentWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerId = request.WorkerId;
            var departmentId = request.DepartmentId;

            var worker = await workerRepository.GetWorkerById(workerId);
            var department = await departmentRepository.GetDepartmentById(departmentId);

            await workerRepository.DeleteDepartment(department, workerId);
            await departmentRepository.DeleteWorker(worker, departmentId);

            return Unit.Value;
        }
    }
}
