using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Products
{
    public class GetProductByIdCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GetProductByIdFromRepository()
        {
            //Arrange
            int productId = 1;

            var product = new Product
            {
                Id = productId,
                Name = "TestName",
                DepartmentId = 1
            };

            var command = new GetProductByIdCommand(productId);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync(product)
                .Verifiable();

            var handler = new GetProductByIdCommandHandler(mockProductRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            mockProductRepository.Verify(repo => repo.GetProductById(productId), Times.Once());

            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.DepartmentId, result.DepartmentId);
        }
    }
}
