using InnowiseProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task UpdateProduct(int id, string name, int departmentId);
        Task DeleteProduct(int id);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProductsByName(string name);
        Task<List<Product>> GetProducts();
    }
}
