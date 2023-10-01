using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Database.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InnowiseProject.Application.UnitTests.Commands.Products
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteProductFromRepository()
        {
            // Arrange
            int productId = 1;

            var command = new DeleteProductCommand(productId);

            var mocProductRepository = new Mock<IProductRepository>();

            mocProductRepository
                .Setup(repo => repo.DeleteProduct(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new DeleteProductCommandHandler(mocProductRepository.Object);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assets
            mocProductRepository.Verify(repo => repo.DeleteProduct(productId), Times.Once());
        }
    }
}
