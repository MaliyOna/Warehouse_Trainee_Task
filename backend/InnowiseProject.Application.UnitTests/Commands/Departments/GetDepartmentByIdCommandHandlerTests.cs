using InnowiseProject.Application.Commands.Departments;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Departments
{
    public class GetDepartmentByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GetDepartmentByIdFromRepository()
        {
            int departmentId = 1;

            var department = new Department
            {
                Id = departmentId,
                Name = "TestName"
            };

            var command = new GetDepartmentByIdCommand(departmentId);

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockDepartmentRepository
                .Setup(repo => repo.GetDepartmentById(departmentId))
                .ReturnsAsync(department)
                .Verifiable();

            var handler = new GetDepartmentByIdCommandHandler(mockDepartmentRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentRepository.Verify(repo => repo.GetDepartmentById(departmentId), Times.Once());

            Assert.NotNull(result);
            Assert.Equal(departmentId, result.Id);
            Assert.Equal(department.Name, result.Name);
        }
    }
}
