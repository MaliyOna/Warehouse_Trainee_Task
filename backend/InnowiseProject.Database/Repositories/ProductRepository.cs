using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace InnowiseProject.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateProduct(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            await dbContext.Products
                .Where(x => x.Id == id)
                .DeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await dbContext.Products
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsByName(string name)
        {
            return await dbContext.Products
                .Where(x => x.Name == name)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByDepartment(int departmentId)
        {
            return await dbContext.Products.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }
    }
}
