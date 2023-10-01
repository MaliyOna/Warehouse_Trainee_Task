using InnowiseProject.Application.Commands.Departments;
using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Application.Commands.Workers;
using InnowiseProject.Application.DTO;
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

        [HttpGet("{id}/workers")]
        public async Task<IEnumerable<WorkerDTO>> GetWorkersByDepartment(int id)
        {
            return await mediator.Send(new GetWorkersByDepartmentCommand(id));
        }

        [HttpGet(("{id}/products"))]
        public async Task<IEnumerable<ProductDTO>> GetProductByDepartment(int id)
        {
            return await mediator.Send(new GetProductsByDepartmentCommand(id));
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

        [HttpPut("{departmentId}/workers/{workerId}")]
        public async Task AddDepartamentWorker(int departmentId, string workerId)
        {
            await mediator.Send(new AddDepartmentWorkerCommand(departmentId, workerId));
        }

        [HttpDelete("{departmentId}/workers/{workerId}")]
        public async Task DeleteDepartamentWorker(int departmentId, string workerId)
        {
            await mediator.Send(new DeleteDepartamentWorkerCommand(departmentId, workerId));
        }

        [HttpGet("details")]
        public async Task<IEnumerable<DepartmentDetailsDTO>> GetDepartmentsWithDetails()
        {
            return await mediator.Send(new GetDepartmentsWithDetailsCommand());
        }
    }
}
