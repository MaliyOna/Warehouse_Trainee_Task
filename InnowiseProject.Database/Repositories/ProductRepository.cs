using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await dbContext.Products.Where(x => x.Id == id).DeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return product != null ? product : null;
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await dbContext.Products.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task UpdateProduct(int id, string name)
        {
            var product = await dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            product.Name = name;
            await dbContext.SaveChangesAsync();
        }
    }
}
