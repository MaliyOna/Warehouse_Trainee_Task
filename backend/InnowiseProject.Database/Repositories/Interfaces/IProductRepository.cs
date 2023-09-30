using InnowiseProject.Database.Models;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);

        Task UpdateProduct(int id, string name, int departmentId);

        Task DeleteProduct(int id);

        Task<Product> GetProductById(int id);

        Task<IReadOnlyList<Product>> GetProductsByName(string name);

        Task<IReadOnlyList<Product>> GetProducts();
    }
}
