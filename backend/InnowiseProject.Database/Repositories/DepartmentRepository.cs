using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace InnowiseProject.Database.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateDepartment(Department department)
        {
            dbContext.Departments.Add(department);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            await dbContext.Departments
                .Where(x => x.Id == id)
                .DeleteAsync();

            await dbContext.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await dbContext.Departments
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return department;
        }

        public async Task<IReadOnlyList<Department>> GetDepartmentsByName(string name)
        {
            return await dbContext.Departments
                .Where(x => x.Name == name)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Department>> GetDepartments()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task UpdateDepartmentName(Department department)
        {
            dbContext.Departments.Update(department);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Department>> GetDepartmentsWithDetails()
        {
            return await dbContext.Departments
                .Include(x => x.Workers)
                .Include(x => x.Products)
                .ToListAsync();
        }

        public async Task AddProduct(Product product)
        {
            var department = await dbContext.Departments
                .Include(x => x.Products)
                .Where(x => x.Id == product.DepartmentId)
                .FirstOrDefaultAsync();

            department.Products.Add(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentByIdDetails(int id)
        {
            var department = await dbContext.Departments
                .Include(x => x.Workers)
                .Include(x => x.Products)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return department;
        }

        public async Task AddWorker(Worker worker, int departmentId)
        {
            var department = await dbContext.Departments
                .Include(x => x.Workers)
                .Where(x => x.Id == departmentId)
                .FirstOrDefaultAsync();

            department.Workers.Add(worker);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteWorker(Worker worker, int departmentId)
        {
            var department = await dbContext.Departments
                .Include(x => x.Workers)
                .Where(x => x.Id == departmentId)
                .FirstOrDefaultAsync();

            department.Workers.Remove(worker);
            await dbContext.SaveChangesAsync();
        }
    }
}
