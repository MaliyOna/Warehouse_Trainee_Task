using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
{
    public class UpdateDepartmentCommand : IRequest
    {
        public UpdateDepartmentCommand(DepartmentDTO departmentDTO)
        {
            DepartmentDTO = departmentDTO;
        }

        public DepartmentDTO DepartmentDTO { get; }
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentDTO = request.DepartmentDTO;

            await departmentRepository.UpdateDepartment(departmentDTO.Id, departmentDTO.Name);

            return Unit.Value;
        }
    }
}
