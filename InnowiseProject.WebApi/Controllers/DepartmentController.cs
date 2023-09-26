using InnowiseProject.Database.Models;
using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IMediator mediator { get; }

        [HttpPost("add")]
        public async Task CreateDepartment(DepartmentDTO departmentDTO)
        {
            await mediator.Send(new CreateDepartmentCommand(departmentDTO));
        }

        [HttpDelete("delete")]
        public async Task DeleteDepartment(DepartmentDTO departmentDTO)
        {
            await mediator.Send(new DeleteDepartmentCommand(departmentDTO.Id));
        }

        [HttpGet("departmentById")]
        public async Task<DepartmentDTO> GetDepartmentById(DepartmentDTO departmentDTO)
        {
            return await mediator.Send(new GetDepartmentByIdCommand(departmentDTO.Id));
        }

        [HttpGet("departmentsByName")]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsByName(DepartmentDTO departmentDTO)
        {
            return await mediator.Send(new GetDepartmentsByNameCommand(departmentDTO.Name));
        }

        [HttpGet()]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return await mediator.Send(new GetDepartmentsCommand());
        }

        [HttpPut()]
        public async Task UpdateDepartment(DepartmentDTO departmentDTO)
        {
            await mediator.Send(new UpdateDepartmentCommand(departmentDTO));
        }
    }
}
