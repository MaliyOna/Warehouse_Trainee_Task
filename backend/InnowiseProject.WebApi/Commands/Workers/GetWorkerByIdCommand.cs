﻿using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Workers
{
    public class GetWorkerByIdCommand : IRequest<WorkerDTO>
    {
        public GetWorkerByIdCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    public class GetWorkerByIdCommandHandler : IRequestHandler<GetWorkerByIdCommand, WorkerDTO>
    {
        private readonly IWorkerRepository workerRepository;

        public GetWorkerByIdCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<WorkerDTO> Handle(GetWorkerByIdCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetWorkerById(request.Id);

            return worker == null
                ? null
                : new WorkerDTO
                {
                    Id = worker.Id,
                    FirstName = worker.FirstName,
                    LastName = worker.LastName,
                };
        }
    }
}
