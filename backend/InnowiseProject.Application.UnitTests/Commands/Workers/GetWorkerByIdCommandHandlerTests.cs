using InnowiseProject.Application.Commands.Workers;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Workers
{
    public class GetWorkerByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GetWorkerByIdFromRepository()
        {
            // Arrange
            string workerId = "1";

            var worker = new Worker
            {
                Id = workerId,
                FirstName = "workerFirstName",
                LastName = "workerLastName"
            };

            var command = new GetWorkerByIdCommand(workerId);

            var mockWorkerRepository = new Mock<IWorkerRepository>();

            mockWorkerRepository
                .Setup(repo => repo.GetWorkerById(workerId))
                .ReturnsAsync(worker)
                .Verifiable();

            var handler = new GetWorkerByIdCommandHandler(mockWorkerRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWorkerRepository.Verify(repo => repo.GetWorkerById(worker.Id), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(worker.Id, result.Id);
            Assert.Equal(worker.FirstName, result.FirstName);
            Assert.Equal(worker.LastName, result.LastName);
        }
    }
}
