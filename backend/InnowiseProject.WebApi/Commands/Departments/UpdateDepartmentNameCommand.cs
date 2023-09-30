using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
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

            await departmentRepository.UpdateDepartmentName(departmentDTO.Id, departmentDTO.Name);

            return Unit.Value;
        }
    }
}
