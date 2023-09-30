using InnowiseProject.WebApi.Commands.Departments;
using InnowiseProject.WebApi.Commands.Products;
using InnowiseProject.WebApi.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost()]
        public async Task CreateProduct(ProductDTO productDTO)
        {
            await mediator.Send(new CreateProductCommand(productDTO));
        }

        [HttpDelete()]
        public async Task DeleteProduct(ProductDTO productDTO)
        {
            await mediator.Send(new DeleteProductCommand(productDTO.Id));
        }

        [HttpGet()]
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return await mediator.Send(new GetProductsCommand());
        }

        [HttpGet("productById")]
        public async Task<ProductDTO> GetProductById(ProductDTO productDTO)
        {
            return await mediator.Send(new GetProductByIdCommand(productDTO.Id));
        }

        [HttpGet("productsByName")]
        public async Task<IEnumerable<ProductDTO>> GetProductsByName(ProductDTO productDTO)
        {
            return await mediator.Send(new GetProductsByNameCommand(productDTO.Name));
        }

        [HttpPut()]
        public async Task UpdateProductName(ProductDTO productDTO)
        {
            await mediator.Send(new UpdateProductCommand(productDTO));
        }
    }
}
