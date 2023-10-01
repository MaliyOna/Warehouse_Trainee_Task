using InnowiseProject.Database.Models;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);

        Task<Product> GetProductById(int id);

        Task<IEnumerable<Product>> GetProductsByDepartment(int departmentId);

        Task<IReadOnlyList<Product>> GetProductsByName(string name);

        Task<IReadOnlyList<Product>> GetProducts();
    }
}
