using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.Application.DTO;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class GetDepartmentsCommand : IRequest<IEnumerable<DepartmentDTO>>
    {
        public GetDepartmentsCommand()
        {
        }
    }

    public class GetDepartmentsCommandHandler : IRequestHandler<GetDepartmentsCommand, IEnumerable<DepartmentDTO>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetDepartmentsCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDTO>> Handle(GetDepartmentsCommand request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetDepartments();

            return departments
                .Select(x => new DepartmentDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
        }
    }
}
