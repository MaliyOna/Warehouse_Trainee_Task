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
    public class DeleteWorkerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteWorkerFromRepository()
        {
            // Arrange
            string workerId = "1";

            var command = new DeleteWorkerCommand(workerId);

            var mockWorkerRepository = new Mock<IWorkerRepository>();

            mockWorkerRepository
                .Setup(repo => repo.DeleteWorker(It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new DeleteWorkerCommandHandler(mockWorkerRepository.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWorkerRepository.Verify(repo => repo.DeleteWorker(workerId), Times.Once);
        }
    }
}
