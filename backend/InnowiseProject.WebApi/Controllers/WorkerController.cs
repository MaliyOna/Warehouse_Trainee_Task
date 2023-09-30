using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.Commands.Workers;
using InnowiseProject.WebApi.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator mediator;

        public WorkerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost()]
        public async Task CreateWorker(WorkerDTO workerDTO)
        {
            await mediator.Send(new CreateWorkerCommand(workerDTO));
        }

        [HttpDelete()]
        public async Task DeleteWorker(WorkerDTO workerDTO)
        {
            await mediator.Send(new DeleteWorkerCommand(workerDTO.Id));
        }

        [HttpGet()]
        public async Task<IEnumerable<WorkerDTO>> GetWorkers()
        {
            return await mediator.Send(new GetWorkersCommand());
        }

        [HttpGet("workerById")]
        public async Task<WorkerDTO> GetWorkerById(WorkerDTO workerDTO)
        {
            return await mediator.Send(new GetWorkerByIdCommand(workerDTO.Id));
        }
        
        [HttpGet("workerDetailsById")]
        public async Task<WorkerDetailsDTO> GetWorkerDetailsById(WorkerDTO workerDTO)
        {
            return await mediator.Send(new GetWorkerDetailsByIdCommand(workerDTO.Id));
        }
        
        [HttpGet("workersByFirstName")]
        public async Task<IEnumerable<WorkerDTO>> GetWorkersByFirstName(WorkerDTO workerDTO)
        {
            return await mediator.Send(new GetWorkersByFirstNameCommand(workerDTO.FirstName));
        }

        [HttpGet("workersDetails")]
        public async Task<IEnumerable<WorkerDetailsDTO>> GetWorkersDetails()
        {
            return await mediator.Send(new GetWorkersDetailsCommand());
        }
    }
}
