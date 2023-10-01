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
    public class WorkersController : ControllerBase
    {
        private readonly IMediator mediator;

        public WorkersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task CreateWorker(WorkerDTO workerDTO)
        {
            await mediator.Send(new CreateWorkerCommand(workerDTO));
        }

        [HttpDelete("{id}")]
        public async Task DeleteWorker(string id)
        {
            await mediator.Send(new DeleteWorkerCommand(id));
        }

        [HttpGet]
        public async Task<IEnumerable<WorkerDTO>> GetWorkers()
        {
            return await mediator.Send(new GetWorkersCommand());
        }

        [HttpGet("{id}")]
        public async Task<WorkerDTO> GetWorkerById(string id)
        {
            return await mediator.Send(new GetWorkerByIdCommand(id));
        }

        [HttpGet("{id}/details")]
        public async Task<WorkerDetailsDTO> GetWorkerDetailsById(string id)
        {
            return await mediator.Send(new GetWorkerDetailsByIdCommand(id));
        }

        [HttpGet("by-first-name")]
        public async Task<IEnumerable<WorkerDTO>> GetWorkersByFirstName([FromQuery] string firstName)
        {
            return await mediator.Send(new GetWorkersByFirstNameCommand(firstName));
        }

        [HttpGet("details")]
        public async Task<IEnumerable<WorkerDetailsDTO>> GetWorkersDetails()
        {
            return await mediator.Send(new GetWorkersDetailsCommand());
        }

        [HttpPut]
        public async Task UpdateWorker(WorkerDTO workerDTO)
        {
            await mediator.Send(new UpdateWorkerCommand(workerDTO));
        }
    }
}
