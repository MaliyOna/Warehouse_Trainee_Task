using InnowiseProject.Application.Commands.Departments;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Departments
{
    public class CreateDepartmentCommandHandlerTests
    {
        [Fact]

        public async Task Handle_CreateDepartmentInRepository()
        {
            //Arrange
            var departmentDTO = new DepartmentDTO
            {
                Name = "TestName",
            };

            var command = new CreateDepartmentCommand(departmentDTO);

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockDepartmentRepository
                .Setup(repo => repo.CreateDepartment(It.IsAny<Department>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateDepartmentCommandHandler(mockDepartmentRepository.Object);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentRepository.Verify(repo => repo.CreateDepartment(It.Is<Department>(x => 
                x.Name == "TestName"
            )), Times.Once);
        }
    }
}
