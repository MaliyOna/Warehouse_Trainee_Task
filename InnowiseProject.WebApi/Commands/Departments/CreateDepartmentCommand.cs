using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
{
    public class CreateDepartmentCommand : IRequest
    {
        public CreateDepartmentCommand(DepartmentDTO departmentDTO)
        {
            DepartmentDTO = departmentDTO;
        }

        public DepartmentDTO DepartmentDTO { get; }
    }
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public async Task<Unit> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentDTO = request.DepartmentDTO;

            var department = new Department
            {
                Name = departmentDTO.Name,
                Products = null,
                Workers = null
            };

            await departmentRepository.CreateDepartment(department);

            return Unit.Value;
        }
    }
}
