using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Products
{
    public class GetProductsCommand : IRequest<IEnumerable<ProductDTO>>
    {
        public GetProductsCommand()
        {
        }
    }

    public class GetProductsCommandHandler : IRequestHandler<GetProductsCommand, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProducts();

            return products
                .Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    DepartmentId = x.DepartmentId
                })
                .ToList();
        }
    }
}
