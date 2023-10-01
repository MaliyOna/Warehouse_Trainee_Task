using InnowiseProject.Application.Commands.Workers;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Workers
{
    public class UpdateWorkerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_UpdatesWorkerDetailsInRepository()
        {
            // Arrange
            var originalWorker = new Worker
            {
                Id = "1",
                FirstName = "OriginalFirst",
                LastName = "OriginalLast",
                UserName = "OriginalFirstOriginalLast"
            };

            var workerDTO = new WorkerDTO
            {
                Id = "1",
                FirstName = "UpdatedFirst",
                LastName = "UpdatedLast"
            };

            var command = new UpdateWorkerCommand(workerDTO);

            var mockWorkerRepository = new Mock<IWorkerRepository>();

            mockWorkerRepository
                .Setup(repo => repo.GetWorkerDetailsById(It.IsAny<string>()))
                .ReturnsAsync(originalWorker)
                .Verifiable();

            mockWorkerRepository
                .Setup(repo => repo.UpdateWorker(It.IsAny<Worker>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UpdateWorkerCommandHandler(mockWorkerRepository.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWorkerRepository.Verify(repo => repo.GetWorkerDetailsById(workerDTO.Id), Times.Once);

            mockWorkerRepository.Verify(repo => repo.UpdateWorker(It.Is<Worker>(w =>
                w.Id == workerDTO.Id &&
                w.FirstName == "UpdatedFirst" &&
                w.LastName == "UpdatedLast" &&
                w.UserName == "UpdatedFirstUpdatedLast"
            )), Times.Once);
        }
    }
}
