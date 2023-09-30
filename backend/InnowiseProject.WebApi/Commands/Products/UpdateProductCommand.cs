using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Products
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
        private readonly IDepartmentRepository departmentRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, IDepartmentRepository departmentRepository)
        {
            this.productRepository = productRepository;
            this.departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productDTO = request.ProductDTO;
            var product = new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                DepartmentId = productDTO.DepartmentId,
            };

            await productRepository.UpdateProduct(productDTO.Id, productDTO.Name, productDTO.DepartmentId);
            await departmentRepository.AddProduct(product);

            return Unit.Value;
        }
    }
}
