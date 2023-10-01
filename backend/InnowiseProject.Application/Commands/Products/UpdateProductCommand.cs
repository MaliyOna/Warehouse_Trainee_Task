using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.Application.Commands.Departments;
using InnowiseProject.Application.DTO;
using MediatR;

namespace InnowiseProject.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest
    {
        public UpdateProductCommand(ProductDTO productDTO)
        {
            ProductDTO = productDTO;
        }

        public ProductDTO ProductDTO { get; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productDTO = request.ProductDTO;

            var product = await productRepository.GetProductById(productDTO.Id);

            product.Name = productDTO.Name;

            await productRepository.UpdateProduct(product);

            return Unit.Value;
        }
    }
}
