using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class UpdateDepartmentNameCommand : IRequest
    {
        public UpdateDepartmentNameCommand(DepartmentDTO departmentDTO)
        {
            DepartmentDTO = departmentDTO;
        }

        public DepartmentDTO DepartmentDTO { get; }
    }

    public class UpdateDepartmentNameCommandHandler : IRequestHandler<UpdateDepartmentNameCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public UpdateDepartmentNameCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(UpdateDepartmentNameCommand request, CancellationToken cancellationToken)
        {
            var departmentDTO = request.DepartmentDTO;

            var department = await departmentRepository.GetDepartmentById(request.DepartmentDTO.Id);

            department.Name = departmentDTO.Name;

            await departmentRepository.UpdateDepartmentName(department);

            return Unit.Value;
        }
    }
}
