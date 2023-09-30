﻿using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Workers
{
    public class CreateWorkerCommand : IRequest
    {
        public CreateWorkerCommand(WorkerDTO workerDTO)
        {
            WorkerDTO = workerDTO;
        }

        public WorkerDTO WorkerDTO { get; }
    }

    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand>
    {
        private readonly IWorkerRepository workerRepository;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<Unit> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var workerDTO = request.WorkerDTO;

            var worker = new Worker
            {
                FirstName = workerDTO.FirstName,
                LastName = workerDTO.LastName,
            };

            await workerRepository.CreateWorker(worker);
            return Unit.Value;
        }
    }
}
