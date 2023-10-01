using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnowiseProject.Application.Commands.Departments
{
    public class AddDepartmentWorkerCommand : IRequest
    {
        public AddDepartmentWorkerCommand(int departmentId, string workerId)
        {
            DepartmentId = departmentId;
            WorkerId = workerId;
        }

        public int DepartmentId { get; }
        public string WorkerId { get; }
    }

    public class AddDepartmentWorkerCommandHandler : IRequestHandler<AddDepartmentWorkerCommand>
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IWorkerRepository workerRepository;

        public AddDepartmentWorkerCommandHandler(IDepartmentRepository departmentRepository, IWorkerRepository workerRepository)
        {
            this.departmentRepository = departmentRepository;
            this.workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(AddDepartmentWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerId = request.WorkerId;
            var departmentId = request.DepartmentId;

            var worker = await workerRepository.GetWorkerById(workerId);
            var department = await departmentRepository.GetDepartmentById(departmentId);

            await workerRepository.AddDepartment(department, workerId);
            await departmentRepository.AddWorker(worker, departmentId);

            return Unit.Value;
        }
    }
}
