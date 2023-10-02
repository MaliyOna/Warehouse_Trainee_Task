using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Products
{
    public class GetProductsByNameCommand : IRequest<IEnumerable<ProductDTO>>
    {
        public GetProductsByNameCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class GetProductsByNameCommandHandler : IRequestHandler<GetProductsByNameCommand, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsByNameCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsByNameCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsByName(request.Name);

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
