using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
{
    public class GetDepartmentByIdCommand : IRequest<DepartmentDTO>
    {
        public GetDepartmentByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetDepartmentByIdCommandHandler : IRequestHandler<GetDepartmentByIdCommand, DepartmentDTO>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetDepartmentByIdCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        
        public async Task<DepartmentDTO> Handle(GetDepartmentByIdCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetDepartmentById(request.Id);

            return department == null
                ? null
                : new DepartmentDTO
                {
                    Id = department.Id,
                    Name = department.Name
                };
        }
    }
}
