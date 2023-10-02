using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Products
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]

        public async Task Handle_UpdateProductInRepository()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Old",
                DepartmentId = 1
            };

            var productDTO = new ProductDTO
            {
                Id = 1,
                Name = "New",
                DepartmentId = 1
            };

            var command = new UpdateProductCommand(productDTO);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(repo => repo.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(product)
                .Verifiable();

            mockProductRepository
                .Setup(repo => repo.UpdateProduct(It.IsAny<Product>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            
            var handler = new UpdateProductCommandHandler(mockProductRepository.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockProductRepository.Verify(repo => repo.UpdateProduct(It.Is<Product>(x =>
            x.Id == productDTO.Id &&
            x.Name == productDTO.Name &&
            x.DepartmentId == productDTO.DepartmentId
            )), Times.Once);
        }
    }
}
