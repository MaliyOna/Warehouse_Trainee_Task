using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
{
    public class GetDepartmentsWithDetailsCommand : IRequest<IEnumerable<DepartmentDetailsDTO>>
    {
        public GetDepartmentsWithDetailsCommand()
        {
        }
    }

    public class GetDepartmentsWithDetailsCommandHandler : IRequestHandler<GetDepartmentsWithDetailsCommand, IEnumerable<DepartmentDetailsDTO>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetDepartmentsWithDetailsCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDetailsDTO>> Handle(GetDepartmentsWithDetailsCommand request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetDepartmentsWithDetails();

            return departments
                .Select(x => new DepartmentDetailsDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = x.Products
                        .Select(y => new ProductDTO 
                        {
                            Id = y.Id,
                            Name = y.Name,
                            DepartmentId = y.Id
                        }),
                    Workers = x.Workers
                        .Select(y => new WorkerDetailsDTO 
                        {
                            Id = y.Id,
                            FirstName = y.FirstName,
                            LastName = y.LastName
                        })
                }).ToList();
        }
    }
}
