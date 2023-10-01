using InnowiseProject.Application.Commands.Departments;
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

namespace InnowiseProject.Application.UnitTests.Commands.Departments
{
    public class UpdateDepartmentNameCommandHandlerTests
    {
        [Fact]
        public async Task Handle_UpdateDepartmentNameInRepository()
        {
            var department = new Department
            {
                Id = 1,
                Name = "TestName"
            };

            var departmentDTO = new DepartmentDTO
            {
                Id = 1,
                Name = "newName"
            };

            var command = new UpdateDepartmentNameCommand(departmentDTO);

            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockDepartmentRepository
                .Setup(repo => repo.GetDepartmentById(It.IsAny<int>()))
                .ReturnsAsync(department)
                .Verifiable();

            mockDepartmentRepository
                .Setup(repo => repo.UpdateDepartmentName(It.IsAny<Department>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UpdateDepartmentNameCommandHandler(mockDepartmentRepository.Object);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentRepository.Verify(repo => repo.UpdateDepartmentName(It.Is<Department>(x =>
            x.Id == departmentDTO.Id &&
            x.Name == departmentDTO.Name
            )), Times.Once);
        }

    }
}
