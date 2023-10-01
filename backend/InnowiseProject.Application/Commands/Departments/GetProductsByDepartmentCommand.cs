using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Departments
{
    public class GetProductsByDepartmentCommand : IRequest<IEnumerable<ProductDTO>>
    {
        public GetProductsByDepartmentCommand(int departamentId)
        {
            DepartamentId = departamentId;
        }

        public int DepartamentId { get; }
    }

    public class GetProductsByDepartmentCommandHandler : IRequestHandler<GetProductsByDepartmentCommand, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository productRepository;

        public GetProductsByDepartmentCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsByDepartmentCommand request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetProductsByDepartment(request.DepartamentId);

            return products.Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentId = x.DepartmentId
            })
            .ToList();
        }
    }
}
