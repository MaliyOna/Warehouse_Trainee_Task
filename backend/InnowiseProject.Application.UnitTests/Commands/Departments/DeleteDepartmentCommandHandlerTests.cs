using InnowiseProject.Application.Commands.Departments;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Departments
{
    public class DeleteDepartmentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteDepartmentFromRepository()
        {
            //Arrange
            int departmentId = 1;

            var command = new DeleteDepartmentCommand(departmentId);

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockDepartmentRepository
                .Setup(repo => repo.DeleteDepartment(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new DeleteDepartmentCommandHandler(mockDepartmentRepository.Object);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentRepository.Verify(repo => repo.DeleteDepartment(departmentId), Times.Once);
        }
    }
}
