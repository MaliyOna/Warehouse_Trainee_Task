using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Products
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_CreateProductInRepository()
        {
            // Arrange
            var productDTO = new ProductDTO
            {
                Name = "TestName",
                DepartmentId = 1
            };

            var command = new CreateProductCommand(productDTO);

            var mockWorkerRepository = new Mock<IProductRepository>();
            var mockDepartmentRepository = new Mock<IDepartmentRepository>();

            mockWorkerRepository
                .Setup(repo => repo.CreateProduct(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockDepartmentRepository
                .Setup(repo => repo.AddProduct(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new CreateProductCommandHandler(mockWorkerRepository.Object, mockDepartmentRepository.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWorkerRepository.Verify(repo => repo.CreateProduct(It.Is<Product>(x =>
                x.Name == productDTO.Name &&
                x.DepartmentId == productDTO.DepartmentId
            )), Times.Once);

            mockDepartmentRepository.Verify(repo => repo.AddProduct(It.Is<Product>(x =>
            x.Name == productDTO.Name &&
            x.DepartmentId == productDTO.DepartmentId
            )), Times.Once );
        }
    }
}
