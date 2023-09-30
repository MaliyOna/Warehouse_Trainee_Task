using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task CreateDepartment(DepartmentDTO departmentDTO)
        {
            await mediator.Send(new CreateDepartmentCommand(departmentDTO));
        }

        [HttpDelete("{id}")]
        public async Task DeleteDepartment(int id)
        {
            await mediator.Send(new DeleteDepartmentCommand(id));
        }

        [HttpGet("{id}")]
        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            return await mediator.Send(new GetDepartmentByIdCommand(id));
        }

        [HttpGet("by-name")]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsByName([FromQuery] string name)
        {
            return await mediator.Send(new GetDepartmentsByNameCommand(name));
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return await mediator.Send(new GetDepartmentsCommand());
        }

        [HttpPut]
        public async Task UpdateDepartmentName(DepartmentDTO departmentDTO)
        {
            await mediator.Send(new UpdateDepartmentNameCommand(departmentDTO));
        }

        [HttpGet("details")]
        public async Task<IEnumerable<DepartmentDetailsDTO>> GetDepartmentsWithDetails()
        {
            return await mediator.Send(new GetDepartmentsWithDetailsCommand());
        }
    }
}
