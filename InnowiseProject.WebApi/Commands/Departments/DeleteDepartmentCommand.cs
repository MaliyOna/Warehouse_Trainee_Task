using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Departments
{
    public class DeleteDepartmentCommand : IRequest
    {
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            await departmentRepository.DeleteDepartment(request.Id);

            return Unit.Value;
        }
    }
}
