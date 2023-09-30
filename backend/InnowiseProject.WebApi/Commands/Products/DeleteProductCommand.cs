using InnowiseProject.Database.Repositories.Interfaces;
using InnowiseProject.WebApi.Commands.Departments;
using MediatR;

namespace InnowiseProject.WebApi.Commands.Products
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productRepository.DeleteProduct(request.Id);

            return Unit.Value;
        }
    }
}
