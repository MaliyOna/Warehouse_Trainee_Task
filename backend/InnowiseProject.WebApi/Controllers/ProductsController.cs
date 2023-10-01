using InnowiseProject.Application.Commands.Products;
using InnowiseProject.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnowiseProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task CreateProduct(ProductDTO productDTO)
        {
            await mediator.Send(new CreateProductCommand(productDTO));
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return await mediator.Send(new GetProductsCommand());
        }

        [HttpGet("{id}")]
        public async Task<ProductDTO> GetProductById(int id)
        {
            return await mediator.Send(new GetProductByIdCommand(id));
        }

        [HttpGet("by-name")]
        public async Task<IEnumerable<ProductDTO>> GetProductsByName([FromQuery] string name)
        {
            return await mediator.Send(new GetProductsByNameCommand(name));
        }

        [HttpPut]
        public async Task UpdateProductName(ProductDTO productDTO)
        {
            await mediator.Send(new UpdateProductCommand(productDTO));
        }
    }
}
