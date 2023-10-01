using InnowiseProject.Application.Commands.Workers;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Workers
{
    public class CreateWorkerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_CreatesAWorkerInRepository()
        {
            // Arrange
            var workerDTO = new WorkerDTO
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var command = new CreateWorkerCommand(workerDTO);

            var mockWorkerRepository = new Mock<IWorkerRepository>();
            mockWorkerRepository
                .Setup(repo => repo.CreateWorker(It.IsAny<Worker>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateWorkerCommandHandler(mockWorkerRepository.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWorkerRepository.Verify(repo => repo.CreateWorker(It.Is<Worker>(w =>
                w.FirstName == "John" &&
                w.LastName == "Doe" &&
                w.UserName == "JohnDoe"
            )), Times.Once);
        }
    }
}
