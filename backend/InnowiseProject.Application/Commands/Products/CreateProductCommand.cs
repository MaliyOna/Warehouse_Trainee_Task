﻿using InnowiseProject.Application.DTO;
using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using MediatR;

namespace InnowiseProject.Application.Commands.Products
{
    public class CreateProductCommand : IRequest
    {
        public CreateProductCommand(ProductDTO productDTO)
        {
            ProductDTO = productDTO;
        }

        public ProductDTO ProductDTO { get; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly IDepartmentRepository departmentRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IDepartmentRepository departmentRepository)
        {
            this.productRepository = productRepository;
            this.departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productDTO = request.ProductDTO;

            var product = new Product
            {
                Name = productDTO.Name,
                DepartmentId = productDTO.DepartmentId
            };

            await productRepository.CreateProduct(product);
            await departmentRepository.AddProduct(product);

            return Unit.Value;
        }
    }
}
