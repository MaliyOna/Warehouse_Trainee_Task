﻿using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class GetDepartmentsByNameCommand : IRequest<IEnumerable<DepartmentDTO>>
    {
        public GetDepartmentsByNameCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
    public class GetDepartmentsByNameCommandHandler : IRequestHandler<GetDepartmentsByNameCommand, IEnumerable<DepartmentDTO>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetDepartmentsByNameCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<DepartmentDTO>> Handle(GetDepartmentsByNameCommand request, CancellationToken cancellationToken)
        {
            var name = request.Name;

            var departments = await departmentRepository.GetDepartmentsByName(name);

            return departments
                .Select(x => new DepartmentDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
        }
    }
}
