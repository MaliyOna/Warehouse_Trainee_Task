﻿using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.DTO;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Products
{
    public class GetProductByIdCommand : IRequest<ProductDTO>
    {
        public GetProductByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetProductByIdCommandHandler : IRequestHandler<GetProductByIdCommand, ProductDTO>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductById(request.Id);

            return product == null
                ? null
                : new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    DepartmentId = product.DepartmentId
                };
        }
    }
}
